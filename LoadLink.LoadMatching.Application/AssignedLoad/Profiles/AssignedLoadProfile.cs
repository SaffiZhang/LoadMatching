
using LoadLink.LoadMatching.Application.AssignedLoad.Models.Queries;
using LoadLink.LoadMatching.Domain.Procedures;

namespace LoadLink.LoadMatching.Application.AssignedLoad.Profiles
{
    public class AssignedLoadProfile : AutoMapper.Profile
    {
        public AssignedLoadProfile()
        {
            CreateMap<UspGetAssignedLoadResult, GetAssignedLoadQuery>();
            CreateMap<UspDeleteAssignedLoadResult, DeleteAssignedLoadQuery>();
        }
    }
}
