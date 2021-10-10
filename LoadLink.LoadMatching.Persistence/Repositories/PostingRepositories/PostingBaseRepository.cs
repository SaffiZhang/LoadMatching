using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;
using LoadLink.LoadMatching.Persistence.Data;
using AutoMapper;
using LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories.Models;
using LoadLink.LoadMatching.Persistence.Utilities;
using System.Linq;


namespace LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories
{
    public abstract class PostingBaseRepository : IPostingBaseRepository
    {
        private readonly string _dbConnectionStr;
        private readonly IMapper _mapper;
        private readonly ILeadCaching _leadCaching;
        public PostingBaseRepository(IConnectionFactory connectionFactory,IMapper mapper 
            //ILeadCaching leadCaching
            )
        {
            _dbConnectionStr = connectionFactory.ConnectionString;
            Usp_GetDistanceAndPointId = "Usp_GetDistanceAndPointId";
            _mapper = mapper;
            //_leadCaching = leadCaching;
        }

        protected string Usp_GetDatPostingForMatching { get; set; }
        protected string Usp_GetLegacyPostingForMatching { get; set; }
        protected string Usp_GetPlatformPostingForMatching { get; set; }
        protected string Usp_GetDistanceAndPointId { get; set; }
        protected string Usp_SavePosting { get; set; }

        protected string Usp_UpdatePostingForMatchingCompleted { get; set; }
        protected string Usp_UpdatePostingLeadCount { get; set; }

        public async Task<IEnumerable<PostingBase>> GetDatPostingForMatching(DateTime dateAvail, int vSize, int vType, int srceCountry, int destCountry, string postMode, int networkId)
        {

            var connection = new SqlConnection(_dbConnectionStr);
            return await SqlMapper.QueryAsync<PostingBase>(connection,
                                                        sql: this.Usp_GetDatPostingForMatching,
                                                        param: DynamicParametersForQueryMatchingDatPosting(dateAvail, vSize, vType, srceCountry, destCountry, postMode, networkId),
                                                        commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<PostingBase>> GetLegacyPostingForMatching(string custCD, DateTime dateAvail, int vSize, int vType, int srceCountry, int destCountry, string postMode, int networkId)
        {
            var connection = new SqlConnection(_dbConnectionStr);
            return await SqlMapper.QueryAsync<PostingBase>(connection,
                                                        sql: this.Usp_GetLegacyPostingForMatching,
                                                        param: DynamicParametersForQueryMatchingLinkPosting(custCD, dateAvail, vSize, vType, srceCountry, destCountry, postMode, networkId),
                                                        commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PostingBase>> GetPlatformPostingForMatching(string custCD, DateTime dateAvail, int vSize, int vType, int srceCountry, int destCountry, string postMode, int networkId)
        {
            var connection = new SqlConnection(_dbConnectionStr);
           

            return await SqlMapper.QueryAsync<PostingBase>(connection,
                                                            sql: this.Usp_GetPlatformPostingForMatching,
                                                            param: DynamicParametersForQueryMatchingLinkPosting(custCD, dateAvail, vSize, vType, srceCountry, destCountry, postMode, networkId),
                                                            commandType: CommandType.StoredProcedure);
           


        }
        public async Task<PostingDistanceAndPointId> GetDistanceAndPointId(string srceCity, string srceSt, string destCity, string destSt)
        {
            var connection = new SqlConnection(_dbConnectionStr);
            
            return await SqlMapper.QueryFirstAsync<PostingDistanceAndPointId>(connection,
                                                        sql: this.Usp_GetDistanceAndPointId,
                                                        param: DynamicParametersForQueryDistanceAndPointId(srceCity, srceSt, destCity, destSt),
                                                        commandType: CommandType.StoredProcedure);
            

        }
        public async Task<PostingDistanceAndPointId> SavePosting(PostingBase posting)
        {
            var postingPara= _mapper.Map<UspSavePostingParameters>(posting);
            var param = new DynamicParameters(postingPara);
            var connection = new SqlConnection(_dbConnectionStr);
            try
            {
                return await SqlMapper.QueryFirstAsync<PostingDistanceAndPointId>(connection,
                                                       sql: this.Usp_SavePosting,
                                                       param: param,
                                                       commandType: CommandType.StoredProcedure);

            }
            catch(Exception ex)
            {
                throw new Exception("Exception in SavePosting: " + ex.Message);
            }


        }
    
        public async Task UpdatePostingForPlatformLeadCompleted(int token, int initialLeadsCount)
        {

            var connection = new SqlConnection(_dbConnectionStr);
            await SqlMapper.ExecuteAsync(connection,
                                                        sql: this.Usp_UpdatePostingForMatchingCompleted,
                                                        param: DynamicParametersForLeadCompleted(token, initialLeadsCount, MatchingType.Platform.ToString()),
                                                        commandType: CommandType.StoredProcedure);
        }
        public async Task UpdatePostingForDatLeadCompleted(int token, int initialLeadsCount)
        {
            var connection = new SqlConnection(_dbConnectionStr);
            await SqlMapper.ExecuteAsync(connection,
                                                        sql: this.Usp_UpdatePostingForMatchingCompleted,
                                                        param: DynamicParametersForLeadCompleted(token, initialLeadsCount, MatchingType.Dat.ToString()),
                                                        commandType: CommandType.StoredProcedure);
        }

        public async Task UpdatePostingForLegacyLeadCompleted(int token, int initialLeadsCount)
        {
            var connection = new SqlConnection(_dbConnectionStr);
            await SqlMapper.ExecuteAsync(connection,
                                                        sql: this.Usp_UpdatePostingForMatchingCompleted,
                                                        param: DynamicParametersForLeadCompleted(token, initialLeadsCount, MatchingType.Legacy.ToString()),
                                                        commandType: CommandType.StoredProcedure);
        }


        public async Task UpdatePostingLeadCount(int token, int changes)
        {

            var connection = new SqlConnection(_dbConnectionStr);
            await SqlMapper.ExecuteAsync(connection,
                                                        sql: this.Usp_UpdatePostingLeadCount,
                                                        param: new DynamicParameters(new { token = token, changes = changes }),
                                                        commandType: CommandType.StoredProcedure);
        }
        private DynamicParameters DynamicParametersForQueryMatchingLinkPosting(string custCD, DateTime dateAvail, int vSize, int vType, int srceCountry, int destCountry, string postMode, int networkId)
        {
            var input = new
            {
                CustCD = custCD,
                DateAvail = dateAvail,
                VSize = vSize,
                VType = vType,
                SrceCountry = srceCountry,
                destCountry = destCountry,
                PostMode = postMode,
                NetworkId = networkId
            };
            return new DynamicParameters(input);
        }
        private DynamicParameters DynamicParametersForQueryMatchingDatPosting(DateTime dateAvail, int vSize, int vType, int srceCountry, int destCountry, string postMode, int networkId)
        {
            var input = new { DateAvail = dateAvail, vSize = vSize, vType = vType, destCountry = destCountry, postMode = postMode, networkId = networkId };
            return new DynamicParameters(input);
        }
        private DynamicParameters DynamicParametersForQueryDistanceAndPointId(string srceCity, string srceSt, string destCity, string destSt)
        {
            var input = new { srceCity = srceCity, srceSt = srceSt, destCity = destCity, destSt = destSt };
            return new DynamicParameters(input);
        }
        private DynamicParameters DynamicParametersForLeadCompleted(int token, int initialLeadsCount, string matchingType)
        {
            var input = new { token = token, initialLeadsCount = initialLeadsCount, matchingType = matchingType };
            return new DynamicParameters(input);
        }





        public abstract Task<IEnumerable<LeadBase>> BulkInsertLeadTable(IEnumerable<LeadBase> Leads);


        public abstract Task<IEnumerable<LeadBase>> BulkInsertDatLeadTable(IEnumerable<LeadBase> Leads);

        public abstract Task<IEnumerable<LeadBase>> BulkInsert2ndLead(IEnumerable<LeadBase> Leads);
       

        public Task Save2ndLead(LeadBase LeadBase)
        {
            throw new NotImplementedException();
        }

        public abstract Task<IEnumerable<LeadBase>> GetLeadsByToken(int token);
        public abstract Task<IEnumerable<PostingBase>> GetPostingByCustCD(string custCD);
        
    }
}
