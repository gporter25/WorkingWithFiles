using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp1.Classes;
using WindowsFormsApp1.Properties;


namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private readonly BindingSource _bsValidData = new BindingSource();
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

            // uncomment the next two lines to see how OleDb handles reading lines.
            //dataGridView1.DataSource = ops.LoadCsvFileOleDb();
            //return;

            var (success, validRows, invalidRows, _) = ops.LoadCsvFileTextFieldParser();

            if (!success)
            {
                MessageBox.Show(ops.LastExceptionMessage);
                return;
            }

            var results = validRows.Select(item => item.NcicCode);
            _bsValidData.DataSource = validRows;
            dataGridView1.DataSource = _bsValidData;

            dataGridView1.Columns["id"].HeaderText = "Row index";
            dataGridView1.Columns["inspect"].DisplayIndex = 0;
            dataGridView1.Columns["Address"].Width = 300;
            dataGridView1.Columns["Description"].Width = 215;
            dataGridView1.Columns["line"].Visible = false;

            cboInspectRowIndices.DataSource = 
                validRows.Where(item => item.Inspect).Select(item => item.Id).ToList();

            dataGridView2.DataSource = invalidRows;

            dataGridView2.ExpandColumns();

        }
        /// <summary>
        /// Find the selected row in the DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInspectRows_Click(object sender, EventArgs e)
        {
            if (cboInspectRowIndices.DataSource == null) return;

            var item = _bsValidData.List.OfType<DataItem>()
                .ToList()
                .Find(dataItem => dataItem.Id == Convert.ToInt32(cboInspectRowIndices.Text));

            var pos = _bsValidData.IndexOf(item);
            if (pos > -1)
            {
                _bsValidData.Position = pos;
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
            if (_bsValidData.DataSource == null) return;     
            
            var results = 
                ((List<DataItem>) _bsValidData.DataSource).Where(item => item.Inspect).ToList();

            var f = new ReviewForm(results);

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
                        var Item = _bsValidData.List.OfType<DataItem>().ToList() .Find(item => item.Id == dataItem.Id);
                        Item.Inspect = false;
                        Item.Beat = dataItem.Beat;
                    }

                    // update ComboBox to excluded updated rows from review form.
                    results = ((List<DataItem>)_bsValidData.DataSource).Where(item => item.Inspect).ToList();
                    cboInspectRowIndices.DataSource = results;

                }
            }
            finally
            {
                f.Dispose();
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OleDbLoadButton_Click(object sender, EventArgs e)
        {
            var operations = new FileOperations();

            var (table, exceptions) = operations.LoadCsvFileOleDb();

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
    }
}
