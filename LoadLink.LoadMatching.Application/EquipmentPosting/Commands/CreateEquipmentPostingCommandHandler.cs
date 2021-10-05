using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;
using System.Linq;
using LoadLink.LoadMatching.IntegrationEventManager;
using LoadLink.LoadMatching.RabbitMQIntegrationEventManager;
using RabbitMQ.Client;
using System.Text.Json;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models;
using LoadLink.LoadMatching.Application.EquipmentPosting.IntetrationEvents;


namespace LoadLink.LoadMatching.Application.EquipmentPosting.Commands
{
    public class CreateEquipmentPostingCommandHandler : IRequestHandler<CreateEquipmentPostingCommand, int?>
    {
        private readonly IEquipmentPostingRepository _equipmentPostingRespository;

       
        
        private readonly MqConfig _mqConfig;
        private readonly IPublishIntegrationEvent _publishIntegrationEvent;
        

        public CreateEquipmentPostingCommandHandler(IEquipmentPostingRepository equipmentPostingRespository, MqConfig mqConfig, IPublishIntegrationEvent publishIntegrationEvent)
        {
            _equipmentPostingRespository = equipmentPostingRespository;
            _mqConfig = mqConfig;
            _publishIntegrationEvent = publishIntegrationEvent;
        }

        public async Task<int?> Handle(CreateEquipmentPostingCommand request, CancellationToken cancellationToken)
        {
            var vSize = CommonLM.EquipmentVSizeStringToNum(request.VehicleSize);
            if (vSize == 0)
                return null;
            var vType = CommonLM.VTypeStringToNum(request.VehicleType);
            if (vType == 0)
                return null;
            int pAttrib;
            if (request.PostingAttrib != "")
            {
                pAttrib = CommonLM.PostingAttributeStringToNum(request.PostingAttrib);
                if (pAttrib == 0)
                    return null;
            }
            else 
                pAttrib = 0;


            var posting = new PostingBase(
                                          request.CustCD,
                                          request.DateAvail,

                                          request.SrceCity,
                                          request.SrceSt,
                                          request.SrceRadius,
                                          
                                          request.DestCity,
                                          request.DestSt,
                                          request.DestRadius,
                                          
                                          vSize,
                                          vType,
                                          request.VehicleSize,
                                          request.VehicleType,
                                          
                                          pAttrib,
                                          request.PostingAttrib,
                                          
                                          request.Comment,
                                          request.PostMode,
                                          request.ClientRefNum,
                                          request.ProductName,
                                        
                                          request.NetworkId,
                                          request.Corridor,
                                       
                                          request.CustomerTracking,
                                          request.CreatedBy);

            var resultFromDB = await _equipmentPostingRespository.SavePosting(posting);
            posting.UpdateDistanceAndPointId(resultFromDB);
            
            _publishIntegrationEvent.Publish(new PostingCreatedEvent(posting, request.GlobalExcluded), LoadMatchingQue.PostingCreated.ToString());

            
            
            return resultFromDB.Token;
            
        }
        


        }
        
        
    }

