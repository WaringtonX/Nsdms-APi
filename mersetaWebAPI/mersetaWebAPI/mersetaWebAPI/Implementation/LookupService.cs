using mersetaWebAPI.CustomModels;
using mersetaWebAPI.CustomModels.LookupsModels;
using mersetaWebAPI.CustomModels.UserModels;
using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.Json.Serialization;
using Task = mersetaWebAPI.Models.Task;

namespace mersetaWebAPI.Implementation
{
    //get method
    //private static class SqlDataReaderExtensions
    //{
    //    public static T Get<T>(this SqlDataReader reader, string columnName)
    //    {
    //        if (reader.IsDBNull(columnName))
    //            return default;
    //        return reader.GetFieldValue<T>(columnName);
    //    }
    //}
    public class LookupService : ILookups
    {
        private static string ConnString = "Server=127.0.0.1;Database=merseta;Uid=merseta;Pwd=M3rseta@345; persist Security Info=False; Connect Timeout=300";
        //private static string ConnString = Configuration.GetConnectionString("DefaultConnection");
        private SqlConnection Conn = new SqlConnection(ConnString);
        private readonly mersetaContext MmersetaContext;
        private readonly IConfiguration configuration;

        public LookupService(mersetaContext MmersetaContext, IConfiguration _configuration)
        {

            this.MmersetaContext = MmersetaContext;
            this.configuration = _configuration;
        }
        public Response<List<Address>> GetAllAddress(int CompanyId)
        {
            try
            {
                List<Address> addresses = new List<Address>();
                addresses = this.MmersetaContext.Addresses.Where(x => x.CompanyId == CompanyId).ToList();
                return new Response<List<Address>>(addresses);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public async Task<Response<List<Municipality>>> GetAllMunicipality()
        {
            try
            {
                List<Municipality> municipalities = new List<Municipality>();
                var _municipalities = await this.MmersetaContext.Municipalities.Select(x => new { x.MunicipalityDesc, x.Iddistrict, x.Idmunicipalitytype, x.ProvincesIdprovinces, x.Code, x.Id }).ToListAsync();
                foreach (var municipality in _municipalities)
                {
                    municipalities.Add(new Municipality { MunicipalityDesc = municipality.MunicipalityDesc, Iddistrict = municipality.Iddistrict, Idmunicipalitytype = municipality.Idmunicipalitytype, ProvincesIdprovinces = municipality.ProvincesIdprovinces, Code = municipality.Code, Id = municipality.Id });
                }
                return new Response<List<Municipality>>(municipalities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public Response<List<Role>> GetAllUserRoles()
        {
            /* try
             {
                 List<Role> roles = new List<Role>();
                 var _roles = this.MmersetaContext.Roles.Select(x => new { x.Description, x.Id });
                 foreach (var role in _roles)
                 {
                     roles.Add(new Role { Description = roles.Description, Id = roles.Id });
                 }
                 return new Response<List<Role>>(roles);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }

             throw new NotImplementedException();*/
            try
            {
                List<Role> roles = new List<Role>();
                roles = this.MmersetaContext.Roles.ToList();
                return new Response<List<Role>>(roles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public Response<List<Province>> GetAllProvince()
        {
            try
            {
                List<Province> provinces = new List<Province>();
                provinces = this.MmersetaContext.Provinces.ToList();
                return new Response<List<Province>>(provinces);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }
        public Response<List<Province>> GetAll()
        {
            try
            {
                List<Province> provinces = new List<Province>();
                provinces = this.MmersetaContext.Provinces.ToList();
                return new Response<List<Province>>(provinces);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public Response<List<Town>> GetAllTown()
        {
            try
            {
                List<Town> towns = new List<Town>();
                towns = this.MmersetaContext.Towns.ToList();
                return new Response<List<Town>>(towns);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }
        public Response<List<DisabilityRating>> GetAllDisabilityRating()
        {
            try
            {
                List<DisabilityRating> disabilityRating = new List<DisabilityRating>();
                disabilityRating = this.MmersetaContext.DisabilityRatings.ToList();
                return new Response<List<DisabilityRating>>(disabilityRating);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }
        public Response<List<DisabilityStatus>> GetAllDisabilityStatus()
        {
            try
            {
                List<DisabilityStatus> disabilityStatus = new List<DisabilityStatus>();
                disabilityStatus = this.MmersetaContext.DisabilityStatuses.ToList();
                return new Response<List<DisabilityStatus>>(disabilityStatus);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }
        public Response<List<TvetFetQualification>> GetAllTvetFetQualification()
        {
            try
            {
                List<TvetFetQualification> tvetFetQualification = new List<TvetFetQualification>();
                tvetFetQualification = this.MmersetaContext.TvetFetQualifications.ToList();
                return new Response<List<TvetFetQualification>>(tvetFetQualification);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public Response<List<OfoCode>> GetAllOfoCode()
        {
            try
            {
                List<OfoCode> ofoCode = new List<OfoCode>();
                ofoCode = this.MmersetaContext.OfoCodes.Take(200).ToList();
                return new Response<List<OfoCode>>(ofoCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }
        public Response<List<HighestQualificationRequired>> GetAllHighestQualificationRequired()
        {
            try
            {
                List<HighestQualificationRequired> highestQualificationRequired = new List<HighestQualificationRequired>();
                highestQualificationRequired = this.MmersetaContext.HighestQualificationRequireds.ToList();
                return new Response<List<HighestQualificationRequired>>(highestQualificationRequired);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }
        public Response<List<SchoolResponse>> GetAllPreviousSchools()
        {
            try
            {
                List<SchoolResponse> previousSchool = new List<SchoolResponse>();
                string query = @"SELECT id,official_institution_name FROM merseta.previous_schools order by id desc limit 500;";

                DataRowCollection ds = runSQLSSH(query);
                if (ds.Count > 0)
                {
                    foreach (DataRow row in ds)
                     {
                        var schools = new SchoolResponse 
                        {
                            Id =Convert.ToInt64( row["id"]),
                            SchoolName = row["official_institution_name"].ToString()
                        };
                        previousSchool.Add(schools);
                    }
                }
                   
                return new Response<List<SchoolResponse>>(previousSchool);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }
        public Response<List<Region>> GetAllRegions()
        {
            try
            {
                List<Region> region = new List<Region>();
                region = this.MmersetaContext.Regions.ToList();
                return new Response<List<Region>>(region);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }
        public Response<List<RegionTown>> GetAllRegionTowns()
        {
            try
            {
                List<RegionTown> regionTown = new List<RegionTown>();
                regionTown = this.MmersetaContext.RegionTowns.ToList();
                return new Response<List<RegionTown>>(regionTown);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public Response<List<HighestEducation>> GetAllHighestEducation()
        {
            
            List<HighestEducation> highestEducation = new List<HighestEducation>();
            string query = @"SELECT * FROM merseta.highest_education;";

            DataRowCollection ds = runSQLSSH(query);

            foreach (DataRow row in ds)
            {
                var _highestEducation= new HighestEducation 
                {
                    id = Convert.ToInt32(row["id"]),
                    code= row["code"].ToString(),
                    description = row["description"].ToString()
                };
                highestEducation.Add(_highestEducation);    
            }


            return new Response<List<HighestEducation>>(highestEducation);
        }

        public Response<List<Language>> GetAllLanguages()
        {
            try
            {
                List<Language> lang = new List<Language>();
                lang = this.MmersetaContext.Languages.ToList();
                return new Response<List<Language>>(lang);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public Response<List<Learnership>> GetAllLearnership()
        {
            try
            {
                List<Learnership> learn = new List<Learnership>();
                learn = this.MmersetaContext.Learnerships.Take(500).ToList();
                return new Response<List<Learnership>>(learn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public Response<List<StatssaAreaCode>> GetAllStatssaAreaCode()
        {
            try
            {
                List<StatssaAreaCode> learn = new List<StatssaAreaCode>();
                learn = this.MmersetaContext.StatssaAreaCodes.Take(500).ToList();
                return new Response<List<StatssaAreaCode>>(learn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public Response<List<Address>> GetAllUserAddress(int useraddressid)
        {
            try
            {
                List<Address> addresses = new List<Address>();
                addresses = this.MmersetaContext.Addresses.Where(x => x.Id == useraddressid).ToList();
                return new Response<List<Address>>(addresses);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public Response<List<Company>> GetSingleCompanies(int id)
        {


            try
            {
                List<Company> company = new List<Company>();
                company = this.MmersetaContext.Companies.Where(x => x.Id == id).ToList();
                return new Response<List<Company>>(company);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public Response<List<SdfCompany>> GetSdfCompany(int companyid)
        {
            try
            {
                List<SdfCompany> sdf = new List<SdfCompany>();
                sdf = this.MmersetaContext.SdfCompanies.Where(x => x.CompanyId == companyid && x.SdfTypeId == 1).ToList();
                return new Response<List<SdfCompany>>(sdf);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public Response<List<SicCode>> GetSicCode(int sicode)
        {
            try
            {
                List<SicCode> sic = new List<SicCode>();
                sic = this.MmersetaContext.SicCodes.Where(x => x.Id == sicode).ToList();
                return new Response<List<SicCode>>(sic);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public Response<List<Models.Task>> GetAllTasks(int targetKey)
        {
            try
            {
                List<Task> task = new List<Task>();
                var _tasks = this.MmersetaContext.Tasks.Where(x => x.TargetKey == targetKey && x.TargetClass == "haj.com.entity.CompanyLearners")
                    .Select(x => new { x.TargetKey, x.Id, x.TargetClass, x.TaskStatus, x.ActionDate, x.CompletionDate, x.CreateDate, x.Description, x.DueDate, x.ActionUserId, x.CreateUserId }).Take(10).ToList();

                foreach (var tk in _tasks)
                {
                    task.Add(new Task { TargetKey = tk.TargetKey, Id = tk.Id, TaskStatus = tk.TaskStatus, TargetClass = tk.TargetClass, ActionDate = tk.ActionDate, Description = tk.Description, CompletionDate = tk.CompletionDate, CreateDate = tk.CreateDate, DueDate = tk.DueDate, ActionUserId = tk.ActionUserId, CreateUserId = tk.ActionUserId });
                }
                return new Response<List<Task>>(task);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }
        //get approval task
        public Response<List<LearnerApplicationLifecycle>> GetRecommendationComments(int apcomlearnID)
        {
            try
            {
                List<LearnerApplicationLifecycle> _apcoment = new List<LearnerApplicationLifecycle>();
                _apcoment = this.MmersetaContext.LearnerApplicationLifecycles.Where(x => x.Action_User_Id == apcomlearnID).ToList();

                return new Response<List<LearnerApplicationLifecycle>>(_apcoment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }
        public Response<List<LearnerApplicationComments>> GetApplicationComents(int comlearnID)
        {
            try
            {
                List<LearnerApplicationComments> _coment = new List<LearnerApplicationComments>();
                _coment = this.MmersetaContext.LearnerApplicationCommentss.Where(x => x.CompanyLearnerId == comlearnID).ToList();
                return new Response<List<LearnerApplicationComments>>(_coment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }
        //approval insert
        public string ApprovalComments(int apcomlearnID, DateTime statusdate, int snetNotification, string apcomment, ulong apisdeleted,
            DateTime apdeletedon, int actionuser, int companylearnerid)
        {
            try
            {
                LearnerApplicationLifecycle coments = new LearnerApplicationLifecycle();
                coments.Learner_Status_Id = apcomlearnID;
                coments.Learner_Status_Date = statusdate;
                coments.Is_Notification_Set = snetNotification;
                coments.Notes = apcomment;
                coments.Is_Deleted = apisdeleted;
                coments.Deleted_On = apdeletedon;
                coments.Action_User_Id = actionuser;
                coments.Company_Learner_Id = companylearnerid;
                this.MmersetaContext.LearnerApplicationLifecycles.Add(coments);
                this.MmersetaContext.SaveChanges();
                return "completed";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }
        public string InsertChatComments(int comlearnID, DateTime date, int createid, string comment, ulong isdeleted, DateTime deletedon)
        {
            try
            {
                LearnerApplicationComments coments = new LearnerApplicationComments();
                coments.CompanyLearnerId = comlearnID;
                coments.CreateDate = date;
                coments.CreateUserId = createid;
                coments.Comments = comment;
                coments.IsDeleted = isdeleted;
                coments.DeletedOn = deletedon; ;
                this.MmersetaContext.LearnerApplicationCommentss.Add(coments);
                this.MmersetaContext.SaveChanges(); ;
                return "completed";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public Response<List<ConfigDoc>> GetAllconfigDoc()
        {
            try
            {
                List<ConfigDoc> conf = new List<ConfigDoc>();
                var _confile = this.MmersetaContext.ConfigDocs.Select(x => new { x.Id, x.CreateDate, x.Name, x.ReqImmediate }).ToList();
                foreach (var con in _confile)
                {
                    conf.Add(new ConfigDoc { Id = con.Id, CreateDate = con.CreateDate, Name = con.Name, ReqImmediate = con.ReqImmediate });
                }

                return new Response<List<ConfigDoc>>(conf);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            throw new NotImplementedException();
        }

        public Response<List<Doc>> GetAllLearnerconfigDoc(int user_id, int target_key)
        {
            try
            {
                List<Doc> _docs = new List<Doc>();
                var documents = this.MmersetaContext.Docs.Where(x => x.UserId == user_id && x.TargetClass == "haj.com.entity.CompanyLearners" && x.TargetKey == target_key).Select(x => new { x.Id, x.CreateDate, x.Extension, x.OriginalFname, x.VersionNo, x.OriginalDocId, x.ConfigDocId, x.TargetClass, x.TargetKey, x.UserId }).ToList();
                foreach (var doc in documents)
                {
                    _docs.Add(new Doc { Id = doc.Id, CreateDate = doc.CreateDate, Extension = doc.Extension, OriginalFname = doc.OriginalFname, VersionNo = doc.VersionNo, OriginalDocId = doc.OriginalDocId, ConfigDocId = doc.ConfigDocId, TargetClass = doc.TargetClass, TargetKey = doc.TargetKey, UserId = doc.UserId });
                }

                return new Response<List<Doc>>(_docs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public Response<List<DocByte>> GetAllUserDocuments(int docid)
        {
            try
            {
                List<DocByte> _docb = new List<DocByte>();
                _docb = this.MmersetaContext.DocBytes.Where(x => x.DocId == docid).ToList();
                return new Response<List<DocByte>>(_docb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public Response<List<Company>> GetAllCompaniesfoview()
        {
            List<Company> company = new List<Company>();
            company.AddRange(MmersetaContext.Companies.OrderByDescending(x => x.Id).Take(10).ToList());

            if (company != null)
            {
                return new Response<List<Company>>(company);
            }
            return new Response<List<Company>>(company);
        }

        public async Task<Response<List<Title>>> GetAllTitles()
        {
            List<Title> titles = new List<Title>();
            titles.AddRange(await MmersetaContext.Titles.OrderBy(x => x.Id).ToListAsync());

            if (titles != null)
            {
                return new Response<List<Title>>(titles);
            }
            return new Response<List<Title>>(titles);
        }

        public async Task<Response<List<OccupationCategory>>> GetOccupations()
        {
            var occupations = await MmersetaContext.OccupationCategories.ToListAsync();
            if (occupations == null) throw new Exception("Occupation not found");

            return new Response<List<OccupationCategory>>(occupations);
        }

        public async Task<Response<List<LearnerAchievementStatus>>> GetAllLearnerAchievementStatus()
        {
            List<LearnerAchievementStatus> learner_achievement_status = new List<LearnerAchievementStatus>();
            learner_achievement_status.AddRange(await MmersetaContext.LearnerAchievementStatuss.OrderBy(x => x.Id).ToListAsync());

            if (learner_achievement_status != null)
            {
                return new Response<List<LearnerAchievementStatus>>(learner_achievement_status);
            }
            return new Response<List<LearnerAchievementStatus>>(learner_achievement_status);
        }

        public async Task<Response<List<MaritalStatus>>> GetAllMaritalStatus()
        {
            List<MaritalStatus> marital_status = new List<MaritalStatus>();
            try
            {
                var maritalStatus = await MmersetaContext.MaritalStatuses.ToListAsync();
                marital_status.AddRange(maritalStatus);
                return new Response<List<MaritalStatus>>(marital_status);
            }
            catch (Exception ex)
            {


            }
            return new Response<List<MaritalStatus>>(marital_status);
        }

        public async Task<Response<SdpCompany>> getSdpCompany(int sdp_id)
        {
            try
            {
                SdpCompany sdps = new SdpCompany();
                var _confile = await this.MmersetaContext.SdpCompanies.Select(x => new { x.Id, x.CreateDate, x.ApprovalStatus, x.CompanyId, x.SdpId, x.SpfTypeId, x.TrainingSiteId }).Where(x => x.SdpId == sdp_id).FirstOrDefaultAsync();
                sdps = new SdpCompany { Id = _confile.Id, CreateDate = _confile.CreateDate, ApprovalStatus = _confile.ApprovalStatus, CompanyId = _confile.CompanyId, SdpId = _confile.SdpId, SpfTypeId = _confile.SpfTypeId, TrainingSiteId = _confile.TrainingSiteId };

                return new Response<SdpCompany>(sdps);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        public async Task<Response<List<Equity>>> GetAllEquityRace()
        {
            List<Equity> _equity = new List<Equity>();
            try
            {
                var equity = await MmersetaContext.Equities.ToListAsync();
                _equity.AddRange(equity);
                return new Response<List<Equity>>(_equity);
            }
            catch (Exception ex)
            {
            }
            return new Response<List<Equity>>(_equity);
        }

        public async Task<Response<Doc>> GetModeratorconfigDoc(int user_id, int target_key)
        {
            try
            {
                Doc _docs = new Doc();
                var _docum = await this.MmersetaContext.Docs.Where(x => x.UserId == user_id && x.TargetClass == "haj.com.entity.AssessmmentModeratorBatch" && x.TargetKey == target_key).OrderByDescending(s => s.Id).Select(x => new { x.Id, x.CreateDate, x.Extension, x.OriginalFname, x.VersionNo, x.OriginalDocId, x.ConfigDocId, x.TargetClass, x.TargetKey, x.UserId }).FirstOrDefaultAsync();
                _docs = new Doc { Id = _docum.Id, CreateDate = _docum.CreateDate, Extension = _docum.Extension, OriginalFname = _docum.OriginalFname, VersionNo = _docum.VersionNo, OriginalDocId = _docum.OriginalDocId, ConfigDocId = _docum.ConfigDocId, TargetClass = _docum.TargetClass, TargetKey = _docum.TargetKey, UserId = _docum.UserId };

                return new Response<Doc>(_docs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Response<List<Nationality>>> GetAllCountries()
        {
            try
            {
                var _countries = await MmersetaContext.Nationalities.ToListAsync();
                return new Response<List<Nationality>>(_countries);
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }


        }

        public async Task<Response<List<Gender>>> GetAllGender()
        {
            try
            {
                var _gender = await MmersetaContext.Genders.ToListAsync();
                return new Response<List<Gender>>(_gender);
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }

        public async Task<Response<List<OrganisationType>>> GetAllOrganizationTypes()
        {
            try
            {
                var _organisationTypes = await MmersetaContext.OrganisationTypes.ToListAsync();
                return new Response<List<OrganisationType>>(_organisationTypes);
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }

        public async Task<Response<List<SetaResponse>>> GetAllSetas()
        {
            List<SetaResponse> response = new List<SetaResponse>();
            string query = @"SELECT * FROM merseta.seta";

            DataRowCollection ds = runSQLSSH(query);
            if (ds.Count > 0)
            {
                foreach (DataRow row in ds)
                {
                    var setaResponses = new SetaResponse
                    {
                        Id = Convert.ToInt64(row["id"]),
                        Code = row["code"].ToString(),
                        CreateDate =Convert.ToDateTime( row["create_date"]),
                        Description = row["description"].ToString(),
                        SetmisCode =Convert.ToInt64( row["setmis_code"]),
                        
                    };
                    response.Add(setaResponses);
                }
            }
            return new Response<List<SetaResponse>>(response);
        }

        public async Task<Response<List<LearningProgramTypeResponse>>> GetAllLearningProgramTypes()
        {
            List<LearningProgramTypeResponse> response = new List<LearningProgramTypeResponse>();
            string query = @"SELECT * FROM merseta.learning_program_type";

            DataRowCollection ds = runSQLSSH(query);
            if (ds.Count > 0)
            {
                foreach (DataRow row in ds)
                {
                    var programResponses = new LearningProgramTypeResponse
                    {
                       LearningProgramId = Convert.ToInt64(row["learning_program_type_id"]),
                       Description=row["description"].ToString(),

                    };
                    response.Add(programResponses);
                }
            }
            return new Response<List<LearningProgramTypeResponse>>(response);
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

        public async Task<Response<List<SocioeconomicStatusCode>>> GetAllSocioEconomicStatus()
        {
            try
            {
                var _socioeconomicStatusCodes = await MmersetaContext.SocioeconomicStatusCodes.ToListAsync();
                return new Response<List<SocioeconomicStatusCode>>(_socioeconomicStatusCodes);
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }

        public async Task<Response<List<AlternativeIdType>>> GetAllAlternativeIdType()
        {
            try
            {
                var _alternativeIdTypes = await MmersetaContext.AlternativeIdTypes.ToListAsync();
                return new Response<List<AlternativeIdType>>(_alternativeIdTypes);
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }

        public async Task<Response<List<CitizenResidentStatus>>> GetAllCitizenResidentStatus()
        {
            try
            {
                var _CitizenResidentStatus = await MmersetaContext.CitizenResidentStatuses.ToListAsync();
                return new Response<List<CitizenResidentStatus>>(_CitizenResidentStatus);
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }

        public async Task<Response<List<AccreditationStatus>>> GetAllAccreditionStatuses()
        {
            try
            {
                var _accreditationStatus = await MmersetaContext.AccreditationStatuses.ToListAsync();
                return new Response<List<AccreditationStatus>>(_accreditationStatus);
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
    }
}
