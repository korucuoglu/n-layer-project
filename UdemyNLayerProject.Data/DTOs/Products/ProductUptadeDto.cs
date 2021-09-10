using System;
using System.ComponentModel.DataAnnotations;

namespace UdemyNLayerProject.Data.DTOs.Products
{
    public class ProductUptadeDto
    {
        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} alanı 1'den büyük bir değer olmalıdır.")]
        public int Stock { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "{0} alanı 1'den büyük bir değer olmalıdır.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public int CategoryId { get; set; }
    }
}
