using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mousse.ViewModel
{
    public class CustomArrow
    {
        #region MainLine
        public double X1MainLine { get; set; }
        public double X2MainLine { get; set; }
        public double Y1MainLine { get; set; }
        public double Y2MainLine { get; set; }

        public string StrokeEndLineCap { get; set; }
        #endregion

        #region LineWeightEllipce
        public double XLineWeightBorder { get; set; }
        public double YLineWeightBorder { get; set; }
        #endregion

        public int WeightText { get; set; } = 0;

        public CustomArrow(double x1, double x2, double y1, double y2, int weight, bool orient)
        {
            X1MainLine = x1;
            X2MainLine = x2;
            Y1MainLine = y1;
            Y2MainLine = y2;
            XLineWeightBorder = ((x1 + x2) / 2) - 15;
            YLineWeightBorder = ((y1 + y2) / 2) - 15;
            WeightText = weight;
            StrokeEndLineCap = orient ? "Triangle" : "Square";
        }
    }
}


