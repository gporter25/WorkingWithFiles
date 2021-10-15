using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Operations;
using Operations.SacramentoClasses;
using ValidatingFilesApplication.Classes;

namespace ValidatingFilesApplication
{
    public partial class MainForm : Form
    {
        
        private readonly BindingSource _validDataBindingSource = new BindingSource();

        private readonly string _inputFileName = 
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SacramentocrimeJanuary2006.csv");


        public MainForm()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Width = 1500;
            CenterToScreen();
        }

        private void cmdProcess_Click(object sender, EventArgs e)
        {
            var ops = new FileOperations();
            var (success, validRows, invalidRows, _) = ops.LoadCsvFileTextFieldParser(_inputFileName);

            if (!success)
            {
                MessageBox.Show(ops.LastExceptionMessage);
                return;
            }

            IEnumerable<int> niciCodes = validRows.Select(item => item.NcicCode);

            _validDataBindingSource.DataSource = validRows;
            dataGridViewMain.DataSource = _validDataBindingSource;
            bindingNavigator1.BindingSource = _validDataBindingSource;

            #region configure DataGridView columns
            dataGridViewMain.Columns["id"].HeaderText = "Row index";
            dataGridViewMain.Columns["inspect"].DisplayIndex = 0;
            dataGridViewMain.Columns["Address"].Width = 300;
            dataGridViewMain.Columns["Description"].Width = 215;
            dataGridViewMain.Columns["line"].Visible = false;
            #endregion

            cboInspectRowIndices.DataSource = validRows.Where(item => item.Inspect).Select(item => item.Id).ToList();

            dataGridViewInvalid.DataSource = invalidRows;
            dataGridViewInvalid.ExpandColumns();

        }
        /// <summary>
        /// Find the selected row in the DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInspectRow_Click(object sender, EventArgs e)
        {
            if (cboInspectRowIndices.DataSource == null) return;

            var item = _validDataBindingSource.List.OfType<DataItem>().ToList()
                .Find(dataItem => dataItem.Id == Convert.ToInt32(cboInspectRowIndices.Text));

            var pos = _validDataBindingSource.IndexOf(item);
            if (pos > -1)
            {
                _validDataBindingSource.Position = pos;
            }
        }
        /// <summary>
        /// Display rows marked for inspection and allow edits on Beat field. This has been 
        /// kept to one field to allow for easy learning as the basics are there e.g. casting
        /// of items setting the inspect field to false which signified changes are to be
        /// reflected in the DataGridView so that later these records may be pushed to the
        /// database (part 2 of this series).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdReview_Click(object sender, EventArgs e)
        {
            if (_validDataBindingSource.DataSource == null) return;     
            
            var results = ((List<DataItem>) _validDataBindingSource.DataSource).Where(item => item.Inspect).ToList();
            var f = new ReviewForm(results, (DataItem)_validDataBindingSource.Current);
            
            f.PositionChange += ItemPositionChange;

            try
            {               
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // get changed rows
                    var changedData = f.Data.Where(item => item.Inspect == false).ToList();
                    // bail out if no changed rows
                    if (changedData.Count <= 0) return;

                    // update rows in DataGridView
                    foreach (var dataItem in changedData)
                    {
                        var Item = _validDataBindingSource.List.OfType<DataItem>().ToList() .Find(item => item.Id == dataItem.Id);
                        Item.Inspect = false;
                        Item.Beat = dataItem.Beat;
                    }

                    // update ComboBox to excluded updated rows from review form.
                    results = ((List<DataItem>)_validDataBindingSource.DataSource).Where(item => item.Inspect).ToList();
                    cboInspectRowIndices.DataSource = results;

                }
            }
            finally
            {
                f.PositionChange -= ItemPositionChange;
                f.Dispose();
            }
        }

        private void ItemPositionChange(DataItem current)
        {
            var position = _validDataBindingSource.IndexOf(current);
            if (position > -1)
            {
                _validDataBindingSource.Position = position;
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OleDbLoadButton_Click(object sender, EventArgs e)
        {
            var operations = new FileOperations();

            var (table, exceptions) = operations.LoadCsvFileOleDb(_inputFileName);

            if (exceptions != null)
            {
                MessageBox.Show($"Issue loading data\n{exceptions.Message}");
                return;
            }
            
            var f = new OleDbReadForm(table);
            try
            {
                f.ShowDialog();
            }
            finally
            {
                f.Dispose();
            }
        }

        /// <summary>
        /// Has no true use other than showing how to get to the checked rows
        /// which should never be touched, instead always access via the underlying data source
        ///
        /// We can access each row into <see cref="_validDataBindingSource"/> by it's index
        /// of the DataGridViewRow.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InspectButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.DataSource != null)
            {
                List<DataGridViewRow> items = dataGridViewMain.GetCheckedRows("Inspect");
                MessageBox.Show($"There are {items.Count} checked currently");
            }
            else
            {
                MessageBox.Show("No data source");
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is where a help screen should appear","Help");
        }
    }
}
