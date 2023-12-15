using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using org.mariuszgromada.math.mxparser;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Serialization;
using ElnurCourseWork.Finder;
using DB_Lib;
using DB_Lib.ExtAPI;

namespace ElnurCourseWork
{
    public partial class Form1 : Form
    {
        //Серии точек
        public LineSeries ls_fx;
        public LineSeries ls_dx;
        public LineSeries ls_extremums;
        public LineSeries ls_non_extremums;

        //Конструктор основной формы. Инициализирует элементы управления
        public Form1()
        {
            InitializeComponent();
            //запрет форме произвольно растягиваться
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            timer1.Tick += (sender, e) => Text += ".";

            //включение зума по двум осям и отключение анимации для оптимизации
            cartesianChart1.Zoom = ZoomingOptions.Xy;
            cartesianChart1.DisableAnimations = true;
            
            //механизм сопоставления полей точки и осей графика
            var mapper = Mappers.Xy<MyPoint>()
            .X(x => x.X)
            .Y(y => y.Y);
            Charting.For<MyPoint>(mapper);
            
            //настройска оси Х
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "X",         
                Separator = new Separator
                {
                    Step = 1,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection(2),
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))

                }
            });

            //настройка оси У
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Y",
                Separator = new Separator
                {
                    //Step = 1,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection(2),
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))

                }
            });

            //серия значений функции
            ls_fx = new LineSeries()
            {
                Title = "Y =",
                StrokeThickness = 1,
                PointGeometrySize = 0,
                Values = new ChartValues<MyPoint>(),
                Fill = Brushes.Transparent
            };

            //серия производной функции в точках
            ls_dx = new LineSeries()
            {
                Title = "dx =",
                StrokeThickness = 1,
                PointGeometrySize = 0,
                Values = new ChartValues<MyPoint>(),
                Fill = Brushes.Transparent,
            };

            //серия истиных экстремумов
            ls_extremums = new LineSeries()
            {
                Title = "Extr y: ",                
                StrokeThickness = 0,
                PointGeometrySize = 10,
                Values = new ChartValues<MyPoint>(),
                Fill = Brushes.Transparent,

                LineSmoothness = 0,
                Stroke = Brushes.Green
            };

            //серия ложных экстремумов
            ls_non_extremums = new LineSeries()
            {
                Title = "NOT Extr y: ",
                StrokeThickness = 0,
                PointGeometrySize = 10,
                Values = new ChartValues<MyPoint>(),
                Fill = Brushes.Transparent,

                LineSmoothness = 0,
                Stroke = Brushes.Pink
            };

            //добавлние серий в график
            cartesianChart1.Series = new SeriesCollection
            {
                ls_fx,
                ls_dx,
                ls_extremums,
                ls_non_extremums
            };
        }

        //Обработчк кнопки поиска экстремума
        //Параметры стандартные, не используются
        private async void btnFind_Click(object sender, EventArgs e)
        {
            //Определение функции одной переменной
            Function mxFunc = new Function($"f(x) = {tbParce.Text}");
            if (!mxFunc.checkSyntax())
            {
                textBox1.Text = "Выражение некорректно";

                WriteCalcRes(
                    (double)numL.Value,
                    (double)numR.Value,
                    tbParce.Text,
                    textBox1.Text
                );

                return;
            }

            //Очистка прошлых точек
            ls_fx.Values.Clear();
            ls_dx.Values.Clear();
            ls_extremums.Values.Clear();
            ls_non_extremums.Values.Clear();

            //создание экземпляра модели поисковика
            ExtremumFinder finder = new ExtremumFinder();

            //запись границ в модель
            finder.LBorder = (double)numL.Value;
            finder.RBorder = (double)numR.Value;

            //запись функции в модель
            finder.f = 
                x => mxFunc.calculate(x);

            //блокировка пользоватлеьского интерфейса
            LockUI(true);

            //асихронный поиск экстремумов
            var answer = await finder.GetAnswerAsync();  

            //обработка ошибки при ее наличии
            if(answer.Error != null)
            {
                textBox1.Text = answer.Error;

                WriteCalcRes(
                    (double)numL.Value,
                    (double)numR.Value,
                    tbParce.Text,
                    textBox1.Text
                );

                LockUI(false);
                return;
            }

            //добавлние точек на график из модели
            for(int i = 0; i < answer.X_es.Count; i++)
            {
                if((i & 0b1111) == 0)//вывод на график каждого 16 значения
                {
                    ls_fx.Values.Add(  new MyPoint(answer.X_es[i], answer.Y_es[i])  );
                    ls_dx.Values.Add(  new MyPoint(answer.X_es[i], answer.dX_es[i])  );
                }
            }

            //запись экстремумов в текстовое поле
            textBox1.Text = string.Empty;
            foreach (var item in answer.res)
            {
                //если экстремум истиный, то он добавляется в соотв серию
                if (item.real)
                {
                    ls_extremums.Values.Add(item.point);
                    textBox1.Text += $"Истиный, {item.point.ToStringPrecision(3)}\r\n";
                }
                else
                {
                    ls_non_extremums.Values.Add(item.point);
                    textBox1.Text += $"Ложный, {item.point.ToStringPrecision(3)}\r\n";
                }
                
            }
            //если экстремумов нет !!!
            if (textBox1.Text == string.Empty)
                textBox1.Text += "Экстремумов на данном диапазоне нет";

            //разблокировка пользовательского интерфейса
            LockUI(false);

            WriteCalcRes(
                (double)numL.Value,
                (double)numR.Value,
                tbParce.Text,
                textBox1.Text
            );
        }

        //Обработчк кнопки сброса масштабирования и перемещения графика
        //Параметры стандартные, не используются
        private void btnResetChart_Click(object sender, EventArgs e)
        {
            cartesianChart1.AxisX[0].MinValue = double.NaN;
            cartesianChart1.AxisX[0].MaxValue = double.NaN;
            cartesianChart1.AxisY[0].MinValue = double.NaN;
            cartesianChart1.AxisY[0].MaxValue = double.NaN;
        }

        //Блокировка польз. интерфейса на время поиска экстремума
        //Параметр - флаг заблокировано/разблокировано
        private void LockUI(bool @lock)
        {
            if (@lock)
            {
                timer1.Start();
                Text = "Расчет";               
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                timer1.Stop();
                Text = $"[ {tbParce.Text} ] Готово";
                Cursor.Current = Cursors.Default;
            }

            //блокировка / разблокировка всех элементов управления
            foreach (var c in Controls)
            {
                if(c is Control control)
                {
                    control.Enabled = !@lock;
                }
            }
        }

        //Рисование элемента управления в класс Image
        //Параметры: ctrl - эл. управления, img - изображение
        static void DrawControlToImage(Control ctrl, System.Drawing.Image img)
        {
            //прямоугольник клиентской отрисвоки
            var sourceRect = ctrl.ClientRectangle;
            //размер захватываемого графика
            var targetSize = new System.Drawing.Size(img.Width, img.Height);
            
            using (var tmp = new System.Drawing.Bitmap(sourceRect.Width, sourceRect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                //захват изображнеия с экрана
                ctrl.DrawToBitmap(tmp, sourceRect);
                using (var g = System.Drawing.Graphics.FromImage(img))
                {
                    //рисование в изображение
                    g.DrawImage(tmp, new System.Drawing.Rectangle(System.Drawing.Point.Empty, targetSize));
                }
            }
        }
        //-----------------------------------------------------------

        //Обработчик пункта меню "График в изображение"
        //Параметры стандартные, не используются
        private void графикВИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {         
            //бинарный поток
            Stream myStream;
            //стандартное окно сохранения файла
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            //конфигурация диалога
            saveFileDialog1.Filter = "BMP (*.bmp)|*.bmp";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            //прекращение в случае отказа
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            //открытие потока
            if ((myStream = saveFileDialog1.OpenFile()) != null)
            {
                var c = cartesianChart1;
                var img = new System.Drawing.Bitmap(c.Width, c.Height);
                DrawControlToImage(c, img);
                //сохранение в файл
                img.Save(myStream, System.Drawing.Imaging.ImageFormat.Bmp);
                //закрытие потока
                myStream.Close();
            }
        }

        //Обработчик пункта меню "Сохранить входные параметры"
        //Параметры стандартные, не используются
        private void сохранитьВходныеПараметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //бинарный поток
            Stream myStream;
            //стандартное окно сохранения файла
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            //конфигурация диалога
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            //прекращение в случае отказа
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            //открытие потока
            if ((myStream = saveFileDialog1.OpenFile()) != null)
            {
                //создание сериалайзера соотв. типа
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(InputParam));
                //вызов сериалайзера
                xmlSerializer.Serialize(myStream,
                    new InputParam()
                    {
                        LBorder = numL.Value,
                        RBorder = numR.Value,
                        Foo = tbParce.Text
                    }
                    );
                //закрытие потока
                myStream.Close();
            }
        }

        //Обработчик пункта меню "Загрузить входные параметры"
        //Параметры стандартные, не используются
        private void загрузитьВходныеПарамертыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //бинарный поток
            Stream myStream;
            //стандартное окно открытия файла
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //конфигурация диалога
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            //прекращение в случае отказа
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            //открытие потока
            if ((myStream = openFileDialog1.OpenFile()) != null)
            {
                //создание сериалайзера соотв. типа
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(InputParam));
                object res = null;
                try
                {
                    //попытка парсинга
                    res = xmlSerializer.Deserialize(myStream);
                }
                catch
                {
                    MessageBox.Show("Ошибка чтения файла");
                }

                //если распаршеный тип совпадает с требуемым
                if (res is InputParam input)
                {
                    numL.Value = input.LBorder;
                    numR.Value = input.RBorder;
                    tbParce.Text = input.Foo;
                }
                else MessageBox.Show("Файл не распознан");

                //закрытие потока
                myStream.Close();
            }
        }

        private void логиИспользованияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUsageLog f = new FormUsageLog();
            f.ShowDialog();
        }
        //----------------------------------------------
        private void WriteCalcRes(double LBorder, double RBorder, string Foo, string Result)
        {
            try
            {
                ExtAPI.WriteCalcRes(LBorder, RBorder, Foo, Result);
            }
            catch
            {
                MessageBox.Show("Расчет выполнен, но база данных недоступна. Логи записаны не были");
            }
        }

        private void numL_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numR_ValueChanged(object sender, EventArgs e)
        {

        }
    }
    
}
