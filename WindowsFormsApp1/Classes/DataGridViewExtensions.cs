using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Classes
{
    public static class DataGridViewExtensions
    {
        /// <summary>
        /// Expand all columns excluding in this case Orders column
        /// </summary>
        /// <param name="sender"></param>
        /// <remarks>
        /// 
        /// </remarks>
        public static async Task ExpandColumnsAsync(this DataGridView sender)
        {
            /*
             * the following works well with a few columns but with a many columns and rows will be
             * slow
             */
            //sender.Columns.Cast<DataGridViewColumn>().ToList().ForEach(col => col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells);


            foreach (var column in sender.Columns.Cast<DataGridViewColumn>())
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                await Task.Delay(1);
            }
        }
        public static void ExpandColumns(this DataGridView sender)
        {
            sender.Columns.Cast<DataGridViewColumn>().ToList().ForEach(col => col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells);
        }

        /// <summary>
        /// Used to determine if the current cell type is a ComboBoxCell
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool IsComboBoxCell(this DataGridViewCell sender)
        {
            var result = false;
            if (sender.EditType != null)
            {
                if (sender.EditType == typeof(DataGridViewComboBoxEditingControl))
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
