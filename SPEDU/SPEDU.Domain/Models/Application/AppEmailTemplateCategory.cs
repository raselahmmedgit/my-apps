using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPEDU.Domain.Models.Application
{
    public class AppEmailTemplateCategory : BaseNotMapModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 AppEmailTemplateCategoryId { get; set; }

        [Display(Name = "Name")]
        [Required]
        [MaxLength(128)]
        public String Name { get; set; }
    }
}
