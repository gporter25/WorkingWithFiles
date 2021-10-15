using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using StudentUnitTestProject.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentBaseLibrary;

namespace StudentUnitTestProject
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

        }

        [TestMethod]
        [TestTraits(Trait.Delimited)]
        public void GetCommaDelimitedFileNameOnly()
        {


        }

        [TestMethod]
        [TestTraits(Trait.MultipleFileTypes)]
        public void GetDelimitedAndJsonWithFullPath()
        {


        }

        [TestMethod]
        [TestTraits(Trait.RemoteFiles)]
        public void RemoteFolderExits()
        {

        }

        [TestMethod]
        [TestTraits(Trait.FileRead)]
        public void ReadJsonFile()
        {

        }

        [TestMethod]
        [TestTraits(Trait.FileReadWrite)]
        public void AppendLinesTest()
        {

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