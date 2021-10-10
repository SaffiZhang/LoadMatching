using LoadLink.LoadMatching.Domain.AggregatesModel.PostingAggregate.Equipment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoadLink.LoadMatching.Persistence.Repositories.PostingRepositories.EntityConfigurations
{
    public class LoadLeadEntityConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.PostingAggregate.Load.LoadLead>

    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.PostingAggregate.Load.LoadLead> builder)
        {
            builder.HasKey(l => l.ID);
            builder.Ignore(l => l.DomainEvents);
          
            
        }
    }
}
