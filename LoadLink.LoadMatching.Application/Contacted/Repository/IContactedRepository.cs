using LoadLink.LoadMatching.Application.Contacted.Models.Commands;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Contacted.Repository
{
    public interface IContactedRepository
    {
        Task<int> UpdateAsync(UpdateContactedCommand updateContactedCommand);
    }
}
