using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    public class AppMenuUserPermission : BaseNotMapModel
    {
        [Key]
        public Int32 AppMenuUserPermissionId { get; set; }

        public Int32 AppMenuId { get; set; }
        [ForeignKey("AppMenuId")]
        public virtual AppMenu AppMenu { get; set; }

        public Int32 UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
