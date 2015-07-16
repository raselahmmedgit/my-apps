using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPEDU.Domain.Models.Application
{
    public class AppWidgetCategory : BaseNotMapModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 AppWidgetCategoryId { get; set; }

        [Display(Name = "Name")]
        [Required]
        [MaxLength(100)]
        public String Name { get; set; }
    }
}
