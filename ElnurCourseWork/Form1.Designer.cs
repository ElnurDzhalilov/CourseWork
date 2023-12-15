namespace ElnurCourseWork
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.btnCalc = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.tbParce = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numL = new System.Windows.Forms.NumericUpDown();
            this.numR = new System.Windows.Forms.NumericUpDown();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.графикВИзображениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьВходныеПараметрыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьВходныеПарамертыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.логиИспользованияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelBorderL = new System.Windows.Forms.Label();
            this.labelBorderR = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numR)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(11, 36);
            this.cartesianChart1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(679, 222);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(11, 262);
            this.btnCalc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(107, 30);
            this.btnCalc.TabIndex = 1;
            this.btnCalc.Text = "Поиск";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(246, 262);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(444, 63);
            this.textBox1.TabIndex = 2;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(123, 262);
            this.btnTest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(107, 30);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Сброс сетки";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnResetChart_Click);
            // 
            // tbParce
            // 
            this.tbParce.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbParce.Location = new System.Drawing.Point(336, 327);
            this.tbParce.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbParce.Name = "tbParce";
            this.tbParce.Size = new System.Drawing.Size(354, 26);
            this.tbParce.TabIndex = 4;
            this.tbParce.Text = "abs(cos(x))";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(231, 331);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Функция от Х";
            // 
            // numL
            // 
            this.numL.DecimalPlaces = 6;
            this.numL.Location = new System.Drawing.Point(11, 330);
            this.numL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numL.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numL.Name = "numL";
            this.numL.Size = new System.Drawing.Size(107, 22);
            this.numL.TabIndex = 6;
            this.numL.Value = new decimal(new int[] {
            4,
            0,
            0,
            -2147483648});
            this.numL.ValueChanged += new System.EventHandler(this.numL_ValueChanged);
            // 
            // numR
            // 
            this.numR.DecimalPlaces = 6;
            this.numR.Location = new System.Drawing.Point(123, 330);
            this.numR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numR.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numR.Name = "numR";
            this.numR.Size = new System.Drawing.Size(107, 22);
            this.numR.TabIndex = 7;
            this.numR.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numR.ValueChanged += new System.EventHandler(this.numR_ValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.логиИспользованияToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(711, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.графикВИзображениеToolStripMenuItem,
            this.сохранитьВходныеПараметрыToolStripMenuItem,
            this.загрузитьВходныеПарамертыToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // графикВИзображениеToolStripMenuItem
            // 
            this.графикВИзображениеToolStripMenuItem.Name = "графикВИзображениеToolStripMenuItem";
            this.графикВИзображениеToolStripMenuItem.Size = new System.Drawing.Size(313, 26);
            this.графикВИзображениеToolStripMenuItem.Text = "График в изображение";
            this.графикВИзображениеToolStripMenuItem.Click += new System.EventHandler(this.графикВИзображениеToolStripMenuItem_Click);
            // 
            // сохранитьВходныеПараметрыToolStripMenuItem
            // 
            this.сохранитьВходныеПараметрыToolStripMenuItem.Name = "сохранитьВходныеПараметрыToolStripMenuItem";
            this.сохранитьВходныеПараметрыToolStripMenuItem.Size = new System.Drawing.Size(313, 26);
            this.сохранитьВходныеПараметрыToolStripMenuItem.Text = "Сохранить входные параметры";
            this.сохранитьВходныеПараметрыToolStripMenuItem.Click += new System.EventHandler(this.сохранитьВходныеПараметрыToolStripMenuItem_Click);
            // 
            // загрузитьВходныеПарамертыToolStripMenuItem
            // 
            this.загрузитьВходныеПарамертыToolStripMenuItem.Name = "загрузитьВходныеПарамертыToolStripMenuItem";
            this.загрузитьВходныеПарамертыToolStripMenuItem.Size = new System.Drawing.Size(313, 26);
            this.загрузитьВходныеПарамертыToolStripMenuItem.Text = "Загрузить входные параметры";
            this.загрузитьВходныеПарамертыToolStripMenuItem.Click += new System.EventHandler(this.загрузитьВходныеПарамертыToolStripMenuItem_Click);
            // 
            // логиИспользованияToolStripMenuItem
            // 
            this.логиИспользованияToolStripMenuItem.Name = "логиИспользованияToolStripMenuItem";
            this.логиИспользованияToolStripMenuItem.Size = new System.Drawing.Size(169, 24);
            this.логиИспользованияToolStripMenuItem.Text = "Логи использования";
            this.логиИспользованияToolStripMenuItem.Click += new System.EventHandler(this.логиИспользованияToolStripMenuItem_Click);
            // 
            // labelBorderL
            // 
            this.labelBorderL.AutoSize = true;
            this.labelBorderL.Location = new System.Drawing.Point(12, 308);
            this.labelBorderL.Name = "labelBorderL";
            this.labelBorderL.Size = new System.Drawing.Size(78, 16);
            this.labelBorderL.TabIndex = 9;
            this.labelBorderL.Text = "Левая гр. Х";
            // 
            // labelBorderR
            // 
            this.labelBorderR.AutoSize = true;
            this.labelBorderR.Location = new System.Drawing.Point(119, 308);
            this.labelBorderR.Name = "labelBorderR";
            this.labelBorderR.Size = new System.Drawing.Size(87, 16);
            this.labelBorderR.TabIndex = 10;
            this.labelBorderR.Text = "Правая гр. Х";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 360);
            this.Controls.Add(this.labelBorderR);
            this.Controls.Add(this.labelBorderL);
            this.Controls.Add(this.numR);
            this.Controls.Add(this.numL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbParce);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.cartesianChart1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Главная форма";
            ((System.ComponentModel.ISupportInitialize)(this.numL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numR)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox tbParce;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numL;
        private System.Windows.Forms.NumericUpDown numR;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem графикВИзображениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьВходныеПараметрыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьВходныеПарамертыToolStripMenuItem;
        private System.Windows.Forms.Label labelBorderL;
        private System.Windows.Forms.Label labelBorderR;
        private System.Windows.Forms.ToolStripMenuItem логиИспользованияToolStripMenuItem;
    }
}

