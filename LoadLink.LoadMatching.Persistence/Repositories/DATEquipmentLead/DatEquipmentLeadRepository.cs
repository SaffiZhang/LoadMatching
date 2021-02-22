using Dapper;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Repository;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Persistence.Configuration;
using LoadLink.LoadMatching.Persistence.Data;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories.DATEquipmentLead
{
    public class DatEquipmentLeadRepository : IDatEquipmentLeadRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly AppSettings _settings;

        public DatEquipmentLeadRepository(IConnectionFactory connectionFactory,
                                            IOptions<AppSettings> settings)
        {
            _dbConnection = new SqlConnection(connectionFactory.ConnectionString);
            _settings = settings.Value;
        }
        public async Task<IEnumerable<UspGetDatEquipmentLeadResult>> GetByPosting(string custCd, int postingId)
        {
            var proc = "usp_GetDATEquipmentLead";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", _settings.MileageProvider);
            param.Add("@EToken", postingId);

            var result = await SqlMapper.QueryAsync<UspGetDatEquipmentLeadResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<IEnumerable<UspGetDatEquipmentLeadResult>> GetList(string custCd)
        {
            var proc = "usp_GetDATEquipmentLead";
            var param = new DynamicParameters();
            param.Add("@CustCD", custCd);
            param.Add("@MileageProvider", _settings.MileageProvider);

            var result = await SqlMapper.QueryAsync<UspGetDatEquipmentLeadResult>(
               _dbConnection, sql: proc, param, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
