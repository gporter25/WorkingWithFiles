using System;
using System.Collections.Generic;
using System.IO;

namespace StudentBaseLibrary
{

    public static class DirectoryHelper
    {
        /// <summary>
        /// Provides access to folders above folderName using level
        /// to climb up.
        /// </summary>
        /// <param name="folderName">root folder</param>
        /// <param name="level">How many levels to go above folderName</param>
        /// <returns>
        /// Folder name matching level
        /// </returns>
        /// <remarks>
        /// Example where a Windows Service installer finds the service using this method
        /// https://github.com/karenpayneoregon/WindowsInstaller/tree/master
        /// </remarks>
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

 
        public static string StudentFilesFolder() => AppDomain.CurrentDomain.BaseDirectory.UpperFolder(4);

    }
}