using Dapper;
using LoadLink.LoadMatching.Application.LoadPosting.Models.Commands;
using LoadLink.LoadMatching.Application.LoadPosting.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.LoadPosting
{
    public class LoadPostingRepository : ILoadPostingRepository
    {

        private readonly IDbConnection _dbConnection;
        public LoadPostingRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<int> CreateAsync(UspCreateLoadPostingCommand createCommand)
        {
            var proc = "dbo.usp_CreateLoad";

            createCommand.Comment = string.IsNullOrEmpty(createCommand.Comment) ?
                                            createCommand.Comment : await CleanseComment(createCommand.Comment);
            var param = new DynamicParameters(createCommand);

            param.Add("@Token", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return param.Get<int>("@Token");
        }

        public async Task DeleteAsync(int token, string custCd, int userId)
        {
            var proc = "dbo.usp_DeleteLoadPosting";
            var param = new DynamicParameters();
            param.Add("@Token", token);
            param.Add("@CustCD", custCd);
            param.Add("@UserId", userId);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }

        public async Task<UspGetLoadPostingResult> GetAsync(int token, string custCd, string mileageProvider)
        {
            var proc = "dbo.usp_GetLoadPosting";
            var param = new DynamicParameters();
            param.Add("@Token", token);
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspGetLoadPostingResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetLoadPostingResult>> GetListAsync(string custCd, string mileageProvider, bool? getDAT = false)
        {
            var proc = "dbo.usp_GetLoadPosting";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@GetDAT", getDAT);

            var result = await SqlMapper.QueryAsync<UspGetLoadPostingResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task UpdateAsync(int token, string pstatus)
        {
            var proc = "dbo.usp_UpdateLoadPostingStatus";
            var param = new DynamicParameters();
            param.Add("@Token", token);
            param.Add("@PStatus", pstatus);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateLeadCount(int token, int initialCount)
        {
            var proc = "dbo.usp_UpdateLoadPostingLeadsCount";
            var param = new DynamicParameters();
            param.Add("@Token", token);
            param.Add("@InitialLeadsCount", initialCount);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }

        private async Task<string> CleanseComment(string comment)
        {
            var proc = "usp_GetBadWord";
            var param = new DynamicParameters();
            param.Add("@Comment", comment);

            var result = await SqlMapper.QueryAsync(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            if (result != null)
                comment = string.Empty;

            return comment;
        }
    }
}
