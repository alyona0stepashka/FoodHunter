using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Infrastructure;
using FH.BLL.VMs;
using FH.Models.EnumModels;
using FH.Models.StaticModels;

namespace FH.BLL.Interfaces
{
    public interface IStaticService
    {
        List<SearchTabVM> GetSearchResult(SearchQueryVM search);
        List<Sex> GetSexes();
        List<IconVM> GetIcons();
        List<CompanySpecification> GetSpecifications();
        ChartDataClientVM GetChartDataClient(string userId);
        ChartDataManagerVM GetChartDataManager(string userId);
    }
}
