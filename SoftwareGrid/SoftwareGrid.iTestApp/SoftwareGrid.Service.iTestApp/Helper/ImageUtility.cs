using SoftwareGrid.Model.iTestApp.Utility;
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web.Mvc;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;

namespace SoftwareGrid.Service.iTestApp.Helper
{
    public class ImageUtility
    {
        private ImageFactory factory = null;
        private int _minWidth = 800;
        private int _minHeight = 600;

        private double ratio
        {
            get
            {
                return (double)_minWidth / _minHeight;
            }
        }

        public ImageUtility()
        {
            factory = new ImageFactory(preserveExifData: true);
        }
        public ImageUtility(int minWidth, int minHeight)
        {
            _minWidth = minWidth;
            _minHeight = minHeight;
            factory = new ImageFactory(preserveExifData: true);
        }

        public bool Crop(string path, ImageCropViewModel imageCropViewModel)
        {
            bool result = true;
            var fileExtension = Path.GetExtension(path);
            var imageDirectory = Path.GetDirectoryName(path);
            if (imageDirectory == null) return result;

            var tempImage = Path.Combine(imageDirectory, String.Format("{0}{1}", Guid.NewGuid(), fileExtension));
            File.Copy(path, tempImage);

            var bitmap = Image.FromFile(tempImage) as Bitmap;

            int originalwidth = bitmap.Width;
            int originalHeight = bitmap.Height;
            double factorwidthratio = (double)imageCropViewModel.FactorWidth / originalwidth;
            double factorheightratio = (double)imageCropViewModel.FactorHeight / originalHeight;

            imageCropViewModel.X1 = Convert.ToInt32(imageCropViewModel.X1 / factorwidthratio);
            imageCropViewModel.Y1 = Convert.ToInt32(imageCropViewModel.Y1 / factorheightratio);
            imageCropViewModel.Width = Convert.ToInt32(imageCropViewModel.Width / factorwidthratio);
            imageCropViewModel.Height = Convert.ToInt32(imageCropViewModel.Height / factorheightratio);

            var cropRect = new Rectangle(Convert.ToInt32(imageCropViewModel.X1), Convert.ToInt32(imageCropViewModel.Y1), Convert.ToInt32(imageCropViewModel.Width), Convert.ToInt32(imageCropViewModel.Height));
            Bitmap cropped = null;
            try
            {
                cropped = bitmap.Clone(cropRect, bitmap.PixelFormat);
                ImageCodecInfo jpgEncoder = GetEncoder(GetImageFormatForSave(path));
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                cropped.Save(path, jpgEncoder, myEncoderParameters);
                cropped.Dispose();
                bitmap.Dispose();
                File.Delete(tempImage);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;


        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private ISupportedImageFormat GetImageFormat(string filename)
        {
            ISupportedImageFormat format = null;
            String temp = filename;

            temp.ToLower();

            if (temp.EndsWith(".png"))
                format = new PngFormat { Quality = 70 };

            if (temp.EndsWith(".gif"))
                format = new GifFormat { Quality = 70 };

            if (temp.EndsWith(".jpeg"))
                format = new JpegFormat { Quality = 70 };
            if (temp.EndsWith(".jpg"))
                format = new JpegFormat { Quality = 70 };
            if (format == null)
            {
                format = new JpegFormat { Quality = 70 };
            }

            return format;
        }

        public byte[] EnforceResize(string imageFile)
        {
            using (var srcImage = Image.FromFile(imageFile))
            {
                Size size = GetSize(srcImage.Width, srcImage.Height);
                using (var newImage = new Bitmap(size.Width, size.Height))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(srcImage, new Rectangle(0, 0, size.Width, size.Height));
                    // graphics.DrawImage(srcImage, new Rectangle(0, 0, srcImage.Width, srcImage.Height));
                    var memoryStream = new MemoryStream();
                    ImageFormat format = GetImageFormatForSave(imageFile);

                    newImage.Save(memoryStream, format);


                    //newImage.Save(imageFile);
                    return memoryStream.ToArray();
                }
            }
        }

        public byte[] EnforceResize(int width, int height, byte[] photobytes, string PhotoFileName)
        {
            if (photobytes == null) return null;
            using (var ms = new MemoryStream(photobytes))
            using (Bitmap src = new Bitmap(ms))
            {
                var iH = src.Height;
                var iW = src.Width;
                var nH = 0;
                var nW = 0;


                if (iH > iW && iH > height)
                {
                    nH = height;
                    nW = iW * nH / iH;
                }
                else if (iW > iH && iW > width)
                {
                    nW = width;
                    nH = iH * nW / iW;
                }
                else
                {
                    nH = iH;
                    nW = iW;
                }
                int newWidth = nW;
                int newHeight = nH;

                using (Bitmap bmp = new Bitmap(newWidth, newHeight))
                {
                    bmp.SetResolution(src.HorizontalResolution, src.VerticalResolution);
                    using (var graphics = Graphics.FromImage(bmp))
                    {
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        #region doubleImageAlgorithm

                        //int sourceWidth = src.Width;
                        //int sourceHeight = src.Height;
                        //int sourceX = 0;
                        //int sourceY = 0;
                        //int destX = 0;
                        //int destY = 0;
                        //float nPercent = 0;
                        //float nPercentW = 0;
                        //float nPercentH = 0;

                        //nPercentW = ((float)width / (float)sourceWidth);
                        //nPercentH = ((float)height / (float)sourceHeight);
                        //if (nPercentH < nPercentW)
                        //{
                        //    nPercent = nPercentH;
                        //    destX = Convert.ToInt16((width -
                        //                  (sourceWidth * nPercent)) / 2);
                        //}
                        //else
                        //{
                        //    nPercent = nPercentW;
                        //    destY = Convert.ToInt16((height -
                        //                  (sourceHeight * nPercent)) / 2);
                        //}
                        //int destWidth = (int)(sourceWidth * nPercent);
                        //int destHeight = (int)(sourceHeight * nPercent);
                        //graphics.DrawImage(src, new Rectangle(destX, destX, width, height), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
                        // graphics.DrawImage(srcImage, new Rectangle(0, 0, srcImage.Width, srcImage.Height));

                        #endregion

                        #region Single ImageAlgorithm


                        //double ratioX = (double)width / (double)sourceWidth;
                        //double ratioY = (double)height / (double)sourceHeight;
                        //double ratio = ratioX < ratioY ? ratioX : ratioY;
                        //int newHeight = Convert.ToInt32(sourceHeight * ratio);
                        //int newWidth = Convert.ToInt32(sourceWidth * ratio);
                        //int posX = Convert.ToInt32((width - (sourceWidth * ratio)) / 2);
                        //int posY = Convert.ToInt32((height - (sourceHeight * ratio)) / 2);


                        //graphics.DrawImage(src, posX, posY, newWidth, newHeight);

                        #endregion

                        #region Normal

                        graphics.DrawImage(src, new Rectangle(0, 0, newWidth, newHeight));

                        #endregion

                        var memoryStream = new MemoryStream();
                        ImageFormat format = GetImageFormatForSave(PhotoFileName);

                        ImageCodecInfo jpgEncoder = GetEncoder(GetImageFormatForSave(PhotoFileName));
                        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);

                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 60L);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        bmp.Save(memoryStream, jpgEncoder, myEncoderParameters);
                        //bmp.Save(memoryStream, format);
                        return memoryStream.ToArray();
                    }
                }
            }

        }

        public void Resize(string imageFile, string outputfile)
        {
            using (var srcImage = Image.FromFile(imageFile))
            {
                Size size = GetSize(srcImage.Width, srcImage.Height);
                using (var newImage = new Bitmap(size.Width, size.Height))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(srcImage, new Rectangle(0, 0, size.Width, size.Height));
                    // graphics.DrawImage(srcImage, new Rectangle(0, 0, srcImage.Width, srcImage.Height));
                    newImage.Save(outputfile);
                }
            }
        }

        public Size GetSize(int width, int height)
        {
            double rat = (double)width / height;
            int tempwidth, tempheight;
            if (width < _minWidth)
            {
                tempwidth = _minWidth;
            }
            else
            {
                tempwidth = width;
            }
            if (height < _minHeight)
            {
                tempheight = _minHeight;
            }
            else
            {
                tempheight = height;
            }
            if (tempwidth > tempheight)
            {
                tempheight = Convert.ToInt32(tempwidth / rat);
            }
            else
            {
                tempwidth = Convert.ToInt32(tempheight * rat);
            }

            return new Size(tempwidth, tempheight);
        }

        string GetContentType(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return "Image/bmp";
                case ".gif": return "Image/gif";
                case ".jpg": return "Image/jpeg";
                case ".png": return "Image/png";
                default: break;
            }
            return "";
        }

        ImageFormat GetImageFormatForSave(String path)
        {
            switch (Path.GetExtension(path))
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".jpg": return ImageFormat.Jpeg;
                case ".png": return ImageFormat.Png;
                default: break;
            }
            return ImageFormat.Jpeg;
        }

        public Size GetPreferredImageSize(SoftwareGrid.Model.iTestApp.Utility.Constants.ImageDimensions dimension)
        {
            var size = new Size();
            switch (dimension)
            {
                case SoftwareGrid.Model.iTestApp.Utility.Constants.ImageDimensions.Common:
                    size = new Size(210, 210);
                    break;
                default:
                    size = new Size(1280, 768);
                    break;
            }
            return size;
        }

        public bool ImageSaveToPath(string fullRelativePath, byte[] photobytes, string fileName, Constants.ImageDimensions dimensions)
        {
            bool success = false;
            try
            {
                if (photobytes != null)
                {
                    if (!Directory.Exists(fullRelativePath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(fullRelativePath);
                    }

                    var utility = new ImageUtility();
                    var size = GetPreferredImageSize(dimensions);
                    photobytes = utility.EnforceResize(size.Width, size.Height, photobytes, fileName);

                    IOFileHelper.WriteFile(fullRelativePath + "\\" + fileName, photobytes);

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        public bool ImageRemoveToPath(string fullRelativePath, string fileName)
        {
            try
            {
                string physicalPath = fullRelativePath + fileName;

                // TODO: Verify user permissions
                if (Directory.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    Directory.Delete(physicalPath);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }

    public class ImageResult : ActionResult
    {
        public String ContentType { get; set; }
        public byte[] ImageBytes { get; set; }
        public String SourceFilename { get; set; }

        //This is used for times where you have a physical location
        public ImageResult(String sourceFilename, String contentType)
        {
            SourceFilename = sourceFilename;
            ContentType = contentType;
        }

        //This is used for when you have the actual image in byte form
        //  which is more important for this post.
        public ImageResult(byte[] sourceStream, String contentType)
        {
            ImageBytes = sourceStream;
            ContentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            try
            {
                //response.Clear();
                //response.Cache.SetCacheability(HttpCacheability.NoCache);
                //response.ContentType = ContentType;

                //Check to see if this is done from bytes or physical location
                //  If you're really paranoid you could set a true/false flag in
                //  the constructor.
                if (ImageBytes != null)
                {
                    var stream = new MemoryStream(ImageBytes);
                    stream.WriteTo(response.OutputStream);
                    stream.Dispose();
                }
                else if (!string.IsNullOrEmpty(SourceFilename))
                {
                    response.TransmitFile(SourceFilename);
                }
            }
            catch(Exception ex)
            {
            }
        }
    }
}
