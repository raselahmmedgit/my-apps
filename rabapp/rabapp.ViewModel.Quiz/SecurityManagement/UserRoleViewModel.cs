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
        public int UserRoleId { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
