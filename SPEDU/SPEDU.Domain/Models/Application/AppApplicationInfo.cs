using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SPEDU.Domain.BaseModels;

namespace SPEDU.Domain.Models.Application
{
    public class AppApplicationInfo : BaseModel
    {
        [Key]
        public Int32 AppInformationId { get; set; }

        [Display(Name = "Name")]
        [Required]
        [MaxLength(128)]
        public String Name { get; set; }

        [Display(Name = "Key")]
        [Required]
        [MaxLength(128)]
        public String Key { get; set; }

        [Display(Name = "Value")]
        [Required]
        [MaxLength(256)]
        public String Value { get; set; }

        [Display(Name = "Description")]
        [Required]
        [MaxLength(400)]
        public String Description { get; set; }
    }
}
