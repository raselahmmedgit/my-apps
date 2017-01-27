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
        public int QuestionId { get; set; }

        public int GlobalId { get; set; }

        public Int32 QuestionCategoryId { get; set; }
        [ForeignKey("QuestionCategoryId")]
        public virtual QuestionCategory QuestionCategory { get; set; }

        [Required(ErrorMessage = "Question is required.")]
        public string QuestionText { get; set; }

        [StringLength(500)]
        public string QuestionImageName { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }

        public int NoOfAnswerOption { get; set; }

        public decimal Marks { get; set; }

        public bool IsMultipleAnswer { get; set; }

        public int DifficultyLevel { get; set; }

        public string AnswerExplanation { get; set; }

    }
}
