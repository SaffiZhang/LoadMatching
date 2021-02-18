using Dapper;
using LoadLink.LoadMatching.Application.Exclude.Models.Commands;
using LoadLink.LoadMatching.Application.Exclude.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.Exclude
{
    public class ExcludeRepository : IExcludeRepository
    {
        private readonly IDbConnection _dbConnection;

        public ExcludeRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<IEnumerable<UspGetExcludeResult>> GetListAsync(string custCd)
        {
            var proc = "dbo.usp_GetExclude";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);

            var result = await SqlMapper.QueryAsync<UspGetExcludeResult>(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<CreateExcludeCommand> CreateAsync(CreateExcludeCommand createExcludeCommand)
        {
            var proc = "dbo.usp_CreateExclude";
            var param = new DynamicParameters();

            param.Add("@CustCD", createExcludeCommand.CustCD);
            param.Add("@ExcludeCustCD", createExcludeCommand.ExcludeCustCD);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return createExcludeCommand;
        }

        public async Task DeleteAsync(string custCd, string excludeCustCd)
        {
            var proc = "dbo.usp_DeleteExclude";

            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@ExcludeCustCD", excludeCustCd);

            await SqlMapper.ExecuteAsync(
                _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);
        }
    }
}
