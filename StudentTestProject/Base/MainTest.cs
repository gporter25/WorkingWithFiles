using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentBaseLibrary;

// ReSharper disable once CheckNamespace
namespace StudentUnitTestProject
{
    public partial class MainTest
    {
        [TestInitialize]
        public void Initialization()
        {
            /*
             * Since one or more test will alter applications.json we get a fresh copy
             */
            var file = Path.Combine(DirectoryHelper.StudentFilesFolder(), "Files", "applications.json");
            File.Copy(file, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "applications.json"), true);

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

    }

}