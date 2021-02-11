
using LoadLink.LoadMatching.Application.Contacted.Models.Commands;
using LoadLink.LoadMatching.Application.Contacted.Repository;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.Contacted.Services
{
    public class ContactedService : IContactedService
    {
        private readonly IContactedRepository _contactedRepository;

        public ContactedService(IContactedRepository contactedRepository)
        {
            _contactedRepository = contactedRepository;
        }

        public async Task UpdateAsync(UpdateContactedCommand updateContactedCommand)
        {
            await _contactedRepository.UpdateAsync(updateContactedCommand);
        }

    }
}
