using System;
using System.ComponentModel.DataAnnotations;
using SPEDU.Domain.BaseModels;

namespace SPEDU.Domain.Models.Application
{
    public class AppApplication : BaseModel
    {
        [Key]
        public Int32 AppApplicationId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(128)]
        public String Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(400)]
        public String Description { get; set; }

        [Display(Name = "Version")]
        [Required(ErrorMessage = "Version is required")]
        [MaxLength(100)]
        public String Version { get; set; }
    }
}
