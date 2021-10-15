using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Classes;

namespace WindowsFormsApp1
{
    public partial class OleDbReadForm : Form
    {
        private readonly DataTable _table;

        public OleDbReadForm()
        {
            InitializeComponent();
        }
        public OleDbReadForm(DataTable table)
        {
            InitializeComponent();

            _table = table;

            Shown += OnShown;
        }

        private async void OnShown(object sender, EventArgs e)
        {

            await Task.Delay(1);

            dataGridView1.DataSource = _table;

            try
            {
                dataGridView1.SuspendLayout();
                await dataGridView1.ExpandColumnsAsync();
            }
            finally
            {
                dataGridView1.ResumeLayout();
            }
            
        }
    }
}
