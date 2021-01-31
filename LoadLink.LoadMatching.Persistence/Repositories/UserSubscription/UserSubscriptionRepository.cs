using Dapper;
using LoadLink.LoadMatching.Application.UserSubscription.Repository;
using LoadLink.LoadMatching.Domain.AggregatesModel.UserSubscriptionAggregate;
using LoadLink.LoadMatching.Persistence.Data;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.UserSubscription
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {

        private readonly IDbConnection _dbConnection;

        public UserSubscriptionRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<UserApiKey> UserApiKeys(int userId)
        {

            var proc = "dbo.usp_GetUserSubscribedFeatureKeys";
            var param = new DynamicParameters();

            param.Add("@userId", userId);

            var result = await SqlMapper.QueryAsync<string>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return new UserApiKey { UserId = userId, ApiKeys = result?.ToList() };
        }

    }
}
