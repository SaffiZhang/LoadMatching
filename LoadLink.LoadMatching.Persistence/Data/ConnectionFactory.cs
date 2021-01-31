using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace LoadLink.LoadMatching.Persistence.Data
{
    public interface IConnectionFactory
    {
        string ConnectionString { get; }
        IDbConnection GetConnection { get; }
    }

    public class ConnectionFactory : IConnectionFactory
    {
        public string ConnectionString { get; }

        public ConnectionFactory(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IDbConnection GetConnection
        {
            get
            {
                DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);

                if (DbProviderFactories.TryGetFactory("System.Data.SqlClient", out var factory))
                {
                    var connection = factory.CreateConnection();
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                    return connection;
                }

                return null;
            }
        }


    }
}
