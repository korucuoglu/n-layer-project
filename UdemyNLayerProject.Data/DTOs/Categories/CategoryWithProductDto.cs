using System.Collections.Generic;
using UdemyNLayerProject.Data.DTOs.Products;

namespace UdemyNLayerProject.Data.DTOs.Categories
{
    public class CategoryWithProductDto : CategoryDto
    {
        public ICollection<ProductDto> Products { get; set; }

    }
}
