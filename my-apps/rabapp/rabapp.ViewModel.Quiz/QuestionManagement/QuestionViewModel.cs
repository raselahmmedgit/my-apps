using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.ViewModel.Quiz.ViewModels
{
    public class QuestionViewModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int QuestionId { get; set; }

        public int GlobalId { get; set; }

        public int QuestionCategoryId { get; set; }

        public string QuestionCategoryName { get; set; }

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

        public string QuestionImagePath { get; set; }

        public string QuestionImage { get; set; }

        public string AnswerOptionText { get; set; }

        public string AnswerOptionImagePath { get; set; }

        public string AnswerOptionImage { get; set; }

        public string AnswerOptionImageName { get; set; }

        public bool IsCorrectAnswer { get; set; }

        public string AnswerOptionList { get; set; }

        public IEnumerable<QuestionAnswerOptionViewModel> QuestionAnswerOptionViewModelList { get; set; }

        public int? TestId { get; set; }
    }
}
