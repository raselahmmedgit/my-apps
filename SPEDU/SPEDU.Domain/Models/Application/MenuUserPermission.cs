using SPEDU.Domain.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPEDU.Domain.Models.Application
{
     [Table("MenuUserPermission", Schema = "App")]
    public class MenuUserPermission : BaseNotMapModel
    {
        [Key]
        public Int32 MenuUserPermissionId { get; set; }

        [Display(Name = "Add")]
        public Boolean IsAdd { get; set; }

        [Display(Name = "Remove")]
        public Boolean IsRemove { get; set; }

        public Int32 MenuId { get; set; }
        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

        public Int32 UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
