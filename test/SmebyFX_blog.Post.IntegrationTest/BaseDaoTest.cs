using System.Configuration;
using System.Data.SqlClient;

namespace SmebyFX_blog.Post.IntegrationTest
{
    public class BaseDaoTest
    {
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["blogDBconnectionString"].ConnectionString);
        }
    }
}
