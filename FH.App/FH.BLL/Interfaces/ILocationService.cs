using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.VMs;
using FH.Models.EnumModels;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.Interfaces
{
    public interface ILocationService
    {
        Task<LocationPageVM> GetLocationPageAsync(int id);
        List<LocationTabVM> GetLocationsNearPoint(string lon, string lat);
        List<LocationTabVM> GetLocationsByCompany(int companyId);
        Task<LocationPageVM> CreateLocationAsync(CreateLocationVM location, string userId);
        Task<LocationPageVM> UpdateLocationAsync(CreateLocationVM location);
        Task<IconVM> UploadLocationTopPhoto(IFormFile photo, int locationId);
        Task DeleteLocationAsync(int id);
    }
}
