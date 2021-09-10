using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyNLayerProject.Data.DTOs.Categories
{
    public class CategoryUptadeDto
    {
        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public string Name { get; set; }
    }
}
