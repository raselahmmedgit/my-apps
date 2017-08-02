using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.ViewModel.Quiz.ViewModels
{
    [NotMapped]
    public class QuestionCategoryViewModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int QuestionCategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(150)]
        public string QuestionCategoryName { get; set; }

        [StringLength(50)]
        public string QuestionCategoryIcon { get; set; }
    }
}
