using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.VMs;

namespace FH.BLL.Interfaces
{
    public interface ILocationService
    {
        Task<LocationPageVM> GetLocationPageAsync(int id);
        List<LocationTabVM> GetLocationsNearPoint(decimal lon, decimal lat);
        List<LocationTabVM> GetLocationsByCompany(int CompanyId);
        Task<LocationPageVM> CreateLocationAsync(CreateLocationVM location, string userId);
        Task DeleteLocationAsync(int id);
    }
}
