using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Geometries;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;


namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public interface IPostingBaseRepository 
    {

        Task<PostingDistanceAndPointId> SavePosting(PostingBase posting);
        Task<PostingDistanceAndPointId> GetDistanceAndPointId(string srceCity, string srceSt,string destCity, string destSt);
        Task<IEnumerable<PostingBase>> GetPlatformPostingForMatching(
                                                            string custCD
                                                            , DateTime dateAvail
                                                           , int vSize
                                                           , int vType
                                                           , int srceCountry
                                                           , int destCountry
                                                           , string postMode
                                                           , int networkId);
        Task<IEnumerable<PostingBase>> GetLegacyPostingForMatching(string custCD
                                                           ,DateTime dateAvail
                                                          , int vSize
                                                          , int vType
                                                          , int srceCountry
                                                          , int destCountry
                                                          , string postMode
                                                          , int networkId);
        Task<IEnumerable<PostingBase>> GetDatPostingForMatching(DateTime dateAvail
                                                        , int vSize
                                                        , int vType
                                                        , int srceCountry
                                                        , int destCountry
                                                        , string postMode
                                                        , int networkId);
        Task<IEnumerable< LeadBase>> BulkInsertLeadTable(IEnumerable<LeadBase> Leads);
        Task<IEnumerable< LeadBase>> BulkInsertDatLeadTable(IEnumerable<LeadBase> Leads);
        Task<IEnumerable< LeadBase>> BulkInsert2ndLead(IEnumerable<LeadBase> Leads);
        Task<IEnumerable<LeadBase>> GetLeadsByToken(int token);
        Task<IEnumerable<PostingBase>> GetPostingByCustCD(string custCD);
        Task Save2ndLead(LeadBase LeadBase);
        Task UpdatePostingForPlatformLeadCompleted(int token,int initialLeadsCount);
        Task UpdatePostingForDatLeadCompleted(int token, int initialLeadsCount);
        Task UpdatePostingForLegacyLeadCompleted(int token, int initialLeadsCount);
        Task UpdatePostingLeadCount(int token,int change);
        //Task UpdatePostingStatus(int token, string pstatus);
        //Task ContactLead(int userId, string custCd, int eToken, int lToken);
        //Task FlagLead(string custId, int createdBy, int eToken, int lToken);
        //Task RemoveFlag(string custId, int createdBy, int eToken, int lToken);
        //Task DeletePost(int token, int userId);
        /*ask BulkInsertLead(List<LeadBase> leads, string tableName);*/

    }
}
