using mersetaWebAPI.CustomModels.Company;
using mersetaWebAPI.CustomModels.Provider;
using mersetaWebAPI.CustomModule;
using mersetaWebAPI.Data;
using mersetaWebAPI.Extentions;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using XAct;

namespace mersetaWebAPI.Implementation
{
    //get method
    //public static class SqlDataReaderExtensions
    //{
    //    public static T Get<T>(this DataRow reader, string columnName)
    //    {
    //        if (reader.IsNull(columnName))
    //            return default;
    //        return reader.Get<T>(columnName);
    //    }
    //}
    public class CompanyService : ICompany
    {
        private readonly mersetaContext MmersetaContext;
        private IConfiguration configuration;

        public CompanyService(mersetaContext MmersetaContext, IConfiguration configuration)
        {
            this.MmersetaContext = MmersetaContext;
            this.configuration = configuration;
        }

        public Response<List<Company>> GetAllCompanies()
        {
            List<Company> company = new List<Company>();
            company.AddRange(MmersetaContext.Companies.Take(500).ToList());

            if (company != null)
            {
                return new Response<List<Company>>(company);
            }
            return new Response<List<Company>>(company);
        }

        public async Task<Response<List<Company>>> GetAllCompaniesInLearships()
        {
            var company = new List<Company>();

            QueryExecutor queryExecutor = new QueryExecutor(configuration);

            string query = @"select distinct t4.*
                            from training_provider_application t1
                            inner join company_qualifications t2 on t1.company_id = t2.company_id
                            inner join learnership t3 on t3.qualification_id = t2.qualification_id
                            inner join company t4 on t4.id = t1.company_id
                            where t1.approval_status = 0";

            DataRowCollection ds = queryExecutor.runSQLSSH(query);



            if (ds.Count > 0)
            {
                foreach (DataRow row in ds)
                {
                    var _company = new Company();
                    //{
                    _company.Id = Convert.ToInt64(row["id"]);
                    _company.CompanyGuid = row["company_guid"].ToString();
                    _company.CompanyName = row["company_name"].ToString();
                    _company.Email = row["email"].ToString();
                    _company.CompanyRegistrationNumber = row["company_registration_number"].ToString();
                    if (!row.IsNull("company_status"))
                    {
                        _company.CompanyStatus = Convert.ToInt32(row["company_status"]);
                        if (!row.IsNull("company_type")) { _company.CompanyType = Convert.ToInt32(row["company_type"]); }

                        if (!row.IsNull("create_date")) { _company.CreateDate = Convert.ToDateTime(row["create_date"]); }

                        _company.FaxNumber = row["fax_number"].ToString();
                        _company.LevyNumber = row["levy_number"].ToString();
                        _company.LevyNumberAdditional = row["levy_number_additional"].ToString();
                        if (!row.IsNull("number_of_employees")) { _company.NumberOfEmployees = Convert.ToInt32(row["number_of_employees"]); }

                        _company.PayeSdlNumber = row["paye_sdl_number"].ToString();
                        _company.TelNumber = row["tel_number"].ToString();
                        _company.TradingName = row["trading_name"].ToString();
                        if (!row.IsNull("banking_details_id")) { _company.BankingDetailsId = Convert.ToInt64(row["banking_details_id"]); }
                        if (!row.IsNull("chamber_id")) { _company.ChamberId = Convert.ToInt64(row["chamber_id"]); }

                        if (!row.IsNull("form_user_id")) { _company.FormUserId = Convert.ToInt64(row["form_user_id"]); }
                        if (!row.IsNull("institution_type_id")) { _company.InstitutionTypeId = Convert.ToInt64(row["institution_type_id"]); }
                        if (!row.IsNull("postal_address_id")) { _company.PostalAddressId = Convert.ToInt64(row["postal_address_id"]); }
                        if (!row.IsNull("registered_address_id")) { _company.RegisteredAddressId = Convert.ToInt64(row["registered_address_id"]); }

                        if (!row.IsNull("residential_address_id")) { _company.ResidentialAddressId = Convert.ToInt64(row["residential_address_id"]); }
                        _company.AccreditationNumber = row["accreditation_number"].ToString();


                        // };
                        company.Add(_company);
                    }
                }



                return new Response<List<Company>>(company);

            }
            return new Response<List<Company>>(company);
        }

        public async Task<Response<List<SaqaQualification>>> GetCompanyQualification(long companyId)
        {
            var companyQualification = await MmersetaContext.CompanyQualifications.Where(x => x.CompanyId == companyId).Select(x => x.QualificationId).ToListAsync();
            var saqaQualification = await MmersetaContext.SaqaQualifications.Where(x => companyQualification.Contains(x.Id)).ToListAsync();
            return new Response<List<SaqaQualification>>(saqaQualification);
        }

        public async Task<Response<List<Site>>> GetCompanySites(long companyId)
        {
            List<Site> SiteList = new List<Site>();
            string query = @"SELECT * FROM merseta.sites where company_id=" + companyId;
            DataRowCollection ds = runSQLSSH(query);

            if (ds.Count > 0)
            {
                foreach (DataRow row in ds)
                {

                    var _site = new Site
                    {
                        Id = Convert.ToInt64(row["id"]),
                        CompanyName = row["company_name"].ToString(),
                        NumberOfEmployees = Convert.ToInt32(row["number_of_employees"]),
                        CompanyId = Convert.ToInt64(row["company_id"]),
                       
                      
                    };
                    SiteList.Add(_site);
                }
            }

            //var sites =await MmersetaContext.Sites.Where(x => x.CompanyId == companyId).ToListAsync();

            return new Response<List<Site>>(SiteList);
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
        public Response<List<SaqaQualificationResponse>> GetQualificationByCompany(long CompanyId)
        {
            QueryExecutor queryExecutor = new QueryExecutor(configuration);
            List<SaqaQualificationResponse> saqaQualificationResponses = new List<SaqaQualificationResponse>();
            //raw query
            string query = @"select distinct saqa_q.nqf_level_id
                      ,saqa_q.workplace_approval_required
                      ,c_qual.qualification_id
                      ,saqa_q.qualificationtitle
                    --  ----- 6 EXTRA COLUMNS
                      ,l_ship.code 'learnership_code'
                      ,ofo.description 'ofo_code_description'
                      ,ofo.ofo_code
                      ,saqa_q.qualificationid 'saqa_qualification_id'
                      ,last_date_for_enrolment
                      ,lastdateforachievement
                    -- -----------------------  
                from company_qualifications c_qual
                left join learnership l_ship on l_ship.qualification_id = c_qual.qualification_id
                left join ofo_codes ofo on ofo.id = l_ship.ofo_codes_id
                left join saqa_qualification saqa_q on saqa_q.id = l_ship.qualification_id
                where c_qual.company_id =" + CompanyId;

            DataRowCollection ds = queryExecutor.runSQLSSH(query);
            if (ds.Count > 0)
            {
                foreach (DataRow row in ds)
                {
                    string ofoCode = ""; string ofoDesc = ""; int nqfleveid = 0; bool workPlaceApprovalRequired = false; string? title = ""; long saqaQualificationId = 0;
                    string learnershipCode = "";
                    DateTime enrollment = DateTime.Now; DateTime achievemnt = DateTime.Now;
                    if (!row.IsNull("nqf_level_id")) { nqfleveid = Convert.ToInt32(row["nqf_level_id"]); }
                    if (!row.IsNull("workplace_approval_required")) { workPlaceApprovalRequired = Convert.ToBoolean(row["workplace_approval_required"]); }
                    if (!row.IsNull("qualificationtitle")) { title = row["qualificationtitle"].ToString(); }
                    if (!row.IsNull("ofo_code")) { ofoCode = row["ofo_code"].ToString(); }
                    if (!row.IsNull("ofo_code_description")) { ofoDesc = row["ofo_code_description"].ToString(); }
                    if (!row.IsNull("saqa_qualification_id")) { saqaQualificationId = Convert.ToInt64(row["saqa_qualification_id"]); }
                    if (!row.IsNull("last_date_for_enrolment")) { enrollment = Convert.ToDateTime(row["last_date_for_enrolment"]); }
                    if (!row.IsNull("lastdateforachievement")) { achievemnt = Convert.ToDateTime(row["lastdateforachievement"]); }
                    if (!row.IsNull("learnership_code")) { learnershipCode = row["learnership_code"].ToString(); }
                    var saqaResponse = new SaqaQualificationResponse
                    {
                        NqfLevelId = nqfleveid,
                        WorkplaceApprovalRequired = workPlaceApprovalRequired,
                        QualificationId = Convert.ToInt64(row["qualification_id"]),
                        QualificationTitle = title,
                        OfoCodeDescription = ofoDesc,
                        OfoCode = ofoCode,
                        SaqaQualificationId = saqaQualificationId,
                        LastDateForEnrolment = enrollment,
                        LastDateForAchievement = achievemnt,
                        LearnershipCode=learnershipCode,

                    };
                    saqaQualificationResponses.Add(saqaResponse);
                }
            }
            return new Response<List<SaqaQualificationResponse>>(saqaQualificationResponses);
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

            throw new Exception ("No company found");
        }

        public Response<List<ProviderResponse>> GetSingleCompaniesByAccreditationNumber(string accreditationNumber)
        {
            try
            {
                List<ProviderResponse> providers = new List<ProviderResponse>();
                QueryExecutor queryExecutor = new QueryExecutor(configuration);
                string query = $"select t1.id,t1.company_guid,t1.company_name,t1.company_registration_number,t1.company_type,t1.levy_number,t1.levy_number_additional, t1.trading_name,t1.company_status,t1.sic_code_id,t1.seta_id,t2.accreditation_application_type,t2.accreditation_status,t2.etqa_review_committee_date,t2.accreditation_number as AccreditationNumber,t4.first_name,t4.last_name,t4.email,t4.fax_number,t4.tel_number,t4.cell_number from company t1 inner join training_provider_application t2 on t1.id=t2.company_id inner join sdf_company t3 on t1.id=t3.company_id inner join users t4 on t3.sdf_id=t4.id where   t3.sdf_type_id=1 and t2.accreditation_number= '" + accreditationNumber + "';";
                DataRowCollection ds = queryExecutor.runSQLSSH(query);
                if (ds.Count > 0)
                {
                    foreach (DataRow row in ds)
                    {
                        var providerResponse = new ProviderResponse
                        {
                            //int? val = dr["status"] == DBNull.Value ? (int?) null : Convert.ToInt32(dr["status"]);
                            //Address.Street = row["STREET"] == DBNull.Value ? "" : row["STREET"].ToString();
                            Id =(long?)row["id"],
                            CompanyGuid = row["company_guid"] == DBNull.Value ? "" : row["company_guid"].ToString(),
                            CompanyName = row["company_name"] == DBNull.Value ? "" : row["company_name"].ToString(),
                            CompanyRegistrationNumber = row["company_registration_number"] == DBNull.Value ? "" : row["company_registration_number"].ToString(),
                            CompanyType =row["company_type"] == DBNull.Value ? 0 : Convert.ToInt32(row["company_type"]),
                            LevyNumber = row["levy_number"] == DBNull.Value ? "" : row["levy_number"].ToString(),
                            levy_number_additional = row["levy_number_additional"] == DBNull.Value ? "" : row["levy_number_additional"].ToString(),
                            CompanyStatus =row["company_status"] == DBNull.Value ? 0 : Convert.ToInt32(row["company_status"]),
                            SicCodeId =row["sic_code_id"] == DBNull.Value ? 0 : Convert.ToInt64(row["sic_code_id"]),
                            SetaId = row["seta_id"] == DBNull.Value ? 0 : Convert.ToInt32(row["seta_id"]),
                            AccreditationApplicationType = row["accreditation_application_type"] == DBNull.Value ? 0 : Convert.ToInt32(row["accreditation_application_type"]),
                            AccreditationStatus = row["accreditation_status"] == DBNull.Value ? 0 : Convert.ToInt32(row["accreditation_status"]),
                            EtqaReviewCommitteeDate = row["etqa_review_committee_date"] == DBNull.Value ? DateTime.Now.Date : Convert.ToDateTime(row["seta_id"]),
                            AccreditationNumber= row["AccreditationNumber"] == DBNull.Value ? "" : row["AccreditationNumber"].ToString(),
                            FirstName = row["first_name"] == DBNull.Value ? "" : row["first_name"].ToString(),
                            LastName = row["last_name"] == DBNull.Value ? "" : row["last_name"].ToString(),
                            Email = row["email"] == DBNull.Value ? "" : row["email"].ToString(),
                            FaxNumber = row["fax_number"] == DBNull.Value ? "" : row["fax_number"].ToString(),
                            TelNumber= row["tel_number"] == DBNull.Value ? "" : row["tel_number"].ToString(),
                            CellNumber = row["cell_number"] == DBNull.Value ? "" : row["cell_number"].ToString()
                        };
                        providers.Add(providerResponse);
                    }
                }
                return new Response<List<ProviderResponse>>(providers);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new Exception("No company found");
        }

        public Response<List<Company>> GetSingleCompaniesByLevyNumber(string levyNumber)
        {
            try
            {
                List<Company> company = new List<Company>();
                company = this.MmersetaContext.Companies.Where(x => x.LevyNumber == levyNumber).ToList();
                return new Response<List<Company>>(company);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            throw new Exception("No company found");
        }
    }
}
