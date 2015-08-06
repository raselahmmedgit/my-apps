using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
    public class AppRightUserPermission : BaseNotMapModel
    {
        [Key]
        public Int32 AppRightUserPermissionId { get; set; }

        public Int32 AppRightId { get; set; }
        [ForeignKey("AppRightId")]
        public virtual AppRight AppRight { get; set; }

        public Int32 UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
