using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).UseIdentityColumn();
            builder.Property(i => i.Name).IsRequired().HasMaxLength(200);
            builder.Property(i => i.Stock).IsRequired();
            builder.Property(i => i.Price).IsRequired().HasColumnType("decimal(18,2)"); // en fazla 18 karakter olacak ve virgülden sonra 2 basamak alacak.
            builder.Property(i => i.InnerBarcode).HasMaxLength(50);
            builder.ToTable("Products");
        }
    }
}
