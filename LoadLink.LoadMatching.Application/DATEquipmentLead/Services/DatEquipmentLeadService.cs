﻿using AutoMapper;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Models;
using LoadLink.LoadMatching.Application.DATEquipmentLead.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Application.DATEquipmentLead.Services
{
    public class DatEquipmentLeadService : IDatEquipmentLeadService
    {

        private readonly IDatEquipmentLeadRepository _datEquipmentLeadRepository;
        private readonly IMapper _mapper;


        public bool HasEQSubscription { get; set; } = false;
        public bool HasQPSubscription { get; set; } = false;
        public bool HasTCUSSubscription { get; set; } = false;
        public bool HasTCCSubscription { get; set; } = false;


        public DatEquipmentLeadService(IDatEquipmentLeadRepository datEquipmentLeadRepository, IMapper mapper)
        {
            _datEquipmentLeadRepository = datEquipmentLeadRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<GetDatEquipmentLeadQuery>> GetAsyncByPosting(string custCd, string mileageProvider, int postingId)
        {
            var result = await _datEquipmentLeadRepository.GetByPosting(custCd,mileageProvider, postingId);
            if (!result.Any())
                return null;


            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = HasEQSubscription ? row.Equifax : -1;
                    row.TCC = HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = HasTCUSSubscription ? row.TCUS : -1;
                    row.QPStatus = HasQPSubscription ? row.QPStatus : 0;
                });

            return _mapper.Map<IEnumerable<GetDatEquipmentLeadQuery>>(resultList);
        }

        public async Task<IEnumerable<GetDatEquipmentLeadQuery>> GetAsyncList(string custCd, string mileageProvider)
        {
            var result = await _datEquipmentLeadRepository.GetList(custCd, mileageProvider);
            if (!result.Any())
                return null;


            //Filter the result based on user's feature access before returning the reuslt.
            //i.e. if user has access to Equifax data send it as part of the result else hide the result.
            var resultList = result.ToList();
            resultList.ForEach(
                row => {
                    row.Equifax = HasEQSubscription ? row.Equifax : -1;
                    row.TCC = HasTCCSubscription ? row.TCC : -1;
                    row.TCUS = HasTCUSSubscription ? row.TCUS : -1;
                    row.QPStatus = HasQPSubscription ? row.QPStatus : 0;
                });

            return _mapper.Map<IEnumerable<GetDatEquipmentLeadQuery>>(resultList);
        }
    }
}