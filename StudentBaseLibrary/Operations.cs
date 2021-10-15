using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBaseLibrary
{
    /// <summary>
    /// File and directory methods for testing
    /// </summary>
    public class Operations
    {
        /// <summary>
        /// Location of files to work with
        /// </summary>
        public static string Folder => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");

        /// <summary>
        /// Location of style sheets on devweb02
        /// </summary>
        public static string RemoteFolder => "\\\\devweb02\\HTTP\\headfoot\\css";

        public static bool RemoteFolderExists => Directory.Exists(RemoteFolder);

        /// <summary>
        /// Get all .csv files from <see cref="Folder"/>
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDelimitedFiles()
        {
            return Directory.GetFiles(Folder, "*.csv").ToList();
        }

        /// <summary>
        /// Get all style sheets from <see cref="RemoteFolder"/>
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRemoteStyleSheetFiles()
        {
            return Directory.GetFiles(RemoteFolder, "*.css").ToList();
        }

        /// <summary>
        /// Get two types of files, json and csv
        /// </summary>
        /// <returns></returns>
        public static List<FileInfo> GetDelimitedAndJsonFiles()
        {
            return new DirectoryInfo(Folder).GetFilesByExtensions("*.json", "*.csv").ToList();
        }

        /// <summary>
        /// Read a file, in this case a json file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> ReadJsonFile(string fileName)
        {
            return File.ReadAllLines(Path.Combine(Folder, fileName)).ToList();
        }

        /// <summary>
        /// Append lines to an existing file, if the file does not exists we create and populate
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> AppendLines(string fileName)
        {
            // get lines in the file
            var lines = ReadJsonFile(fileName);

            // append some lines
            File.AppendAllLines(Path.Combine(Folder,fileName), 
                Enumerable.Range(1, 12).Select((index) => DateTimeFormatInfo.CurrentInfo.GetMonthName(index)));

            // return file with appended lines
            return ReadJsonFile(fileName);

        }
    }
}
