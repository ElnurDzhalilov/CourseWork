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

namespace ElnurCourseWork
{
    public partial class Form1 : Form
    {
        public LineSeries ls_fx;
        public LineSeries ls_dx;
        public LineSeries ls_extremums;
        public LineSeries ls_non_extremums;

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            timer1.Tick += (sender, e) => Text += ".";

            cartesianChart1.Zoom = ZoomingOptions.Xy;
            cartesianChart1.DisableAnimations = true;
            
            var mapper = Mappers.Xy<MyPoint>()
            .X(x => x.X)
            .Y(y => y.Y);
            Charting.For<MyPoint>(mapper);
            
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

            ls_fx = new LineSeries()
            {
                Title = "Y =",
                StrokeThickness = 1,
                PointGeometrySize = 0,
                Values = new ChartValues<MyPoint>(),
                Fill = Brushes.Transparent
            };

            ls_dx = new LineSeries()
            {
                Title = "dx =",
                StrokeThickness = 1,
                PointGeometrySize = 0,
                Values = new ChartValues<MyPoint>(),
                Fill = Brushes.Transparent,
            };

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

            cartesianChart1.Series = new SeriesCollection
            {
                ls_fx,
                ls_dx,
                ls_extremums,
                ls_non_extremums
            };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Function mxFunc = new Function($"f(x) = {tbParce.Text}");
            if (!mxFunc.checkSyntax())
            {
                textBox1.Text = "Выражение некорректно";
                return;
            }

            ls_fx.Values.Clear();
            ls_dx.Values.Clear();
            ls_extremums.Values.Clear();
            ls_non_extremums.Values.Clear();

            ExtremumFinder finder = new ExtremumFinder();

            finder.LBorder = (double)numL.Value;
            finder.RBorder = (double)numR.Value;

            //finder.f = Foo;
            finder.f = 
                x => mxFunc.calculate(x);

            LockUI(true);

            var answer = await finder.GetAnswerAsync();  

            if(answer.Error != null)
            {
                textBox1.Text = answer.Error;
                LockUI(false);
                return;
            }

            for(int i = 0; i < answer.X_es.Count; i++)
            {
                if((i & 0b1111) == 0)
                {
                    ls_fx.Values.Add(  new MyPoint(answer.X_es[i], answer.Y_es[i])  );
                    ls_dx.Values.Add(  new MyPoint(answer.X_es[i], answer.dX_es[i])  );
                }
            }

            textBox1.Text = string.Empty;
            foreach (var item in answer.res)
            {
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

            LockUI(false);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            cartesianChart1.AxisX[0].MinValue = double.NaN;
            cartesianChart1.AxisX[0].MaxValue = double.NaN;
            cartesianChart1.AxisY[0].MinValue = double.NaN;
            cartesianChart1.AxisY[0].MaxValue = double.NaN;
        }

        private void LockUI(bool @lock)
        {
            if (@lock)
            {
                Text = "Расчет";
                timer1.Start();
                Cursor.Current = Cursors.WaitCursor;
            }
            else
            {
                timer1.Stop();
                Text = $"[ {tbParce.Text} ] Готово";
                Cursor.Current = Cursors.Default;
            }

            foreach (var c in Controls)
            {
                if(c is Control control)
                {
                    control.Enabled = !@lock;
                }
            }
        }

        //-----------------------------------------------------
        static void DrawControlToImage(Control ctrl, System.Drawing.Image img)
        {
            var sourceRect = ctrl.ClientRectangle;
            var targetSize = new System.Drawing.Size(img.Width, img.Height);
            using (var tmp = new System.Drawing.Bitmap(sourceRect.Width, sourceRect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                ctrl.DrawToBitmap(tmp, sourceRect);
                using (var g = System.Drawing.Graphics.FromImage(img))
                {
                    g.DrawImage(tmp, new System.Drawing.Rectangle(System.Drawing.Point.Empty, targetSize));
                    //g.CopyFromScreen(
                    //    ctrl.Location,
                    //    new System.Drawing.Point(0, 0),
                    //    ctrl.Size
                    //    );
                }
            }
        }
        //------------------------------------------------
        private void графикВИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {         
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "BMP (*.bmp)|*.bmp";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            if ((myStream = saveFileDialog1.OpenFile()) != null)
            {
                var c = cartesianChart1;
                var img = new System.Drawing.Bitmap(c.Width, c.Height);
                DrawControlToImage(c, img);
                img.Save(myStream, System.Drawing.Imaging.ImageFormat.Bmp);
                //img.Save("graphFolder.Bmp", System.Drawing.Imaging.ImageFormat.Bmp);

                myStream.Close();
            }
        }

        private void сохранитьВходныеПараметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            if ((myStream = saveFileDialog1.OpenFile()) != null)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(InputParam));
                xmlSerializer.Serialize(myStream,
                    new InputParam()
                    {
                        LBorder = numL.Value,
                        RBorder = numR.Value,
                        Foo = tbParce.Text
                    }
                    );

                myStream.Close();
            }
        }

        private void загрузитьВходныеПарамертыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            if ((myStream = openFileDialog1.OpenFile()) != null)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(InputParam));
                object res = null;
                try
                {
                    res = xmlSerializer.Deserialize(myStream);
                }
                catch
                {
                    MessageBox.Show("Ошибка чтения файла");
                }

                if (res is InputParam input)
                {
                    numL.Value = input.LBorder;
                    numR.Value = input.RBorder;
                    tbParce.Text = input.Foo;
                }
                else MessageBox.Show("Файл не распознан");

                myStream.Close();
            }
        }
    }
    
}
