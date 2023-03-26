using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;

namespace BaltaBot.Domain.Infra.Context
{
    public class DataContext
    {
        public IDbConnection connection { get; set; }

        public DataContext()
        {
            connection = new SqliteConnection("DataSource=file::memory:?cache=shared");
            connection.Open();
        }

        public DataContext(string conn)
        {
            connection = new SqlConnection(conn);
            connection.Open();
        }

        public void Dispose()
        {
            connection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
