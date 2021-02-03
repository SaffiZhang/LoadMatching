using Dapper;
using LoadLink.LoadMatching.Application.RepostAll.Repository;
using LoadLink.LoadMatching.Persistence.Data;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.RepostAll
{
    public class RepostAllRepository : IRepostAllRepository
    {
        private readonly IDbConnection _dbConnection;

        public RepostAllRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<int> RepostAllAsync(string custCd, int userId)
        {
            var proc = "usp_RepostAll";

            var param = new DynamicParameters();
            param.Add("@CustCd", custCd);
            param.Add("@UserId", userId);
            
            param.Add("@RepostStatus", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return param.Get<int>("@RepostStatus");
        }
    }
}
