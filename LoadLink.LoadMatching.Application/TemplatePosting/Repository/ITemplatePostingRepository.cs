using LoadLink.LoadMatching.Application.TemplatePosting.Models.Commands;
using LoadLink.LoadMatching.Domain.Procedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.TemplatePosting.Repository
{
    public interface ITemplatePostingRepository
    {
        Task<UspGetTemplatePostingResult> GetAsync(string custCd, int templateId);
        Task<IEnumerable<UspGetTemplatePostingResult>> GetListAsync(string custCd);
        Task<int> CreateAsync(CreateTemplatePostingSPCommand templatePosting);
        Task<int> UpdateAsync(UpdateTemplatePostingSPCommand templatePosting);
        Task DeleteAsync(int templateId, int userId);
    }
}
