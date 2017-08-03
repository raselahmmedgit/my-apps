using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabapp.Model.Common;
using rabapp.Model.Quiz.SecurityManagement;

namespace rabapp.Model.Quiz.TestManagement
{
    [Table("FavoriteTest", Schema = "dbo")]
    public class FavoriteTest : BaseModel
    {
        [Key]
        [Required]
        [Display(AutoGenerateField = false)]
        public int FavoriteTestId { get; set; }

        public int TestId { get; set; }
        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
