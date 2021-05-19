using AutoMapper;
using LoadLink.LoadMatching.Application.Common;
using LoadLink.LoadMatching.Application.LoadPosting.Models.Commands;
using LoadLink.LoadMatching.Application.LoadPosting.Models.Queries;
using LoadLink.LoadMatching.Application.LoadPosting.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.LoadPosting.Services
{
    public class LoadPostingService : ILoadPostingService
    {
        private readonly ILoadPostingRepository _loadPostingRepository;
        private readonly IMapper _mapper;

        public LoadPostingService(ILoadPostingRepository loadPostingRepository, IMapper mapper)
        {
            _loadPostingRepository = loadPostingRepository;
            _mapper = mapper;
        }


        public async Task<CreateLoadPostingCommand> CreateAsync(CreateLoadPostingCommand createCommand)
        {   
            var result = createCommand;
            var uspCreateCommand = new UspCreateLoadPostingCommand 
            {  
                CustCD = createCommand.CustCD,
                DateAvail = createCommand.DateAvail,
                SrceCity = createCommand.SrceCity,
                SrceSt = createCommand.SrceSt,
                SrceRadius = createCommand.SrceRadius,
                DestCity = createCommand.DestCity,
                DestSt = createCommand.DestSt,
                DestRadius = createCommand.DestRadius,
                VSize = CommonLM.LoadVSizeStringToNum(createCommand.VehicleSize),
                VType = CommonLM.VTypeStringToNum(string.IsNullOrEmpty(createCommand.VehicleType) ? "" : createCommand.VehicleType),
                Comment = createCommand.Comment,
                PostMode = createCommand.PostMode,
                ClientRefNum = createCommand.ClientRefNum,
                ProductName = createCommand.ProductName,
                PAttrib = CommonLM.PostingAttributeStringToNum(string.IsNullOrEmpty(createCommand.PostingAttrib) ? "" : createCommand.PostingAttrib),
                CreatedBy = createCommand.CreatedBy,
                NetworkId = createCommand.NetworkId,
                GlobalExcluded = createCommand.GlobalExcluded == true ? 1 : 0
            };

            var createResult = await _loadPostingRepository.CreateAsync(uspCreateCommand);

            if (createResult == -1)
                return null;

            //SP return a token on sucessful creation of the posting.
            result.Token = createResult;
            return result;
        }

        public async Task DeleteAsync(int token, string custCd, int userId)
        {
            await _loadPostingRepository.DeleteAsync(token, custCd, userId);
        }

        public async Task<GetLoadPostingQuery> GetAsync(int token, string custCd, string mileageProvider, int leadsCap)
        {
            var result = await _loadPostingRepository.GetAsync(token ,custCd, mileageProvider);
            if (result == null)
                return null;

            var ret = _mapper.Map<GetLoadPostingQuery>(result);

            ret.DisplayLeadsCount = (leadsCap > 0 && ret.LeadsCount >= leadsCap) ? leadsCap : ret.LeadsCount;

            return ret;
        }

        public async Task<IEnumerable<GetLoadPostingQuery>> GetListAsync(string custCd, string mileageProvider, int leadsCap, bool? getDAT = false)
        {
            var result = await _loadPostingRepository.GetListAsync(custCd, mileageProvider, getDAT);
            if (!result.Any())
                return null;

            var ret = _mapper.Map<IEnumerable<GetLoadPostingQuery>>(result);

            var resultList = ret.ToList();
            resultList.ForEach(
                row => {
                    row.DisplayLeadsCount = (leadsCap > 0 && row.LeadsCount >= leadsCap) ? leadsCap : row.LeadsCount;
                });

            return ret;
        }

        public async Task<IEnumerable<GetLoadPostingLLQuery>> GetListLLAsync(string custCd, string mileageProvider, int leadsCap, DateTime liveLeadTime, bool? getDAT = false)
        {
            var result = await _loadPostingRepository.GetListLLAsync(custCd, mileageProvider, liveLeadTime, getDAT);
            if (!result.Any())
                return null;

            var ret = _mapper.Map<IEnumerable<GetLoadPostingLLQuery>>(result);

            var resultList = ret.ToList();
            resultList.ForEach(
                row => {
                    row.DisplayLeadsCount = (leadsCap > 0 && row.LeadsCount >= leadsCap) ? leadsCap : row.LeadsCount;
                });

            return ret;
        }

        public async Task UpdateAsync(int token, string pstatus)
        {
            await _loadPostingRepository.UpdateAsync(token, pstatus);
        }

        public async Task UpdateLeadCount(int token, int initialCount)
        {
            await _loadPostingRepository.UpdateLeadCount(token, initialCount);
        }
    
    }
}
