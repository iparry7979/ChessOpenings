using ChessOpenings.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.UnitTests.Models
{
    [TestClass]
    public class OpeningTests
    {
        [TestMethod]
        public void TestShortName()
        {
            Opening o = new Opening();
            o.Name = "Slav: 4.e3 Bf5 5.Nc3 e6 6.Nh4";
            Assert.IsTrue(o.ShortName == "Slav");

            o = new Opening();
            o.Name = "Slav: Geller Gambit, 6.e5 Nd5 7.a4 e6";
            Assert.IsTrue(o.ShortName == "Slav: Geller Gambit");

            o = new Opening();
            o.Name = "Slav: Alapin";
            Assert.IsTrue(o.ShortName == "Slav: Alapin");

            o = new Opening();
            o.Name = "Queen's Gambit Accepted (QGA)";
            Assert.IsTrue(o.ShortName == "Queen's Gambit Accepted (QGA)");
        }
    }
}
