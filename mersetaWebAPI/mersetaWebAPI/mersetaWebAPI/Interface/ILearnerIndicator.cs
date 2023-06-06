using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Interface
{
    public interface ILearnerIndicator
    {
        public Response<List<LearnerProgramIndicator>> GetAllLearnerProgramIndicator(long id);
    }
}
