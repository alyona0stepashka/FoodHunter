using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using FH.DAL.EF.Interfaces;
using FH.Models.Models;

namespace FH.BLL.Services
{
    public class LocationService : ILocationService
    {
        private IUnitOfWork _db { get; set; }
        //private readonly 
        public LocationService(IUnitOfWork uow)
        {
            _db = uow;
        }

        public async Task<LocationPageVM> GetLocationPageAsync(int id)
        {
            try
            {
                var location = await _db.Locations.GetByIdAsync(id);
                if (location == null)
                {
                    throw new Exception("Location not found");
                }

                var locationPhotos = _db.FileModels.GetWhere(m => m.LocationId == id).ToList();
                var locationPage = new LocationPageVM(location)
                {
                    Menus = _db.Menus.GetWhere(m => m.LocationId == location.Id).ToList(),
                    LocationPhotos = locationPhotos.Select(m=> $"{m.Path}{m.Name}{m.Extension}").ToList()
                };

                return locationPage;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<LocationTabVM> GetLocationsNearPoint(decimal lon, decimal lat)
        {
            try
            {
                var locations = _db.Locations.GetWhere(m => IsInside(lon, lat, 5, m.Longitude, m.Latitude));
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<LocationTabVM> GetLocationsByCompany(int CompanyId)
        {
            try
            {
                var locations = _db.Locations.GetWhere(m =>m.CompanyId==CompanyId);
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<LocationPageVM> CreateLocationAsync(CreateLocationVM location, string userId)
        {
            try
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
                return new LocationPageVM(await _db.Locations.CreateAsync(newLocation));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteLocationAsync(int id)
        {
            try
            {
                await _db.Locations.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }





        static bool IsInside(decimal lon_x, decimal lat_y, int distance, decimal x, decimal y)
        {
            // Compare radius of circle with 
            // distance of its center from 
            // given point 
            return (((x - lon_x) * (x - lon_x) + (y - lat_y) * (y - lat_y) <= distance * distance));
        }

    }
}
