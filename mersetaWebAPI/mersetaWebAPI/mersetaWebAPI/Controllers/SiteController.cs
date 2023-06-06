using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace mersetaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteController : Controller
    {
        private ISite _sites;
        public SiteController(ISite sites)
        {

            _sites = sites;
        }

        [HttpPost("CreateSiteVisit")]
        public async Task<ActionResult> CreateSiteVisit(SiteVisit sitevisit)
        {
            var response = await _sites.CreateSiteVisit(sitevisit);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            return NotFound("User not found");
        }

        [HttpGet("GetSiteVisit")]
        public async Task<ActionResult> GetSiteVisit(long assessmentbacthid)
        {
            var response = await _sites.GetSiteVisit(assessmentbacthid);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            return NotFound("User not found");
        }

        [HttpGet("getCompanySite")]
        public async Task<ActionResult> getCompanySite(long companyid)
        {
            var response = await _sites.getCompanySite(companyid);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            return NotFound("User not found");
        }
    }
}
