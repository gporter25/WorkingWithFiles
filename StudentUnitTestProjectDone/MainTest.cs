using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentBaseLibrary;
using StudentUnitTestProjectDone.Base;

namespace StudentUnitTestProjectDone
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public partial class MainTest : TestBase
    {

        [TestMethod]
        [TestTraits(Trait.Delimited)]
        public void GetCommaDelimitedWithFullPath()
        {
            var files = Operations.GetDelimitedFiles();
            Assert.AreEqual(files.Count,5);

            foreach (var file in files)
            {
                Debug.WriteLine(file);
            }
        }

        [TestMethod]
        [TestTraits(Trait.Delimited)]
        public void GetCommaDelimitedFileNameOnly()
        {
            var files = Operations.GetDelimitedFiles().Select(Path.GetFileName).ToList();

            Assert.AreEqual(files.Count, 5);

            foreach (var file in files)
            {
                Debug.WriteLine(file);
            }

        }

        [TestMethod]
        [TestTraits(Trait.MultipleFileTypes)]
        public void GetDelimitedAndJsonWithFullPath()
        {
            var files = Operations.GetDelimitedAndJsonFiles();
            Assert.AreEqual(files.Count, 8);


            Debug.WriteLine("CSV files");
            var delimitedFiles = files.Where(fileInfo => Path.GetExtension(fileInfo.Name) == ".csv");
            Assert.AreEqual(delimitedFiles.Count(), 5);
            Debug.WriteLine(string.Join(" | ", delimitedFiles.Select(x => Path.GetFileName(x.Name))));

            Debug.WriteLine("");

            Debug.WriteLine("JSON files");
            var jsonFiles = files.Where(fileInfo => Path.GetExtension(fileInfo.Name) == ".json");
            Assert.AreEqual(jsonFiles.Count(), 3);
            Debug.WriteLine(string.Join(" | ", jsonFiles.Select(fileInfo => Path.GetFileName(fileInfo.Name))));

        }

        [TestMethod]
        [TestTraits(Trait.RemoteFiles)]
        public void RemoteFolderExits()
        {
            Assert.IsTrue(Operations.RemoteFolderExists);
            var files = Operations.GetRemoteStyleSheetFiles();
            Assert.AreEqual(files.Count,8);
        }

        [TestMethod]
        [TestTraits(Trait.FileRead)]
        public void ReadJsonFile()
        {
            Assert.AreEqual(Operations.ReadJsonFile("applications.json").Count, 10);
        }

        [TestMethod]
        [TestTraits(Trait.FileReadWrite)]
        public void AppendLinesTest()
        {
            Assert.AreEqual(Operations.AppendLines("applications.json").Count, 21);
        }

        [TestMethod]
        [TestTraits(Trait.PlaceHolder)]
        public void FileExists()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.UpperFolder(4), "Files", "applications.json");
            Debug.WriteLine(File.Exists(file));
        }




    }

}