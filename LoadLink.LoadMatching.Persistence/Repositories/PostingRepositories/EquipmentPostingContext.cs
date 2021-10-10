using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories.EntityConfigurations;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Load;

namespace LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories
{
    public class EquipmentPostingContext : DbContext
    {
        public EquipmentPostingContext(DbContextOptions<EquipmentPostingContext> options) : base(options)
        {
        }
        public DbSet<Domain.AggregatesModel.PostingAggregate.Equipment.EquipmentPosting> EquipmentPosting { get; set; }
        public DbSet<Domain.AggregatesModel.PostingAggregate.Equipment.EquipmentLead> EquipmentLead { get; set; }
        public DbSet<Domain.AggregatesModel.PostingAggregate.Equipment.DatEquipmentLead> DatEquipmentLead { get; set; }
        public DbSet<Domain.AggregatesModel.PostingAggregate.Load.LoadLead> LoadLead { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EquipmentLeadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DatEquipmentLeadEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LoadLeadEntityConfiguration());
        }
    }
}
