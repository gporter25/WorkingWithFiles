using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatingTestProject.Classes
{
    public class NorthErrorContainer
    {
        /// <summary>
        /// Exception thrown
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// Bad line, incorrect deliminators
        /// </summary>
        public string Line { get; set; }
        /// <summary>
        /// Line number of <see cref="Exception"/>
        /// </summary>
        public int LineNumber { get; set; }
    }
}
