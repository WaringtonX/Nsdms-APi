using AutoMapper;
using mersetaWebAPI.CustomModels.Task;
using mersetaWebAPI.Data;
using mersetaWebAPI.Exceptions;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System.Data;

namespace mersetaWebAPI.Implementation
{
    public class TaskService : ITasks
    {
        private IConfiguration configuration;
        private mersetaContext MmersetaContext;
        private readonly IMapper _mapper;

        public TaskService(IConfiguration configuration, mersetaContext MmersetaContext, IMapper mapper)
        {
            this.configuration = configuration;
            this.MmersetaContext = MmersetaContext;
            this._mapper = mapper;
        }
        public async Task<Response<string>> CreateTaskForBacthModeration(TaskResponse taskResponse)
        {
            taskResponse.CreateDate = DateTime.Now;
            taskResponse.DueDate = DateTime.Now;

            var response = _mapper.Map<mersetaWebAPI.Models.Task>(taskResponse);
            await MmersetaContext.Tasks.AddAsync(response);
            await MmersetaContext.SaveChangesAsync();
            return new Response<string>(response.Id.ToString(), "Added successfully");
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

        public async Task<Response<List<Models.Task>>> GetAssessmentTask()
        {
            var assessmenttask = await MmersetaContext.Tasks.Where(x => x.TargetClass == "haj.com.entity.CompanyLearnerBacthList" && x.TaskStatus == 0).ToListAsync();
            return new Response<List<Models.Task>>(assessmenttask);
        }

        public async Task<Response<string>> UpdateTaskStatus(long taskid, int status)
        {
            try
            {
                var _task = await MmersetaContext.Tasks.Where(x => x.Id == taskid).AsNoTracking().FirstAsync();
                if (_task == null) { throw new ApiException("Company  not found"); }

                _task.TaskStatus = status;
                MmersetaContext.Update(_task);
                int result = await MmersetaContext.SaveChangesAsync();
                return new Response<string>(taskid.ToString(), "Company  updated successfully");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating company");
            }
             
        }

        public async Task<Response<string>> CreateTaskUser(TaskUser taskResponse)
        {
            taskResponse.CreateDate = DateTime.Now;

            await MmersetaContext.TaskUsers.AddAsync(taskResponse);
            await MmersetaContext.SaveChangesAsync();
            return new Response<string>(taskResponse.Id.ToString(), "Added successfully");
        }

        public async Task<Response<List<TaskUser>>> GetTaskUser(long userid)
        {
            var taskusers = await MmersetaContext.TaskUsers.Where(x => x.UserId == userid).ToListAsync();
            return new Response<List<TaskUser>>(taskusers);
        }

        public async Task<Response<List<Models.Task>>> GetuserTasks(long userid)
        {
            List<Models.Task> usertasks = new List<Models.Task>();
            string query = @"select *
       from tasks t1 
       left join task_users  t2 on t2.task_id = t1.id
       where t2.user_id ="+ userid + " and t1.target_class in ('haj.com.entity.CompanyLearnerBacthList','haj.com.entity.ExternalModerationBatchRejection','haj.com.entity.CompanyLearners') and t1.task_status in (0,1,2);";

            DataRowCollection ds = runSQLSSH(query);


            if (ds.Count > 0)
            {
                foreach (DataRow row in ds)
                {
                     DateTime? action_date = null; DateTime? completion_date = null; DateTime? create_date = null; string? description = null; DateTime? due_date = null; string? guid = null; DateTime? start_date = null;
                    string? target_class = null; long? target_key = null; int ? task_status = null; int? workflow_process = null;long? action_user_id = null;long? company_id = null;long? create_user_id = null;long? hosting_company_process_id = null;
                    long? process_role_id =null;ulong? first_in_process = null;long? previous_task_id = null;

                    if (row.IsNull("action_date")) { action_date = null; } else { action_date = Convert.ToDateTime(row["action_date"]); }
                    if (row.IsNull("completion_date")) { completion_date = null; } else { completion_date = Convert.ToDateTime(row["completion_date"]); }
                    if (row.IsNull("create_date")) { create_date = null; } else { create_date = Convert.ToDateTime(row["create_date"]); }
                    if (row.IsNull("description")) { description = null; } else { description = row["description"].ToString(); }
                    if (row.IsNull("due_date")) { due_date = null; } else { due_date = Convert.ToDateTime(row["due_date"]); }
                    if (row.IsNull("guid")) { guid = null; } else { guid = row["guid"].ToString(); }
                    if (row.IsNull("start_date")) { start_date = null; } else { start_date = Convert.ToDateTime(row["start_date"]); }
                    if (row.IsNull("target_class")) { target_class = null; } else { target_class = row["target_class"].ToString(); }
                    if (row.IsNull("target_key")) { target_key = null; } else { target_key = Convert.ToInt64(row["target_key"]); }
                    if (row.IsNull("task_status")) { task_status = null; } else { task_status = (int)row["task_status"]; }
                    if (row.IsNull("workflow_process")) { workflow_process = null; } else { workflow_process = (int)row["workflow_process"]; }
                    if (row.IsNull("action_user_id")) { action_user_id = null; } else { action_user_id = Convert.ToInt64(row["action_user_id"]); }
                    if (row.IsNull("company_id")) { company_id = null; } else { company_id = Convert.ToInt64(row["company_id"]); }
                    if (row.IsNull("create_user_id")) { create_user_id = null; } else { create_user_id = Convert.ToInt64(row["create_user_id"]); }
                    if (row.IsNull("hosting_company_process_id")) { hosting_company_process_id = null; } else { hosting_company_process_id = Convert.ToInt64(row["hosting_company_process_id"]); }
                    if (row.IsNull("process_role_id")) { process_role_id = null; } else { process_role_id = Convert.ToInt64(row["process_role_id"]); }
                    if (row.IsNull("first_in_process")) { first_in_process = null; } else { first_in_process = (ulong)row["first_in_process"]; }
                    if (row.IsNull("previous_task_id")) { previous_task_id = null; } else { previous_task_id = Convert.ToInt64(row["previous_task_id"]); }
                    var _tasks = new Models.Task
                    {
                        Id = Convert.ToInt64(row["id"]),
                        ActionDate = action_date,
                        CompletionDate = completion_date,
                        CreateDate = create_date,
                        Description = description,
                        DueDate = due_date,
                        Guid = guid,
                        StartDate = start_date,
                        TargetClass = target_class,
                        TargetKey = target_key,
                        TaskStatus = task_status,
                        WorkflowProcess = workflow_process,
                        ActionUserId = action_user_id,
                        CompanyId = company_id,
                        CreateUserId = create_user_id,
                        HostingCompanyProcessId = hosting_company_process_id,
                        ProcessRoleId = process_role_id,
                        FirstInProcess = first_in_process,
                        PreviousTaskId = previous_task_id,

                    };
                    usertasks.Add(_tasks);
                }
            }



            return new Response<List<Models.Task>>(usertasks);
        }

        public async Task<Response<Models.Task>> GetSingleTasks(long taskid)
        {
            var task = await MmersetaContext.Tasks.Where(x => x.Id == taskid).FirstOrDefaultAsync();
            return new Response<Models.Task>(task);
        }
    }
}
