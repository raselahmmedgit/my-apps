using System.ComponentModel.DataAnnotations.Schema;
using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPEDU.Domain.Models.Application
{
    [Table("EmailTemplateCategory", Schema = "App")]
    public class EmailTemplateCategory : BaseNotMapModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 EmailTemplateCategoryId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(128)]
        public String Name { get; set; }
    }
}
