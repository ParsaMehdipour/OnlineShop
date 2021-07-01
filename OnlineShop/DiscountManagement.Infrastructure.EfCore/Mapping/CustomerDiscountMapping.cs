using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EfCore.Mapping
{
    public class CustomerDiscountMapping : IEntityTypeConfiguration<CustomerDiscount>
    {
        public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
        {
            builder.ToTable("CustomerDiscounts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DiscountRate);

            builder.Property(x => x.Reason);

            builder.Property(x => x.StartDate);

            builder.Property(x => x.EndDate);

        }
    }
}
