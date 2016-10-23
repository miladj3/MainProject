using DataLayer.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer.EFServices;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.EFServices.Tests
{
    [TestClass()]
    public class RoleServiceTests
    {
        [TestMethod()]
        public void GetRoleByUserIdTest()
        {
            IUnitOfWork unit = new ShopDbContext();
            IRoleService user = new RoleService(unit);
            var t = user.GetRoleByUserId(3).Result;
            Assert.AreEqual(null, t);
        }
    }
}