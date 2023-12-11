using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElnurCourseWork.Finder
{
    public class MyPoint
    {
        public MyPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return $"({X},   {Y})";
        }

        public string ToStringPrecision(int cnt)
        {
            return $"({Math.Round(X, cnt)} | {Math.Round(Y, cnt)})";
        }
    }
}
