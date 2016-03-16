using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    [Table("Widget", Schema = "App")]
    public class Widget : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 WidgetId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(256)]
        public String Name { get; set; }

        [Display(Name = "Is Admin")]
        public Boolean IsAdmin { get; set; }

        [Display(Name = "Is Shared")]
        public Boolean IsShared { get; set; }

        public Int32 WidgetCategoryId { get; set; }
        [ForeignKey("WidgetCategoryId")]
        public virtual WidgetCategory WidgetCategory { get; set; }
    }
}
