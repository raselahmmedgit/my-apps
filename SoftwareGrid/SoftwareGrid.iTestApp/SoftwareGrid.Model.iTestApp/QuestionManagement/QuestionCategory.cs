using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareGrid.Model.iTestApp.QuestionManagement
{

    
    [Table("QuestionCategory", Schema = "dbo")]
    public class QuestionCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionCategoryId { get; set; }
        [Required]
        [StringLength(150)]
        public string QuestionCategoryName { get; set; }
        [StringLength(50)]
        public string QuestionCategoryIcon { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
