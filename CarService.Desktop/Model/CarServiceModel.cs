using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.Desktop.Persistence;
using CarService.Data;

namespace CarService.Desktop.Model
{
    class CarServiceModel : ICarServiceModel
    {
        private readonly ICarServicePersistence _persistence;
        private List<ReservationDTO> _reservations;
        private List<Cost> _costs;
        private HashSet<Cost> _addedCosts;
        private string _mechanicUserName;

        public ReservationDTO SelectedReservation { get; set; }

        public void ClearAddedCosts()
        {
            _addedCosts = new HashSet<Cost>();
        }

        public IReadOnlyList<Cost> AddedCosts
        {
            get { return _addedCosts.ToList(); }
        }

        public void AddCost(Cost cost)
        {
            _addedCosts.Add(cost);
        }

        public void RemoveCost(Cost cost)
        {
            _addedCosts.Remove(cost);
        }

        public CarServiceModel(ICarServicePersistence persistence)
        {
            IsUserLoggedIn = false;
            _persistence = persistence ?? throw new ArgumentNullException(nameof(persistence));
        }

        public IReadOnlyList<ReservationDTO> Reservations => _reservations;

        public IReadOnlyList<Cost> Costs => _costs;

        public Boolean IsUserLoggedIn { get; private set; }

        public async Task LoadAsync()
        {
            _reservations = (await _persistence.ReadReservationsAsync(_mechanicUserName)).ToList();
            _costs = (await _persistence.ReadCostsAsync()).ToList();
        }

        public async Task SaveAsync()
        {
            WorksheetDTO worksheetDTO = new WorksheetDTO
            {
                ReservationId = SelectedReservation.Id,
                CostIds = new List<int>()
            };
            foreach (var addedCost in AddedCosts)
            {
                worksheetDTO.CostIds.Add(addedCost.Id);
            }

            await _persistence.CreateWorksheetAsync(worksheetDTO);            
        }

        public async Task<Boolean> LoginAsync(String userName, String userPassword)
        {
            IsUserLoggedIn = await _persistence.LoginAsync(userName, userPassword);
            if (IsUserLoggedIn) _mechanicUserName = userName;
            return IsUserLoggedIn;
        }

        public async Task<Boolean> LogoutAsync()
        {
            if (!IsUserLoggedIn)
                return true;

            IsUserLoggedIn = !(await _persistence.LogoutAsync());

            if (!IsUserLoggedIn) _mechanicUserName = null;

            return IsUserLoggedIn;
        }
    }
}
