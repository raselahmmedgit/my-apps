using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    public class AppWidgetPermission : BaseNotMapModel
    {
        [Key]
        public Int32 AppWidgetPermissionId { get; set; }

        public Int32 AppWidgetId { get; set; }
        [ForeignKey("AppWidgetId")]
        public virtual AppWidget AppWidget { get; set; }

        public Int32 RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
