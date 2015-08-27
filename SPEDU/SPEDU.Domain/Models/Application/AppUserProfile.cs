using System;
using System.ComponentModel.DataAnnotations;
using SPEDU.Domain.BaseModels;

namespace SPEDU.Domain.Models.Application
{
    public class AppUserProfile : BaseModel
    {
        [Key]
        [Required]
        public Int32 UserProfileId { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(100)]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(100)]
        public String LastName { get; set; }

        [Display(Name = "Sur Name")]
        [MaxLength(100)]
        public String SurName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Address")]
        [MaxLength(200)]
        public String Address { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(50)]
        public String PhoneNumber { get; set; }

        [Display(Name = "Mobile Number")]
        [MaxLength(50)]
        public String MobileNumber { get; set; }

        [Display(Name = "Photo or Logo")]
        public Byte[] Photo { get; set; }

        [Display(Name = "Photo File Name With Extension")]
        [StringLength(256)]
        public virtual String PhotoFileName { get; set; }

        [Display(Name = "Photo File Size")]
        public virtual Int64? PhotoFileSize { get; set; }

        //one to one relationship with user
        public String UserName { get; set; }
        public User User { get; set; }
    }
}
