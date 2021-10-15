using System;
using System.Collections.Generic;
using System.IO;

namespace BaseLibrary
{
    public static class DirectoryHelper
    {
        public static string UpperFolder(this string folderName, int level)
        {
            var folderList = new List<string>();

            while (!string.IsNullOrWhiteSpace(folderName))
            {
                var parentFolder = Directory.GetParent(folderName);

                if (parentFolder == null) break;

                folderName = Directory.GetParent(folderName)?.FullName;
                folderList.Add(folderName);
            }

            return folderList.Count > 0 && level > 0 ? level - 1 <= folderList.Count - 1 ? 
                folderList[level - 1] : folderName : folderName;
        }

        /// <summary>
        /// Get solution folder path from a project directly beneath the
        /// current solution folder
        /// </summary>
        public static string SolutionFolder() 
            => AppDomain.CurrentDomain.BaseDirectory.UpperFolder(4);

        public static string DatFileName 
            => Path.Combine(SolutionFolder(), 
                "SearchEngine\\Objects\\Publication\\settings.dat");

        public static bool DataFileExists 
            => File.Exists(DatFileName);
    }

}