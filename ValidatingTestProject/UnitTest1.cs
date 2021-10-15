using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ValidatingTestProject.Base;
using static ValidatingTestProject.Base.Trait;

namespace ValidatingTestProject
{
    [TestClass]
    public partial class UnitTest1 : TestBase
    {
        [TestMethod]
        [TestTraits(PlaceHolder)]
        public void Name()
        {
        }

    }
}
