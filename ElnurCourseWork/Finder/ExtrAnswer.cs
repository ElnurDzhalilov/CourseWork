using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElnurCourseWork.Finder
{
    public class ExtrAnswer
    {
        public List<double> X_es = new List<double>();
        public List<double> Y_es = new List<double>();
        public List<double> dX_es = new List<double>();
        public List<int> ExtremumСandidatesIndexes = new List<int>();
        public List<(bool real, MyPoint point)> res = new List<(bool, MyPoint)>();

        public string Error { get; set; }

    }
}
