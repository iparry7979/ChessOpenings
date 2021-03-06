﻿using ChessOpenings.Helpers;
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

        private List<string> Open_Game_Nf6 = new List<string>
        {
            "e4", "e5", "Nf3", "Nc6"
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

        private List<string> English_Closed = new List<string>
        {
            "c4", "e5", "Nc3", "Nc6", "g3", "g6", "Bg2", "Bg7", "d3", "d6", "Nf3"
        };

        private List<string> Blackmar_Diemer_Tartakower = new List<string>
        {
            "d4", "d5", "Nc3", "Nf6", "e4",  "dxe4", "f3", "exf3", "Nxf3", "Bg4"
        };

        private List<string> Open_Game = new List<string>
        {
            "e4", "e5", "Nf3"
        };

        //Ruy Lopez position through normal string of moves
        private List<string> Ruy_Lopez_Standard = new List<string>
        {
            "e4", "e5", "Nf3", "Nc6", "Bb5"
        };

        //Ruy Lopez position through non-standard series of moves - used to test transition from unknown opening
        private List<string> Ruy_Lopez_Mixed = new List<string>
        {
            "Nf3", "Nc6", "e4", "e5", "Bb5"
        };

        private List<string> French_Sstandard = new List<string>
        {
            "e4", "e6", "Nf3"
        }; //scid.eco line 8391

        private List<string> French_Transpose_From_Reti = new List<string>
        {
            "Nf3", "e6", "e4"  
        };

        public OpeningAccessor GetTestObject()
        {
            String path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + "/../../../ChessOpenings.DataConversion/DataFiles/openings.xml";

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

            //Test Case English Closed

            Opening english = accessor.GetOpening(English_Closed);

            Assert.IsTrue(english.Name == "English: Closed, 5.d3 d6 6.Nf3");
            Assert.IsTrue(english.ShortName == "English: Closed");
            Assert.IsTrue(english.lastMove == "Nf3");
            Assert.IsTrue(english.Id == "A26");

            //Test Case Blackmar-Diemer Tartakower Defence

            Opening tartakower = accessor.GetOpening(Blackmar_Diemer_Tartakower);

            Assert.IsTrue(tartakower.Name == "Blackmar-Diemer: Tartakower Defence");
            Assert.IsTrue(tartakower.ShortName == "Blackmar-Diemer: Tartakower Defence");
            Assert.IsTrue(tartakower.lastMove == "Bg4");
            Assert.IsTrue(tartakower.Id == "D00w");

            //Test Case Start of game

            Opening start = accessor.GetOpening(new List<string>());

            Assert.IsTrue(start.Name == "Start");
            Assert.IsTrue(start.Id == "Start");
            Assert.IsTrue(string.IsNullOrEmpty(start.lastMove));

            //Test Case Open Game - test success and frequency

            Opening open = accessor.GetOpening(Open_Game);

            Assert.IsTrue(open.Name == "Open Game");
            Assert.IsTrue(open.ShortName == "Open Game");
            Assert.IsTrue(open.lastMove == "Nf3");
            Assert.IsTrue(open.Id == "C40a");
            Assert.IsTrue(open.Frequency == 0.827638572513288);
            Assert.IsTrue(open.SuccessRate == 0.5284404);

            //Test Transpositions

            Opening ruy_lopez_standard = accessor.GetOpening(Ruy_Lopez_Standard);

            Assert.IsTrue(ruy_lopez_standard.Name == "Spanish (Ruy Lopez)");

            Opening ruy_lopez_non_standard = accessor.GetOpening(Ruy_Lopez_Mixed);

            Assert.IsTrue(ruy_lopez_non_standard.Name == "Spanish (Ruy Lopez)", "Failed Ruy Lopez Transposition");

            Opening french = accessor.GetOpening(French_Sstandard);

            Assert.IsTrue(french.Name == "French: 2.Nf3");

            Opening frenchTransposition = accessor.GetOpening(French_Transpose_From_Reti);

            Assert.IsTrue(frenchTransposition.Name == "French: 2.Nf3", "Failed French Transposition");


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

            List<Opening> tartakowerChildren = accessor.GetChildrenOfOpening(Blackmar_Diemer_Tartakower);
            Assert.IsTrue(tartakowerChildren.Count() == 0);

            //check first move

            List<Opening> firstMoves = accessor.GetChildrenOfOpening(new List<string>());

            Assert.IsTrue(firstMoves.Count() == 20);

            List<Opening> openChildren = accessor.GetChildrenOfOpening(Open_Game_Nf6);

            Assert.IsTrue(openChildren.Count() == 11);
            for (int i = 0; i < openChildren.Count() - 1; i++)
            {
                Assert.IsTrue(openChildren[i].Frequency >= openChildren[i + 1].Frequency);
            }
        }
    }
}
