using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Infrastructure;
using FH.BLL.VMs;

namespace FH.BLL.Interfaces
{
    public interface IFeedbackService
    {
        List<FeedbackVM> GetFeedbacksByLocation(int locationId);
        List<FeedbackVM> GetFeedbacksByMenuItem(int itemId);
        Task<FeedbackVM> CreateFeedbackAsync(CreateFeedbackVM f);
    }
}
