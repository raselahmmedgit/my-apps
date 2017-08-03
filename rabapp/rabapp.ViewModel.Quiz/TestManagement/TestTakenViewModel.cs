using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace rabapp.ViewModel.Quiz.TestManagement
{
    [NotMapped]
    public class TestTakenViewModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int TestTakenId { get; set; }

        public int TestId { get; set; }
        public string TestName { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

        [StringLength(16)]
        public string AccessCode { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? Score { get; set; }
        [StringLength(250)]
        public string IpAddress { get; set; }

        public TestViewModel TestViewModel { get; set; }

        public IEnumerable<TestTakenDetailsViewModel> TestTakenDetailsViewModels { get; set; }

    }
}
