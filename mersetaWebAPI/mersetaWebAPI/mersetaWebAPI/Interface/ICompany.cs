using mersetaWebAPI.CustomModels.Company;
using mersetaWebAPI.CustomModels.Provider;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Interface
{
    public interface ICompany
    {
        public Response<List<Company>> GetAllCompanies();
        public Task<Response<List<Company>>> GetAllCompaniesInLearships();
        public Response<List<Company>> GetSingleCompanies(int id);
        public Response<List<ProviderResponse>> GetSingleCompaniesByAccreditationNumber(string accreditationNumber);
        public Response<List<Company>> GetSingleCompaniesByLevyNumber(string levyNumber);
        public Response<List<SaqaQualificationResponse>> GetQualificationByCompany(long qualification);
        public Task<Response<List<SaqaQualification>>> GetCompanyQualification(long companyId);
        public Task<Response<List<Site>>> GetCompanySites(long companyId);
    }
}
