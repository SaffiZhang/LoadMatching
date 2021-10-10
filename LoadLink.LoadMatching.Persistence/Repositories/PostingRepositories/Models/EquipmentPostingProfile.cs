using LoadLink.LoadMatching.Application.EquipmentPosting.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;


namespace LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories.Models
{
    public class EquipmentPostingProfile : AutoMapper.Profile
    {
      
        public EquipmentPostingProfile()
        {
            CreateMap<PostingBase, UspSavePostingParameters>();

            
             
                
               
        }
    }
}
