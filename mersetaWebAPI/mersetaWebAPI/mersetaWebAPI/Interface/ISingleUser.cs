using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Interface
{
    public interface ISingleUser
    {
        public Response<User> GetSingleUser(int id);


        public Task<Response<List<User>>> GetAssessmentUser(long assessmentid);
        public Task<Response<List<SummativeAssessmentReportTest>>> GetSummativeReportList(long assessmentid);

    }
}
