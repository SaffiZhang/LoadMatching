using Dapper;
using LoadLink.LoadMatching.Application.TemplatePosting.Models.Commands;
using LoadLink.LoadMatching.Application.TemplatePosting.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.TemplatePosting
{
    public class TemplatePostingRepository : ITemplatePostingRepository
    {
        private readonly IDbConnection _dbConnection;

        public TemplatePostingRepository(IConnectionFactory connectionFactory)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
        }

        public async Task<int> CreateAsync(TemplatePostingCommand templatePosting)
        {
            var proc = "dbo.usp_CreateTemplatePosting";
            var param = new DynamicParameters();

            param.Add("@TemplateName", templatePosting.TemplateName);
            param.Add("@PostType", templatePosting.PostType);
            param.Add("@UserId", templatePosting.UserId);
            param.Add("@DateAvail", templatePosting.DateAvail);
            param.Add("@SrceId", templatePosting.SrceID);
            param.Add("@SrceCity", templatePosting.SrceCity);
            param.Add("@SrceSt", templatePosting.SrceSt);
            param.Add("@SrceRadius", templatePosting.SrceRadius);
            param.Add("@DestId", templatePosting.DestID);
            param.Add("@DestCity", templatePosting.DestCity);
            param.Add("@DestSt", templatePosting.DestSt);
            param.Add("@DestRadius", templatePosting.DestRadius);
            param.Add("@VSize", templatePosting.VehicleSizeConverted);
            param.Add("@VType", templatePosting.VehicleTypeConverted);
            param.Add("@Comment", templatePosting.Comment);
            param.Add("@PostMode", templatePosting.PostMode);
            param.Add("@ClientRefNum", templatePosting.ClientRefNum);
            param.Add("@PAttrib", templatePosting.PostingAttribConverted);
            param.Add("@NetworkId", templatePosting.NetworkId);
            param.Add("@Corridor", templatePosting.Corridor);
            param.Add("@CustCd", templatePosting.CustCd);
            param.Add("@CustomerTracking", templatePosting.CustomerTracking);

            param.Add("@TemplateId", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return param.Get<int>("@TemplateId");
        }

        public async Task DeleteAsync(int templateId, int userId)
        {
            var proc = "dbo.usp_DeleteTemplatePosting";

            var param = new DynamicParameters();

            param.Add("@TemplateId", templateId);
            param.Add("@UserId", userId);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);
        }

        public async Task<UspGetTemplatePostingResult> GetAsync(string custCd, int templateId)
        {
            var proc = "dbo.usp_GetTemplatePosting";

            var param = new DynamicParameters();
            param.Add("@CustCd", custCd);
            param.Add("@TemplateId", templateId);

            var result = await SqlMapper.QueryFirstOrDefaultAsync<UspGetTemplatePostingResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetTemplatePostingResult>> GetListAsync(string custCd)
        {
            var proc = "dbo.usp_GetTemplatePosting";

            var param = new DynamicParameters();
            param.Add("@CustCd", custCd);

            var result = await SqlMapper.QueryAsync<UspGetTemplatePostingResult>(
                _dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<int> UpdateAsync(TemplatePostingCommand templatePosting)
        {
            var proc = "dbo.usp_UpdateTemplatePosting";
            var param = new DynamicParameters();

            param.Add("@TemplateId", templatePosting.TemplateID);
            param.Add("@TemplateName", templatePosting.TemplateName);
            param.Add("@PostType", templatePosting.PostType);
            param.Add("@UserId", templatePosting.UserId);
            param.Add("@DateAvail", templatePosting.DateAvail);
            param.Add("@SrceId", templatePosting.SrceID);
            param.Add("@SrceCity", templatePosting.SrceCity);
            param.Add("@SrceSt", templatePosting.SrceSt);
            param.Add("@SrceRadius", templatePosting.SrceRadius);
            param.Add("@DestId", templatePosting.DestID);
            param.Add("@DestCity", templatePosting.DestCity);
            param.Add("@DestSt", templatePosting.DestSt);
            param.Add("@DestRadius", templatePosting.DestRadius);
            param.Add("@VSize", templatePosting.VehicleSizeConverted);
            param.Add("@VType", templatePosting.VehicleTypeConverted);
            param.Add("@Comment", templatePosting.Comment);
            param.Add("@PostMode", templatePosting.PostMode);
            param.Add("@ClientRefNum", templatePosting.ClientRefNum);
            param.Add("@PAttrib", templatePosting.PostingAttribConverted);
            param.Add("@NetworkId", templatePosting.NetworkId);
            param.Add("@Corridor", templatePosting.Corridor);
            param.Add("@CustCd", templatePosting.CustCd);
            param.Add("@CustomerTracking", templatePosting.CustomerTracking);

            param.Add("@ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await SqlMapper.ExecuteAsync(_dbConnection, sql: proc, param: param, commandType: CommandType.StoredProcedure);

            return param.Get<int>("@ReturnValue");
        }
    }
}
