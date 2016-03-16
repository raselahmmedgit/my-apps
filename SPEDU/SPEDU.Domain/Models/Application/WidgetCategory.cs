using System.ComponentModel.DataAnnotations.Schema;
using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPEDU.Domain.Models.Application
{
    [Table("WidgetCategory", Schema = "App")]
    public class WidgetCategory : BaseNotMapModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 WidgetCategoryId { get; set; }

        [Display(Name = "Name")]
        [Required]
        [MaxLength(100)]
        public String Name { get; set; }
    }
}
