using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IocConfig;
using DataLayer.Context;
using ServiceLayer.Interfaces;

namespace MVCUI.Tests
{
    [TestClass]
    public class SampleObjectFactory
    {
        [TestMethod]
        public void SampleObjectFactoryForCheckIsNull()
        {
            var d = IocConfig.SampleObjectFactory.Container.GetInstance<IProductService>();
            Assert.IsNull(d);
        }
    }
}
