using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using BaseLibrary;

// For TextFieldParser class
using Microsoft.VisualBasic.FileIO;
using Operations.SacramentoClasses;

namespace Operations
{
    /// <summary>
    /// Sample file operations
    /// </summary>
    public class FileOperations : BaseExceptionProperties
    {

        private List<int> _districtValidItems = new List<int>() { 1, 2, 3, 4, 5, 6 };

        /// <summary>
        /// Load file via OleDb
        /// </summary>
        /// <remarks>
        /// This method is fine if each column data is the correct type,
        /// if not then a manual parse (as shown with StreamReader and TextFieldParser are better chooses)
        /// </remarks>
        public (DataTable table, Exception exception) LoadCsvFileOleDb(string inputFileName)
        {
            var connectionString = 
                $@"Provider=Microsoft.Jet.OleDb.4.0; " + 
                $"Data Source={Path.GetDirectoryName(inputFileName)};Extended Properties=\"Text;HDR=YES;FMT=Delimited\"";

            var table = new DataTable();

            try
            {
                using (var cn = new OleDbConnection(connectionString))
                {
                    cn.Open();

                    var selectStatement = "SELECT * FROM [" + Path.GetFileName(inputFileName) + "]";

                    using (var adapter = new OleDbDataAdapter(selectStatement, cn))
                    {
                        var ds = new DataSet("Demo");
                        var recordCount = adapter.Fill(ds); 

                        ds.Tables[0].TableName = Path.GetFileNameWithoutExtension(inputFileName);
                        table = ds.Tables[0];

                    }
                }
            }
            catch (Exception exceptionLocal)
            {
                return (null, exceptionLocal);
            }

            return (table, null);
        }

        /// <summary>
        /// Load file via StreamReader
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <returns></returns>
        public (bool Success, List<DataItem>, List<DataItemInvalid>) LoadCsvFileStreamReader(string inputFileName)
        {
            if (!File.Exists(inputFileName))
            {
                mHasException = true;
                mLastException = new FileNotFoundException($"Missing {inputFileName}");

                return (mHasException, new List<DataItem>(),new List<DataItemInvalid>() );
            }

            
            mHasException = false;

            var validRows = new List<DataItem>();
            var invalidRows = new List<DataItemInvalid>();
            var validateBad = 0;

            int index = 0;


            int district = 0;
            int grid = 0;
            int ucrNcicCode = 0;
            int nCode = 0;
            float latitude = 0;
            float longitude = 0;

            try
            {
                using (var readFile = new StreamReader(inputFileName))
                {
                    string line;
                    string[] parts;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        parts = line.Split(',');
                        index += 1;

                        if (parts == null)
                        {
                            break;
                        }

                        index += 1;
                        validateBad = 0;

                        if (parts.Length != 9)
                        {
                            invalidRows.Add(new DataItemInvalid() { Row = index, Line = string.Join(",", parts) });
                            continue;

                        }

                        // Skip first row which in this case is a header with column names
                        if (index <= 1) continue;
                        /*
                         * These columns are checked for proper types
                         */

                        var validRow = ValidSingleRow(parts, out var cdatetime, ref latitude, ref longitude, ref district, ref grid, ref ucrNcicCode);

                        /*
                         * Questionable fields
                         */
                        if (string.IsNullOrWhiteSpace(parts[1]))
                        {
                            validateBad += 1;
                        }
                        if (string.IsNullOrWhiteSpace(parts[3]))
                        {
                            validateBad += 1;
                        }

                        // NICI code must be 909 or greater
                        if (nCode < 909)
                        {
                            validateBad += 1;
                        }

                        if (validRow)
                        {

                            validRows.Add(new DataItem()
                            {
                                Id = index,
                                Date = cdatetime,
                                Address = parts[1],
                                District = district,
                                Beat = parts[3],
                                Grid = grid,
                                Description = parts[5],
                                NcicCode = nCode,
                                Latitude = latitude,
                                Longitude = longitude,
                                Inspect = validateBad > 0
                            });

                            

                        }
                        else
                        {
                            // fields to review in specific rows
                            invalidRows.Add(new DataItemInvalid() { Row = index, Line = string.Join(",", parts) });
                        }
                    }
                }
            }
            catch (Exception exceptionLocal)
            {
                mHasException = true;
                mLastException = exceptionLocal;
            }



            return (IsSuccessFul, validRows, invalidRows);

        }
        /// <summary>
        /// Load file via VB TextFieldParser
        /// </summary>
        /// <returns></returns>
        public (bool Success, List<DataItem>, List<DataItemInvalid>, int EmptyLineCount) LoadCsvFileTextFieldParser(string pInputFileName)
        {
            mHasException = false;


            var validRows = new List<DataItem>();
            var invalidRows = new List<DataItemInvalid>();
            // ReSharper disable once TooWideLocalVariableScope
            var validateBad = 0;

            int index = 0;

            int district = 0;
            int grid = 0;
            int ucrNcicCode = 0;
            float latitude = 0;
            float longitude = 0;

            var emptyLineCount = 0;
            // ReSharper disable once TooWideLocalVariableScope
            var line = "";

            try
            {
                /*
                 * If interested in blank line count
                 */
                using (var reader = File.OpenText(pInputFileName))
                {
                    while ((line = reader.ReadLine()) != null) // EOF
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            emptyLineCount++;
                        }
                    }
                }

                using (var parser = new TextFieldParser(pInputFileName))
                {
                    
                    parser.Delimiters = new[] { "," };
                    while (true)
                    {

                        string[] parts = parser.ReadFields();

                        if (parts == null)
                        {
                            break;
                        }

                        index += 1;
                        validateBad = 0;

                        if (parts.Length != 9)
                        {
                            
                            invalidRows.Add(new DataItemInvalid()
                            {
                                Row = index, 
                                Line = string.Join(",", parts)
                            });

                            continue;

                        }

                        // Skip first row which in this case is a header with column names
                        if (index <= 1) continue;

                        /*
                         * These columns are checked for proper types
                         */
                        var validRow = ValidSingleRow(parts, out var cdatetime, ref latitude, ref longitude, ref district, ref grid, ref ucrNcicCode);

                        /*---------------------------------------------------------------
                         * Questionable fields
                         ---------------------------------------------------------------*/
                        if (string.IsNullOrWhiteSpace(parts[1]))
                        {
                            validateBad += 1;
                        }
                        if (string.IsNullOrWhiteSpace(parts[3]))
                        {
                            validateBad += 1;
                        }

                        // NICI code must be 909 or greater
                        if (ucrNcicCode < 909)
                        {
                            validateBad += 1;
                        }

                        if (validRow)
                        {

                            validRows.Add(new DataItem()
                            {
                                Id = index,
                                Date = cdatetime,
                                Address = parts[1],
                                District = district,
                                Beat = parts[3],
                                Grid = grid,
                                Description = parts[5],
                                NcicCode = ucrNcicCode,
                                Latitude = latitude,
                                Longitude = longitude,
                                Inspect = validateBad > 0
                            });


                        }
                        else
                        {
                            // fields to review in specific rows
                            invalidRows.Add(new DataItemInvalid()
                            {
                                Row = index, 
                                Line = string.Join(",", parts)
                            });
                        }

                    }
                }

            }
            catch (Exception exceptionLocal)
            {
                mHasException = true;
                mLastException = exceptionLocal;
            }


            return (IsSuccessFul, validRows, invalidRows,emptyLineCount);

        }

        private static bool ValidSingleRow(
            IReadOnlyList<string> parts, 
            out DateTime cdatetime, 
            ref float latitude, 
            ref float longitude, 
            ref int district, 
            ref int grid, 
            ref int ucrNcicCode)
        {
            bool validRow =
                DateTime.TryParse(parts[0], out cdatetime) &&
                float.TryParse(parts[7].Trim(), out latitude) &&
                float.TryParse(parts[8].Trim(), out longitude) &&
                int.TryParse(parts[2], out district) &&
                int.TryParse(parts[4], out grid) &&
                !string.IsNullOrWhiteSpace(parts[5]) &&
                int.TryParse(parts[6], out ucrNcicCode);

            return validRow;
        }
    }
}
