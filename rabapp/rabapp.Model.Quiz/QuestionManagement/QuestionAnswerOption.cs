using rabapp.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.Model.Quiz.QuestionManagement
{
    [Table("QuestionAnswerOption", Schema = "dbo")]
    public class QuestionAnswerOption : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int QuestionAnswerOptionId { get; set; }

        public int GlobalId { get; set; }

        public Int32 QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        [Required(ErrorMessage = "Answer Option is required.")]
        public string QuestionAnswerOptionText { get; set; }

        [StringLength(500)]
        public string QuestionAnswerOptionImageName { get; set; }

        public bool IsCorrectAnswer { get; set; }

    }
}
