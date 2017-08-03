using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.ViewModel.Quiz.QuestionManagement
{
    [NotMapped]
    public class QuestionAnswerOptionViewModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int QuestionAnswerOptionId { get; set; }

        public int GlobalId { get; set; }

        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Answer Option is required.")]
        public string QuestionAnswerOptionText { get; set; }

        [StringLength(500)]
        public string QuestionAnswerOptionImageName { get; set; }

        public bool IsCorrectAnswer { get; set; }

    }
}
