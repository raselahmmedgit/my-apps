using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    public class AppWidgetUserPermission : BaseNotMapModel
    {
        [Key]
        public Int32 AppWidgetPermissionId { get; set; }

        [Display(Name = "Add")]
        public Boolean IsAdd { get; set; }

        [Display(Name = "Remove")]
        public Boolean IsRemove { get; set; }

        public Int32 AppWidgetId { get; set; }
        [ForeignKey("AppWidgetId")]
        public virtual AppWidget AppWidget { get; set; }

        public Int32 UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
