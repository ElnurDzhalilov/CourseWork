using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Math;


namespace ElnurCourseWork.Finder
{
    public class ExtremumFinder
    {
        //минамальное расстояние между границами
        public double Eps = 0.01;
        //границы аргумента
        public double LBorder;
        public double RBorder;
        //функция для расчета
        public Func<double, double> f;

        //экземпляр ответа
        private ExtrAnswer answer;

        //Фасад для получения ответа
        public ExtrAnswer GetAnswer()
        {
            answer = new ExtrAnswer();

            //если данные некорректны, отказ
            if (!CheckCorrectness())
            {
                return answer;
            }

            CalcPoints();
            ExtremumСandidates();
            СandidatesСlassification();
            return answer;
        }

        //Фасад для получения ответа, асинхронно
        public Task<ExtrAnswer> GetAnswerAsync()
        {
            //запуск асинхронной задачи
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

        //Проверка корректности границ
        private bool CheckCorrectness()
        {
            if(LBorder >= RBorder)
            {
                answer.Error = "Границы перепутаны";
                return false;
            }

            if(LBorder + Eps > RBorder)
            {
                answer.Error = "Расстояние между границами меньше допустимого";
                return false;
            }

            return true;
        }

        //Дискретный расчет значения функции и ее производной 
        private void CalcPoints()
        {            
            //расчет колва шагов
            double step = (RBorder - LBorder) / 10000; 
            //расчет точек
            for(double i = LBorder + step; i < RBorder - 2*step; i += step)
            {
                //аргументы в точках
                double x1 = i - step;
                double x2 = i + step;

                //зачения в точках
                double y1 = f(x1);
                double y2 = f(x2);

                //вычисление производной
                double k = (y1 - y2) / (x1 - x2);

                //добавлние арзумента, функции и производной в серии
                answer.X_es.Add(i);
                answer.Y_es.Add(f(i));
                answer.dX_es.Add(k);
            }
        }

        //Поиск кандидатов на экстремум
        private void ExtremumСandidates()//indexes
        {
            //предыдыцщая и текущие точки
            double prev;
            double cur = answer.dX_es.First();
            
            //проход по всем точкам
            for(int i = 0; i < answer.dX_es.Count; i++)
            {
                prev = cur;
                cur = answer.dX_es[i];
                //точка есть кандидат если знаки производной отличны слева и справа
                if (prev * cur <= 0) answer.ExtremumСandidatesIndexes.Add(i);
            }

        }

        //Классификация кандидатов
        private void СandidatesСlassification()
        {
            foreach(var index in answer.ExtremumСandidatesIndexes)
            {
                //листы анализа влево с вправо
                List<double> moveL = new List<double>();
                List<double> moveR = new List<double>();
                //колво шагов для анализа
                const int stepsLim = 5;

                //начальный индекс поиска
                var v = index;
                //колво сделаных шагов в сторону
                var steps = 0;
                //анализ влево
                while (v > 0 && steps < stepsLim)
                {
                    //добавлние левых соседей во времнный лист
                    moveL.Add(answer.dX_es[v]);
                    v--;
                    steps++;
                }

                //начальный индекс поиска
                v = index;
                //колво сделаных шагов в сторону
                steps = 0;
                //анализ вправо
                while (index < answer.dX_es.Count && steps < stepsLim)
                {
                    //добавлние правых соседей во времнный лист
                    moveR.Add(answer.dX_es[v]);
                    v++;
                    steps++;
                }

                //тест влево и справо
                bool bL = СandidateTest(moveL);
                bool bR = СandidateTest(moveR);

                //добавление кандидата в ответ
                answer.res.Add(
                    ( bL && bR, new MyPoint(answer.X_es[index], answer.Y_es[index]) )
                    );
            }
        }

        //Классификация одного кандидата
        private bool СandidateTest(List<double> candidateNeighborhood)
        {
            double near;
            double far = Abs(candidateNeighborhood.First());
            //лист записи весов точек соседей
            List<int> list = new List<int>();

            //цикл по всем соседям точки с одно стороны
            for (int i = 0; i < candidateNeighborhood.Count; i++)
            {
                near = Abs(far);
                far = Abs(candidateNeighborhood[i]);
                //если абс. величина производной растет, то добавляем вес истиности кандидата
                if (far > near) list.Add(1);
                //иначе понижаем вес
                else list.Add(-1);
            }
            //суммируем веса
            int sum = list.Sum();

            //если вес больше нуля то это экстремум
            return sum > 0;
        }
    }
}
