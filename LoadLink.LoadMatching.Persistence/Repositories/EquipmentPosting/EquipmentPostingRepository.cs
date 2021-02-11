using Dapper;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Commands;
using LoadLink.LoadMatching.Application.EquipmentPosting.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public async Task<int> CreateAsync(UspCreateEquipmentPostingCommand createCommand)
        {
            var proc = "dbo.usp_CreateEquipment";
            var param = new DynamicParameters(createCommand);

            param.Add("@Token", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return param.Get<int>("@Token");
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
    }
}
