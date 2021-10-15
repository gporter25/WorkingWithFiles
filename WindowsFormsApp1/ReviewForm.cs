using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Operations;
using Operations.SacramentoClasses;
using ValidatingFilesApplication.Classes;

namespace ValidatingFilesApplication
{
    public partial class ReviewForm : Form
    {
        public delegate void OnPositionChange(DataItem sender);
        public event OnPositionChange PositionChange;

        private readonly BindingSource _bindingSource = new BindingSource();
        private List<DataItem> _dataItemsList;

        /// <summary>
        /// Provide access by the calling form to the data presented
        /// </summary>
        public List<DataItem> Data => _dataItemsList;

        /// <summary>
        /// Acceptable values for beat field. In part 2 these will be read from a database reference table.
        /// </summary>
        private readonly List<string> _beatList = new List<string>()
            {
                "1A", "1B", "1C", "2A", "2B", "2C", "3A", "3B", "3C", "3M", "4A",
                "4B", "4C", "5A", "5B", "5C", "6A", "6B", "6C"
            } ;

        private readonly DataItem _currentItem;

        public ReviewForm()
        {
            InitializeComponent();
        }

        public ReviewForm(List<DataItem> dataItemsList, DataItem currentItem)
        {
            InitializeComponent();

            _dataItemsList = dataItemsList;
            _currentItem = currentItem;
            Shown += ReviewForm_Shown;
        }

        private void ReviewForm_Shown(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            // ReSharper disable once PossibleNullReferenceException
            ((DataGridViewComboBoxColumn) dataGridView1.Columns[nameof(beatColumn)]).DataSource = _beatList;

            _bindingSource.DataSource = _dataItemsList;
            dataGridView1.DataSource = _bindingSource;
            bindingNavigator1.BindingSource = _bindingSource;
            dataGridView1.ExpandColumns();

            if (_currentItem.Inspect)
            {
                DataItem Item = _bindingSource.List.OfType<DataItem>().ToList().Find(item => item.Id == _currentItem.Id);

                foreach (DataItem dataItem in _bindingSource)
                {
                    if (dataItem.Id == _currentItem.Id)
                    {
                        break;
                    }
                    else
                    {
                        _bindingSource.MoveNext();
                    }
                }
                
            }

            dataGridView1.EditingControlShowing += DataGridView1_EditingControlShowing;

            _bindingSource.PositionChanged += BindingSourceOnPositionChanged;

        }

        private void BindingSourceOnPositionChanged(object sender, EventArgs e)
        {
            PositionChange?.Invoke((DataItem)_bindingSource.Current);
        }

        /// <summary>
        /// Setup to provide access to changes to the current row, here we are only interested in the beat field.
        /// Other fields would use similar logic for providing valid selections.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.IsComboBoxCell())
            {
                
                if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name == nameof(beatColumn))
                {
                    if (e.Control is ComboBox cb)
                    {
                        cb.SelectionChangeCommitted -= _SelectionChangeCommitted;
                        cb.SelectionChangeCommitted += _SelectionChangeCommitted;
                    }
                }
            }
        }
        /// <summary>
        /// Update current row beat field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_bindingSource.Current !=null)
            {
                if (!string.IsNullOrWhiteSpace(((DataGridViewComboBoxEditingControl)sender).Text))
                {
                    var currentRow = (DataItem) _bindingSource.Current;
                    currentRow.Beat = ((DataGridViewComboBoxEditingControl) sender).Text;
                    currentRow.Inspect = false;
                }
            }
        }
    }
}
