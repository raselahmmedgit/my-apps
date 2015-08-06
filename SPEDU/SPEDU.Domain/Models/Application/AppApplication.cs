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
        [Required]
        [MaxLength(128)]
        public String Name { get; set; }

        [Display(Name = "Description")]
        [Required]
        [MaxLength(400)]
        public String Description { get; set; }
    }
}
