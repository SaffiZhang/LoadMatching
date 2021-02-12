using LoadLink.LoadMatching.Application.Contacted.Models.Commands;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Contacted.Services
{
    public interface IContactedService
    {
        Task UpdateAsync(UpdateContactedCommand updateContactedCommand);
    }
}
