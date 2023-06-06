using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Interface
{
    public interface ICompanyLeaners
    {
        public Task<Response<List<User>>> GetAllCompanyLearners(int id);
        public Response<List<User>> GetAllCompanyLearnersByFilters(long? userId, long? companyId, long? region, string? levyNumber, string? accreditationNumber, long? status);
        public Task<Response<long>> UpdateCompanyLearnerStatus(long id,int status);
        public Task<Response<long>> UpdateEmployerId(long CompanyId,long CompanyLearnerId);
        public Task<Response<long>> UpdateJBPMEmployerId(long CompanyId, long CompanyLearnerId);
    }
}
