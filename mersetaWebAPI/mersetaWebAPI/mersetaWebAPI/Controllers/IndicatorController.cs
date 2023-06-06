using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using Microsoft.AspNetCore.Mvc;

namespace mersetaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndicatorController : Controller 
    {
        private readonly mersetaContext MmersetaContext;

        private ILearnerIndicator induction;
        public IndicatorController(mersetaContext MmersetaContext, ILearnerIndicator _induction)
        {
            this.MmersetaContext = MmersetaContext;
            induction = _induction;
        }
        //get all Indicators
        [HttpGet("LearnerProgramIndicator")]
        public async Task<ActionResult> GetAllLearnerProgramIndicator(long id)
        {
            var _inductions = induction.GetAllLearnerProgramIndicator(id);
            if (_inductions.Succeeded)
            {
                return Ok(_inductions.Data);
            }
            return NotFound("Provinces not found");
        }
    }
}
