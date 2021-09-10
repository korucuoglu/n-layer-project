using AutoMapper;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Data.DTOs.Categories;
using UdemyNLayerProject.Data.DTOs.Products;

namespace UdemyNLayerProject.API.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            #region Categories
           
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Category, CategoryWithProductDto>();
            CreateMap<CategoryWithProductDto, Category>();

            CreateMap<Category, CategoryUptadeDto>();
            CreateMap<CategoryUptadeDto, Category>();

            #endregion

            #region Products
            
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<ProductWithCategoryDto, Product>();

            CreateMap<Product, ProductUptadeDto>();
            CreateMap<ProductUptadeDto, Product>();

            #endregion
        }

    }
}
