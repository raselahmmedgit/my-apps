using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    public class AppWidget : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 AppWidgetId { get; set; }

        [Display(Name = "Name")]
        [StringLength(256)]
        public String Name { get; set; }

        [Display(Name = "Is Admin")]
        public Boolean IsAdmin { get; set; }

        [Display(Name = "Is Shared")]
        public Boolean IsShared { get; set; }

        public Int32 AppWidgetCategoryId { get; set; }
        [ForeignKey("AppWidgetCategoryId")]
        public virtual AppWidgetCategory AppWidgetCategory { get; set; }
    }
}
