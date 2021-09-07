using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Data.Seeds
{
    class ProductSeed : IEntityTypeConfiguration<Product>
    {
        private readonly int[] _CategoryIds;
        public ProductSeed(int[] ids)
        {
            _CategoryIds = ids; // burada elimizde CategoryId'ler olacak. 
        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
                (
                new Product { Id = 1, Name = "Pilot Kalem", Price = 12.50m, Stock = 100, CategoryId = _CategoryIds[0] },
                new Product { Id = 2, Name = "Kurşun Kalem", Price = 40.50m, Stock = 200, CategoryId = _CategoryIds[0] },
                new Product { Id = 3, Name = "Tükenmez Kalem", Price = 12.50m, Stock = 500, CategoryId = _CategoryIds[0] },
                new Product { Id = 4, Name = "Küçük Boy Defter", Price = 12.50m, Stock = 500, CategoryId = _CategoryIds[1] },
                new Product { Id = 5, Name = "Orta Boy Defter", Price = 12.50m, Stock = 500, CategoryId = _CategoryIds[1] },
                new Product { Id = 6, Name = "Büyük Boy Defter", Price = 12.50m, Stock = 500, CategoryId = _CategoryIds[1] }

                );
        }
    }
}
