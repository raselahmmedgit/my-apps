using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareGrid.Model.Utility.DocumentManagement
{
    [Table("DocumentInformation", Schema = "dbo")]
    public class DocumentInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GlobalId { get; set; }

        [StringLength(250)]
        public string DocumentName { get; set; }
        public byte[] DocumentByte { get; set; }
        public int? DocumentSize { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }

    }









}
