using ChessOpenings.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.UnitTests
{
    [TestClass]
    public class PieceTests
    {
        [TestMethod]
        public void TestEqualsOverride()
        {
            Piece p1 = new Bishop(Enums.Colour.White);
            Piece p2 = new Rook(Enums.Colour.White);
            Assert.IsFalse(p1.Equals(p2));

            p2 = new Bishop(Enums.Colour.Black);
            Assert.IsFalse(p1.Equals(p2));

            p2 = new Bishop(Enums.Colour.White);
            Assert.IsTrue(p1.Equals(p2));
        }
    }
}
