using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    [Table("MenuRolePermission", Schema = "App")]
    public class MenuRolePermission : BaseNotMapModel
    {
        [Key]
        public Int32 MenuRolePermissionId { get; set; }

        public Int32 MenuId { get; set; }
        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

        public Int32 RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
