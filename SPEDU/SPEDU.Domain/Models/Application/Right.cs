using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SPEDU.Domain.BaseModels;

namespace SPEDU.Domain.Models.Application
{
    [Table("Right", Schema = "App")]
    public class Right : BaseModel
    {
        [Key]
        public Int32 RightId { get; set; }

        [DisplayName("Name: ")]
        [Required(ErrorMessage = "Right Name is required")]
        [MaxLength(128)]
        public String Name { get; set; }

        [DisplayName("Description: ")]
        [MaxLength(256)]
        public String Description { get; set; }

    }
}
