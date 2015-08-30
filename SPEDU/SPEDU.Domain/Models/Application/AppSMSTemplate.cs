using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    public class AppSMSTemplate : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 AppSMSTemplateId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(256)]
        public String Name { get; set; }

        [Display(Name = "SMS Subject")]
        [StringLength(256)]
        public String SMSSubject { get; set; }

        [Display(Name = "SMS Message")]
        [MaxLength]
        public String SMSMessage { get; set; }

        [Display(Name = "Is Admin")]
        public Boolean IsAdmin { get; set; }

        [Display(Name = "Is Shared")]
        public Boolean IsShared { get; set; }

        public Int32 AppSMSTemplateCategoryId { get; set; }
        [ForeignKey("AppSMSTemplateCategoryId")]
        public virtual AppSMSTemplateCategory AppSMSTemplateCategory { get; set; }
    }
}
