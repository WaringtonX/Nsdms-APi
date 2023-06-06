using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using Microsoft.AspNetCore.Mvc;

namespace mersetaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RejectionReasonController : Controller
    {
        private readonly mersetaContext MmersetaContext;

        private IRejectionReason rejection;
        public RejectionReasonController(mersetaContext MmersetaContext, IRejectionReason _rejection)
        {
            this.MmersetaContext = MmersetaContext;
            rejection = _rejection;
        }

        //get all Rejections
        [HttpGet("RejectReason")]
        public async Task<ActionResult> GetAllRejections()
        {
            var _rejections = rejection.GetAllRejections();
            if (_rejections.Succeeded)
            {
                return Ok(_rejections.Data);
            }
            return NotFound("Provinces not found");
        }
    }
}
