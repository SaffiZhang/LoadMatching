using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Persistence.Data;

namespace LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories
{
    public class EquipmentPostingRepository : PostingBaseRepository, IEquipmentPostingRepository
    {
        public EquipmentPostingRepository(IConnectionFactory connectionFactory, IMapper mapper) : base(connectionFactory, mapper)
        {
            Usp_GetDatPostingForMatching = "usp_GetDatLoadPostingForMatching";
            Usp_GetLegacyPostingForMatching = "usp_GetLegacyLoadPostingForMatching";
            Usp_GetPlatformPostingForMatching = "usp_GetPlatformLoadPostingForMatching";
            Usp_SavePosting = "usp_SaveEquipmentPosting";
            Usp_SaveLinkLead = "usp_SaveLinkEquipmentLead";
            Usp_Save2ndLead = "usp_Save2ndLoadLead";
            Usp_SaveDatLead = "usp_SaveDatEquipmentLead";
            Usp_UpdatePostingForMatchingCompleted = "Usp_UpdateEquipmentPostingForMatchingCompleted";
            Usp_UpdatePostingLeadCount = "usp_UpdateLoadPostingLeadsCount";
        }
    }
}
