using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentBaseLibrary
{
    public static class FileExtensions
    {
        /// <summary>
        /// Get files by multiple file extensions in a folder
        /// </summary>
        /// <param name="sender">DirectoryInfo instance of a folder to work with</param>
        /// <param name="extensions">string array of extensions</param>
        /// <returns>IEnumerable&lt;FileInfo&gt; of files matching extensions passed in</returns>
        /// <remarks>
        /// If the pattern is *. we remove the * symbol
        /// </remarks>
        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo sender, params string[] extensions)
        {

            var allowedExtensions = new HashSet<string>(
                extensions.Select(extension => extension.Replace("*","")), 
                StringComparer.OrdinalIgnoreCase);

            return sender.EnumerateFiles().Where(fileInfo => allowedExtensions.Contains(fileInfo.Extension));
        }
    }
}