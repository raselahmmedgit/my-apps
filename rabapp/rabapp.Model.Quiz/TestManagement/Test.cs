using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using rabapp.Model.Quiz.QuestionManagement;
using rabapp.Model.Common;

namespace rabapp.Model.Quiz.TestManagement
{
    [Table("Test", Schema = "dbo")]
    public class Test : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int TestId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(150)]
        public string TestName { get; set; }

        public int GlobalId { get; set; }

        public Int32 TestCategoryId { get; set; }
        [ForeignKey("TestCategoryId")]
        public virtual TestCategory TestCategory { get; set; }

        [StringLength(250)]
        public string TestIconName { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }

        [Required(ErrorMessage = "About is required.")]
        [AllowHtml]
        public string About { get; set; }

        public int NoOfQuestion { get; set; }

        public int TestOrder { get; set; }

        public bool IsFree { get; set; }

        public bool IsSingleOrder { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int Duration { get; set; }

        public int ValidedFor { get; set; }

        public int NoOfSession { get; set; }

        public bool IsActive { get; set; }

        public bool IsPublished { get; set; }

    }
}
