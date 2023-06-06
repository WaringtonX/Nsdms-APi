using mersetaWebAPI.Data;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System.Data;

namespace mersetaWebAPI.Implementation
{
    public class CompLearnerService : ICompLearner
    {
        private readonly mersetaContext MmersetaContext;
        private IConfiguration configuration;

        public CompLearnerService(mersetaContext MmersetaContext, IConfiguration configuration)
        {
            this.MmersetaContext = MmersetaContext;
            this.configuration = configuration;
        }
        public Response<CompanyLearner> GetSingCompLearner(int userid, int companyid)
        {
            List<CompanyLearner> company = new List<CompanyLearner>();
            CompanyLearner comp = new CompanyLearner();
            var compvar = MmersetaContext.CompanyLearners.
                Where(x => x.UserId == userid && x.CompanyId == companyid).FirstOrDefault();

            comp = compvar;
            if (comp != null)
            {
                return new Response<CompanyLearner>(comp);
            }
            return new Response<CompanyLearner>(comp);
        }
        public Response<CompanyLearner> GetSingCompLearnerById(long id)
        {
            List<CompanyLearner> company = new List<CompanyLearner>();
            CompanyLearner comp = new CompanyLearner();
            var compvar = MmersetaContext.CompanyLearners.
                Where(x => x.Id==id).FirstOrDefault();

            comp = compvar;
            if (comp != null)
            {
                return new Response<CompanyLearner>(comp);
            }
            return new Response<CompanyLearner>(comp);
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


    }
}
