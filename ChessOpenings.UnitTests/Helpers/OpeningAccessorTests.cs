using ChessOpenings.Helpers;
using ChessOpenings.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ChessOpenings.UnitTests.Helpers
{
    [TestClass]
    public class OpeningAccessorTests
    {
        //scid.eco line 3556
        private List<string> Dutch_Stonewall_BotvinnikVariation = new List<string>
        {
            "d4", "f5", "c4", "Nf6", "g3", "e6", "Bg2", "Be7", "Nf3", "O-O", "O-O", "d5", "b3"
        };

        //line 12991 scid.eco
        private List<string> Queens_Gambit = new List<string>
        {
            "d4", "d5", "c4"
        };

        //line 12830 scid.eco
        private List<string> Blackmar_Diemer_Accepted = new List<string>
        {
            "d4", "d5", "Nc3", "Nf6", "e4", "dxe4", "f3", "exf3"
        };

        public OpeningAccessor GetTestObject()
        {
            String path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + "/../../../ChessOpenings/ChessOpenings/Data/openings.xml";

            Stream fs = File.OpenRead(path);
            XDocument XDocument = XDocument.Load(fs);

            OpeningAccessor accessor = new OpeningAccessor(XDocument);
            return accessor;
        }

        [TestMethod]
        public void TestGetOpening()
        {
            OpeningAccessor accessor = GetTestObject();

            //Test Case - Dutch: Stonewall, Botvinnik Variation - scid.eco line 3556
            Opening dutchOpening = accessor.GetOpening(Dutch_Stonewall_BotvinnikVariation);

            Assert.IsTrue(dutchOpening.Name == "Dutch: Stonewall, Botvinnik Variation", "Incorrect Opening Name");
            Assert.IsTrue(dutchOpening.lastMove == "b3", "Incorrect last move");
            Assert.IsTrue(dutchOpening.Id == "A93", "Incorrect opening id");

            //Test Case - Queens gambit

            Opening queensGambit = accessor.GetOpening(Queens_Gambit);

            Assert.IsTrue(queensGambit.Name == "Queen's Gambit");
            Assert.IsTrue(queensGambit.lastMove == "c4");
            Assert.IsTrue(queensGambit.Id == "D06a");

            //Test Case Blackmar-Diemer Accepted

            Opening blackmar = accessor.GetOpening(Blackmar_Diemer_Accepted);

            Assert.IsTrue(blackmar.Name == "Blackmar-Diemer: Accepted");
            Assert.IsTrue(blackmar.lastMove == "exf3");
            Assert.IsTrue(blackmar.Id == "D00v");
        }

        [TestMethod]
        public void TestGetChildOpenings()
        {
            OpeningAccessor accessor = GetTestObject();

            List<Opening> dutchChildren = accessor.GetChildrenOfOpening(Dutch_Stonewall_BotvinnikVariation);

            Assert.IsTrue(dutchChildren.Count() == 3);

            List<string> names = dutchChildren.Select(c => c.Name).ToList();

            Assert.IsTrue(names.Contains("Dutch: Stonewall, Botvinnik, 7...b6"));
            Assert.IsTrue(names.Contains("Dutch: Stonewall, Botvinnik, 7...Nc6"));
            Assert.IsTrue(names.Contains("Dutch: Stonewall, Botvinnik, 7...c6"));

            List<string> moves = dutchChildren.Select(c => c.lastMove).ToList();

            Assert.IsTrue(moves.Contains("b6"));
            Assert.IsTrue(moves.Contains("Nc6"));
            Assert.IsTrue(moves.Contains("c6"));
        }
    }
}
