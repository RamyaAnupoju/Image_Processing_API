using System;
using System.Collections.Generic;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageProcessor
{
    public static class ImageTransformer
    {
        internal static string ProcessImage(IDictionary<string, string> userData)
        {
            string filePath = userData.First().Value;

            using (Image image = Image.Load(filePath))
            {
                foreach (var filter in userData)
                {
                    var key = filter.Key;
                    var value = filter.Value;

                    switch (key)
                    {
                        case "Flip":
                            FlipImage(image, value);
                            break;
                        case "RotateNDegree":
                            RotateNDegreeImage(image, value);
                            break;
                        case "ConvertToFixedGrayScale":
                            ConvertToFixedGrayScale(image, value);
                            break;
                        case "ConvertToGrayScale":
                            ConvertToGrayScale(image, value);
                            break;
                        case "SaturateOrDeSaturate":
                            SaturateOrDeSaturate(image, value);
                            break;
                        case "Resize":
                            Resize(image, value);
                            break;
                        case "ResizePercentage":
                            ResizePercentage(image, value);
                            break;
                        case "GenerateTumbnail":
                            GenerateTumbnail(image, value);
                            break;
                        case "RotateLeft":
                            RotateLeft(image, value);
                            break;
                        case "RotateRight":
                            RotateRight(image, value);
                            break;
                        default:
                            break;
                    }
                }
                image.Save(filePath);
            }
            return filePath;
        }

        private static void ConvertToGrayScale(Image image, string value)
        {
            float i = 0;
            if (value != "" && float.TryParse(value, out i))
            {
                float amount = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                image.Mutate(x => x.Grayscale(amount));
            }
        }

        private static void SaturateOrDeSaturate(Image image, string value)
        {
            float i = 0;
            if (value != "" && float.TryParse(value, out i))
            {
                float amount = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                image.Mutate(x => x.Saturate(amount));
            }
        }

        private static void Resize(Image image, string value)
        {
            if (value != "" && value.Split(",").Length == 2)
            {
                string[] valArr = value.Split(",");
                int width = Int16.Parse(valArr[0]);
                int height = Int16.Parse(valArr[1]);
                image.Mutate(x => x.Resize(width, height));
            }
        }

        private static void ResizePercentage(Image image, string value)
        {
            int i = 0;
            if (value != "" && int.TryParse(value, out i))
            {
                int percentage = Int16.Parse(value);
                int width = (int)(image.Width * (percentage / 100.0));
                int height = (int)(image.Height * (percentage / 100.0));
                image.Mutate(x => x.Resize(width, height));
            }
        }

        private static void GenerateTumbnail(Image image, string value)
        {
            if(value.ToLower().Equals("true"))
            {
                image.Mutate(i => i.Resize(new ResizeOptions
                {       
                    Size = new Size(300, 300),
                    Mode = ResizeMode.Crop
                }));
            }
            else if (value.ToLower().Equals("false"))
            {
                return;
            }
        }

        private static void RotateLeft(Image image, string value)
        {
            if (value.ToLower().Equals("true"))
            {
                image.Mutate(x => x.Rotate(RotateMode.Rotate270));
            }
            else if (value.ToLower().Equals("false"))
            {
                return;
            }
        }

        private static void RotateRight(Image image, string value)
        {
            if (value.ToLower().Equals("true"))
            {
                image.Mutate(x => x.Rotate(RotateMode.Rotate90));
            }
            else if (value.ToLower().Equals("false"))
            {
                return;
            }
        }

        private static void ConvertToFixedGrayScale(Image image, string value)
        {
            if (value.ToLower().Equals("true"))
            {
                image.Mutate(x => x.Grayscale(1));
            }
            else if(value.ToLower().Equals("false"))
            {
                return;
            }
        }

        private static void FlipImage(Image image, string value) 
        {
            if (value.Equals("horizontal"))
            {
                image.Mutate(x => x.Flip(FlipMode.Horizontal));
            }
            else if (value.Equals("vertical"))
            {
                image.Mutate(x => x.Flip(FlipMode.Vertical));
            }
        }

        private static void RotateNDegreeImage(Image image, string value) 
        {
            float i = 0;
            if (value != "" && float.TryParse(value, out i))
            {
                float degree = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                image.Mutate(x => x.Rotate(degree));
            }
        }
    }
}
