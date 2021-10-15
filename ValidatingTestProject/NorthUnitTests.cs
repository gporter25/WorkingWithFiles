using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ValidatingTestProject.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Operations.NorthWindClasses;
using ValidatingTestProject.Classes;

namespace ValidatingTestProject
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public partial class NorthUnitTests : TestBase
    {
        
        /// <summary>
        /// Read perfect file
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.NorthWindOperations)]
        public void SimpleReadWithAddEventTest()
        {
            NorthOperations.ReadLineHandler += NorthOperationsOnReadLineHandler;
            NorthOperations.DelegateReadFile2(FileName);
            NorthOperations.ReadLineHandler -= NorthOperationsOnReadLineHandler;

            var test = CustomerItemsList;
            Assert.AreEqual(CustomerItemsList.Count, 16);

        }
        

        [TestMethod]
        [TestTraits(Trait.NorthWindOperations)]
        public void HasEmptyLinesTest()
        {
            var list = NorthOperations.DelegateReadFile1(FileName);
            Assert.AreEqual(list.Count, 16);
        }

        [TestMethod]
        [TestTraits(Trait.NorthWindOperations)]
        public void IncorrectDeliminatorsTest()
        {
            List<int> expected = new List<int>() { 3, 7 };
            NorthOperations.ReadLineErrorHandler += NorthOperationsOnReadLineErrorHandler;
            NorthOperations.DelegateReadFile2(FileName);

            CollectionAssert.AreEqual(expected,InvalidIndices);

        }

        [TestMethod]
        [TestTraits(Trait.NorthWindOperations)]
        public void TextFieldParserCleanTest()
        {
            var list = NorthOperations.TextFileParser(FileName);

            Assert.AreEqual(list.Count, 16);

        }
        [TestMethod]
        [TestTraits(Trait.NorthWindOperations)]
        public void TextFieldParserWrongTypeTest()
        {
            NorthOperations.ReadLineErrorHandler += NorthOperationsOnReadLineErrorHandler;
            List<CustomerItem> list = NorthOperations.TextFileParser(FileName);
            NorthOperations.ReadLineErrorHandler -= NorthOperationsOnReadLineErrorHandler;
            Assert.AreEqual(InvalidIndices.Count, 1);
            
        }
    }




}