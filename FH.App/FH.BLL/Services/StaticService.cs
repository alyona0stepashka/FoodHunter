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
using FH.Models.StaticModels;

namespace FH.BLL.Services
{
    public class StaticService: IStaticService
    {
        private IUnitOfWork _db { get; set; }
        //private readonly 
        public StaticService(IUnitOfWork uow)
        {
            _db = uow;
        }

        public List<SearchTabVM> GetSearchResult(SearchQueryVM search)
        {
            var retList = new List<SearchTabVM>();

            if (search.SearchObjs.Contains("location"))
            {
                var locs = _db.Locations.GetAll().Where(m => m.Name.Contains(search.Key)).ToList();
                retList.AddRange(locs.Select(m=>new SearchTabVM(m)));
            }

            if (search.SearchObjs.Contains("company"))
            {
                var comps = _db.Companys.GetAll().Where(m => m.Name.Contains(search.Key)).ToList();
                retList.AddRange(comps.Select(m => new SearchTabVM(m)));
            }

            return retList;
        }

        public ChartDataClientVM GetChartDataClient(string userId)
        {
            var retVal = new ChartDataClientVM();
            var client = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId);
            var orders = client.OrderUsers.Select(m=>m.Order);
            retVal.AverageRate = !client.Feedbacks.Any() ? 0 : client.Feedbacks.Average(m => m.Stars);
            retVal.BadRateCount = client.Feedbacks.Count(m => m.Stars < 3);
            retVal.NormalRateCount = client.Feedbacks.Count(m => m.Stars == 3 || m.Stars == 4);
            retVal.PerfectRateCount = client.Feedbacks.Count(m => m.Stars == 5);
            var ordersLastMonth = client.OrderUsers.Where(m =>
                m.Order.EndDate != null && m.Order.EndDate.Value.Month == DateTime.Now.Month);
            retVal.OrdersLastMonth = ordersLastMonth.Count();
            for (var i = 1; i <= 12; i++)
            {
                var ordersForMonth = orders.Where(b => b.EndDate != null && ((DateTime) b.EndDate).Month == i);
                var countPayment = ordersForMonth.Select(m =>
                    m.OrderItems.Where(e=>e.UserId==client.Id).Select(n => (n.PricePerItem * (decimal)n.Count)).Sum()).Sum();
                retVal.ClientPaymentActivity.Add(countPayment);
            }
            retVal.PaymentLastMonth = retVal.ClientPaymentActivity[DateTime.Now.Month - 1];
            retVal.VisitedLocationsLastMonth = ordersLastMonth.Select(m => m.Order.Manager.LocationId).Distinct().Count();
            return retVal;
        }

        public ChartDataManagerVM GetChartDataManager(string userId)
        {
            var retVal = new ChartDataManagerVM(); 
            var manager = _db.Managers.GetAll().FirstOrDefault(m => m.UserProfile.UserId == userId); 
            var location = manager.Location;
            var orders = location.Orders;
            retVal.OrdersLastMonth = orders.Count;
            retVal.AverageRate = !location.Feedbacks.Any() ? 0 : location.Feedbacks.Average(m => m.Stars);
            retVal.BadRateCount = location.Feedbacks.Count(m => m.Stars<3);
            retVal.NormalRateCount = location.Feedbacks.Count(m => m.Stars == 3 || m.Stars == 4);
            retVal.PerfectRateCount = location.Feedbacks.Count(m => m.Stars==5);
            for (var i = 1; i <= 12; i++)
            {
                var ordersForMonth = orders.Where(b => b.EndDate != null && ((DateTime) b.EndDate).Month == i);
                var countPayment = ordersForMonth.Select(m =>
                    m.OrderItems.Select(n => ( n.PricePerItem * (decimal)n.Count)).Sum()).Sum();
                retVal.ClientPaymentActivity.Add(countPayment);
            } 
            retVal.PaymentLastMonth = retVal.ClientPaymentActivity[DateTime.Now.Month - 1];
            var ordersToday = orders.Where(b => (b.StartDate) == DateTime.Today);
            var ordersYesterday = orders.Where(b => (b.StartDate) == DateTime.Today.AddDays(-1));
            retVal.ClientsToday = ordersToday.Select(m => m.OrderUsers.Count).Sum();
            for (var i = 0; i < 24; i++)
            {
                var tablesToday = ordersToday.Count(m => (m.StartDate.Hour <= i && m.EndDate == null) ||
                                                         (m.StartDate.Hour <= i && m.EndDate.Value.Hour >= i));
                var tablesYesterday = ordersYesterday.Count(m => (m.StartDate.Hour <= i && m.EndDate == null) ||
                                                         (m.StartDate.Hour <= i && m.EndDate.Value.Hour >= i));
                retVal.TableOccupancyToday.Add(tablesToday);
                retVal.TableOccupancyYesterday.Add(tablesYesterday);
            }
            return retVal;
        }

        public List<SexVM> GetSexes()
        {
            var items = _db.Sexes.GetAll().Select(m=>new SexVM(m)).ToList();
            if (items == null)
            {
                throw new Exception("Sexes not found");
            }
            return items; 
        }
        public List<CompanySpecificationVM> GetSpecifications()
        {
            var items = _db.CompanySpecifications.GetAll().Select(m => new CompanySpecificationVM(m)).ToList();
            if (items == null)
            {
                throw new Exception("Specifications not found");
            }
            return items; 
        }

        public List<IconVM> GetIcons()
        {
            var items = _db.Icons.GetAll().Select(m=>new IconVM(m)).ToList();
            if (items == null)
            {
                throw new Exception("Icons not found");
            }
            return items; 
        }
    }
}
