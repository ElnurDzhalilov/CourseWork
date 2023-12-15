using DB_Lib.ExtAPI;
using DB_Lib.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace ElnurCourseWork
{
    public partial class FormUsageLog : Form
    {
        public FormUsageLog()
        {
            InitializeComponent();

            try
            {
                dataGridView1.DataSource = ExtAPI.GetCalcRes();
                RenameColumns();

            }
            catch
            {
                MessageBox.Show("База данных недоступна");
            }

        }

        private void RenameColumns()
        {
            if(dataGridView1.Columns.Count == 5)
            {
                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Gray;

                dataGridView1.Columns[nameof(CalcRes.Id)].HeaderText = "Ид";
                dataGridView1.Columns[nameof(CalcRes.LBorder)].HeaderText = "Лев. граница";
                dataGridView1.Columns[nameof(CalcRes.RBorder)].HeaderText = "Прав. граница";
                dataGridView1.Columns[nameof(CalcRes.Foo)].HeaderText = "Функция";
                dataGridView1.Columns[nameof(CalcRes.Result)].HeaderText = "Результат";
            }
            
        }
    }
}
