using ChessOpenings.Models;
using ChessOpenings.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.UnitTests.Models
{
    [TestClass]
    public class BoardPositionTests
    {
        //Start Position
        public BoardPosition startPosition = new Board().ToBoardPosition();

        //Empty Board
        public BoardPosition emptyPosition = new BoardPosition(new Dictionary<string, Piece>());

        //SimplePosition
        public BoardPosition simplePosition = new BoardPosition(new Dictionary<string, Piece>
        {
            {"a1", new Queen(Enums.Colour.White) },
            {"a2", new Queen(Enums.Colour.Black) },
            {"f6", new Rook(Enums.Colour.White) },
            {"h7", new Pawn(Enums.Colour.Black) }
        });

        [TestMethod]
        public void TestEqualsOverride()
        {
            //Different Object Type not equal
            Assert.IsFalse(startPosition.Equals(new Queen(Enums.Colour.White)));

            //Different board positions not equal
            Assert.IsFalse(startPosition.Equals(emptyPosition));

            //Different object with same position equal
            Board equalComparitor = new Board();
            Assert.IsTrue(startPosition.Equals(equalComparitor.ToBoardPosition()));

            //One piece different unequal
            Board unequalComparitor = new Board("1.h4");
            Assert.IsFalse(startPosition.Equals(unequalComparitor.ToBoardPosition()));
        }

        [TestMethod]
        public void TestGenerateComparisonValue()
        {
            Assert.IsTrue(emptyPosition.GenerateComparisonValue() == 0);
            Assert.IsTrue(simplePosition.GenerateComparisonValue() == 5358020);
        }
    }
}
