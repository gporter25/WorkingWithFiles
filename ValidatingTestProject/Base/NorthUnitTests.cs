using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Operations.NorthWindClasses;
using ValidatingTestProject.Classes;

// ReSharper disable once CheckNamespace
namespace ValidatingTestProject
{
    public partial class NorthUnitTests
    {
        /// <summary>
        /// Comma delimited file to read
        /// </summary>
        public string FileName { get; set; }

        public List<CustomerItem> CustomerItemsList { get; set; }

        /// <summary>
        /// Setup for test methods
        /// </summary>
        /// <remarks>
        /// Logical if statements can be optimized but keeping things
        /// simple for the sake of learning.
        ///
        /// Once into Entity Framework in memory test you will see optimization for test method logic
        /// </remarks>
        [TestInitialize]
        public void Initialization()
        {
            if (TestContext.TestName == nameof(SimpleReadWithAddEventTest) || 
                TestContext.TestName == nameof(TextFieldParserCleanTest)) {

                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NorthWind1.csv");
                CustomerItemsList = new List<CustomerItem>();

            }

            if (TestContext.TestName == nameof(HasEmptyLinesTest))
            {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NorthWindWithEmptyLines.csv");
            }

            if (TestContext.TestName == nameof(IncorrectDeliminatorsTest))
            {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NorthWindIncorrectDelimitors.csv");
                InvalidIndices = new List<int>();
            }

            if (TestContext.TestName == nameof(TextFieldParserWrongTypeTest))
            {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NorthWindWrongTypes.csv");
                InvalidIndices = new List<int>();
            }

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

        private void NorthOperationsOnReadLineHandler(CustomerItem sender)
        {
            CustomerItemsList.Add(sender);
        }

        private List<int> InvalidIndices = new List<int>();
        private void NorthOperationsOnReadLineErrorHandler(NorthErrorContainer container)
        {
            InvalidIndices.Add(container.LineNumber);
            Debug.WriteLine($"{container.Exception.Message} | {container.LineNumber,-4:D3}[{container.Line}]");
        }

    }

}