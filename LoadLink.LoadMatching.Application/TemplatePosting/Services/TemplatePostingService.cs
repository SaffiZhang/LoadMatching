using AutoMapper;
using LoadLink.LoadMatching.Application.TemplatePosting.Models.Commands;
using LoadLink.LoadMatching.Application.TemplatePosting.Models.Queries;
using LoadLink.LoadMatching.Application.TemplatePosting.Repository;
using LoadLink.LoadMatching.Application.Common;
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

        public async Task<CreateTemplatePostingCommand> CreateAsync(CreateTemplatePostingCommand templatePosting)
        {
            var tmpltPostingCmd = new CreateTemplatePostingSPCommand()
            {

                UserId = templatePosting.UserId,
                TemplateID = templatePosting.TemplateID,
                TemplateName = string.IsNullOrEmpty(templatePosting.TemplateName) ? "" : templatePosting.TemplateName,
                PostType = templatePosting.PostType,
                DateAvail = templatePosting.DateAvail,
                SrceID = templatePosting.SrceID,
                SrceCity = templatePosting.SrceCity,
                SrceSt = templatePosting.SrceSt,
                SrceRadius = templatePosting.SrceRadius,
                DestID = templatePosting.DestID,
                DestCity = templatePosting.DestCity,
                DestSt = templatePosting.DestSt,
                DestRadius = templatePosting.DestRadius,
                VSize = templatePosting.PostType == "E" ? CommonLM.EquipmentVSizeStringToNum(templatePosting.VehicleSize) : CommonLM.LoadVSizeStringToNum(templatePosting.VehicleSize),
                VType = CommonLM.VTypeStringToNum(string.IsNullOrEmpty(templatePosting.VehicleType) ? "" : templatePosting.VehicleType),
                PAttrib = CommonLM.PostingAttributeStringToNum(string.IsNullOrEmpty(templatePosting.PostingAttrib) ? "" : templatePosting.PostingAttrib),
                Comment = templatePosting.Comment,
                PostMode = templatePosting.PostMode,
                ClientRefNum = string.IsNullOrEmpty(templatePosting.ClientRefNum) ? "" : templatePosting.ClientRefNum,
                CustCd = templatePosting.CustCd,
                Corridor = templatePosting.Corridor,
                CustomerTracking = templatePosting.CustomerTracking,
                NetworkId = templatePosting.NetworkId
            };
            templatePosting.TemplateID = await _TemplatePostingRepository.CreateAsync(tmpltPostingCmd);

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

        public async Task<UpdateTemplatePostingCommand> UpdateAsync(UpdateTemplatePostingCommand templatePosting)
        {
            var tmpltPostingCmd = new UpdateTemplatePostingSPCommand()
            {

                UserId = templatePosting.UserId,
                TemplateID = templatePosting.TemplateID,
                TemplateName = templatePosting.TemplateName,
                PostType = templatePosting.PostType,
                DateAvail = templatePosting.DateAvail,
                SrceID = templatePosting.SrceID,
                SrceCity = templatePosting.SrceCity,
                SrceSt = templatePosting.SrceSt,
                SrceRadius = templatePosting.SrceRadius,
                DestID = templatePosting.DestID,
                DestCity = templatePosting.DestCity,
                DestSt = templatePosting.DestSt,
                DestRadius = templatePosting.DestRadius,
                VSize = templatePosting.PostType == "E" ? CommonLM.EquipmentVSizeStringToNum(templatePosting.VehicleSize) : CommonLM.LoadVSizeStringToNum(templatePosting.VehicleSize),
                VType = CommonLM.VTypeStringToNum(string.IsNullOrEmpty(templatePosting.VehicleType) ? "" : templatePosting.VehicleType),
                PAttrib = CommonLM.PostingAttributeStringToNum(string.IsNullOrEmpty(templatePosting.PostingAttrib) ? "" : templatePosting.PostingAttrib),
                Comment = templatePosting.Comment,
                PostMode = templatePosting.PostMode,
                ClientRefNum = string.IsNullOrEmpty(templatePosting.ClientRefNum) ? "" : templatePosting.ClientRefNum,
                CustCd = templatePosting.CustCd,
                Corridor = templatePosting.Corridor,
                CustomerTracking = templatePosting.CustomerTracking,
                NetworkId = templatePosting.NetworkId
            };
           
            templatePosting.TemplateID = await _TemplatePostingRepository.UpdateAsync(tmpltPostingCmd);

            return templatePosting;
        }
    }
}
