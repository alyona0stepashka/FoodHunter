using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using FH.DAL.EF.Interfaces;
using FH.Models.EnumModels;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.Services
{
    public class LocationService : ILocationService
    {
        private IUnitOfWork _db { get; set; }
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        //private readonly 
        public LocationService(IUnitOfWork uow, IFileService fileService, IUserService userService)
        {
            _db = uow;
            _fileService = fileService;
            _userService = userService;
        }

        public async Task<LocationPageVM> UpdateLocationAsync(CreateLocationVM location)
        {
            var locationDb = await _db.Locations.GetByIdAsync(location.Id.Value);
            locationDb.Name = location.Name;
            locationDb.Address = location.Address;
            locationDb.Longitude = location.Longitude;
            locationDb.Latitude = location.Latitude;
            locationDb.CompanyId = location.CompanyId;
            var locNew = await _db.Locations.UpdateAsync(locationDb);
            return new LocationPageVM(locNew);
        }

        public async Task<Icon> UploadLocationTopPhoto(IFormFile photo, int locationId)
        {
            var file = await _fileService.CreateFileDbAsync(photo);
            var location = await _db.Locations.GetByIdAsync(locationId);
            location.TopFileId = file.Id;
            await _db.Locations.UpdateAsync(location);
            return file;
        }

        public async Task<LocationPageVM> GetLocationPageAsync(int id)
        {
                var location = await _db.Locations.GetByIdAsync(id);
                if (location == null)
                {
                    throw new Exception("Location not found");
                }

                var locationPhotos = _db.FileModels.GetAll().Where(m => m.LocationId == id).ToList();
                var locationPage = new LocationPageVM(location)
                {
                    //Menus = _db.Menus.GetAll().Where(m => m.LocationId == location.Id).ToList(),
                    //LocationPhotos = locationPhotos.Select(m=> $"{m.Path}{m.Name}{m.Extension}").ToList()
                };

                return locationPage;
          
        }
        public List<LocationTabVM> GetLocationsNearPoint(string lon, string lat)
        {
                var locations = _db.Locations.GetAll().Where(m => IsInside(Convert.ToDecimal(lon.Replace('.', ',')), Convert.ToDecimal(lat.Replace('.', ',')), 5, Convert.ToDecimal(m.Longitude.Replace('.', ',')), Convert.ToDecimal(m.Latitude.Replace('.', ','))));
                if (!locations.Any())
                {
                    throw new Exception("No one location found");
                }
                var locationsReturn = new List<LocationTabVM>();
                foreach (var location in locations)
                {
                    locationsReturn.Add(new LocationTabVM(location));
                }

                return locationsReturn;
          
        }

        public List<LocationTabVM> GetLocationsByCompany(int companyId)
        {
                var locations = _db.Locations.GetAll().Where(m =>m.CompanyId==companyId);
                if (!locations.Any())
                {
                    throw new Exception("No one location found");
                }
                var locationsReturn = new List<LocationTabVM>();
                foreach (var location in locations)
                {
                    locationsReturn.Add(new LocationTabVM(location));
                }

                return locationsReturn;
          
        }

        public async Task<LocationPageVM> CreateLocationAsync(CreateLocationVM location, string userId)
        {
                var newLocation = new Location()
                {
                    Name = location.Name,
                    Address = location.Address,
                    Longitude = location.Longitude,
                    Latitude = location.Latitude,
                    CompanyId = location.CompanyId,
                    AdminId = userId
                };
                var dbLocation = await _db.Locations.CreateAsync(newLocation);
                var manager = new Manager
                {
                    LocationId = newLocation.Id,
                    UserProfileId = _userService.GetUserTabVM(userId).UserProfileId
                };
                var dbManager = await _db.Managers.CreateAsync(manager);
            return new LocationPageVM(dbLocation);
          
        }

        public async Task DeleteLocationAsync(int id)
        {
                await _db.Locations.DeleteAsync(id);
          
        }
        

        static bool IsInside(decimal lonX, decimal latY, int distance, decimal x, decimal y)
        {
            return (((x - lonX) * (x - lonX) + (y - latY) * (y - latY) <= distance * distance));
        }

    }
}
