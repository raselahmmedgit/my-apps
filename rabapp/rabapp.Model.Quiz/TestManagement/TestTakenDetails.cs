using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    [Table("TestTakenDetails", Schema = "dbo")]
    public class TestTakenDetails : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int TestTakenDetailsId { get; set; }

        public int TakenId { get; set; }
        public int TestId { get; set; }
        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        public int QuestionAnswerOptionId { get; set; }
        [ForeignKey("QuestionAnswerOptionId")]
        public virtual QuestionAnswerOption QuestionAnswerOption { get; set; }

        public bool IsCorrectAnswer { get; set; }
    }
}
