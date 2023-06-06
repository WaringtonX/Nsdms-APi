using mersetaWebAPI.CustomModels.Company;
using mersetaWebAPI.Data;
using mersetaWebAPI.Extentions;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;

namespace mersetaWebAPI.Implementation
{
    public class CompanyLearnerService : ICompanyLeaners
    {
        private readonly mersetaContext MmersetaContext;
        private IConfiguration configuration;

        public CompanyLearnerService(mersetaContext MmersetaContext, IConfiguration configuration)
        {
            this.MmersetaContext = MmersetaContext;
            this.configuration = configuration;
        }

        public async Task<Response<List<User?>>> GetAllCompanyLearners(int id)
        {
            List<CompanyLearner> company = new List<CompanyLearner>();
            List<User> users = new List<User>();
            //get all company learners select userid
            var employee = MmersetaContext.CompanyLearners
                     .Where(x => x.CompanyId == id)
                     .OrderByDescending(x=> x.CreateDate)
                    .Select(x => x.UserId ).Take(500).ToList();


            // get user based on the companylearner userid
            var companyLearnerUsers =await MmersetaContext.Users.Where(x => employee.Contains(x.Id)).ToListAsync();

             
            if (companyLearnerUsers != null)
            {
                return new Response<List<User>>(companyLearnerUsers);
            }
            return new Response<List<User>>(companyLearnerUsers);

        }
        public Response<List<User>> GetAllCompanyLearnersByFilters(long? userId, long? companyId, long? region, string? levyNumber, string? accreditationNumber, long? status)
        {
            List<User> users = new List<User>();
            QueryExecutor queryExecutor = new QueryExecutor(configuration);
            string query = "";
            if (userId != null && companyId == null && region == null && string.IsNullOrEmpty(levyNumber) && string.IsNullOrEmpty(accreditationNumber) && status == null)
            {
                query = @"Select t2.* from company_learners learner
                            inner join users t2 on learner.user_id = t2.id
                            inner join training_provider_application provider on provider.id = learner.training_provider_application_id
                            inner join company company on company.id = provider.company_id
                            inner join address address on address.id = company.residential_address_id
                            inner join region_town town on town.town_id = address.town_id
                            inner join region region on region.id = town.region_id
                            Where t2.id=" + userId;

            }
            else if (userId == null && companyId != null && region == null && string.IsNullOrEmpty(levyNumber) && string.IsNullOrEmpty(accreditationNumber) && status == null)
            {
                query = @"Select t2.* from company_learners learner
                        inner join users t2 on learner.user_id = t2.id
                        inner join training_provider_application provider on provider.id = learner.training_provider_application_id
                        inner join company company on company.id = provider.company_id
                        -- inner join address address on address.id = company.residential_address_id
                        -- inner join region_town town on town.town_id = address.town_id
                        -- inner join region region on region.id = town.region_id
                        Where company.id="+companyId;
            }
            else if (userId == null && companyId == null && region != null && string.IsNullOrEmpty(levyNumber) && string.IsNullOrEmpty(accreditationNumber) && status == null)
            {
                query = @"Select t2.* from company_learners learner
                        inner join users t2 on learner.user_id = t2.id
                        inner join training_provider_application provider on provider.id = learner.training_provider_application_id
                        inner join company company on company.id = provider.company_id
                        inner join address address on address.id = company.residential_address_id
                        inner join region_town town on town.town_id = address.town_id
                        inner join region region on region.id = town.region_id
                        Where region.id=" + region;
            }
            else if (userId == null && companyId == null && region == null && !string.IsNullOrEmpty(levyNumber) && string.IsNullOrEmpty(accreditationNumber) && status == null)
            {
                query = @"Select t2.* from company_learners learner
                        inner join users t2 on learner.user_id = t2.id
                        inner join training_provider_application provider on provider.id = learner.training_provider_application_id
                        inner join company company on company.id = provider.company_id
                        -- inner join address address on address.id = company.residential_address_id
                        -- inner join region_town town on town.town_id = address.town_id
                        -- inner join region region on region.id = town.region_id
                        Where company.levy_number='" + levyNumber + "'";
            }
            else if (userId == null && companyId == null && region == null && string.IsNullOrEmpty(levyNumber) && !string.IsNullOrEmpty(accreditationNumber) && status == null)
            {
                query = @"Select t2.* from company_learners learner
                        inner join users t2 on learner.user_id = t2.id
                        inner join training_provider_application provider on provider.id = learner.training_provider_application_id
                        inner join company company on company.id = provider.company_id
                        -- inner join address address on address.id = company.residential_address_id
                        -- inner join region_town town on town.town_id = address.town_id
                        -- inner join region region on region.id = town.region_id
                        Where provider.accreditation_number='" + accreditationNumber + "'";
            }
            else if (userId == null && companyId == null && region == null && string.IsNullOrEmpty(levyNumber) && string.IsNullOrEmpty(accreditationNumber) && status != null)
            {
                query = @"Select t2.* from company_learners learner
                        inner join users t2 on learner.user_id = t2.id
                        inner join training_provider_application provider on provider.id = learner.training_provider_application_id
                        inner join company company on company.id = provider.company_id
                        -- inner join address address on address.id = company.residential_address_id
                        -- inner join region_town town on town.town_id = address.town_id
                        -- inner join region region on region.id = town.region_id
                        Where learner.status=" + status ;
            }
            else if (userId == null && companyId == null && region != null && string.IsNullOrEmpty(levyNumber) && string.IsNullOrEmpty(accreditationNumber) && status != null)
            {
                query = @"Select t2.* from company_learners learner
                        inner join users t2 on learner.user_id = t2.id
                        inner join training_provider_application provider on provider.id = learner.training_provider_application_id
                        inner join company company on company.id = provider.company_id
                        inner join address address on address.id = company.residential_address_id
                        inner join region_town town on town.town_id = address.town_id
                        inner join region region on region.id = town.region_id
                        Where region.id=" + region +" And  learner.status = " + status;
            }
            else if (userId == null && companyId != null && region != null && string.IsNullOrEmpty(levyNumber) && string.IsNullOrEmpty(accreditationNumber) && status != null)
            {
                query = @"Select t2.* from company_learners learner
                        inner join users t2 on learner.user_id = t2.id
                        inner join training_provider_application provider on provider.id = learner.training_provider_application_id
                        inner join company company on company.id = provider.company_id
                        inner join address address on address.id = company.residential_address_id
                        inner join region_town town on town.town_id = address.town_id
                        inner join region region on region.id = town.region_id
                        Where region.id=" + region + " And  learner.status = " + status + " And company.id="+companyId;
            }
            else if (userId == null && companyId != null && region == null && string.IsNullOrEmpty(levyNumber) && string.IsNullOrEmpty(accreditationNumber) && status != null)
            {
                query = @"Select t2.* from company_learners learner
                        inner join users t2 on learner.user_id = t2.id
                        inner join training_provider_application provider on provider.id = learner.training_provider_application_id
                        inner join company company on company.id = provider.company_id
                        inner join address address on address.id = company.residential_address_id
                        inner join region_town town on town.town_id = address.town_id
                        inner join region region on region.id = town.region_id
                        Where  learner.status = " + status + " And company.id=" + companyId;
            }
            else if (userId == null && companyId != null && region != null && string.IsNullOrEmpty(levyNumber) && string.IsNullOrEmpty(accreditationNumber) && status == null)
            {
                query = @"Select t2.* from company_learners learner
                        inner join users t2 on learner.user_id = t2.id
                        inner join training_provider_application provider on provider.id = learner.training_provider_application_id
                        inner join company company on company.id = provider.company_id
                        inner join address address on address.id = company.residential_address_id
                        inner join region_town town on town.town_id = address.town_id
                        inner join region region on region.id = town.region_id
                        Where region.id=" + region +  " And company.id=" + companyId;
            }
            DataRowCollection ds = queryExecutor.runSQLSSH(query);
                if (ds.Count > 0)
                {
                    foreach (DataRow row in ds)

                    {
                    ulong? _approved = 0; long _gender = 0; long _disabilityStatus = 0; long _nationality=0;long _postalAddress = 0;long _residentialAddress=0;long? _equity=0;
                    DateTime? _createDate=null;DateTime? _registrationDate = null;DateTime? _dateOfBirth = null;


                    if (row.IsNull("approved")) { _approved = 0; } else { _approved = (ulong?)row["approved"]; }
                    if (row.IsNull("gender_id")) { _gender = 0; } else { _gender = (long)row["gender_id"]; }
                    if (row.IsNull("disabilityStatus")) { _disabilityStatus = 0; } else { _disabilityStatus = (long)row["disabilityStatus"]; }
                    if (row.IsNull("nationality_id")) { _nationality = 0; } else { _nationality = (long)row["nationality_id"]; }
                    if (row.IsNull("postal_address_id")) { _postalAddress = 0; } else { _postalAddress = (long)row["postal_address_id"]; }
                    if (row.IsNull("residential_address_id")) { _residentialAddress = 0; } else { _residentialAddress = (long)row["residential_address_id"]; }
                    if (row.IsNull("equity_id")) { _postalAddress = 0; } else { _postalAddress = (long)row["equity_id"]; }
                    if (row.IsNull("create_date")) { _createDate=null ; } else { _createDate = (DateTime?)row["create_date"]; }
                    if (row.IsNull("registration_date")) { _registrationDate = null; } else { _registrationDate = (DateTime?)row["registration_date"]; }
                    if (row.IsNull("date_of_birth")) { _dateOfBirth = null; } else { _dateOfBirth = (DateTime?)row["date_of_birth"]; }
                    var user = new User 
                        {
                            //int? val = dr["status"] == DBNull.Value ? (int?) null : Convert.ToInt32(dr["status"]);
                            //Address.Street = row["STREET"] == DBNull.Value ? "" : row["STREET"].ToString();
                            Id = Convert.ToInt64(row["id"]),
                           // QualificationId = Convert.ToInt64(row["qualification_id"]),
                            Approved = _approved,
                            FirstName = row["first_name"].ToString(),
                            LastName = row["last_name"].ToString(),
                            CreateDate = _createDate,
                            CellNumber= row["cell_number"].ToString(),
                            DateOfBirth= _dateOfBirth,
                            Email = row["email"].ToString(),
                            RsaIdNumber = row["rsa_id_number"].ToString(),
                            RegistrationDate =_registrationDate,
                            GenderId= _gender,
                            DisabilityStatus = _disabilityStatus,
                            NationalityId = _nationality,
                            PostalAddressId = _postalAddress,
                            ResidentialAddressId = _residentialAddress,
                            EquityId = _equity

                        };
                        users.Add(user);
                    }
                
            }
            return new Response<List<User>>(users);

        }

        public async Task<Response<long>> UpdateCompanyLearnerStatus(long id, int status)
        {
            var _companyLearner = await MmersetaContext.CompanyLearners.FindAsync(id);
            _companyLearner.LearnerStatus= status;
            _companyLearner.LastUpdateDate= DateTime.Now;
             MmersetaContext.Update(_companyLearner);
            await MmersetaContext.SaveChangesAsync();
            return new Response<long>(_companyLearner.Id,"Updated successfully");
        }

        public async Task<Response<long>> UpdateEmployerId(long CompanyId,long CompanyLearnerId)
        {
            var _companyLearner=await MmersetaContext.CompanyLearners.FindAsync(CompanyLearnerId);
            _companyLearner.CompanyId= CompanyId;
            MmersetaContext.Update(_companyLearner);
            await MmersetaContext.SaveChangesAsync();

            return new Response<long>(_companyLearner.Id);
        }
        public async Task<Response<long>> UpdateJBPMEmployerId(long CompanyId, long CompanyLearnerId)
        {
            var _companyLearner = await MmersetaContext.CompanyLearners.FindAsync(CompanyLearnerId);
            _companyLearner.CompanyId = CompanyId;
            MmersetaContext.Update(_companyLearner);
            await MmersetaContext.SaveChangesAsync();

            return new Response<long>(_companyLearner.Id);
        }
    }
}
