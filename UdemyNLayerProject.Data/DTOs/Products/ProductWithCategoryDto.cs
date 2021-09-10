
using UdemyNLayerProject.Data.DTOs.Categories;

namespace UdemyNLayerProject.Data.DTOs.Products
{
    public class ProductWithCategoryDto : ProductDto
    {
        public CategoryDto Category { get; set; }

    }
}
