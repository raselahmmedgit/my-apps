using System.ComponentModel.DataAnnotations.Schema;
using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPEDU.Domain.Models.Application
{
    [Table("SMSTemplateCategory", Schema = "App")]
    public class SMSTemplateCategory : BaseNotMapModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 SMSTemplateCategoryId { get; set; }

        [Display(Name = "Name")]
        [Required]
        [MaxLength(128)]
        public String Name { get; set; }
    }
}
