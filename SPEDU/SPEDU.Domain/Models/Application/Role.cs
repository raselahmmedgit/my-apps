using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPEDU.Domain.Models.Application
{
    public class Role : BaseModel
    {
        //[Key]
        //public virtual Guid RoleId { get; set; }
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 RoleId { get; set; }

        [Display(Name = "Role Name")]
        [Required]
        [MaxLength(100)]
        public String RoleName { get; set; }

        //public virtual ICollection<User> Users { get; set; }
    }
}
