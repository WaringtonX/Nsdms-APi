using mersetaWebAPI.CustomModels.EmailModels;

namespace mersetaWebAPI.Interface
{
    public interface IEmailService
    {
        void Send(EmailRequest request);

    }
}
