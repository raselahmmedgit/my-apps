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
    public class CompanyBranchViewModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int CompanyBranchId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(150)]
        public string CompanyBranchName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(200)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(150)]
        public string MobileNo { get; set; }

        [StringLength(150)]
        public string PhoneNo { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }
    }
}
