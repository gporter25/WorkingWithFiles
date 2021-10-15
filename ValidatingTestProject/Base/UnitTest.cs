using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace ValidatingTestProject
{
    public partial class SacramentoUnitTest
    {
        private readonly string _inputFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SacramentocrimeJanuary2006.csv");
        private readonly string _inputFileNameSmall = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SacramentocrimeJanuary2006_1.csv");

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
    }
}
