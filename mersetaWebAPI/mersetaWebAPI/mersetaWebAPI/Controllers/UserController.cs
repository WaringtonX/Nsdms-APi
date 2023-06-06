using mersetaWebAPI.CustomModels.Address;
using mersetaWebAPI.CustomModels.Company;
using mersetaWebAPI.CustomModels.EmailModels;
using mersetaWebAPI.CustomModels.UserModels;
using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using mersetaWebAPI.CustomModels.Provider;

namespace mersetaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

    

 
        private readonly mersetaContext MmersetaContext;
        // MySqlConnection conn = new MySqlConnection("server=156.38.134.211;user id=merseta;password=M3rseta@345;port=3306;database=merseta;T");
        private IUsers users;
        public UserController(mersetaContext MmersetaContext, IUsers users)
        {
            this.MmersetaContext = MmersetaContext;
            this.users = users;
        }

        //get all users
        [HttpGet]
        public async Task<ActionResult> GetAllUSers()
        {
            var user = users.GetAllUsers();
            if (user.Succeeded)
            {
                return Ok(user.Data);
            }
            return NotFound("User not found");
        }
        //Authenticate
        [HttpPost("Authenticate")]
        public async Task<ActionResult> Authenticate(AuthenticationRequest request,string ip)
        {
            
            try
            {
                var user = await users.AuthenticateAsync(request, ip);
                if (user.Succeeded)
                {
                    return Ok(user.Data);
                }
                throw new Exception(user.Message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           
        }
     

        //Authenticate
        [HttpPost("Registration")]
        public async Task<ActionResult> Register(RegistrationRequest request, string origin)
        {
            try
            {
                var user = await users.RegisterAsync(request, origin);
                if (user.Succeeded)
                {
                    return Ok(user.Data);
                }
                throw new Exception(user.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
          
        }
        //reset password
        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequest request,int id, string ip)
        {
            try
            {
                //var handler = new JwtSecurityTokenHandler();
                //string authHeader = Request.Headers["Authorization"];
                //authHeader = authHeader.Replace("Bearer ", "");
                //var jsonToken = handler.ReadToken(authHeader);
                //var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
                //var id = tokenS.Claims.First(claim => claim.Type == "id").Value;
                var user = await users.ResetPassword(request,id, ip);
                if (user.Succeeded)
                {
                    return Ok(user);
                }
                throw new Exception(user.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        //Authenticate
        [HttpPost("VerifyAccount")]
        public async Task<ActionResult> ConfirmEmail(VerifyEmailRequest request)
        {
            try
            {
                var user = await users.ConfirmEmailAsync(request);
                if (user.Succeeded)
                {
                    return Ok(user.Data);
                }
                throw new Exception(user.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost("resendVerificationCode")]
        public IActionResult ResentVerificationOTP(ResentOTPRequest model)
        {
            try
            {
                return Ok(users.ResendVericationCodeAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("LearnerByIdNumber")]
        public async Task<IActionResult> GetLearnerByIdNumberAsync(string idNumber)
        {
            try
            {
               // return Ok(users.GetLearnerByIDNumber(idNumber));
                var user = await users.GetLearnerByIDNumber(idNumber);
                if (user.Succeeded)
                {
                    return Ok(user.Data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();

        }
        [HttpGet("SDPCompanyByUserId")]
        public async Task<IActionResult> SDPCompanyByUserId(long userId)
        {
            try
            {
                // return Ok(users.GetLearnerByIDNumber(idNumber));
                var user = await users.GetSDPCompanyByUserId(userId);
                if (user.Succeeded)
                {
                    return Ok(user.Data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();

        }

        [HttpGet("SDFCompanyByUserId")]
        public async Task<IActionResult> SDFCompanyByUserId(long userId)
        {
            try
            {
                // return Ok(users.GetLearnerByIDNumber(idNumber));
                var user = await users.GetSDFCompanyByUserId(userId);
                if (user.Succeeded)
                {
                    return Ok(user.Data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();

        }
        //Authenticate
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            try
            {
                var user = await users.ForgotPassword(email, Request.Headers["origin"]);
                if (user.Succeeded)
                {
                    return Ok(user);
                }
                throw new Exception(user.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        //update
        [HttpPut("UpdatePersonalDetails")]
        public async Task<ActionResult> UpdatePersonalDetails(UserUpdate userUpdate, long id)
        {
            try
            {
                var user = await users.UpdateProfile(userUpdate, userUpdate.Id);
                if (user.Succeeded)
                {
                    return Ok(user.Data);
                }
                throw new Exception(user.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        //update
        [HttpPut("UpdateAddress")]
        public async Task<ActionResult> UpdateUserAddress(AddressRequest request, long id)
        {
            try
            {
                var address = await users.UpdateUserAddress(request, id);
                if (address.Succeeded)
                {
                    return Ok(address.Data);
                }
                throw new Exception(address.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        //update company
        [HttpPut("CompanyAddress")]
        public async Task<ActionResult> UpdateUserCompanyAddress(AddressRequest request, long id)
        {
            try
            {
                var address = await users.UpdateCompanyAddress(request, id);
                if (address.Succeeded)
                {
                    return Ok(address.Data);
                }
                throw new Exception(address.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        //update
        [HttpPut("UpdateUserCompany")]
        public async Task<ActionResult> UpdateUserCompany(long companyId, long id)
        {
            try
            {
                var address = await users.UpdateUserCompany(companyId, id);
                if (address.Succeeded)
                {
                    return Ok(address.Data);
                }
                throw new Exception(address.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        //update
        [HttpPut("UpdateTrainingInformation")]
        public async Task<ActionResult> UpdateTrainingInformation(TraininingInformationRequest request, long id)
        {
            try
            {
                var user = await users.UpdateTrainingInformation(request, id);
                if (user.Succeeded)
                {
                    return Ok(user.Data);
                }
                throw new Exception(user.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        //update
        [HttpPut("UpdateProviderInformation")]
        public async Task<ActionResult> UpdateProviderInformation(long providerId, long id)
        {
            try
            {
                var user = await users.UpdateProviderInformation(providerId, id);
                if (user.Succeeded)
                {
                    return Ok(user.Data);
                }
                throw new Exception(user.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
