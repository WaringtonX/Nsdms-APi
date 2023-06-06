using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Implementation
{
    public class RejectionReasonsList : IRejectionReason
    {
        private readonly mersetaContext MmersetaContext;

        public RejectionReasonsList(mersetaContext MmersetaContext)
        {
            this.MmersetaContext = MmersetaContext;
        }

        public Response<List<RejectReason>> GetAllRejections()
        {
            try
            {
                List<RejectReason> rejectReasons = new List<RejectReason>();
                rejectReasons = this.MmersetaContext.RejectReasons.ToList();
                return new Response<List<RejectReason>>(rejectReasons);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }
    }
}
