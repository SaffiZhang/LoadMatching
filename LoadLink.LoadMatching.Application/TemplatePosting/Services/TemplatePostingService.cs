using AutoMapper;
using LoadLink.LoadMatching.Application.TemplatePosting.Models.Commands;
using LoadLink.LoadMatching.Application.TemplatePosting.Models.Queries;
using LoadLink.LoadMatching.Application.TemplatePosting.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.TemplatePosting.Services
{
    public class TemplatePostingService : ITemplatePostingService
    {
        private readonly ITemplatePostingRepository _TemplatePostingRepository;
        private readonly IMapper _mapper;

        public TemplatePostingService(ITemplatePostingRepository TemplatePostingRepository, IMapper mapper)
        {
            _TemplatePostingRepository = TemplatePostingRepository;
            _mapper = mapper;
        }

        public async Task<TemplatePostingCommand> CreateAsync(TemplatePostingCommand templatePosting)
        {
            templatePosting.TemplateID = await _TemplatePostingRepository.CreateAsync(templatePosting);

            return templatePosting;
        }

        public async Task DeleteAsync(int templateId, int userId)
        {
            await _TemplatePostingRepository.DeleteAsync(templateId, userId);
        }

        public async Task<GetTemplatePostingQuery> GetAsync(string custCd, int templateId)
        {
            var result = await _TemplatePostingRepository.GetAsync(custCd, templateId);

            if (result == null)
                return null;

            return _mapper.Map<GetTemplatePostingQuery>(result);
        }

        public async Task<IEnumerable<GetTemplatePostingQuery>> GetListAsync(string custCd)
        {
            var result = await _TemplatePostingRepository.GetListAsync(custCd);
            if (!result.Any())
                return null;

            return _mapper.Map<IEnumerable<GetTemplatePostingQuery>>(result);
        }

        public async Task<TemplatePostingCommand> UpdateAsync(TemplatePostingCommand templatePosting)
        {
            templatePosting.TemplateID = await _TemplatePostingRepository.UpdateAsync(templatePosting);

            return templatePosting;
        }
    }
}
