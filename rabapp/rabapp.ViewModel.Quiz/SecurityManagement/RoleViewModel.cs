using rabapp.ViewModel.Quiz.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.ViewModel.Quiz.SecurityManagement
{
    [NotMapped]
    public class RoleViewModel
    {
        //[Key]
        //public virtual Guid RoleId { get; set; }
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)]
        public string RoleName { get; set; }

        public IEnumerable<UserViewModel> UserViewModels { get; set; }
    }
}
