using Dapper;
using LoadLink.LoadMatching.Application.NetworkMembers.Models.Commands;
using LoadLink.LoadMatching.Application.NetworkMembers.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.NetworkMember
{
    public class NetworkMembersRepository : INetworkMembersRepository
    {
        private readonly IDbConnection _dbConnection;

        public NetworkMembersRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<CreateNetworkMembersCommand> Create(CreateNetworkMembersCommand createCommand)
        {
            var proc = "dbo.usp_CreateNetworkMember";
            var param = new DynamicParameters(createCommand);


            param.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@RegisteredName", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
            param.Add("@CommonName", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
            param.Add("@CompanyPhone", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
            param.Add("@CompanyLocation", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
            param.Add("@ContactPhone", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
            param.Add("@PrimaryContactName", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            createCommand.Id = param.Get<int>("@Id");
            createCommand.RegisteredName = param.Get<string>("@RegisteredName");
            createCommand.CommonName = param.Get<string>("@CommonName");
            createCommand.CompanyPhone = param.Get<string>("@CompanyPhone");
            createCommand.CompanyLocation = param.Get<string>("@CompanyLocation");
            createCommand.ContactPhone = param.Get<string>("@ContactPhone");
            createCommand.PrimaryContactName = param.Get<string>("@PrimaryContactName");

            return createCommand;
        }

        public async Task Delete(int networkId, string custCd)
        {
            var proc = "dbo.usp_DeleteNetworkMember";

            var param = new DynamicParameters();
            param.Add("@NetworkId", networkId);
            param.Add("@MemberCustCD", custCd);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

        }

        public async Task<UspGetNetworkMemberResult> Get(int id)
        {
            var proc = "dbo.usp_GetNetworkMember";
            var param = new DynamicParameters();
            param.Add("@Id", id);;

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspGetNetworkMemberResult>(
               _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetNetworkMemberResult>> GetList(string custCd)
        {
            var proc = "dbo.usp_GetNetworkMemberList";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);

            var result = await SqlMapper.QueryAsync<UspGetNetworkMemberResult>(
               _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetNetworkMemberResult>> GetList(int networkId, string custCd)
        {
            var proc = "dbo.usp_GetNetworkMemberList";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@NetworkId", networkId);

            var result = await SqlMapper.QueryAsync<UspGetNetworkMemberResult>(
               _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;

        }
    }
}
