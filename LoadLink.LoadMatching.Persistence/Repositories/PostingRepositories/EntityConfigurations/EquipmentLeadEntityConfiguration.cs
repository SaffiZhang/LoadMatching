using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate;

namespace LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories.EntityConfigurations
{
    public class EquipmentLeadEntityConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.PostingAggregate.Equipment.EquipmentLead>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.PostingAggregate.Equipment.EquipmentLead> builder)
        {
            builder.HasKey(l => l.ID);
            builder.Ignore(l => l.DomainEvents);
           
        }
    }
}
