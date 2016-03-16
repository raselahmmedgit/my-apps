using System.ComponentModel.DataAnnotations.Schema;
using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPEDU.Domain.Models.Application
{
    [Table("UserActivity", Schema = "App")]
    public class UserActivity : BaseNotMapModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 UserActivityId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100)]
        public String Name { get; set; }
    }
}
