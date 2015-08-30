using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace SPEDU.Domain.Models.Application
{
    public class AppEmailTemplate : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 AppEmailTemplateId { get; set; }
        
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(256)]
        public String Name { get; set; }

        [Display(Name = "Email Subject")]
        [StringLength(256)]
        [AllowHtml]
        public String EmailSubject { get; set; }

        [Display(Name = "Email Message")]
        [MaxLength]
        [DataType(DataType.Html)]
        [AllowHtml]
        public String EmailMessage { get; set; }

        [Display(Name = "Is Admin")]
        public Boolean IsAdmin { get; set; }

        [Display(Name = "Is Shared")]
        public Boolean IsShared { get; set; }

        public Int32 AppEmailTemplateCategoryId { get; set; }
        [ForeignKey("AppEmailTemplateCategoryId")]
        public virtual AppEmailTemplateCategory AppEmailTemplateCategory { get; set; }

    }
}
