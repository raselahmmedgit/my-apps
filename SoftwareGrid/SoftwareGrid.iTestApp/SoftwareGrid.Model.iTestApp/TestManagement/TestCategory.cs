using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareGrid.Model.iTestApp.TestManagement
{
    [Table("TestCategory", Schema = "dbo")]
    public class TestCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TestCategoryId { get; set; }

        [Required]
        [StringLength(150)]
        public string TestCategoryName { get; set; }

        [StringLength(50)]
        public string TestCategoryIcon { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
