using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;

namespace LoadLink.LoadMatching.Application.EquipmentPosting.Models
{
    public class EquipmentPostingProfile : AutoMapper.Profile
    {
      
        public EquipmentPostingProfile()
        {
            CreateMap<PostingBase, UspSavePostingParameters>();

            CreateMap<LeadBase, UspSaveLeadParameters>()
                .ForMember(d => d.DestId, opt => opt.MapFrom(s => s.GetDestId()))
                .ForMember(d => d.SrceId, opt => opt.MapFrom(s => s.GetSrceId()));
             
                
               
        }
    }
}
