using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessOpenings.Models;
using ChessOpenings.Pieces;

namespace ChessOpenings.UnitTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void TestGetDiagonalVectors()
        {
            Square square = new Square(new Bishop(Enums.Colour.White), Enums.Colour.White)
            {
                File = 'e',
                Rank = 4
            };

            Board board = new Board();
            BoardVector[] boardVector = board.GetDiagonalsFromSquare(square);

            Assert.IsTrue(boardVector.Length == 4);

            
        }
    }
}
