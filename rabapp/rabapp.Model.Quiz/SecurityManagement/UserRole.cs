using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Common;

namespace rabapp.Model.Quiz.SecurityManagement
{
    [Table("UserRole", Schema = "dbo")]
    public class UserRole : BaseNotMapModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int UserRoleId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
