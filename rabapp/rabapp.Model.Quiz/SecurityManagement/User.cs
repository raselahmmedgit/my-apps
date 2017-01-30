﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Common;

namespace rabapp.Model.Quiz.SecurityManagement
{
    [Table("User", Schema = "dbo")]
    public class User : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 UserId { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        [MaxLength(100)]
        public String UserName { get; set; }

        [Required]
        [MaxLength(64)]
        //public Byte[] Password { get; set; }
        public String Password { get; set; }
        
        //[Required]
        //[MaxLength(64)]
        //public byte[] PasswordHash { get; set; }

        //[Required]
        //[MaxLength(128)]
        //public byte[] PasswordSalt { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email address.")]
        [MaxLength(200)]
        public string Email { get; set; }

        [MaxLength(200)]
        public String Comment { get; set; }

        public Boolean IsApproved { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime? LastActivityDate { get; set; }

        public DateTime? LastPasswordChangeDate { get; set; }

        //public Boolean IsLoggedIn { get; set; }

        //public virtual ICollection<Role> Roles { get; set; }
    }
}
