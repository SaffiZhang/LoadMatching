using LoadLink.LoadMatching.Application.TemplatePosting.Models.Commands;
using LoadLink.LoadMatching.Application.TemplatePosting.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.TemplatePosting.Profiles
{
    public class TemplatePostingProfile : AutoMapper.Profile
    {
        public TemplatePostingProfile()
        {
            CreateMap<UspGetTemplatePostingResult, GetTemplatePostingQuery>();
            CreateMap<TemplatePostingCommand, UspGetTemplatePostingResult>();
        }
    }
}
