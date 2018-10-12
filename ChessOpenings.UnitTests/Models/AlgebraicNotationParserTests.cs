using ChessOpenings.Helpers;
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
    public class AlgebraicNotationParserTests
    {
        Board board = new Board(
            new List<string>
            {
                "c4", "c5", "Nf3", "Nf6", "d4", "cxd4", "Nxd4", "e6", "Nc3", "Bb4"
            });

        Board board2 = new Board(
            new List<string>
            {
                "c4", "c5", "Nf3", "Nf6", "d4", "cxd4", "Nxd4", "e6", "Nc3", "a6"
            });

        Board startPosition = new Board();

        Board castleTakePosition = new Board(
            new Dictionary<string, Piece>
            {
                { "e1", new King(Enums.Colour.White) },
                { "h1", new Rook(Enums.Colour.White) },
                {"e4", new Pawn(Enums.Colour.White) },
                {"d5", new Pawn(Enums.Colour.Black) }
            });

        [TestMethod]
        public void TestExtractSourceSquare()
        {
            AlgebraicNotationParser parser = new AlgebraicNotationParser("Nb5", Enums.Colour.White, board);
            Square s = parser.ExtractSourceSquare();
            Assert.IsNotNull(s, "Source square is null");
            Assert.IsTrue(s.Notation == "d4", "Incorrect Source Square");

            parser = new AlgebraicNotationParser("Ncb5", Enums.Colour.White, board2);
            s = parser.ExtractSourceSquare();
            Assert.IsNotNull(s, "Source square is null");
            Assert.IsTrue(s.Notation == "c3", "Incorrect Source Square");

            parser = new AlgebraicNotationParser("e4", Enums.Colour.White, startPosition);
            s = parser.ExtractSourceSquare();
            Assert.IsNotNull(s, "Source square is null");
            Assert.IsTrue(s.Notation == "e2", "Incorrect Source Square");

            parser = new AlgebraicNotationParser("e3", Enums.Colour.White, startPosition);
            s = parser.ExtractSourceSquare();
            Assert.IsNotNull(s, "Source square is null");
            Assert.IsTrue(s.Notation == "e2", "Incorrect Source Square");

            parser = new AlgebraicNotationParser("exd5", Enums.Colour.White, castleTakePosition);
            s = parser.ExtractSourceSquare();
            Assert.IsNotNull(s, "Source square is null");
            Assert.IsTrue(s.Notation == "e4", "Incorrect Source Square");
        }

    }
}
