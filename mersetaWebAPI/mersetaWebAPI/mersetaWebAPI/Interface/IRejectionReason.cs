using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Interface
{
    public interface IRejectionReason
    {
        public Response<List<RejectReason>> GetAllRejections();
    }
}
