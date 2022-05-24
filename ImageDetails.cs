using System;
using System.Collections.Generic;

namespace ImageProcessor
{
    public class ImageDetails
    {
        internal string FilePath { get; set; }

        internal IDictionary<string, string> Filters { get; set; }

        /*public string Flip { get; set; }

        public string RotateNDegree { get; set; }

        public bool ConvertToFixedGrayScale { get; set; }

        public string ConvertToGrayScale { get; set; }

        public string SaturateOrDeSaturate { get; set; }

        public string Resize { get; set; }

        public string ResizePercentage { get; set; }

        public string GenerateTumbnail { get; set; }

        public string Rotate { get; set; }*/
    }
}
