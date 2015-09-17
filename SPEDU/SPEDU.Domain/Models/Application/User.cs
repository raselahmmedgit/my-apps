using System.ComponentModel.DataAnnotations.Schema;
using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPEDU.Domain.Models.Application
{
    [Table("User", Schema = "App")]
    public class User : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 UserId { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]
        [MaxLength(100)]
        public String UserName { get; set; }

        [Required]
        [MaxLength(64)]
        public Byte[] Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(200)]
        public String Email { get; set; }

        [MaxLength(200)]
        public String Comment { get; set; }

        [Display(Name = "Approved?")]
        public Boolean IsApproved { get; set; }

        [Display(Name = "Last Login Date")]
        public DateTime? LastLoginDate { get; set; }

        [Display(Name = "Last Activity Date")]
        public DateTime? LastActivityDate { get; set; }

        [Display(Name = "Last Password Change Date")]
        public DateTime LastPasswordChangeDate { get; set; }

        //public Boolean IsLoggedIn { get; set; }

        //public virtual ICollection<Role> Roles { get; set; }
    }
}
