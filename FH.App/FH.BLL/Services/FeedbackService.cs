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
    public class FeedbackService : IFeedbackService
    {
        private IUnitOfWork _db { get; set; }

        private readonly IFileService _fileService;
        //private readonly 
        public FeedbackService(IUnitOfWork uow, IFileService fileService)
        {
            _db = uow;
            _fileService = fileService;
        }

       public List<FeedbackVM> GetFeedbacksByLocation(int locationId)
        {
            var ret = _db.Feedbacks.GetAll().Where(m => m.LocationId == locationId).Select(m=>new FeedbackVM(m, m.UserProfile, m.Photos)).ToList();
            return ret;
        }

       public List<FeedbackVM> GetFeedbacksByMenuItem(int itemId)
       {
           var ret = _db.Feedbacks.GetAll().Where(m => m.MenuItemId == itemId).Select(m => new FeedbackVM(m, m.UserProfile, m.Photos)).ToList();
           return ret;
        }

       public async Task<FeedbackVM> CreateFeedbackAsync(CreateFeedbackVM f)
       {
           var dbFeedback = new Feedback
           {
               Stars = f.Stars,
               Comment = f.Comment,
               UserProfileId = f.UserProfileId
           };
           if (f.LocationId != null)
           {
               dbFeedback.LocationId = f.LocationId;
           }
           if (f.MenuItemId != null)
           {
               dbFeedback.MenuItemId = f.MenuItemId;
           }

           var newFeedback = await _db.Feedbacks.CreateAsync(dbFeedback);
           var f2 = _db.Feedbacks.GetAll().FirstOrDefault(m => m.Id == newFeedback.Id);
           if (f2 != null)
           {
               var retFeedback = new FeedbackVM(f2, f2.UserProfile, f2.Photos);
               if (f.UploadPhoto != null)
               {
                   await _fileService.CreateFileDbAsync(f.UploadPhoto, retFeedback.Id);
               }
               return retFeedback;
           }
           throw new Exception("feedback not found");
       }
    }
}
