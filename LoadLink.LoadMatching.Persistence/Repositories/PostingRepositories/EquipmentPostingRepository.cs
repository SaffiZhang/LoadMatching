using AutoMapper;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Persistence.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment;
using System.Linq;

namespace LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories
{
    public class EquipmentPostingRepository : PostingBaseRepository, IEquipmentPostingRepository
    {
        private readonly EquipmentPostingContext _context;

        public EquipmentPostingRepository(IConnectionFactory connectionFactory, IMapper mapper, EquipmentPostingContext context) : base(connectionFactory, mapper)
        {
            Usp_GetDatPostingForMatching = "usp_GetDatLoadPostingForMatching";
            Usp_GetLegacyPostingForMatching = "usp_GetLegacyLoadPostingForMatching";
            Usp_GetPlatformPostingForMatching = "usp_GetPlatformLoadPostingForMatching";
            Usp_SavePosting = "usp_SavEquipmentPosting";

            Usp_UpdatePostingForMatchingCompleted = "Usp_UpdatEquipmentPostingForMatchingCompleted";
            Usp_UpdatePostingLeadCount = "usp_UpdateLoadPostingLeadsCount";
            _context = context;
        }

        public override async Task<IEnumerable<LeadBase>> BulkInsert2ndLead(IEnumerable<LeadBase> Leads)
        {
            var secondaryLeads = new List<LeadBase>().Cast<Domain.AggregatesModel.PostingAggregate.Load.LoadLead>().ToList();
            await _context.LoadLead.AddRangeAsync(secondaryLeads);
            _context.SaveChanges();
            return secondaryLeads;
        }

        public override async Task<IEnumerable<LeadBase>> BulkInsertDatLeadTable(IEnumerable<LeadBase> Leads)
        {
            var datLeads = new List<LeadBase>().Cast<Domain.AggregatesModel.PostingAggregate.Equipment.DatEquipmentLead>().ToList();
            await _context.DatEquipmentLead.AddRangeAsync(datLeads);
            _context.SaveChanges();
            return datLeads;
        }

        public override async Task<IEnumerable<LeadBase>> BulkInsertLeadTable(IEnumerable<LeadBase> Leads)
        {
            var equipmentLeads = new List<LeadBase>().Cast<Domain.AggregatesModel.PostingAggregate.Equipment.EquipmentLead>().ToList();
            await _context.EquipmentLead.AddRangeAsync(equipmentLeads);
            _context.SaveChanges();
            return equipmentLeads;
        }

        public override async Task<IEnumerable<LeadBase>> GetLeadsByToken(int token)
        {
            var today = DateTime.Now.Date;
            var leads = new List<LeadBase>();
            var equipmentLeads = await _context.EquipmentLead.Where(e => e.EToken == token
                                            && e.DateAvail >= today && e.MDateAvail >= today
                                                ).ToListAsync();
            leads.AddRange(equipmentLeads.ConvertAll(e => (LeadBase)e));
            var datEquipmentLeads = await _context.DatEquipmentLead.Where(e => e.EToken == token
                                            && e.DateAvail >= today && e.MDateAvail >= today
                                                ).ToListAsync();
            leads.AddRange(datEquipmentLeads.ConvertAll(e => (LeadBase)e));
            return leads;
        }

        public override async Task<IEnumerable<PostingBase>> GetPostingByCustCD(string custCD)
        {
            var today = DateTime.Now.Date;
            return await _context.EquipmentPosting.Where(p => p.CustCD == custCD
                        && p.DateAvail >= today && p.DeletedOn == null).ToListAsync();
        }
    }
}
