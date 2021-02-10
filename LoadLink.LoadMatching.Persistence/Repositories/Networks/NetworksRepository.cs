using Dapper;
using LoadLink.LoadMatching.Application.Networks.Models.Commands;
using LoadLink.LoadMatching.Application.Networks.Models.Queries;
using LoadLink.LoadMatching.Application.Networks.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.Networks
{
    public class NetworksRepository : INetworksRepository
    {
        private readonly IDbConnection _dbConnection;

        public NetworksRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<NetworksCommand> CreateAsync(NetworksCommand networks)
        {
            var proc = "dbo.usp_CreateNetwork";
            var param = new DynamicParameters();

            param.Add("@UserId", networks.UserId);
            param.Add("@Name", networks.Name);
            param.Add("@Type", networks.Type);
            param.Add("@CustCD", networks.CustCD);
            param.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            networks.Id = param.Get<int>("@Id");

            return networks;
        }

        public async Task DeleteAsync(int networksId)
        {
            var proc = "dbo.usp_DeleteNetwork";

            var param = new DynamicParameters();

            param.Add("@Id", networksId);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }

        public async Task<UspGetNetworkResult> GetAsync(int networksId)
        {
            var proc = "dbo.usp_GetNetwork";
            var param = new DynamicParameters();

            param.Add("@Id", networksId);

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspGetNetworkResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetNetworkResult>> GetListAsync(string custCd, int userId)
        {
            var proc = "dbo.usp_GetNetworkList";
            var param = new DynamicParameters();

            param.Add("@CustCd", custCd);
            param.Add("@UserID", userId);

            var result = await SqlMapper.QueryAsync<UspGetNetworkResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task UpdateAsync(int networksId, string name)
        {
            var proc = "dbo.usp_UpdateNetworkName";
            var param = new DynamicParameters();

            param.Add("@Name", name);
            param.Add("@Id", networksId);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }
    }
}
