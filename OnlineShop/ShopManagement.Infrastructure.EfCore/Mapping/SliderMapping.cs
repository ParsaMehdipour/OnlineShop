using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class SliderMapping : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.ToTable("Sliders");
            builder.HasKey(s => s.Id);


            builder.Property(s => s.Picture)
                .HasMaxLength(10000)
                .IsRequired();

            builder.Property(s => s.PictureAlt)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(s => s.PictureTitle)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(s => s.Heading)
                .HasMaxLength(225)
                .IsRequired();

            builder.Property(s => s.Title)
                .HasMaxLength(225);

            builder.Property(s => s.Text)
                .HasMaxLength(255);

            builder.Property(s => s.BtnText)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
