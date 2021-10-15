using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BaseLibrary.FoldersOperations;

// ReSharper disable once CheckNamespace
namespace FileDirOperationsUnitTest
{
    public partial class DirectoryTest
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        [TestInitialize]
        public void Initialization()
        {

        }
        /// <summary>
        /// Perform any initialize for the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }


        private void TraverseEvent(string status)
        {
            //Console.WriteLine($"{status}");
        }

        private void OnProcessFilesEvent(string sender)
        {
            Console.WriteLine(sender);
        }

        private void TraverseExcludeFolderEvent(string sender)
        {
            //Console.WriteLine($"{sender}");
            ProcessDirectory(sender);
        }

        private void UnauthorizedAccessExceptionEvent(string folderName)
        {
            Debug.WriteLine($"{nameof(UnauthorizedAccessExceptionEvent)}: {folderName}");
        }
    }
}
