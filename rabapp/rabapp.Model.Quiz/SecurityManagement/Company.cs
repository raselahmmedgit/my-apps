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
    [Table("Company", Schema = "dbo")]
    public class Company : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(150)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(200)]
        public String Email { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(150)]
        public string MobileNo { get; set; }

        [StringLength(150)]
        public string PhoneNo { get; set; }

    }
}
