using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValidatingFilesApplication.Classes
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
        public static List<DataGridViewRow> GetCheckedRows(this DataGridView sender, string columnName) 
            => sender.Rows.Cast<DataGridViewRow>().Where(row => !row.IsNewRow && Convert.ToBoolean(row.Cells[columnName].Value)).ToList();

        /// <summary>
        /// Create a string array from DataGridView rows using by default a comma
        /// between cell data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        /// <remarks>
        /// Complex one liner.
        ///  * Important check to detect if there is a new row in the mix via !row.IsNewRow
        /// 
        ///  * <see cref="Array.ConvertAll"/> is the same as in
        ///       https://github.com/karenpayneoregon/kp-converters/blob/master/ConverterLibrary/LanguageExtensions/NumericArrays.cs#L50
        /// 
        ///  * Ternary operator ensures null values are done as an empty string
        ///       ((cell.Value == null) ? "" : cell.Value.ToString())
        /// 
        /// </remarks>
        public static string[] CreateRowsArray(this DataGridView sender, string delimiter = ",") =>
        (
            from row in sender.Rows.Cast<DataGridViewRow>()
            where !row.IsNewRow
            let rowItem = string.Join(delimiter, Array.ConvertAll(row.Cells.Cast<DataGridViewCell>().ToArray(),
                cell => ((cell.Value == null) ? "" : cell.Value.ToString())))
            select rowItem
        ).ToArray();

        /// <summary>
        /// Export row data comma delimited to a text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="fileName">path combined with file name and extension</param>
        public static void ExportCsv(this DataGridView sender, string fileName)
        {
            string[] rowData = sender.CreateRowsArray();
            File.WriteAllLines(fileName, rowData);
        }

        /// <summary>
        /// Simple export header and data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="fileName"></param>
        /// <param name="delimiter"></param>
        public static void ExportRows(this DataGridView sender, string fileName, string delimiter = ",")
        {
            if (sender.RowCount > 0)
            {
                var sb = new StringBuilder();

                var headers = sender.Columns.Cast<DataGridViewColumn>();
                sb.AppendLine(string.Join(delimiter, headers.Select(column => column.HeaderText)));

                foreach (DataGridViewRow row in sender.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        var cells = row.Cells.Cast<DataGridViewCell>();
                        sb.AppendLine(string.Join(delimiter, cells.Select(cell => cell.Value)));
                    }
                }
                File.WriteAllText(fileName, sb.ToString());
            }
            else
            {
                // Do nothing
            }
        }
    }
}
