using AutoMapper;
using mersetaWebAPI.CustomModels.Address;
using mersetaWebAPI.CustomModels.EmailModels;
using mersetaWebAPI.CustomModels.Provider;
using mersetaWebAPI.CustomModels.UserModels;
using mersetaWebAPI.CustomModule;
using mersetaWebAPI.Data;
using mersetaWebAPI.Exceptions;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XAct;

namespace mersetaWebAPI.Implementation
{
    public class UserService : IUsers
    {
        private readonly mersetaContext MmersetaContext;
        //private readonly  CryptographyProcessor crypto;
        private readonly IEmailService _emailService;
        private readonly ILearnerApplicationLifecycle learnerApplicationLifecycle;
        private IConfiguration configuration;
        private readonly IMapper _mapper;
        private string RoleDescription = "";

        public UserService(mersetaContext MmersetaContext, IEmailService emailService, IConfiguration configuration, IMapper mapper, ILearnerApplicationLifecycle learnerApplicationLifecycle)
        {
            this.MmersetaContext = MmersetaContext;
            _emailService = emailService;
            this.configuration = configuration;
            this._mapper = mapper;
            this.learnerApplicationLifecycle = learnerApplicationLifecycle;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            try
            {
                //login


                var user = MmersetaContext.Users.Where(x => x.Email == request.Email).FirstOrDefault();
                //
                if (user == null)
                {
                    return new Response<AuthenticationResponse>(message: $"No Accounts Registered with {request.Email}.");
                    //throw new ApiException($"No Accounts Registered with {request.Email}.");
                }
                if (!user.EmailConfirmed)
                {
                    return new Response<AuthenticationResponse>(message: $"Account Not Confirmed for '{request.Email}'.");
                }
                if (user.Active == 0 && user.TemporaryPasswordExpiresOn > DateTime.Now.AddDays(1))
                {
                    return new Response<AuthenticationResponse>(message: $"Temporary password has expired for {request.Email}.");
                }
                //check password match
                //var salt = Convert.ToBase64String(CreateSalt());
                var pass = BCrypt.Net.BCrypt.HashPassword(request.Password);
                bool verified = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
                if (verified == false)
                {
                    return new Response<AuthenticationResponse>(message: $"Password does not match for {request.Email}.");
                }

                // authentication successful
                //SELECT* FROM hosting_company_employees he

                //                inner join job_title jt

                //                ON he.job_title_id = jt.id

                //                inner join roles r

                //                ON jt.roles_id = r.id

                //                where he.user_id = "+UserID+"

                //                                var _role = (
                //              from p in this.MmersetaContext.HostingCompanyEmployees
                //              join pc in this.MmersetaContext.JobTitles
                //            on p.JobTitleId equals pc.Id
                //          join c in this.MmersetaContext.Roles on pc.RolesId equals c.Id
                //        where p.UserId == user.Id
                //      select new
                //    {
                //  pc.Description
                //        // other assignments
                //}).ToList();


                AuthenticationResponse authenticationResponse = new AuthenticationResponse();
                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, AuthenticationResponse>());
                var mmapper = config.CreateMapper();
                var response = mmapper.Map<AuthenticationResponse>(user);

                response.RoleDescription = getRoleDescription(user.Id);

                //response.RoleDescription = _role[0].Description;
                // map objects
                //var response = _mapper.Map<AuthenticationResponse>(user);
                //generate token
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                return new Response<AuthenticationResponse>(response, $"Authenticated {user.Email}");
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }

        }
        private async Task<JwtSecurityToken> GenerateJWToken(User user)
        {
            //var userClaims = await _userManager.GetClaimsAsync(user);
            //var roles = await _userManager.GetRolesAsync(user);

            //var roleClaims = new List<Claim>();

            //for (int i = 0; i < roles.Count; i++)
            //{
            //    roleClaims.Add(new Claim("role", roles[i]));
            //}

            //string ipAddress = IpHelper.GetIpAddress();
            string Title = ""; string Nationality = ""; string VerifiedOn = ""; string gender = "";

            //check null values

            if (user.Nationality == null) { Nationality = ""; } else { Nationality = user.Nationality.ToString(); }
            if (user.Title == null) { Title = ""; } else { Title = user.Title.ToString(); }

            var claims = new[]
            {
                    new Claim("id", user.Id.ToString()),
                    new Claim("FirstName",user.FirstName),
                    new Claim("email",user.Email),
                    new Claim("LastName",user.LastName),
                    new Claim("Title",Title),
                    new Claim("Nationality",Nationality),
                    new Claim("RsaIdNumber",user.RsaIdNumber),

                    //new Claim("acceptTerms",user.AcceptTerms ? "True":"False"),
                    //new Claim("alternative",user.Alternative),
                    //new Claim("isVerified",user.VerifiedOn.HasValue ? "True":"False"),
                    //new Claim("isActive",user.Active ?"True":"False"),
                    //new Claim("isOnTemporaryPassword",user.isOnTemporaryPassword ?"True":"False"),
                    //new Claim("lastDeviceID",user.DeviceId),
                    //new Claim("schoolId",user.SchoolId),
                    //new Claim("deActivatedOn",De_ActivatedOn),
                    //new Claim("addedOn",user.AddedOn.ToString()),
                    //new Claim("verifiedOn",VerifiedOn),
                    //new Claim("updatedOn",UpdatedOn),
                    //new Claim("lastDeviceId",user.LastDeviceId),
                    //new Claim("Genger",gender),
            }
            //.Union(userClaims)
            //.Union(roleClaims)
            ;

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Jwt:Duration"])),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
        public async Task<Response<string>> ConfirmEmailAsync(VerifyEmailRequest request)
        {
            var user = MmersetaContext.Users.Where(x => x.Email == request.Email).FirstOrDefault();
            //var encodedOTP = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.VerificationOTP));
            //request.VerificationOTP = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedOTP));
            ////var result = await _userManager.(user, request.VerificationOTP);

            if (user.Otp.ToString() == request.VerificationOTP)
            {
                user.LastUpdateDate = DateTime.Now;
                user.Otp = 0;
                user.Active = 1;
                user.EmailConfirmed = true;
                //update users table
                MmersetaContext.Users.Update(user);
                await MmersetaContext.SaveChangesAsync();

                return new Response<string>(user.Id.ToString(), message: $"Account Confirmed for {user.Email}. You can now use your credentials to log in.");
            }
            else
            {
                throw new ApiException($"An error occured while confirming {user.Email}.");
            }
        }

        public async Task<Response<string>> ForgotPassword(string email, string origin)
        {
            try
            {

                var account = MmersetaContext.Users.Where(x => x.Email == email).FirstOrDefault();

                // always return ok response to prevent email enumeration
                if (account == null) { return new Response<string>(message: "Account Does not exist"); }

                var TempPasword = CreatePassword(8);
                //account.PasswordReset = DateTime.UtcNow;
                account.Otp = Convert.ToInt32(RandomizeNumbers(6));
                account.Password = BCrypt.Net.BCrypt.HashPassword(TempPasword);
                //account.PasswordHash = HashPassword(TempPasword);
                account.Active = 0;
                account.TemporaryPasswordExpiresOn = DateTime.UtcNow.AddDays(1);
                MmersetaContext.Users.Update(account);
                await MmersetaContext.SaveChangesAsync();
                var emailRequest = new EmailRequest()
                {
                    Body = $"You reset Temporary Password is - {TempPasword} and your otp is - {account.Otp}",
                    To = email,
                    Subject = "Reset Password",
                };
                _emailService.Send(emailRequest);
                return new Response<string>(emailRequest.To, message: "Please check your email for further instructions!");
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }

        }
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public Response<List<User>> GetAllUsers()
        {
            List<User> user = new List<User>();
            user.AddRange(MmersetaContext.Users.OrderByDescending(x => x.Id).Take(10).ToList());

            if (user != null)
            {
                return new Response<List<User>>(user);
            }
            return new Response<List<User>>(user);
        }

        public Task<AuthenticationResponse> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> RegisterAsync(RegistrationRequest request, string origin)
        {
            var userWithSameUserName = MmersetaContext.Users.Where(x => x.Email == request.Email).FirstOrDefault();
            if (userWithSameUserName != null)
            {
                throw new ApiException($"Username '{request.Email}' is already taken.");
            }

            var hashedPass = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Email = request.Email,
                Active = 0,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CellNumber = request.CellNumber,
                TelNumber = request.TelNumber,
                Password = hashedPass,
                RsaIdNumber = request.RsaIdNumber,
                FaxNumber = request.FaxNumber,
                PassportNumber = request.PassportNumber,
                DateOfBirth = request.DateOfBirth,
                RegistrationDate = DateTime.Now,
                CreateDate = DateTime.Now,
                Username = request.Username,
                Otp = Convert.ToInt32(RandomizeNumbers(4))
            };
            var _user = await MmersetaContext.Users.AddAsync(user);
            await MmersetaContext.SaveChangesAsync();
            //
            var _userId = MmersetaContext.Users.Where(x => x.Email == user.Email).FirstOrDefault();
            //
            var verificationUri = await SendVerificationEmail(user, origin);
            //TODO: Attach Email Service here and configure it via appsettings
            _emailService.Send(new EmailRequest() { To = user.Email, Body = $"Please confirm your account using this code: {user.Otp}", Subject = "Confirm Registration" });

            return new Response<string>(_userId.Id.ToString(), $"User Registered. Please check your email inbox for account verification instrustions");
        }

        public async Task<string> ResendVericationCodeAsync(ResentOTPRequest model)
        {
            try
            {
                var user = MmersetaContext.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                //var verificationUrl =await SendVerificationEmail(user,model.DeviceId);
                //TODO: Attach Email Service here and configure it via appsettings
                _emailService.Send(new EmailRequest() { To = user.Email, Body = $"Please confirm your account using this code: {user.Otp}", Subject = "Resend Confirm Registration Code" });
                return "Verification code has been resent";
            }
            catch
            {
                throw new ApiException($"Failed to log out.");
            }
        }
        private async Task<string> SendVerificationEmail(User user, string origin)
        {
            //await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.Otp.ToString()));
            var route = "api/user/confirm-email/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id.ToString());
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;

        }
        private string RandomizeNumbers(int size = 6)
        {
            string _numbers = "0123456789";
            Random random = new Random();
            StringBuilder builder = new StringBuilder(6);
            for (var i = 0; i < size; i++)
            {
                builder.Append(_numbers[random.Next(0, _numbers.Length)]);
            }

            return builder.ToString();

        }
        public async Task<Response<string>> ResetPassword(ResetPasswordRequest model, int id, string IpAddress)
        {
            var account = MmersetaContext.Users.Where(x => x.Id == id).FirstOrDefault();
            if (account == null) throw new ApiException(message: $"No Accounts Registered with {id}.");

            //use custom checks for reset password by comparing otp with account record
            if (account.Otp.ToString() != model.OTP) return new Response<string>(id.ToString(), "OTP does not match");

            bool verified = BCrypt.Net.BCrypt.Verify(model.OldPassword, account.Password);
            if (!verified) return new Response<string>(id.ToString(), message: "Old password does not match");

            if (account != null)
            {
                try
                {
                    //account. = DateTime.Now;
                    account.Otp = 0;
                    account.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                    account.TemporaryPasswordExpiresOn = null;
                    account.Active = 1;
                    //account. = false;
                    MmersetaContext.Update(account);
                    await MmersetaContext.SaveChangesAsync();
                }
                catch
                {
                    throw new ApiException(message: $"Error occured while reseting the password.");
                }
                var authenticationRequest = new AuthenticationRequest();
                authenticationRequest.Email = account.Email;
                authenticationRequest.Password = model.NewPassword;
                authenticationRequest.DeviceId = model.DeviceId;
                //var data = await AuthenticateAsync(authenticationRequest, IpAddress);

                return new Response<string>(account.Id.ToString(), message: "Password Reset was successfull");
            }
            else
            {
                throw new ApiException(message: $"Error occured while reseting the password.");
            }
            throw new NotImplementedException();
        }

        public async Task<Response<string>> UpdateProfile(UserUpdate model, long id)
        {
            try
            {
                //check if user exist
                var _user = await MmersetaContext.Users.FindAsync(id);
                if (_user == null) { throw new Exception("User does not exist"); }
                // map objects

                model.LastUpdateDate = DateTime.Now;
                model.Password = _user.Password;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserUpdate, User>());
                var mmapper = config.CreateMapper();
                var response = mmapper.Map<User>(model);
                //update
                MmersetaContext.Entry(_user).CurrentValues.SetValues(response);
                MmersetaContext.SaveChanges();

                return new Response<string>(id.ToString(), "Personal information updated successfully");
            }
            catch (Exception ex)
            { }
            throw new Exception("error while saving");
        }

        public async Task<Response<string>> UpdateUserAddress(AddressRequest request, long id)
        {
            try
            {
                //find user address
                var _address = MmersetaContext.Addresses.Where(x => x.UserId == id).FirstOrDefault();
                if (_address == null) { throw new Exception("Address not found"); }

                var config = new MapperConfiguration(cfg => cfg.CreateMap<AddressRequest, Address>());
                var mmapper = config.CreateMapper();
                var response = mmapper.Map<Address>(request);
                //update
                MmersetaContext.Entry(_address).CurrentValues.SetValues(response);

                await MmersetaContext.SaveChangesAsync();
                return new Response<string>(id.ToString(), "Address updated successfully");
            }
            catch (Exception ex)
            { throw new Exception("Error while updating address"); }
        }

        public async Task<Response<string>> UpdateCompanyAddress(AddressRequest request, long id)
        {
            try
            {
                //find company address
                var _address = MmersetaContext.Addresses.Where(x => x.CompanyId == id).FirstOrDefault();
                if (_address == null) { throw new Exception("Company Address not found"); }

                var config = new MapperConfiguration(cfg => cfg.CreateMap<AddressRequest, Address>());
                var mmapper = config.CreateMapper();
                var response = mmapper.Map<Address>(request);
                //update
                MmersetaContext.Entry(_address).CurrentValues.SetValues(response);


                await MmersetaContext.SaveChangesAsync();
                return new Response<string>(id.ToString(), "Company Address updated successfully");

            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating address");
            }
        }
        public async Task<Response<string>> UpdateUserCompany(long companyId, long id)
        {
            try
            {
                //find company address
                var _companyLearner = await MmersetaContext.CompanyLearners.Where(x => x.UserId == id).AsNoTracking().FirstAsync();
                if (_companyLearner == null) { throw new ApiException("Company  not found"); }

                _companyLearner.CompanyId = companyId;
                // MmersetaContext.Entry(_company).CurrentValues.SetValues(response);
                MmersetaContext.Update(_companyLearner);
                int result = await MmersetaContext.SaveChangesAsync();
                if (result > 0)
                {
                    var appCycle = new LearnerApplicationLifecycle
                    {
                        Action_User_Id = _companyLearner.UserId,
                        Company_Learner_Id = _companyLearner.Id,
                        Is_Deleted = 0,
                        Is_Notification_Set = 0,
                        Learner_Status_Date = DateTime.Now,
                        Notes = "",
                        Learner_Status_Id = _companyLearner.LearnerStatus,
                    };
                    await learnerApplicationLifecycle.SaveLearnerApplicationLifecycle(appCycle);
                }
                return new Response<string>(id.ToString(), "Company  updated successfully");

            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating company");
            }
        }
        private String getRoleDescription(long UserID)
        {
            string query = @"
                                SELECT r.description FROM hosting_company_employees he 
								inner join job_title jt
								ON he.job_title_id = jt.id
								inner join roles r
								ON jt.roles_id= r.id
								where he.user_id = " + UserID + ";";

            DataRowCollection ds = runSQLSSH(query);

            if (ds.Count >= 1) { RoleDescription = ds[0]["description"].ToString(); }
            else
            {
                string query1 = @" SELECT * FROM company_learners where user_id=" + UserID + ";";
                DataRowCollection ds1 = runSQLSSH(query1);
                if (ds1.Count >= 1) { RoleDescription = "Learner"; }
                else
                {
                    string query2 = @" SELECT * FROM training_provider_application where users_id=" + UserID + ";";
                    DataRowCollection ds2 = runSQLSSH(query2);
                    if (ds2.Count >= 1) { RoleDescription = "SDP"; }
                    else
                    {
                        string query3 = @" SELECT * FROM sdf_company where sdf_id=" + UserID + ";";
                        DataRowCollection ds3 = runSQLSSH(query3);
                        if (ds3.Count >= 1) { RoleDescription = "SDF"; }
                        else
                        {
                            string query4 = @" SELECT * FROM assessor_moderator_application where users_id=" + UserID + ";";
                            DataRowCollection ds4 = runSQLSSH(query4);
                            if (ds4.Count >= 1) { RoleDescription = "Assessor/Moderator"; }
                        }
                    }
                }
            }

            return RoleDescription;
            ///////////////////////////////////////////////////////////////////////////////////////////
        }

        private DataRowCollection runSQLSSH(String SQL)
        {
            DataSet ds = null;
            try
            {
                using (var client = new SshClient("156.38.134.211", "merseta", "Akhinton345")) // establishing ssh connection to server where MySql is hosted
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        //var portForwarded = new ForwardedPortLocal("127.0.0.1", 3305, "127.0.0.1", 3306);
                        //client.AddForwardedPort(portForwarded);
                        //portForwarded.Start();
                        using (MySqlConnection con = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
                        {
                            using (MySqlCommand com = new MySqlCommand(SQL, con))
                            {
                                com.CommandType = CommandType.Text;
                                ds = new DataSet();
                                MySqlDataAdapter da = new MySqlDataAdapter(com);
                                da.Fill(ds);
                                foreach (DataRow drow in ds.Tables[0].Rows)
                                {
                                    Console.WriteLine("From MySql: " + drow[1].ToString());
                                }
                            }
                        }
                        client.Disconnect();
                    }
                    else
                    {
                        Console.WriteLine("Client cannot be reached...");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return ds.Tables[0].Rows;
        }

        public async Task<Response<string>> UpdateTrainingInformation(TraininingInformationRequest request, long id)
        {

            try
            {
                //find user address
                var _trainingInfo = MmersetaContext.CompanyLearners.Where(x => x.Id == id).FirstOrDefault();
                if (_trainingInfo == null) { throw new Exception("Trainning Information not found"); }

                _trainingInfo.PivotNonPivot = request.PivotNonPivot;
                _trainingInfo.CommencementDate = request.CommencementDate;
                _trainingInfo.CompletionDate = request.CompletionDate;
                _trainingInfo.MersetaFunded = request.MersetaFunded;
                _trainingInfo.InterventionTypeId = request.InterventionTypeId;
                _trainingInfo.NqfAlignedId = request.NqfAlignedId;
                //update
                // MmersetaContext.Entry(_trainingInfo).CurrentValues.SetValues(response);
                MmersetaContext.Update(_trainingInfo);
                await MmersetaContext.SaveChangesAsync();
                return new Response<string>(_trainingInfo.Id.ToString(), "Training Information updated successfully");
            }
            catch (Exception ex)
            { throw new Exception("Error while updating Training Information"); }
        }
        public async Task<Response<string>> UpdateProviderInformation(long PrividerId, long id)
        {
            try
            {
                //find user address
                var _companyLearner = MmersetaContext.CompanyLearners.Where(x => x.Id == id).FirstOrDefault();
                if (_companyLearner == null) { throw new Exception("Trainning Information not found"); }

                //var config = new MapperConfiguration(cfg => cfg.CreateMap<AddressRequest, Address>());
                //var mmapper = config.CreateMapper();
                //var response = mmapper.Map<Address>(request);
                //update
                //MmersetaContext.Entry(_companyLearner).CurrentValues.SetValues(request);
                _companyLearner.EmployerId = PrividerId;
                MmersetaContext.Update(_companyLearner);
                int isSaved = await MmersetaContext.SaveChangesAsync();
                if (isSaved > 0)
                {
                    var appCycle = new LearnerApplicationLifecycle
                    {
                        Action_User_Id = _companyLearner.UserId,
                        Company_Learner_Id = _companyLearner.Id,
                        Is_Deleted = 0,
                        Is_Notification_Set = 0,
                        Learner_Status_Date = DateTime.Now,
                        Notes = "",
                        Learner_Status_Id = _companyLearner.LearnerStatus,
                    };
                    await learnerApplicationLifecycle.SaveLearnerApplicationLifecycle(appCycle);
                }
                return new Response<string>(_companyLearner.UserId.ToString(), "Provider Information updated successfully");
            }
            catch (Exception ex)
            { throw new Exception("Error while updating address"); }
        }

        public async Task<Response<UserLearnerResponse>> GetLearnerByIDNumber(string idnumber)
        {
            UserLearnerResponse learnerResponses = new UserLearnerResponse();
            string query = @"SELECT a.id,a.title_id,a.first_name,a.last_name,a.middle_name,a.maiden_name,a.cell_number,a.tel_number,a.primary_email_address
                    ,a.secondary_email_address,a.marital_status_id,a.date_of_birth,a.gender_id,a.equity_id,a.disability,a.accept_popi,a.accept_popi_date,a.passport_number,
                    a.disabilityStatus,a.learnership_id,a.last_school_year,a.citizen_Resident_Status,a.alternative_id_number,a.alternative_id_type,a.previous_schools
                    ,b.id as CompanyLearnerId,b.qualification_id,a.postal_address_id,a.residential_address_id, a.urban_rural_enum,b.employment_status,a.next_of_kin_name,a.next_of_kin_telephone
                    FROM merseta.users a
                    left join company_learners b on a.id=b.user_id
                    where rsa_id_number = '" + idnumber + "'";

            DataRowCollection ds = runSQLSSH(query);

            //
            if (ds.Count > 0)
            {
                //foreach (DataRow row in ds)
                // {
                string _firstname = ""; string _lastname = ""; string _maidenName = ""; string _middlename = "";string _cellno = ""; string _telno = ""; string _primaryEmail = "";
                string _secondaryEmail = "";string _dateOfbirth = "";string passport = "";string? AlternativeIdNumber = "";long AlternativeIdType = 0;
                long _companyLearnerId = 0; long _title = 0;long _maritalStatus = 0;long _gender = 0;long _equity = 0;long _disability = 0;int CitizenResidentStatus = 0;
                long postal_address_id = 0;long residential_address_id = 0;long urban_rural_enum=0;long employment_status=0;long previous_schools;long qualification_id = 0;
                bool acceptPopi=false;DateTime acceptPopiDate=DateTime.Now; int disabilityStatus = 0;long leanershipId = 0;DateTime LastSchoolYear = DateTime.Now;
                string next_of_kin_telephone = "";string next_of_kin_name = "";
                DataRow row = ds[0];
                //check nulls on the row
                if (row.IsNull("first_name")) { _firstname = ""; }else { _firstname = row["first_name"].ToString(); }
                if (row.IsNull("last_name")) { _lastname = ""; } else { _lastname = row["last_name"].ToString(); }
                if (row.IsNull("title_id")) { _title = 0; } else { _title = Convert.ToInt64(row["title_id"]); }
                if (row.IsNull("CompanyLearnerId")) { _companyLearnerId = 0; } else { _companyLearnerId =Convert.ToInt64( row["CompanyLearnerId"]); }
                if (row.IsNull("middle_name")) { _middlename = ""; } else { _middlename = row["middle_name"].ToString(); }
                if (row.IsNull("maiden_name")) { _maidenName = ""; } else { _maidenName = row["maiden_name"].ToString(); }
                if (row.IsNull("cell_number")) { _cellno = ""; } else { _cellno = row["cell_number"].ToString(); }
                if (row.IsNull("tel_number")) { _telno = ""; } else { _telno = row["tel_number"].ToString(); }
                if (row.IsNull("primary_email_address")) { _primaryEmail = ""; } else { _primaryEmail = row["primary_email_address"].ToString(); }
                if (row.IsNull("secondary_email_address")) { _secondaryEmail = ""; } else { _secondaryEmail = row["secondary_email_address"].ToString(); }
                if (row.IsNull("date_of_birth")) { _dateOfbirth = ""; } else { _dateOfbirth = row["date_of_birth"].ToString(); }
                if (row.IsNull("marital_status_id")) { _maritalStatus = 0; } else { _maritalStatus = Convert.ToInt64(row["marital_status_id"]); }
                if (row.IsNull("gender_id")) { _gender = 0; } else { _gender = Convert.ToInt64(row["gender_id"]); }
                if (row.IsNull("equity_id")) { _equity = 0; } else { _equity = Convert.ToInt64(row["equity_id"]); }
                if (row.IsNull("disability")) { _disability = 0; } else { _disability = Convert.ToInt64(row["disability"]); }
                if (row.IsNull("accept_popi")) { acceptPopi = false; } else { acceptPopi = Convert.ToBoolean(row["accept_popi"]); }
                if (row.IsNull("accept_popi_date")) { acceptPopiDate = DateTime.Now; } else { acceptPopiDate = Convert.ToDateTime(row["accept_popi_date"]); }
                if (row.IsNull("passport_number")) { passport =""; } else { passport = Convert.ToString(row["passport_number"]); }
                if (row.IsNull("last_school_year")) { LastSchoolYear = DateTime.Now; } else { LastSchoolYear = Convert.ToDateTime(row["last_school_year"]); }
                if (row.IsNull("disabilityStatus")) { disabilityStatus = 0; } else { disabilityStatus = Convert.ToInt32(row["disabilityStatus"]); }
                if (row.IsNull("alternative_id_number")) { AlternativeIdNumber = ""; } else { AlternativeIdNumber = Convert.ToString(row["alternative_id_number"]); }
                if (row.IsNull("alternative_id_type")) { AlternativeIdType = 0; } else { AlternativeIdType = Convert.ToInt64(row["alternative_id_type"]); }
                if (row.IsNull("citizen_Resident_Status")) { CitizenResidentStatus = 0; } else { CitizenResidentStatus = Convert.ToInt32(row["citizen_Resident_Status"]); }
                if (row.IsNull("postal_address_id")) { postal_address_id = 0; } else { postal_address_id = Convert.ToInt64(row["postal_address_id"]); }
                if (row.IsNull("residential_address_id")) { residential_address_id = 0; } else { residential_address_id = Convert.ToInt64(row["residential_address_id"]); }
                if (row.IsNull("urban_rural_enum")) { urban_rural_enum = 0; } else { urban_rural_enum = Convert.ToInt64(row["urban_rural_enum"]); }
                if (row.IsNull("employment_status")) { employment_status = 0; } else { employment_status = Convert.ToInt64(row["employment_status"]); }
                if (row.IsNull("previous_schools")) { previous_schools = 0; } else { previous_schools = Convert.ToInt64(row["previous_schools"]); }
                if (row.IsNull("qualification_id")) { qualification_id = 0; } else { qualification_id = Convert.ToInt64(row["qualification_id"]); }
                if (row.IsNull("next_of_kin_telephone")) { next_of_kin_telephone = ""; } else { next_of_kin_telephone = Convert.ToString(row["next_of_kin_telephone"]); }
                if (row.IsNull("next_of_kin_name")) { next_of_kin_name = ""; } else { next_of_kin_name = Convert.ToString(row["next_of_kin_name"]); }
                learnerResponses = new UserLearnerResponse
                    {
                        Id = Convert.ToInt64(row["id"]),
                        FirstName = _firstname,
                        CompanyLearnerId = _companyLearnerId,
                        Title = _title,
                        
                        LastName = _lastname,
                        MiddleName = _middlename,
                        MaidenName = _maidenName,
                        CellNumber=_cellno,
                        DateOfBirth= _dateOfbirth,
                        Disability= _disability,
                        Equity= _equity,    
                        Gender=_gender,
                        MaritalStatus= _maritalStatus,
                        PrimaryEmailAddress=_primaryEmail,
                        SecondaryEmailAddress=_secondaryEmail,
                        TelNumber=_telno,
                        AcceptPopi = acceptPopi,
                        AcceptPopiDate=acceptPopiDate,
                        PassportNumber=passport,
                        DisabilityStatus= disabilityStatus,
                        LearnershipId =leanershipId,
                        LastSchoolYear=LastSchoolYear,
                        AlternativeIdNumber=AlternativeIdNumber,
                        AlternativeIdType=AlternativeIdType,
                        CitizenResidentStatus=CitizenResidentStatus,
                        EmploymentStatus=employment_status,
                        PostalAddressId=postal_address_id,
                        PreviousSchools=previous_schools,
                        ResidentialAddressId=residential_address_id,
                        UrbanRuralEnum=urban_rural_enum,
                    QualificationId=qualification_id,
                    NextOfKinName=next_of_kin_name,
                    NextOfKinTelephone=next_of_kin_telephone,
                };
                   // learnerResponses.Add(_learnerResponse);
                //}
            }

            return new Response<UserLearnerResponse>(learnerResponses);
        }

        public async Task<Response<long?>> GetSDPCompanyByUserId(long userId)
        {
            try
            {
                string query = @"SELECT company_id from sdp_company where sdp_id="+userId;

                DataRowCollection ds = runSQLSSH(query);

                //
                var id = 0;
                if (ds.Count > 0)
                {
                    DataRow row=ds[0];
                    id = Convert.ToInt32(row["company_id"]);
                }
               return new Response<long?>(id);
            }
            catch (Exception ex) 
            { throw new Exception(ex.Message); }
           
        }

        public async Task<Response<long?>> GetSDFCompanyByUserId(long userId)
        {
            try
            {
                string query = @"SELECT * FROM merseta.sdf_company where sdf_id=" + userId;

                DataRowCollection ds = runSQLSSH(query);

                //
                var id = 0;
                if (ds.Count > 0)
                {
                    DataRow row = ds[0];
                    id = Convert.ToInt32(row["company_id"]);
                }
                return new Response<long?>(id);
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }

        }
    }

}
