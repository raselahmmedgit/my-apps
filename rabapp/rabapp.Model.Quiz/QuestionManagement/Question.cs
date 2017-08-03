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
    [Table("Question", Schema = "dbo")]
    public class Question : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public Int32 QuestionId { get; set; }

        public Int32 GlobalId { get; set; }

        public Int32 QuestionCategoryId { get; set; }
        [ForeignKey("QuestionCategoryId")]
        public virtual QuestionCategory QuestionCategory { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        public String QuestionText { get; set; }

        [StringLength(500)]
        public String QuestionImageName { get; set; }

        [StringLength(500)]
        public String Tags { get; set; }

        public Int32 NoOfAnswerOption { get; set; }

        public Decimal Marks { get; set; }

        public Boolean IsMultipleAnswer { get; set; }

        public Int32 DifficultyLevel { get; set; }

        public String AnswerExplanation { get; set; }

    }
}
