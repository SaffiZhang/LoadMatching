using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories.EntityConfigurations
{
    public class DatEquipmentLeadEntityConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.PostingAggregate.Equipment.DatEquipmentLead>
    {
        public void Configure(EntityTypeBuilder<DatEquipmentLead> builder)
        {
            builder.HasKey(l => l.ID);
            builder.Ignore(l => l.DomainEvents);
      
        }
    }
}
