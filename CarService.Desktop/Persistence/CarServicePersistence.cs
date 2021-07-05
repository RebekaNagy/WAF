using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CarService.Data;
using Newtonsoft.Json;

namespace CarService.Desktop.Persistence
{
    class CarServicePersistence : ICarServicePersistence
    {
        private readonly HttpClient _client;

        public CarServicePersistence(String baseAddress)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public async Task<IEnumerable<ReservationDTO>> ReadReservationsAsync(string mechanicUserName)
        {
            try
            {
                
                HttpResponseMessage response = await _client.GetAsync($"api/Home/{mechanicUserName}");
                if (response.IsSuccessStatusCode) 
                {
                    IEnumerable<ReservationDTO> reservations = await response.Content.ReadAsAsync<IEnumerable<ReservationDTO>>();

                    return reservations;
                }
                else
                {
                    throw new PersistenceUnavailableException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }

        }

        public async Task<IEnumerable<Cost>> ReadCostsAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/Home/");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<Cost>>();
                }
                else
                {
                    throw new PersistenceUnavailableException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        public async Task<Boolean> CreateWorksheetAsync(WorksheetDTO worksheetDTO)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/home/", worksheetDTO);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Boolean> LoginAsync(String userName, String userPassword)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/account/login/" + userName + "/" + userPassword);
                return response.IsSuccessStatusCode; 
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        public async Task<Boolean> LogoutAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/account/logout");
                return !response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }
    }
}
