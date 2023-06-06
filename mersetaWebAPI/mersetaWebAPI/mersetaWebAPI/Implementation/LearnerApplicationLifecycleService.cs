using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Implementation
{
    public class LearnerApplicationLifecycleService : ILearnerApplicationLifecycle
    {
        private readonly mersetaContext _mersetaContext;
        public LearnerApplicationLifecycleService(mersetaContext MmersetaContext)
        {
            _mersetaContext = MmersetaContext;
           
        }
        public Task<Response<List<LearnerApplicationLifecycle>>> GetLearnerApplicationLifecycle(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> SaveLearnerApplicationLifecycle(LearnerApplicationLifecycle learnerApplicationLifecycle)
        {
            var lifecycle = new LearnerApplicationLifecycle
            {
                 Learner_Status_Id= learnerApplicationLifecycle.Learner_Status_Id,
                 Learner_Status_Date= learnerApplicationLifecycle.Learner_Status_Date,
                 Notes= learnerApplicationLifecycle.Notes,
                 Action_User_Id= learnerApplicationLifecycle.Action_User_Id,
                 Company_Learner_Id= learnerApplicationLifecycle.Company_Learner_Id,
                 Is_Deleted= learnerApplicationLifecycle.Is_Deleted,
                 Is_Notification_Set= learnerApplicationLifecycle.Is_Notification_Set,
            };
             await _mersetaContext.LearnerApplicationLifecycles.AddAsync(lifecycle);
             await _mersetaContext.SaveChangesAsync();

            return new Response<string>(learnerApplicationLifecycle.Action_User_Id.ToString(),"Lifecycle saved successfully.");
        }

        public async Task<Response<string>> SaveMultipleReason(List<LearnerApplicationReason> learnerApplicationReason)
        {
           
            await _mersetaContext.LearnerApplicationReasons.AddRangeAsync(learnerApplicationReason);
            await _mersetaContext.SaveChangesAsync();
            return new Response<string>(learnerApplicationReason[0].LearnerLifecycleId.ToString(), "Reasons Saved Successfully");
        }

        
    }
}
