using System.ComponentModel.DataAnnotations;

namespace UdemyNLayerProject.Data.DTOs.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public string Name { get; set; }
    }
}
