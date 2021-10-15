using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileDirOperationsUnitTest.Base;
using static BaseLibrary.FoldersOperations;


namespace FileDirOperationsUnitTest
{
    [TestClass]
    public partial class DirectoryTest : TestBase
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        /// <summary>
        /// This is more of a code sample which
        ///     * Demonstrates access denied scanning folders
        ///     * Recursive actions done asynchronously on folders
        ///     * Get files in a folder using an extension method for multiple extensions
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestTraits(Trait.Exceptions)]
        //[Ignore]
        public async Task TraverseFiles()
        {
            /*
             * C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\xtp
             * Access denied 'Access to the path 'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\xtp' is denied.'
             */
            var folderName = "";

            var folders = new List<string>
            {
                "C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\DATA",
                "C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\DATA\\xtp",
                "C:\\OED\\Dotnetland\\karenpayneoregon.github.io" // not Karen, this will not exists
            };

            folderName = folders[2];

            Assert.IsTrue(Directory.Exists(folderName));


            var directoryInfo = new DirectoryInfo(folderName);

            OnTraverseEvent += TraverseEvent;
            UnauthorizedAccessEvent += UnauthorizedAccessExceptionEvent;
            OnTraverseIncludeFolderEvent += TraverseExcludeFolderEvent;
            ProcessFilesEvent += OnProcessFilesEvent;

            try
            {
                await RecursiveFolders(directoryInfo, new[] { "*.css", "*.html", "*.md" }, _cancellationTokenSource.Token);

                if (Cancelled)
                {
                    Console.WriteLine("You cancelled the operation");
                }
                else
                {
                    Console.WriteLine("Done");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {

                OnTraverseEvent -= TraverseEvent;
                UnauthorizedAccessEvent -= UnauthorizedAccessExceptionEvent;
                OnTraverseIncludeFolderEvent -= TraverseExcludeFolderEvent;
                ProcessFilesEvent -= OnProcessFilesEvent;

            }
        }







    }
}
