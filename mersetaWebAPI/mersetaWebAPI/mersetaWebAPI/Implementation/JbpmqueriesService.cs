using mersetaWebAPI.CustomModels;
using mersetaWebAPI.CustomModule;
using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace mersetaWebAPI.Implementation
{
    public class JbpmqueriesService : IJbpmqueries
    {
        private readonly mersetaContext MmersetaContext;
        public JbpmqueriesService(mersetaContext MmersetaContext)
        {

            this.MmersetaContext = MmersetaContext;
        }

        public async Task<Response<long>> AddUserAddressAsync(AddressRequestt request)
        {

            var address = new Address
            {
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                AddressLine3 = request.AddressLine3,
                CreateDate = DateTime.Now,
                MunicipalityId = request.MunicipalityId,
                UserId = request.UserId,
                TownId = request.TownId,
                CompanyId = request.CompanyId,
                Postcode = request.Postcode,
                StatsSaareaCodeId = request.StatsSaareaCodeId,
                SameAddress = request.SameAddress
            };

            var _address = await MmersetaContext.Addresses.AddAsync(address);
            await MmersetaContext.SaveChangesAsync();

            var _address_id = MmersetaContext.Addresses.Where(x => x.UserId == address.UserId).FirstOrDefault();
            return new Response<long>(_address_id.Id, $"Address Added");
        }

        public async Task<Response<long>> UpdateUserNextofkin(UpdateNextofkin model, long id)
        {
            try
            {
                //check if user exist
                var _user = await MmersetaContext.Users.FindAsync(id);
                if (_user == null) { throw new Exception("User does not exist"); }
                // map objects

                _user.LastUpdateDate = DateTime.Now;
                _user.NextOfKinName = model.NextOfKinName;
                _user.NextOfKinTelephone = model.NextOfKinTelephone;
                // model.LastUpdateDate = DateTime.Now;
                // model.Password = _user.Password;
                // var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateNextofkin, User>());
                //var mmapper = config.CreateMapper();
                //var response = mmapper.Map<User>(model);
                //update
                // MmersetaContext.Entry(_user).CurrentValues.SetValues(response);
                MmersetaContext.Update(_user);
                MmersetaContext.SaveChanges();

                return new Response<long>(id, "nextofkinupdated");
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return new Response<long>(ex.Message, ex.Message);
            }
            throw new Exception("error while saving");
        }
        public async Task<Response<long>> RegisterAsync(RegistrationRequestt request, string origin)
        {
            try
            {

                var userWithSameUserName = this.MmersetaContext.Users.Where(x => x.Email == request.Email).FirstOrDefault();
                if (userWithSameUserName != null)
                {
                    throw new Exception($"Username '{request.Email}' is already taken.");
                }

                // var hashedPass = BCrypt.Net.BCrypt.HashPassword(request.Password);
                var user = new User
                {
                    Email = request.Email,
                    Active = 0,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    CellNumber = request.CellNumber,
                    Password = request.Password,
                    RsaIdNumber = request.RsaIdNumber,
                    FaxNumber = request.FaxNumber,
                    PassportNumber = request.PassportNumber,
                    DateOfBirth = request.DateOfBirth,
                    RegistrationDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Username = request.Username,
                    MiddleName = request.MiddleName,
                    TitleId = request.TitleId,
                    NationalityId = request.NationalityId,
                    PrimaryEmailAddress = request.PrimaryEmailAddress,
                    DisabilityStatus = request.DisabilityStatus,
                    DisabledId = request.DisabledId,
                    GenderId = request.GenderId,
                    EquityId = request.EquityId,
                    DisabilityRatingId = request.DisabilityRatingId,
                    Otp = 0,
                    AlternativeIdType = request.AlternativeIdType,
                    AlternativeIdNumber = request.AlternativeIdNumber,
                };
                var _user = await MmersetaContext.Users.AddAsync(user);
                await MmersetaContext.SaveChangesAsync();
                //
                var _userId = MmersetaContext.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                //
                //  var verificationUri = await SendVerificationEmail(user, origin);
                //TODO: Attach Email Service here and configure it via appsettings
                //  _emailService.Send(new EmailRequest() { To = user.Email, Body = $"Please confirm your account using this code: {user.Otp}", Subject = "Confirm Registration" });
                //var userRegisterResponse = new UserRegisterResponse {User_ID=_userId.Id };
                return new Response<long>(_userId.Id, $"User Registered. Please check your email inbox for account verification instrustions");
            } catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return new Response<long>(ex.Message,ex.Message);
            }
        }

        public async Task<Response<long>> AddUserLanguagesAsync(UsersLanguageRequest request)
        {
            try
            {
                var userlanguages = new UsersLanguage
                {
                    CreateDate = DateTime.Now,
                    HomeLanguage = request.HomeLanguage,
                    LanguageId = request.LanguageId,
                    ReadId = request.ReadId,
                    SpearkId = request.SpearkId,
                    UserId = request.UserId,
                    WriteId = request.WriteId

                };


                var _languages = await MmersetaContext.UsersLanguages.AddAsync(userlanguages);
                await MmersetaContext.SaveChangesAsync();

                var _langId = MmersetaContext.UsersLanguages.Where(x => x.UserId == userlanguages.UserId).FirstOrDefault();
                return new Response<long>(_langId.Id, $"Languages Added");

            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return new Response<long>(ex.Message,ex.Message);
            }
            throw new Exception("error while saving");

        }

        public async Task<Response<string>> AddCompanyAsync(CompanyRequestt request)
        {
            var userWithSameUserName = this.MmersetaContext.Companies.Where(x => x.LevyNumber == request.LevyNumber).FirstOrDefault();
            if (userWithSameUserName != null)
            {
                throw new Exception($"LevyNumber '{request.LevyNumber}' is already taken.");
            }

            var company = new Company
            {
                CompanyName = request.CompanyName,
                CompanyRegistrationNumber = request.CompanyRegistrationNumber,
                CompanyStatus = request.CompanyStatus,
                CreateDate = DateTime.Now,
                Email = request.Email,
                FaxNumber = request.FaxNumber,
                LevyNumber = request.LevyNumber,
                NumberOfEmployees = request.NumberOfEmployees,
                TelNumber = request.TelNumber,
                TradingName = request.TradingName,
                SicCodeId = request.SicCodeId,
                OrganisationTypeId = request.OrganisationTypeId

            };

            var _comp = await MmersetaContext.Companies.AddAsync(company);
            await MmersetaContext.SaveChangesAsync();

            var _compo = MmersetaContext.Companies.Where(x => x.LevyNumber == company.LevyNumber).FirstOrDefault();
            return new Response<string>(_compo.Id.ToString(), $"Company Added");
        }

        public async Task<Response<long>> AddCompanyLearnerAsync(CompanyLearnerRequest request)
        {
            try
            {
                var companylearner = new CompanyLearner
                {
                    CreateDate = DateTime.Now,
                    CompanyId = request.CompanyId,
                    UserId = request.UserId,
                    ContractOfEmploymentCopy = request.ContractOfEmploymentCopy,
                    EmployedByEmployerBefore = request.EmployedByEmployerBefore,
                    WorkplaceBasedBefore = request.WorkplaceBasedBefore,
                    HighestEducationEnum = request.HighestEducationEnum,
                    LastUpdateDate = DateTime.Now,
                    LastSchoolYear = request.LastSchoolYear,
                    PreviousSchools = request.PreviousSchools,
                    TvetFetQualificationId = request.TvetFetQualificationId,
                    OfoCodesId = request.OfoCodesId,
                    EmploymentStatus = request.EmploymentStatus,
                    TvetLecturer = request.TvetLecturer,
                    SocioeconomicStatusCodeId = request.SocioeconomicStatusCodeId,

                };

                var _comp = await MmersetaContext.CompanyLearners.AddAsync(companylearner);
                await MmersetaContext.SaveChangesAsync();

                var _compo = MmersetaContext.CompanyLearners.Where(x => x.UserId == companylearner.UserId).OrderByDescending(x=> x.CreateDate).FirstOrDefault();
                return new Response<long>(_compo.Id, $"Company Learner Added");
            }
            catch (Exception ex)
            {
                return new Response<long>(ex.Message, ex.Message);
            }
        }

        public async Task<Response<long>> UpdateCompanyLeaner(UpdateCompanyLearner model, long id)
        {
            try
            {
                //check if user exist
                var complearner = await this.MmersetaContext.CompanyLearners.FindAsync(id);
                var _complearner = await this.MmersetaContext.CompanyLearners.FindAsync(id);
                if (_complearner == null) { throw new Exception("company learner does not exist"); }
                // map objects
                // var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateCompanyLearner, CompanyLearner>());
                // var mmapper = config.CreateMapper();
                // var response = mmapper.Map<CompanyLearner>(model);
                //update
                // MmersetaContext.Entry(_user).CurrentValues.SetValues(response);

                _complearner.LastUpdateDate = DateTime.Now;
                _complearner.LearnershipId = model.LearnershipId;
                _complearner.InterventionTypeId = model.InterventionTypeId;
                _complearner.PivotNonPivot = model.PivotNonPivot;
                _complearner.NqfAlignedId = model.NqfAlignedId;
                _complearner.QualificationId = model.QualificationId;
                _complearner.CommencementDate = model.CommencementDate;
                _complearner.CompletionDate = model.CompletionDate;
                _complearner.MersetaFunded = model.MersetaFunded;
                _complearner.LearnerStatus = model.LearnerStatus;
                _complearner.DundingId = model.DundingId;

                // MmersetaContext.Update(_complearner);
                MmersetaContext.Entry(complearner).CurrentValues.SetValues(_complearner);
                MmersetaContext.SaveChanges();


                return new Response<long>(_complearner.Id, "companyLearner updated");
            }
            catch (Exception ex)
            {
               // throw new Exception(ex.Message);
                return new Response<long>(ex.Message, ex.Message);
            }
            throw new Exception("error while saving");
        }

        public async Task<Response<string>> UpdateUserAddress(UpdateUserAddress model, long id)
        {
            try
            {
                //check if user exist
                var _user = await MmersetaContext.Users.FindAsync(id);
                if (_user == null) { throw new Exception("User does not exist"); }
                // map objects

                model.LastUpdateDate = DateTime.Now;
                //  var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateUserAddress, User>());
                // var mmapper = config.CreateMapper();
                // var response = mmapper.Map<User>(model);
                //update
                _user.LastUpdateDate = DateTime.Now;
                _user.ResidentialAddressId = model.ResidentialAddressId;
                _user.PostalAddressId = model.PostalAddressId;
                // MmersetaContext.Entry(_user).CurrentValues.SetValues(response);
                MmersetaContext.Update(_user);
                MmersetaContext.SaveChanges();

                return new Response<string>(id.ToString(), "nextofkinupdated");
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return new Response<string>(ex.Message, ex.Message);
            }
            throw new Exception("error while saving");
        }

        public async Task<Response<string>> UpdateCompanyAddress(UpdateCompanyAddress model, long id)
        {
            try
            {
                //check if user exist
                var _company = await MmersetaContext.Companies.FindAsync(id);
                if (_company == null) { throw new Exception("Company does not exist"); }
                // map objects
                _company.PostalAddressId = model.PostalAddressId;
                _company.ResidentialAddressId = model.ResidentialAddressId;
                //  var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateCompanyAddress, Company>());
                // var mmapper = config.CreateMapper();
                // var response = mmapper.Map<Company>(model);
                //update
                // MmersetaContext.Entry(_user).CurrentValues.SetValues(response);
                MmersetaContext.Update(_company);
                MmersetaContext.SaveChanges();

                return new Response<string>(id.ToString(), "nextofkinupdated");
            }
            catch (Exception ex)
            {
               // throw new Exception(ex.Message);
                return new Response<string>(ex.Message, ex.Message);
            }
            throw new Exception("error while saving");
        }

        public async Task<Response<long>> UpdateCompanyLeanerEmployer(UpdateProvider model, long id)
        {
            try
            {
                //check if user exist
                var _complearner = await this.MmersetaContext.CompanyLearners.FindAsync(id);
                if (_complearner == null) { throw new Exception("company learner does not exist"); }

                _complearner.EmployerId = model.EmployerId;
                _complearner.CompanyId = model.CompanyId;
                MmersetaContext.Update(_complearner);
                MmersetaContext.SaveChanges();


                return new Response<long>(_complearner.Id, "companyLearner updated");
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return new Response<long>(ex.Message, ex.Message);
            }
            throw new Exception("error while saving");
        }

        public async Task<Response<string>> UpdateCopyofcontract(updatecontract model, long id)
        {
            try
            {
                //check if user exist
                var _complearner = await this.MmersetaContext.CompanyLearners.FindAsync(id);
                if (_complearner == null) { throw new Exception("company learner does not exist"); }
                // map objects
                // var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateCompanyLearner, CompanyLearner>());
                // var mmapper = config.CreateMapper();
                // var response = mmapper.Map<CompanyLearner>(model);
                //update
                // MmersetaContext.Entry(_user).CurrentValues.SetValues(response);

                _complearner.EmploymentStatus = model.EmploymentStatus;
                _complearner.ContractOfEmploymentCopy = model.ContractOfEmploymentCopy;
                _complearner.ContractOfEmploymentSpecified = model.ContractOfEmploymentSpecified;

                MmersetaContext.Update(_complearner);
                MmersetaContext.SaveChanges();


                return new Response<string>(_complearner.Id.ToString(), "companyLearner updated");
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return new Response<string>(ex.Message, ex.Message);
            }
            throw new Exception("error while saving");
        }

        public async Task<Response<string>> AddSdfCompany(sdfrequest model)
        {
            try
            {
                var sdfcompany = new SdfCompany
                {
                    CreateDate = DateTime.Now,
                    CompanyId = model.CompanyId,
                    SdfId = model.SdfId,
                    SdfTypeId = model.SdfTypeId

                };


                var _sdf = await MmersetaContext.SdfCompanies.AddAsync(sdfcompany);
                await MmersetaContext.SaveChangesAsync();

                var _sdftemp = MmersetaContext.SdfCompanies.Where(x => x.Id == sdfcompany.Id).FirstOrDefault();
                return new Response<string>(_sdftemp.Id.ToString(), $"Sdf Added");

            }
            catch (Exception ex)
            {
               // throw new Exception(ex.Message);
                return new Response<string>(ex.Message, ex.Message);
            }
            throw new Exception("error while saving");
        }

        public async Task<Response<string>> GerhardTesting(string name, DateTime? dateTime,int numbers,string address)
        {
            //
            return new Response<string>("123", name + " at " + dateTime.Value.ToString());
        }

        public async Task<Response<StatssaAreaCode>> GetStatsAreaCode(long id)
        {
            try 
            {
                var statsCode= await MmersetaContext.StatssaAreaCodes.FindAsync(id);
                return new Response<StatssaAreaCode>(statsCode);
            }
            catch(Exception ex) 
            { throw new Exception(ex.Message); }
        }

        public async Task<Response<List<UsersLanguage>>> GetUsersLanguages(long id)
        {
            try
            {
                var languages = await MmersetaContext.UsersLanguages.Where(x=> x.UserId==id).ToListAsync();
                return new Response<List<UsersLanguage>>(languages);
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
    }

}
