using AutoMapper;
using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System.Data;

namespace mersetaWebAPI.Implementation
{
    public class SingleUserService : ISingleUser
    {
        private readonly mersetaContext MmersetaContext;
        private IConfiguration configuration;
        private readonly IMapper _mapper;

        public SingleUserService(IConfiguration configuration, mersetaContext MmersetaContext, IMapper mapper)
        {
            this.configuration = configuration;
            this.MmersetaContext = MmersetaContext;
            this._mapper = mapper;
        }

        public async Task<Response<List<User>>> GetAssessmentUser(long assessmentid)
        {

            List<User> _users = new List<User>();
            string query = @"select t4.*
       from assessment_process_batch t1
       inner join assessment_process_batch_detail t2 on t1.assessment_process_batch_id = t2.assessment_process_batch_id
       inner join summative_assessment_report t3 on t2.assessment_id = t3.id
       inner join company_learners t5 on t5.id = t3.company_learners_id
       inner join users t4 on t4.id = t5.user_id
       where t1.assessment_process_batch_id= " + assessmentid;

            DataRowCollection ds = runSQLSSH(query);



            if (ds.Count > 0)
            {
                foreach (DataRow row in ds)
                {
                    var _us = new User
                    {
                        Id = Convert.ToInt64(row["id"]),
                        CellNumber = row["cell_number"].ToString(),
                        CreateDate = DateTime.Parse(row["create_date"].ToString()),
                        DateOfBirth = DateTime.Parse(row["date_of_birth"].ToString()),
                        Email = row["email"].ToString(),
                        FirstName = row["first_name"].ToString(),
                        LastName = row["last_name"].ToString(),
                        RsaIdNumber = row["rsa_id_number"].ToString(),
                        Status = (int)row["status"],
                        TelNumber = row["tel_number"].ToString(),
                        MiddleName = row["middle_name"].ToString(),

                    };
                    _users.Add(_us);
                }
            }



            return new Response<List<User>>(_users);
        }

        public Response<User> GetSingleUser(int id)
        {
            User user = new User();
            var uservar = MmersetaContext.Users.Where(x => x.Id == id).ToList();
            user = uservar[0];
            if (user != null)
            {
                return new Response<User>(user);
            }
            return new Response<User>(user);
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

        public async Task<Response<List<SummativeAssessmentReportTest>>> GetSummativeReportList(long assessmentid)
        {
            List<SummativeAssessmentReportTest> summreportall = new List<SummativeAssessmentReportTest>();
            string query = @"select t3.*
       from assessment_process_batch t1
       inner join assessment_process_batch_detail t2 on t1.assessment_process_batch_id = t2.assessment_process_batch_id
       inner join summative_assessment_report t3 on t2.assessment_id = t3.id
       inner join company_learners t5 on t5.id = t3.company_learners_id
       inner join users t4 on t4.id = t5.user_id
       where t1.assessment_process_batch_id = " + assessmentid;

            DataRowCollection ds = runSQLSSH(query);
            if (ds.Count > 0)
            {
                foreach (DataRow row in ds)
                {
                    //declare variables
                    long? intervention_level_id = 0;long? non_seta_qualifications_completion_id=0;long? nqf_levels_id = 0;
                    long? unit_standard_id = 0; long? users_id = 0; long? skills_set_id=0;
                    //check nulls on the row
                    if (row.IsNull("intervention_level_id")) { intervention_level_id = 0; } else { intervention_level_id =Convert.ToInt64( row["intervention_level_id"]); }
                    if (row.IsNull("non_seta_qualifications_completion_id")) { non_seta_qualifications_completion_id = 0; } else { non_seta_qualifications_completion_id =Convert.ToInt64( row["non_seta_qualifications_completion_id"]); }
                    if (row.IsNull("nqf_levels_id")) { nqf_levels_id = 0; } else { nqf_levels_id = Convert.ToInt64(row["nqf_levels_id"]); }
                    if (row.IsNull("unit_standard_id")) { unit_standard_id = 0; } else { unit_standard_id = Convert.ToInt64(row["unit_standard_id"]); }
                    if (row.IsNull("users_id")) { users_id = 0; } else { users_id = Convert.ToInt64(row["users_id"]); }
                    if (row.IsNull("skills_set_id")) { skills_set_id = 0; } else { skills_set_id = Convert.ToInt64(row["skills_set_id"]); }

                    var _summreport = new SummativeAssessmentReportTest
                    {
                        Id = Convert.ToInt64(row["id"]),
                        CreateDate = DateTime.Parse(row["create_date"].ToString()),
                        PivotNonPivot = (int)row["pivot_non_pivot"],
                        CompanyLearnersId = Convert.ToInt32(row["company_learners_id"]),
                        InterventionTypeId = Convert.ToInt32(row["intervention_type_id"]),
                        NqfAlignedId = Convert.ToInt32(row["nqf_aligned_id"]),
                        QualificationId = Convert.ToInt32(row["qualification_id"]),
                        SkillsProgramId = Convert.ToInt32(!row.IsNull("skills_program_id")),
                        SkillsSetId = skills_set_id,
                        TrainingProviderVerficationId = Convert.ToInt32(row["training_provider_verfication_id"]),
                        UnitStandardId = unit_standard_id,
                        UsersId = users_id,
                        //AssessmentProgressStatusTypeId = Convert.ToInt32(row["assessment_progress_status_type_id"]),
                        InterventionLevelId=intervention_level_id,
                        NonSetaQualificationsCompletionId= non_seta_qualifications_completion_id,
                        NqfLevelsId = nqf_levels_id,
                        
                    };
                    summreportall.Add(_summreport);
                }
            }
            return new Response<List<SummativeAssessmentReportTest>>(summreportall);
        }
    }
}
