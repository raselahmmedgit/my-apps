using System;
using SPEDU.Domain.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace SPEDU.Domain.Models.Application
{
    public class AppDefaultSetting : BaseModel
    {
        [Key]
        public Int32 AppDefaultSettingId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(128)]
        public String Name { get; set; }

        [Display(Name = "Key")]
        [Required(ErrorMessage = "Key is required")]
        [MaxLength(128)]
        public String Key { get; set; }

        [Display(Name = "Value")]
        [Required(ErrorMessage = "Value is required")]
        [MaxLength(256)]
        public String Value { get; set; }

        [Display(Name = "Description")]
        [MaxLength(400)]
        public String Description { get; set; }
    }
}
