using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Infrastructure;
using FH.BLL.VMs;
using FH.Models.EnumModels;

namespace FH.BLL.Interfaces
{
    public interface IStaticService
    {
        List<Sex> GetSexes();
    }
}
