using mersetaWebAPI.CustomModels;
using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mersetaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyLeanerController : Controller
    {
        private readonly mersetaContext MmersetaContext;
        // MySqlConnection conn = new MySqlConnection("server=156.38.134.211;user id=merseta;password=M3rseta@345;port=3306;database=merseta;T");
        private ICompanyLeaners icompanyLeaners;
        public CompanyLeanerController(mersetaContext MmersetaContext, ICompanyLeaners icompanyLeaners)
        {
            this.MmersetaContext = MmersetaContext;
            this.icompanyLeaners = icompanyLeaners;
        }

        //get all users
        [HttpGet]
        public async Task<ActionResult> GetAllCompanyLearners(int id)
        {
            var companyleaners =await icompanyLeaners.GetAllCompanyLearners(id);
            if (companyleaners.Succeeded)
            {
                return Ok(companyleaners.Data);
            }
            return NotFound("User not found");
        }
        //get all users
        [HttpGet("SearchLearners")]
        public async Task<ActionResult> GetAllCompanyLearnersByFilters(long? userId, long? companyId, long? region, string? levyNumber, string? accreditationNumber, long? status)
        {
            var companyleaners = icompanyLeaners.GetAllCompanyLearnersByFilters(userId,companyId,region,levyNumber,accreditationNumber,status);
            if (companyleaners.Succeeded)
            {
                return Ok(companyleaners.Data);
            }
            return NotFound("User not found");
        }

        //update company learner status
        [HttpPut("UpdateCompanyLearnerStatus")]
        public async Task<ActionResult> UpdateCompanyLearnersStatus(long id,int status)
        {
            var companyleaners = await icompanyLeaners.UpdateCompanyLearnerStatus(id,status);
            if (companyleaners.Succeeded)
            {
                return Ok(companyleaners.Data);
            }
            return NotFound(companyleaners.Errors);
        }

        //update company learner status
        [HttpPut("UpdateCompanyId")]
        public async Task<ActionResult> UpdateEmployerId(long companyId, long companyLearnerId)
        {
            var companyleaners = await icompanyLeaners.UpdateEmployerId(companyId, companyLearnerId);
            if (companyleaners.Succeeded)
            {
                return Ok(companyleaners.Data);
            }
            return NotFound("Error while updating!");
        }
        //update company learner status
        [HttpPost("UpdateJBPMCompanyId")]
        public async Task<ActionResult> UpdateJBPMEmployerId(UpdateEmployerRequest request)
        {
            var companyleaners = await icompanyLeaners.UpdateJBPMEmployerId(request.CompanyId, request.CompanyLearnerId);
            if (companyleaners.Succeeded)
            {
                return Ok(companyleaners.Data);
            }
            return NotFound("Error while updating!");
        }
    }
}
