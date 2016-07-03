using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUI.Helpers.Tests
{
    [TestClass]
    public class SeoExtensionTest
    {
        [TestMethod]
        public void GeneratePageTitle()
        {
            string t = "qwertyuioopdkfbvudfbnvoioreinveorinvpernvpoernv'poermv;'oermvo;enrv';oemnrvienbvielrinb;lenbilerbn;lnerbilenbvelbnelibneilbnlenbleinbe;lbn;elib";
            t = Helpers.SeoExtentions.GeneratePageTitle(t);
            Assert.AreEqual(t.Length, 60);
        }

        [TestMethod]
        public void RemoveHtmlTags()
        {
            string t = "<h1>milad</h1>";
            t = Helpers.SeoExtentions.RemoveHtmlTags(t);
            Assert.AreEqual(t, "milad");
        }

        [TestMethod]
        public void RemoveDiacriticsFromString()
        {
            string t = "مَن میلاد هَستم";
            t = Helpers.SeoExtentions.RemoveDiacriticsFromString(t);
            Assert.AreEqual(t, "من میلاد هستم");
        }

        [TestMethod]
        public void RemoveAccent()
        {
            string t = "مَن میلاد هَستم";
            t = Helpers.SeoExtentions.RemoveDiacriticsFromString(t);
            Assert.AreEqual(t, "من میلاد هستم");
        }

        [TestMethod()]
        public void GeneratePageDescriptionTest()
        {
            string t = "qwertyuioopdkfbvudfbnvoioreinveorinvpernvpoernv'poermv;'oermvo;enrv';oedfbnvoioreinveorinvpernvpoernv'poermv;'oermvo;enrv';oedfbnvoioreinveorinvpernvpoernv'poermv;'oermvo;enrv';oemnrvienbvielrinb;lenbilerbn;lnerbilenbvelbnelibneilbnlenbleinbe;lbn;elib";

            t = SeoExtentions.GeneratePageDescription(t);
            Assert.AreEqual(t.Length, 170);
        }
    }
}