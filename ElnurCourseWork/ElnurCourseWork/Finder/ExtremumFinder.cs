using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Math;


namespace ElnurCourseWork.Finder
{
    public class ExtremumFinder
    {
        public double Eps = 0.01;
        public double LBorder;
        public double RBorder;
        public Func<double, double> f;

        private ExtrAnswer answer;

        public ExtrAnswer GetAnswer()
        {
            answer = new ExtrAnswer();

            if (!CheckCorrectness())
            {
                return answer;
            }

            CalcPoints();
            ExtremumСandidates();
            СandidatesСlassification();
            return answer;
        }

        public Task<ExtrAnswer> GetAnswerAsync()
        {
            return Task.Run(() => {
                answer = new ExtrAnswer();

                if (!CheckCorrectness())
                {
                    return answer;
                }

                CalcPoints();
                ExtremumСandidates();
                СandidatesСlassification();
                return answer;
            });
        }

        private bool CheckCorrectness()
        {
            if(LBorder >= RBorder)
            {
                answer.Error = "Swap borders";
                return false;
            }

            if(LBorder + Eps > RBorder)
            {
                answer.Error = "Short borders";
                return false;
            }

            return true;
        }

        private void CalcPoints()
        {            
            double step = (RBorder - LBorder) / 10000; 
            for(double i = LBorder + step; i < RBorder - 2*step; i += step)
            {
                double x1 = i - step;
                double x2 = i + step;

                double y1 = f(x1);
                double y2 = f(x2);

                double k = (y1 - y2) / (x1 - x2);/////???????

                answer.X_es.Add(i);
                answer.Y_es.Add(f(i));
                answer.dX_es.Add(k);
            }
        }

        private void ExtremumСandidates()//indexes
        {
            
            double prev;
            double cur = answer.dX_es.First();
            
            for(int i = 0; i < answer.dX_es.Count; i++)
            {
                prev = cur;
                cur = answer.dX_es[i];
                if (prev * cur < 0) answer.ExtremumСandidatesIndexes.Add(i);
            }

        }

        private void СandidatesСlassification()
        {
            foreach(var index in answer.ExtremumСandidatesIndexes)
            {
                List<double> moveL = new List<double>();
                List<double> moveR = new List<double>();
                const int stepsLim = 5;

                var v = index;
                var steps = 0;
                while (v > 0 && steps < stepsLim)
                {
                    moveL.Add(answer.dX_es[v]);
                    v--;
                    steps++;
                }
                v = index;
                steps = 0;
                while (index < answer.dX_es.Count && steps < stepsLim)
                {
                    moveR.Add(answer.dX_es[v]);
                    v++;
                    steps++;
                }

                bool bL = СandidateTest(moveL);
                bool bR = СandidateTest(moveR);

                //if (bL && bR) answer.res.Add($"OK {answer.X_es[index]}");
                //else answer.res.Add($"XX {answer.X_es[index]}");
                //if (bL && bR) answer.res.Add( (true, new MyPoint(answer.X_es[index], answer.Y_es[index])) );
                //else answer.res.Add((false, new MyPoint(answer.X_es[index], answer.Y_es[index])));

                answer.res.Add(
                    ( bL && bR, new MyPoint(answer.X_es[index], answer.Y_es[index]) )
                    );
            }
        }

        private bool СandidateTest(List<double> candidateNeighborhood)
        {
            double near;
            double far = Abs(candidateNeighborhood.First());
            List<int> list = new List<int>();

            for (int i = 0; i < candidateNeighborhood.Count; i++)
            {
                near = Abs(far);
                far = Abs(candidateNeighborhood[i]);
                if (far > near) list.Add(1);
                else list.Add(-1);
            }

            int sum = list.Sum();

            return sum > 0;
        }
    }
}
