using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Interface
{
    public interface ILearnerApplicationLifecycle
    {
        public Task<Response<List<LearnerApplicationLifecycle>>> GetLearnerApplicationLifecycle(long id);
        public Task<Response<string>> SaveLearnerApplicationLifecycle(LearnerApplicationLifecycle learnerApplicationLifecycle);
        public Task<Response<string>> SaveMultipleReason(List<LearnerApplicationReason> learnerApplicationReason);
    }
}
