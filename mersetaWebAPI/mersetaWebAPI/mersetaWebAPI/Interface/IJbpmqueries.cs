using mersetaWebAPI.CustomModels;
using mersetaWebAPI.CustomModule;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Interface
{
    public interface IJbpmqueries
    {
        // User Application

        public Task<Response<long>> RegisterAsync(RegistrationRequestt request, string origin);
        public Task<Response<long>> AddUserAddressAsync(AddressRequestt request);
        public  Task<Response<long>> UpdateUserNextofkin(UpdateNextofkin model, long id);
        public Task<Response<long>> AddUserLanguagesAsync(UsersLanguageRequest request);
        public Task<Response<string>> AddCompanyAsync(CompanyRequestt request);
        public Task<Response<long>> AddCompanyLearnerAsync(CompanyLearnerRequest request);
        public Task<Response<long>> UpdateCompanyLeaner(UpdateCompanyLearner model, long id);
        public Task<Response<string>> UpdateUserAddress(UpdateUserAddress model, long id);
        public Task<Response<string>> UpdateCompanyAddress(UpdateCompanyAddress model, long id);
        public Task<Response<long>> UpdateCompanyLeanerEmployer(UpdateProvider model, long id);
        public Task<Response<string>> UpdateCopyofcontract(updatecontract model, long id);
        public Task<Response<string>> AddSdfCompany(sdfrequest model);
        public Task<Response<string>> GerhardTesting(string name,DateTime? dateTime, int numbers, string address);
        public Task<Response<StatssaAreaCode>> GetStatsAreaCode(long id);
        public Task<Response<List<UsersLanguage>>> GetUsersLanguages(long id);

    }
}
