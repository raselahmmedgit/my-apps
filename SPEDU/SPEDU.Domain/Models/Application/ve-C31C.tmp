using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    public class AppMenuRolePermission : BaseNotMapModel
    {
        [Key]
        public Int32 AppMenuRolePermissionId { get; set; }

        public Int32 AppMenuId { get; set; }
        [ForeignKey("AppMenuId")]
        public virtual AppMenu AppMenu { get; set; }

        public Int32 RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
