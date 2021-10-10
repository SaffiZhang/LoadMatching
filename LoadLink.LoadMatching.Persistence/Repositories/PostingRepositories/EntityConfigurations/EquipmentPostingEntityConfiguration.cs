using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories.EntityConfigurations
{
    public class EquipmentPostingEntityConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.PostingAggregate.Equipment.EquipmentPosting>

    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.PostingAggregate.Equipment.EquipmentPosting> builder)
        {
            builder.HasKey(l => l.Token);
            builder.Ignore(l => l.DomainEvents);
            builder.Ignore(l => l.SecondaryLeads);
            
        }
    }
}
