using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Geometries;


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
        Task SavePlatformLead(LeadBase LeadBase);
        Task SaveLegacyLead(LeadBase LeadBase);
        Task SaveDatLead(LeadBase LeadBase);
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


    }
}
