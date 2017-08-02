using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.QuestionManagement;
using rabapp.Model.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace rabapp.Model.Quiz.TestManagement
{
    [Table("TestWiseQuestion", Schema = "dbo")]
    public class TestWiseQuestion : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int TestWiseQuestionId { get; set; }

        public int TestId { get; set; }
        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

    }
}
