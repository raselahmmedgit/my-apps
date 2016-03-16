using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    [Table("WidgetUserPermission", Schema = "App")]
    public class WidgetUserPermission : BaseNotMapModel
    {
        [Key]
        public Int32 WidgetPermissionId { get; set; }

        [Display(Name = "Add")]
        public Boolean IsAdd { get; set; }

        [Display(Name = "Remove")]
        public Boolean IsRemove { get; set; }

        public Int32 WidgetId { get; set; }
        [ForeignKey("WidgetId")]
        public virtual Widget Widget { get; set; }

        public Int32 UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
