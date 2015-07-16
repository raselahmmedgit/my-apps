using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SPEDU.Domain.BaseModels;

namespace SPEDU.Domain.Models.Application
{
    public class AppRight : BaseModel
    {
        [Key]
        public Int32 AppRightId { get; set; }

        [DisplayName("Name: ")]
        [Required(ErrorMessage = "Right Name is required")]
        [MaxLength(128)]
        public String Name { get; set; }

        [DisplayName("Description: ")]
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(256)]
        public String Description { get; set; }

    }
}
