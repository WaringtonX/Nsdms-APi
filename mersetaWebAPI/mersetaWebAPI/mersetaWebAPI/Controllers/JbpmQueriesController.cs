using mersetaWebAPI.CustomModels;
using mersetaWebAPI.CustomModule;
using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mersetaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JbpmQueriesController : Controller
    {
        private readonly mersetaContext MmersetaContext;
        // MySqlConnection conn = new MySqlConnection("server=156.38.134.211;user id=merseta;password=M3rseta@345;port=3306;database=merseta;T");
        private IJbpmqueries lookups;

        public JbpmQueriesController(mersetaContext MmersetaContext, IJbpmqueries _lookups)
        {
            this.MmersetaContext = MmersetaContext;
            lookups = _lookups;
        }


        //get all user address
        [HttpPost("Register")]
        public async Task<ActionResult> Registeruser(RegistrationRequestt request, string origin)
        {
            try
            {

                var _com = await lookups.RegisterAsync(request,origin);
                if (_com.Succeeded)
                {
                    return Ok(_com);
                }
                else
                {
                    return Ok(_com);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return NotFound();
        }

        //get all user address
        [HttpPost("Address")]
        public async Task<ActionResult> Address(AddressRequestt request)
        {
            var _com = await lookups.AddUserAddressAsync(request);
            if (_com.Succeeded)
            {
                return Ok(_com.Data);
            }
            else
            {
                return Ok(_com);
            }
            return NotFound("problem with address information");
        }

        //get all user address
        [HttpPost("UpdateNextofKin")]
        public async Task<ActionResult> UpdateNextofkin(UpdateNextofkin model, long id)
        {
            try
            {
                var nextof = await lookups.UpdateUserNextofkin(model, id);
                if (nextof.Succeeded)
                {
                    return Ok(nextof.Data);
                }
                else
                {
                    return Ok(nextof);
                }
                throw new Exception(nextof.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get all user address
        [HttpPost("UserLanguages")]
        public async Task<ActionResult> UpdateUserKanguages(UsersLanguageRequest request)
        {
            try
            {
                var ulang = await lookups.AddUserLanguagesAsync(request);
                if (ulang.Succeeded)
                {
                    return Ok(ulang.Data);
                }
                else
                {
                    return Ok(ulang);
                }   
                throw new Exception(ulang.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get all user address
        [HttpPost("AddCompany")]
        public async Task<ActionResult> AddCompany(CompanyRequestt request)
        {
            var _com = await lookups.AddCompanyAsync(request);
            if (_com.Succeeded)
            {
                return Ok(_com.Data);
            }
            return NotFound("Problem with Company registration");
        }

        //get all user address
        [HttpPost("AddCompanyLearner")]
        public async Task<ActionResult> AddCompanyLearner(CompanyLearnerRequest request)
        {
            var _com = await lookups.AddCompanyLearnerAsync(request);
            if (_com.Succeeded)
            {
                return Ok(_com.Data);
            }
            else
            {
                return Ok(_com);
            }
            return NotFound("Problem with Company Learner registration");
        }

        //get all user address
        [HttpPost("UpdateCompanyLearner")]
        public async Task<ActionResult> UpdateCompanyLearner(UpdateCompanyLearner model, long id)
        {
            try
            {
                var nextof = await lookups.UpdateCompanyLeaner(model, id);
                if (nextof.Succeeded)
                {
                    return Ok(nextof.Data);
                }
                else
                {
                    return Ok(nextof);
                }
                throw new Exception(nextof.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get all user address
        [HttpPost("UpdateUserAddress")]
        public async Task<ActionResult> UpdateUserAddress(UpdateUserAddress model, long id)
        {
            try
            {
                var uaddress = await lookups.UpdateUserAddress(model, id);
                if (uaddress.Succeeded)
                {
                    return Ok(uaddress.Data);
                }
                else
                {
                    return Ok(uaddress);
                }
                throw new Exception(uaddress.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get all user address
        [HttpPost("UpdateCompanyAddress")]
        public async Task<ActionResult> UpdateCompanyAddress(UpdateCompanyAddress model, long id)
        {
            try
            {
                var ucompany = await lookups.UpdateCompanyAddress(model, id);
                if (ucompany.Succeeded)
                {
                    return Ok(ucompany.Data);
                }
                else
                {
                    return Ok(ucompany);
                }   
                throw new Exception(ucompany.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get all user address
        [HttpPost("UpdateCompanyEmnployer")]
        public async Task<ActionResult> UpdateCompanyAddress(UpdateProvider model, long id)
        {
            try
            {
                var ucompany = await lookups.UpdateCompanyLeanerEmployer(model, id);
                if (ucompany.Succeeded)
                {
                    return Ok(ucompany.Data);
                }
                else
                {
                    return Ok(ucompany);
                }
                throw new Exception(ucompany.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UpdateJBPMCompanyEmployer")]
        public async Task<ActionResult> UpdateJBPMCompanyAddress(UpdateProviderRequest model)
        {
            try
            {

                //
                var provider= new UpdateProvider { CompanyId = model.CompanyId ,EmployerId=model.EmployerId};
                var ucompany = await lookups.UpdateCompanyLeanerEmployer(provider, model.LearnerId);
                if (ucompany.Succeeded)
                {
                    return Ok(ucompany.Data);
                }
                else
                {
                    return Ok(ucompany);
                }
                throw new Exception(ucompany.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //get all user address
        [HttpPost("UpdateOfContract")]
        public async Task<ActionResult> UpdateOfContract(updatecontract model, long id)
        {
            try
            {
                var ucompany = await lookups.UpdateCopyofcontract(model, id);
                if (ucompany.Succeeded)
                {
                    return Ok(ucompany.Data);
                }
                else
                {
                    return Ok(ucompany);
                }
                throw new Exception(ucompany.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get all user address
        [HttpPost("AddSdfCompany")]
        public async Task<ActionResult> AddSdfCompany(sdfrequest model)
        {
            try
            {
                var sdf = await lookups.AddSdfCompany(model);
                if (sdf.Succeeded)
                {
                    return Ok(sdf.Data);
                }
                else
                {
                    return Ok(sdf);
                }
                throw new Exception(sdf.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetStatsSaAreacode")]
        public async Task<ActionResult> GetStatsAreacode(long id)
        {
            try
            {
                var sdf = await lookups.GetStatsAreaCode(id);
                if (sdf.Succeeded)
                {
                    return Ok(sdf.Data);
                }
                else
                {
                    return Ok(sdf);
                }
                throw new Exception(sdf.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetUsersLanguages")]
        public async Task<ActionResult> GetUsersLanguages(long Userid)
        {
            try
            {
                var sdf = await lookups.GetUsersLanguages(Userid);
                if (sdf.Succeeded)
                {
                    return Ok(sdf.Data);
                }
                else
                {
                    return Ok(sdf);
                }
                throw new Exception(sdf.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //testing
        [HttpGet("GerhardTesting")]
        public async Task<ActionResult> GerhardTesting(string name,DateTime dateTime, int numbers, string address)
        {
            try
            {
                var sdf = await lookups.GerhardTesting(name,dateTime,numbers,address);
                if (sdf.Succeeded)
                {
                    return Ok(sdf.Data);
                }
                throw new Exception(sdf.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

