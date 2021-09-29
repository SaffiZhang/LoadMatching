using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Matchings;
using System.Linq;
using System.ComponentModel;
using RabbitMQ.Client;
using System.Text.Json;
using LoadLink.LoadMatching.Application.EquipmentPosting.Models;


namespace LoadLink.LoadMatching.Application.EquipmentPosting.Commands
{
    public class CreateEquipmentPostingCommandHandler : IRequestHandler<CreateEquipmentPostingCommand, int?>
    {
        private readonly IEquipmentPostingRepository _equipmentPostingRespository;
       

        private readonly string queueName="matchingQue";

        public CreateEquipmentPostingCommandHandler(IEquipmentPostingRepository equipmentPostingRespository)
        {
            _equipmentPostingRespository = equipmentPostingRespository;
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

            var isGlobalExcluded = request.GlobalExcluded ?? false;

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

            SendToBackGround(new MatchingPara(posting, request.GlobalExcluded));
            
            return resultFromDB.Token;
            
        }
        private void SendToBackGround(MatchingPara para)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" ,DispatchConsumersAsync=true };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                
                string message = JsonSerializer.Serialize(para);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties:properties,
                                     body: body);
                
            }


        }
        
        
    }
}
