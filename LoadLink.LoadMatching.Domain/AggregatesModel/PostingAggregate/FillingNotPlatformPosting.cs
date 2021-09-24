using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate
{
    public class FillingNotPlatformPosting : IFillNotPlatformPosting
    {
        private IEquipmentPostingRepository _equipmentPostingRepository;
        public FillingNotPlatformPosting(IEquipmentPostingRepository equipmentPostingRepository)
        {
            _equipmentPostingRepository = equipmentPostingRepository;
        }

        public async Task<PostingBase> Fill(PostingBase posting)
        {
            posting.UpdateDistanceAndPointId(await _equipmentPostingRepository.GetDistanceAndPointId(posting.SrceCity,
                                                                                                posting.SrceSt,
                                                                                                posting.DestCity,
                                                                                                posting.DestSt));
            posting.PAttribStr = CommonLM.PostingAttributeNumToString((int)posting.PAttrib);
            return posting;
        }
    }
}
