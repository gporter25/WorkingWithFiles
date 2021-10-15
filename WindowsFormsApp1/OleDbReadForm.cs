using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidatingFilesApplication.Classes;

namespace ValidatingFilesApplication
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

        /// <summary>
        /// Populate data
        ///     DataGridView ScrollBars is set to ScrollBars.None in the designer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnShown(object sender, EventArgs e)
        {
            // ensure button is not grey out
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
                dataGridView1.ScrollBars = ScrollBars.Both;
            }
            
        }
    }
}
