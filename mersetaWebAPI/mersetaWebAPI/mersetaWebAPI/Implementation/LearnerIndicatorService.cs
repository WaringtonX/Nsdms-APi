using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Implementation
{
    public class LearnerIndicatorService : ILearnerIndicator
    {
        private readonly mersetaContext MmersetaContext;

        public LearnerIndicatorService(mersetaContext MmersetaContext)
        {
            this.MmersetaContext = MmersetaContext;
        }
        public Response<List<LearnerProgramIndicator>> GetAllLearnerProgramIndicator(long id)
        {
            try
            {
                List<LearnerProgramIndicator> indicator = new List<LearnerProgramIndicator>();
                indicator = this.MmersetaContext.LearnerProgramIndicators.Where(x=> x.Learning_Program_Id==id).ToList();
                return new Response<List<LearnerProgramIndicator>>(indicator);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }
       /* public string AddLearnerProgramIndicator(int indicatorproviderid,int companyid)
         {
             try
             {
                 CompanyLearner comp = new CompanyLearner();
                 //LinkedList<LearnerProgramIndicator> indicators = new List<LearnerProgramIndicator>();
                 comp.LearnerProgramIndicator = indicatorproviderid;
                this.MmersetaContext.LearnerApplicationLifecycles.Add(comp);
                this.MmersetaContext.SaveChanges();
                return "completed";
            }
             catch
             {

             }
         }*/
        /*
         *  [HttpPost]
        public async Task<ActionResult<UserDetails>> PostUserDetails(UserDetails userDetails)
        {
            _context.UserDetails.Add(userDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDetails", new { id = userDetails.UserID }, userDetails);
        }
                public string ApprovalComments(int apcomlearnID, DateTime statusdate, int snetNotification, string apcomment, ulong apisdeleted,
            DateTime apdeletedon, int actionuser, int companylearnerid)
        {
            try
            {
                LearnerApplicationLifecycle coments = new LearnerApplicationLifecycle();
                coments.Learner_Status_Id = apcomlearnID;
                coments.Learner_Status_Date = statusdate;
                coments.Is_Notification_Set = snetNotification;
                coments.Notes = apcomment;
                coments.Is_Deleted = apisdeleted;
                coments.Deleted_On = apdeletedon; 
                coments.Action_User_Id = actionuser;
                coments.Company_Learner_Id = companylearnerid;
                this.MmersetaContext.LearnerApplicationLifecycles.Add(coments);
                this.MmersetaContext.SaveChanges(); 
                return "completed";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }*/
    }
}
