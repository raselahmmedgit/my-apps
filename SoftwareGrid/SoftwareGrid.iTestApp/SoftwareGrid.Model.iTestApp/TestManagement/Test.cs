using SoftwareGrid.Model.iTestApp.QuestionManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SoftwareGrid.Model.iTestApp.TestManagement
{
    [Table("Test", Schema = "dbo")]
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TestId { get; set; }

        [Required]
        [StringLength(150)]
        public string TestName { get; set; }
        public int GlobalId { get; set; }

        public int TestCategoryId { get; set; }

        [StringLength(250)]
        public string TestIconName { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }

        [Required]
        [AllowHtml]
        public string About { get; set; }

        [AllowHtml]
        public string TestTopic { get; set; }

        [AllowHtml]
        public string TestDetails { get; set; }

        public int NoOfQuestion { get; set; }

        public int TestOrder { get; set; }

        public bool IsFree { get; set; }

        public bool IsSingleOrder { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public int Duration { get; set; }

        public int ValidedFor { get; set; }

        public int NoOfSession { get; set; }

        public bool IsActive { get; set; }

        public bool IsPublished { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual TestCategory TestCategory { get; set; }

        [NotMapped]
        public string TestCategoryName { get; set; }

        [NotMapped]
        public string TestIconPath { get; set; }

        [NotMapped]
        public virtual IEnumerable<TestWiseQuestion> TestWiseQuestions { get; set; }
        [NotMapped]
        public virtual List<Question> Questions { get; set; }
        [NotMapped]
        public int TotalRecordCount { get; set; }
        [NotMapped]
        public decimal Score { get; set; }

        [NotMapped]
        public decimal TakenId { get; set; }

        [NotMapped]
        public int FavoriteId { get; set; }

        
        
        
    }
}
