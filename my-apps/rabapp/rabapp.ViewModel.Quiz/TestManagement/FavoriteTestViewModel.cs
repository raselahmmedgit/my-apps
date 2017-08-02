using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace rabapp.ViewModel.Quiz.ViewModels
{
    [NotMapped]
    public class FavoriteTestViewModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int FavoriteTestId { get; set; }

        public int TestId { get; set; }

        [StringLength(150)]
        public string TestName { get; set; }

        public int UserId { get; set; }

        [StringLength(100)]
        public String UserName { get; set; }
    }
}
