using System;
using System.Collections.Generic;
using System.Linq;
using CarService.Data;
using CarService.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarService.Api.Test
{
	public class CarServiceTest : IDisposable
	{
		private readonly ApplicationDbContext _context;
		private List<ReservationDTO> _reservationDTOs;
        private List<WorksheetDTO> _worksheetDTOs;
        private List<Cost> _costs;
        private List<CarServiceUser> _users;
        private readonly UserManager<CarServiceUser> _userManager;

		public CarServiceTest()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase("TravelAgencyTest")
				.Options;

			_context = new ApplicationDbContext(options);
			_context.Database.EnsureCreated();

            this._users = new List<CarServiceUser>
            {
               new CarServiceUser{ UserName = "mechanic@mech.com", Name = "Mechanic", Id = "1" },
               new CarServiceUser{ UserName = "client@client.com", Name = "Client", Id = "2" }
            };

            this._worksheetDTOs = new List<WorksheetDTO>();
            
			var reservationData = new List<Reservation>
			{
				new Reservation { Id = 1, Client = _users[1], Mechanic = _users[0], Type = ReservationType.Malfunction, Comment = "a" },
                new Reservation { Id = 2, Client = _users[1], Mechanic = _users[0], Type = ReservationType.MandatoryService, Comment = "b" },
                new Reservation { Id = 3, Client = _users[1], Mechanic = _users[0], Type = ReservationType.TechnicalExamination, Comment = "c" }
            };
			_context.Reservations.AddRange(reservationData);

			var costsData = new List<Cost>
			{
				new Cost { Id = 1, Name = "Cost1", Amount = 100},
                new Cost { Id = 2, Name = "Cost2", Amount = 200},
                new Cost { Id = 3, Name = "Cost3", Amount = 300}
            };
			_context.Costs.AddRange(costsData);
			_context.SaveChanges();

			this._reservationDTOs = reservationData.Select(reser => new ReservationDTO
			{
				Id = reser.Id,
				ClientName = reser.Client.Name,
                Time = reser.Time,
                Comment = reser.Comment,
                Type = reser.Type.ToString()
			}).ToList();

            this._costs = costsData.Select(r => new Cost
            {
                Id = r.Id,
                Name = r.Name,
                Amount = r.Amount
            }).ToList();

		}

		public void Dispose()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}
        
		[Fact]
		public void GetCostsTest()
		{
			var controller = new HomeController(_context);
			var result = controller.GetCosts();
            var listResult = result.ToList();

            Assert.Equal(_costs[0].Name, listResult[0].Name);
            Assert.Equal(_costs[0].Amount, listResult[0].Amount);
            Assert.Equal(_costs[1].Name, listResult[1].Name);
            Assert.Equal(_costs[1].Amount, listResult[1].Amount);
            Assert.Equal(_costs[2].Name, listResult[2].Name);
            Assert.Equal(_costs[2].Amount, listResult[2].Amount);
            //Assert.True(_costs.SequenceEqual(listResult));
        }
        
		[Fact]
		public void GetReservationsTest()
		{
			var controller = new HomeController(_context);
			var result = controller.GetReservationsByMechanic(_users[0].UserName);
            var listResult = result.ToList();

            Assert.Equal(_reservationDTOs[0].Id, listResult[0].Id);
            Assert.Equal(_reservationDTOs[0].ClientName, listResult[0].ClientName);
            Assert.Equal(_reservationDTOs[0].Type, listResult[0].Type);
            Assert.Equal(_reservationDTOs[0].Time, listResult[0].Time);
            Assert.Equal(_reservationDTOs[0].Comment, listResult[0].Comment);

            Assert.Equal(_reservationDTOs[1].Id, listResult[1].Id);
            Assert.Equal(_reservationDTOs[1].ClientName, listResult[1].ClientName);
            Assert.Equal(_reservationDTOs[1].Type, listResult[1].Type);
            Assert.Equal(_reservationDTOs[1].Time, listResult[1].Time);
            Assert.Equal(_reservationDTOs[1].Comment, listResult[1].Comment);

            Assert.Equal(_reservationDTOs[2].Id, listResult[2].Id);
            Assert.Equal(_reservationDTOs[2].ClientName, listResult[2].ClientName);
            Assert.Equal(_reservationDTOs[2].Type, listResult[2].Type);
            Assert.Equal(_reservationDTOs[2].Time, listResult[2].Time);
            Assert.Equal(_reservationDTOs[2].Comment, listResult[2].Comment);

            //Assert.True(_reservationDTOs.SequenceEqual(listResult));
            //Assert.Equal<IEnumerable<ReservationDTO>>(_reservationDTOs, result);
        }
        
		[Fact]
		public void PostWorksheetTest()
		{
			var newWorksheet = new WorksheetDTO
			{
				CostIds = new List<int> { 1, 2, 3 },
                ReservationId = 1
			};

			var controller = new HomeController(_context);
			var result = controller.PostWorksheet(newWorksheet);

			Assert.Equal(_worksheetDTOs.Count + 1, _context.Worksheets.Count());

            var new2Worksheet = new WorksheetDTO
            {
                CostIds = new List<int> { 1, 2 },
                ReservationId = 2
            };

            var result2 = controller.PostWorksheet(new2Worksheet);

            Assert.Equal(_worksheetDTOs.Count + 2, _context.Worksheets.Count());
        }
	}
}
