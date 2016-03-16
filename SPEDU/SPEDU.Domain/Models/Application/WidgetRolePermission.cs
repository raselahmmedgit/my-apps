using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    [Table("WidgetRolePermission", Schema = "App")]
    public class WidgetRolePermission : BaseNotMapModel
    {
        [Key]
        public Int32 WidgetPermissionId { get; set; }

        public Int32 WidgetId { get; set; }
        [ForeignKey("WidgetId")]
        public virtual Widget Widget { get; set; }

        public Int32 RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
