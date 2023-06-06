using mersetaWebAPI.CustomModels.Address;
using mersetaWebAPI.CustomModels.Company;
using mersetaWebAPI.CustomModels.EmailModels;
using mersetaWebAPI.CustomModels.Provider;
using mersetaWebAPI.CustomModels.UserModels;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Interface
{
    public interface IUsers
    {
        public Response<List<User>> GetAllUsers();
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegistrationRequest request, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model, int id, string IpAddress);
        Task<AuthenticationResponse> GetUserById(Guid id);
        //Task LogoutAsync();
        Task<string> ResendVericationCodeAsync(ResentOTPRequest model);
        Task<Response<string>> UpdateProfile(UserUpdate model, long id);

        Task<Response<string>> ConfirmEmailAsync(VerifyEmailRequest request);
        Task<Response<string>> ForgotPassword(string email, string origin);
        Task<Response<string>> UpdateUserAddress(AddressRequest address, long id);
        Task<Response<string>> UpdateCompanyAddress(AddressRequest address, long id);
        Task<Response<string>> UpdateUserCompany(long companyId , long id);
        public Task<Response<string>> UpdateTrainingInformation(TraininingInformationRequest request,long id);
        public Task<Response<string>> UpdateProviderInformation(long  PrividerId, long id);
        public Task<Response<UserLearnerResponse>> GetLearnerByIDNumber(string idnumber);
        public Task<Response<long?>> GetSDPCompanyByUserId(long userId);
        public Task<Response<long?>> GetSDFCompanyByUserId(long userId);
    }
}
