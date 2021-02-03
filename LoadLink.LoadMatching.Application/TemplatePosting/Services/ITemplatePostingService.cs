using LoadLink.LoadMatching.Application.TemplatePosting.Models.Commands;
using LoadLink.LoadMatching.Application.TemplatePosting.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace LoadLink.LoadMatching.Application.TemplatePosting.Services
{
    public interface ITemplatePostingService
    {
        Task<GetTemplatePostingQuery> GetAsync(string custCd, int templateId);
        Task<IEnumerable<GetTemplatePostingQuery>> GetListAsync(string custCd);
        Task<TemplatePostingCommand> CreateAsync(TemplatePostingCommand templatePosting);
        Task<TemplatePostingCommand> UpdateAsync(TemplatePostingCommand templatePosting);
        Task DeleteAsync(int templateId, int userId);
    }
}
