using Dapper;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Commands;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Queries;
using LoadLink.LoadMatching.Application.EquipmentPosting.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.EquipmentPosting
{
    public class EquipmentPostingRepository : IEquipmentPostingRepository
    {
        private readonly IDbConnection _dbConnection;
        public EquipmentPostingRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<int> CreateAsync_Old(UspCreateEquipmentPostingCommand createCommand)
        {
            var proc = "dbo.usp_CreateEquipment";

            createCommand.Comment = string.IsNullOrEmpty(createCommand.Comment) ? 
                                            createCommand.Comment : await CleanseComment(createCommand.Comment);
            var param = new DynamicParameters(createCommand);

            param.Add("@Token", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return param.Get<int>("@Token");
        }

        public async Task<int> CreateAsync(UspCreateEquipmentPostingCommand createCommand)
        {
            var proc = "dbo.usp_CreateEquipment_Separate";

            createCommand.Comment = string.IsNullOrEmpty(createCommand.Comment) ?
                                            createCommand.Comment : await CleanseComment(createCommand.Comment);
            var param = new DynamicParameters(createCommand);

            var result = await SqlMapper.QueryFirstOrDefaultAsync<CreateEquipmentPostingQuery>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            if (result.Token > 0)
                await CreateMatchesAsync(result);

            return result.Token;
        }

        public async Task<int> CreateMatchesAsync(CreateEquipmentPostingQuery equipPosting)
        {
            var proc = "dbo.usp_MatchEquipment_Separate";
            var param = new DynamicParameters(equipPosting);

            param.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return param.Get<int>("@Result");
        }

        public async Task DeleteAsync(int token, string custCd, int userId)
        {
            var proc = "dbo.usp_DeleteEquipmentPosting";
            var param = new DynamicParameters();
            param.Add("@Token", token);
            param.Add("@CustCD", custCd);
            param.Add("@UserId", userId);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }

        public async Task<UspGetEquipmentPostingResult> GetAsync(int token, string custCd, string mileageProvider)

        {
            var proc = "dbo.usp_GetEquipmentPosting";
            var param = new DynamicParameters();
            param.Add("@Token", token);
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspGetEquipmentPostingResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetEquipmentPostingResult>> GetListAsync(string custCd, string mileageProvider, bool? getDAT = false)
        {
            var proc = "dbo.usp_GetEquipmentPosting";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@GetDAT", getDAT);

            var result = await SqlMapper.QueryAsync<UspGetEquipmentPostingResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetEquipmentPostingLLResult>> GetListLLAsync(string custCd, string mileageProvider, DateTime liveLeadTime, bool? getDAT = false)
        {
            var proc = "dbo.usp_GetEquipmentPosting_LL";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", mileageProvider);
            param.Add("@GetDAT", getDAT);
            param.Add("@LiveLeadTime", liveLeadTime);

            var result = await SqlMapper.QueryAsync<UspGetEquipmentPostingLLResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task UpdateAsync(int token, string pstatus)
        {
            var proc = "dbo.usp_UpdateEquipmentPostingStatus";
            var param = new DynamicParameters();
            param.Add("@Token", token);
            param.Add("@PStatus", pstatus);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateLeadCount(int token, int initialCount)
        {
            var proc = "dbo.usp_UpdateEquipmentPostingLeadsCount";
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

            if (result.Any())
                comment = string.Empty;

            return comment;
        }
    }
}
