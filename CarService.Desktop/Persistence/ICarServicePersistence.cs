using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.Data;

namespace CarService.Desktop.Persistence
{
    public interface ICarServicePersistence
    {

        Task<IEnumerable<ReservationDTO>> ReadReservationsAsync(string mechanicUserName);


        Task<IEnumerable<Cost>> ReadCostsAsync();


        Task<Boolean> CreateWorksheetAsync(WorksheetDTO worksheetDTO);


        Task<Boolean> LoginAsync(String userName, String userPassword);


        Task<Boolean> LogoutAsync();
    }
}
