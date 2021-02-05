﻿using AutoMapper;
using LoadLink.LoadMatching.Application.CarrierSearch.Models.Queries;
using LoadLink.LoadMatching.Application.CarrierSearch.Repository;
using LoadLink.LoadMatching.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.CarrierSearch.Services
{
    public class CarrierSearchService : ICarrierSearchService
    {
        private readonly ICarrierSearchRepository _carrierSearcRepository;
        private readonly IMapper _mapper;

        public bool hasEQSubscription { get; set; } = false;
        public bool hasTCSubscription { get; set; } = false;
        public bool hasTCUSSubscription { get; set; } = false;


        public CarrierSearchService(ICarrierSearchRepository carrierSearchRepository, IMapper mapper)
        {
            _carrierSearcRepository = carrierSearchRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCarrierSearchResult>> GetCarrierSearch(
            GetCarrierSearchRequest searchrequest,  int userId)
        {
            var searchQuery = new GetCarrierSearchQuery()
            {
                UserID = userId,
                SrceSt = searchrequest.SrceSt,
                SrceCity = searchrequest.SrceCity,
                SrceRadius = searchrequest.SrceRadius,
                DestSt = searchrequest.DestSt,
                DestCity = searchrequest.DestCity,
                DestRadius = searchrequest.DestRadius,
                VSize = CommonLM.EquipmentVSizeStringToNum(searchrequest.VehicleSize),
                VType = CommonLM.VTypeStringToNum(searchrequest.VehicleType),
                CompanyName = searchrequest.CompanyName,
                GetDat = searchrequest.GetDat == "Y" ? 1 : 0,
                GetMexico = searchrequest.GetMexico == "Y" ? 1 : 0,
                PAttrib = CommonLM.PostingAttributeStringToNum(searchrequest.PostingAttrib),
                ServerName = Environment.MachineName
            };

            var result = await _carrierSearcRepository.GetListAsync(searchQuery);
            if (!result.Any())
                return null;


            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = hasEQSubscription ? row.Equifax : -1;
                    row.TCC = hasTCSubscription ? row.TCC : -1;
                    row.TCUS = hasTCUSSubscription ? row.TCUS : -1;
                     });
           
            return _mapper.Map<IEnumerable<GetCarrierSearchResult>>(resultList);
               
        }

    }
}
