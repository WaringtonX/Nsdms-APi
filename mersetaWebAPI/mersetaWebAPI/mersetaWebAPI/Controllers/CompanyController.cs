using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mersetaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly mersetaContext MmersetaContext;
        // MySqlConnection conn = new MySqlConnection("server=156.38.134.211;user id=merseta;password=M3rseta@345;port=3306;database=merseta;T");
        private ICompany icompany;
        public CompanyController(mersetaContext MmersetaContext, ICompany icompany)
        {
            this.MmersetaContext = MmersetaContext;
            this.icompany = icompany;
        }

        //get all users
        [HttpGet]
        public async Task<ActionResult> GetAllCompany()
        {
            var company = icompany.GetAllCompanies();
            if (company.Succeeded)
            {
                return Ok(company.Data);
            }
            return NotFound("Providers not found");
        }
        //get all company Sites
        [HttpGet("CompanySites")]
        public async Task<ActionResult> GetAllCompanySites(long companyId)
        {
            var company =await icompany.GetCompanySites(companyId);
            if (company.Succeeded)
            {
                return Ok(company.Data);
            }
            return NotFound("sites not found");
        }

        //get all learneship Providers
        [HttpGet("LearnershipProviders")]
        public async Task<ActionResult> GetAllLearnershipCompany()
        {
            var company =await icompany.GetAllCompaniesInLearships();
            if (company.Succeeded)
            {
                return Ok(company.Data);
            }
            return NotFound("Providers not found");
        }
        [HttpGet("GetSingleProviderByAccreditation")]
        public async Task<ActionResult> GetSinleProvider(string accreditationNumber)
        {
            var company = icompany.GetSingleCompaniesByAccreditationNumber(accreditationNumber);
            if (company.Succeeded)
            {
                return Ok(company.Data);
            }
            return NotFound("Providers not found");
        }

        [HttpGet("GetCompanyQualification")]
        public async Task<ActionResult> GetCompanyQualification(long companyId)
        {
            var company = icompany.GetQualificationByCompany(companyId);
            if (company.Succeeded)
            {
                return Ok(company.Data);
            }
            return NotFound("qualifications not found");
        }

        [HttpGet("CompanyQualificationByCompanyId")]
        public async Task<ActionResult> GetCompanyQualificationById(long companyId)
        {
            var company = await icompany.GetCompanyQualification(companyId);
            if (company.Succeeded)
            {
                return Ok(company.Data);
            }
            return NotFound("qualifications not found");
        }
    }
}
