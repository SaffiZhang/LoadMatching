using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft;
using MediatR;
using System.Threading;
using LoadLink.LoadMatching.Domain.AggregatesModel.BasePostingAggregate;

namespace ClassLibrary2
{
    public class TestCommandHandler : IRequestHandler<TestCommand, string>
    {
        private IEquipmentPostingRepository _equipmentPostingRepository;

        public TestCommandHandler(IEquipmentPostingRepository equipmentPostingRepository)
        {
            _equipmentPostingRepository = equipmentPostingRepository;
        }

        public async Task<string> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            var r = await _equipmentPostingRepository.GetPlatformPostingForMatching("TCORELL", DateTime.Parse("2021-9-1"), 15, 1, 1, 2, "A", 0);
            return r.FirstOrDefault().CustCD;
        }
    }
}
