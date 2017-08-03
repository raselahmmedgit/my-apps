using rabapp.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.Model.Quiz.DocumentManagement
{
    [Table("DocumentInformation", Schema = "dbo")]
    public class DocumentInformation : BaseModel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GlobalId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(250)]
        public string DocumentName { get; set; }

        public byte[] DocumentByte { get; set; }

        public int? DocumentSize { get; set; }

    }
}
