using SPEDU.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEDU.Domain.Models.Application
{
    [Table("DocumentInfo", Schema = "App")]
    public class DocumentInfo : BaseModel
    {
        [Key]
        public int DocumentInfoId { get; set; }
        [StringLength(250)]
        public string DocumentName { get; set; }
        public byte[] DocumentByte { get; set; }
        public int? DocumentSize { get; set; }
    }
}
