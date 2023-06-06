using MySql.Data.MySqlClient;
using Renci.SshNet;
using System.Data;

namespace mersetaWebAPI.Extentions
{
    public class QueryExecutor
    {
        private IConfiguration configuration;
        public QueryExecutor(IConfiguration configuration)
        {
            this.configuration = configuration;
        
        }
        public DataRowCollection runSQLSSH(String SQL)
        {
            DataSet ds = null;
            try
            {

                using (var client = new SshClient("")) // establishing ssh connection to server where MySql is hosted
				 
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                      
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
