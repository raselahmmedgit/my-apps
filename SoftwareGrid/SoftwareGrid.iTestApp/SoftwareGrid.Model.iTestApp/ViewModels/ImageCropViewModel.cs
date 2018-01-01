using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareGrid.Model.iTestApp.Utility
{
    [NotMapped]
    public class ImageCropViewModel
    {
        public float X1 { get; set; }
        public float Y1 { get; set; }
        public float X2 { get; set; }
        public float Y2 { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float FactorWidth { get; set; }
        public float FactorHeight { get; set; }
        public string SourceImage { get; set; }
        public bool Cropped { get; set; }
    }
}
