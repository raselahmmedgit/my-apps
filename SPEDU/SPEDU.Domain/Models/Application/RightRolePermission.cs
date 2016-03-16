using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    [Table("RightRolePermission", Schema = "App")]
    public class RightRolePermission : BaseNotMapModel
    {
        [Key]
        public Int32 RightRolePermissionId { get; set; }

        public Int32 RightId { get; set; }
        [ForeignKey("RightId")]
        public virtual Right Right { get; set; }

        public Int32 RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
