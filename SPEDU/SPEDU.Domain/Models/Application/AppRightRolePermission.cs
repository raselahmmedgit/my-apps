using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    public class AppRightRolePermission : BaseNotMapModel
    {
        [Key]
        public Int32 AppRightRolePermissionId { get; set; }

        public Int32 AppRightId { get; set; }
        [ForeignKey("AppRightId")]
        public virtual AppRight AppRight { get; set; }

        public Int32 RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
