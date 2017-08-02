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
        public Int32 QuestionAnswerOptionId { get; set; }

        public Int32 GlobalId { get; set; }

        public Int32 QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        [Required(ErrorMessage = "Answer Option is required.")]
        public String QuestionAnswerOptionText { get; set; }

        [StringLength(500)]
        public String QuestionAnswerOptionImageName { get; set; }

        public bool IsCorrectAnswer { get; set; }

    }
}
