using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.Data;

namespace CarService.Desktop.Model
{
    public interface ICarServiceModel
    {
        IReadOnlyList<Cost> Costs { get; }

        IReadOnlyList<ReservationDTO> Reservations { get; }

        void ClearAddedCosts();

        ReservationDTO SelectedReservation { get; set; }

        IReadOnlyList<Cost> AddedCosts { get; }

        void AddCost(Cost cost);

        void RemoveCost(Cost cost);

        Boolean IsUserLoggedIn { get; }

        Task LoadAsync();

        Task SaveAsync();

        Task<Boolean> LoginAsync(String userName, String userPassword);

        Task<Boolean> LogoutAsync();
    }
}
