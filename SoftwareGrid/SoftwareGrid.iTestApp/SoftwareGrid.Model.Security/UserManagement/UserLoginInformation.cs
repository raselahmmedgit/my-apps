using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareGrid.Model.Security.UserManagement
{
    [Table("UserLoginInformation", Schema = "dbo")]
    public class UserLoginInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sl { get; set; }
        public int UserId { get; set; }
        public DateTime? LoginDateTime { get; set; }
        public DateTime? LogoutDateTime { get; set; }

        [StringLength(128)]
        public string IpAddress { get; set; }
    }
}




