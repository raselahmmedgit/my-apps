using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.ViewModel.Quiz.ViewModels
{
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
