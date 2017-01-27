using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Quiz.DocumentManagement;
using rabapp.Model.Quiz.QuestionManagement;
using rabapp.Model.Quiz.SecurityManagement;

namespace rabapp.Model.Quiz.ViewModels
{
    [NotMapped]
    public class QuestionViewModel : Question
    {
        public string QuestionCategoryName { get; set; }

        public string QuestionImagePath { get; set; }

        public string QuestionImage { get; set; }

        public string AnswerOptionText { get; set; }

        public string AnswerOptionImagePath { get; set; }

        public string AnswerOptionImage { get; set; }

        public string AnswerOptionImageName { get; set; }

        public bool IsCorrectAnswer { get; set; }

        public string AnswerOptionList { get; set; }

        public List<QuestionAnswerOptionViewModel> QuestionAnswerOptionViewModelList { get; set; }

        public int? TestId { get; set; }
    }
}
