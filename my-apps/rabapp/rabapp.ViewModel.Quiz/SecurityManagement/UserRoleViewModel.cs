using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.ViewModel.Quiz.ViewModels
{
    [NotMapped]
    public class UserRoleViewModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 UserRoleId { get; set; }

        public Int32 UserId { get; set; }
        public String UserName { get; set; }

        public Int32 RoleId { get; set; }
        public String RoleName { get; set; }
    }
}
