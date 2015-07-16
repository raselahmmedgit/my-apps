using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SPEDU.Domain.BaseModels;

namespace SPEDU.Domain.Models.Application
{
    public class AppMenu : BaseModel
    {
        [Key]
        public Int32 AppMenuId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Menu Name is required")]
        [MaxLength(256)]
        public String MenuName { get; set; }

        [DisplayName("Caption")]
        public String MenuCaption { get; set; }

        [DisplayName("Caption Image")]
        public String MenuCaptionBng { get; set; }

        [DisplayName("Icon")]
        public String MenuIcon { get; set; }

        [DisplayName("Page Url")]
        public String PageUrl { get; set; }

        [DisplayName("Serial No")]
        public Int32 SerialNo { get; set; }

        [DisplayName("Order No: ")]
        public Int32 OrderNo { get; set; }

        [Display(Name = "Area Name")]
        [StringLength(128)]
        public String AreaName { get; set; }

        [Display(Name = "Controller Name")]
        [StringLength(128)]
        public String ControllerName { get; set; }

        [Display(Name = "Action Name")]
        [StringLength(128)]
        public String ActionName { get; set; }

        [DisplayName("Parent Menu: ")]
        //[Required(ErrorMessage = "Please Select Parent Menu.")]
        //[Range(1, long.MaxValue, ErrorMessage = "Please Select Parent Menu.")]
        public Int32 ParentAppMenuId { get; set; }
        [DisplayName("Parent Menu Name")]
        public String ParentMenuName { get; set; }
        [ForeignKey("ParentMenuId")]
        public virtual AppMenu ParentAppMenu { get; set; }

    }
}
