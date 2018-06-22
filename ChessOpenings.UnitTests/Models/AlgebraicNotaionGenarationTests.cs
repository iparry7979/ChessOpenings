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
    public class AlgebraicNotaionGenarationTests
    {
        [TestMethod]
        public void TestStandardMove()
        {
            Board board = new Board();
            Move move = new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("e4"));
            AlgebraicNotationGenerator generator = new AlgebraicNotationGenerator(board, move);
            string notation = generator.Generate();
            Assert.IsTrue(notation == "e4");

            board.MakeMove(move);

            Move move1 = new Move(board.GetSquareByNotation("g8"), board.GetSquareByNotation("f6"));
            generator = new AlgebraicNotationGenerator(board, move1);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Nf6");

            board.MakeMove(move1);

            Move move2 = new Move(board.GetSquareByNotation("f1"), board.GetSquareByNotation("d3"));
            generator = new AlgebraicNotationGenerator(board, move2);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Bd3");

            board.MakeMove(move2);

            Move move3 = new Move(board.GetSquareByNotation("e7"), board.GetSquareByNotation("e6"));
            generator = new AlgebraicNotationGenerator(board, move3);
            notation = generator.Generate();
            Assert.IsTrue(notation == "e6");
        }

        [TestMethod]
        public void TestCaptures()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "a3", new Knight(Enums.Colour.White) },
                { "b5", new Pawn(Enums.Colour.Black) },
                { "d4", new Pawn(Enums.Colour.White) },
                { "e5", new Pawn(Enums.Colour.Black) },
                { "h5", new Pawn(Enums.Colour.White) },
                { "g7", new Pawn(Enums.Colour.Black) },
                { "e8", new King(Enums.Colour.Black) },
                { "f7", new Rook(Enums.Colour.White) },
                { "g1", new Queen(Enums.Colour.White) },
                { "h1", new Queen(Enums.Colour.Black) }
            };

            Board board = new Board(boardPosition);

            //Standard Captures

            Move move = new Move(board.GetSquareByNotation("a3"), board.GetSquareByNotation("b5"));
            AlgebraicNotationGenerator generator = new AlgebraicNotationGenerator(board, move);
            string notation = generator.Generate();
            Assert.IsTrue(notation == "Nxb5", "Incorrect notation for standard capture");

            move = new Move(board.GetSquareByNotation("e8"), board.GetSquareByNotation("f7"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Kxf7", "Incorrect notation for standard capture");

            move = new Move(board.GetSquareByNotation("g1"), board.GetSquareByNotation("h1"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Qxh1", "Incorrect notation for standard capture");

            //Pawn Captures

            move = new Move(board.GetSquareByNotation("d4"), board.GetSquareByNotation("e5"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "dxe5", "Incorrect notation for pawn on pawn capture");

            move = new Move(board.GetSquareByNotation("e5"), board.GetSquareByNotation("d4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "exd4");

            //En Passant
            board.ChangeTurn();

            move = new Move(board.GetSquareByNotation("g7"), board.GetSquareByNotation("g5"));
            board.MakeMove(move);

            Move move1 = new Move(board.GetSquareByNotation("h5"), board.GetSquareByNotation("g6"));
            generator = new AlgebraicNotationGenerator(board, move1);
            notation = generator.Generate();
            Assert.IsTrue(notation == "hxg6", "Incorrect notation for en passant capture");
        }

        [TestMethod]
        public void TestCastling()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "a1", new Rook(Enums.Colour.White) },
                { "e1", new King(Enums.Colour.White) },
                { "h1", new Rook(Enums.Colour.White) },
                { "a8", new Rook(Enums.Colour.Black) },
                { "e8", new King(Enums.Colour.Black) },
                { "h8", new Rook(Enums.Colour.Black) }
            };

            Board board = new Board(boardPosition);

            Move move = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("g1"));
            AlgebraicNotationGenerator generator = new AlgebraicNotationGenerator(board, move);
            string notation = generator.Generate();
            Assert.IsTrue(notation == "O-O", "Incorrect notation for white king side castle");

            move = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("c1"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "O-O-O", "Incorrect notation for white queen side castle");

            move = new Move(board.GetSquareByNotation("e8"), board.GetSquareByNotation("g8"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "O-O", "Incorrect notation for black king side castle");

            move = new Move(board.GetSquareByNotation("e8"), board.GetSquareByNotation("c8"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "O-O-O", "Incorrect notation for black queen side castle");
        }

        [TestMethod]
        public void TestPromotions()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>()
            {
                { "e7", new Pawn(Enums.Colour.White) },
                { "d2", new Pawn(Enums.Colour.Black) },
                { "a7", new Pawn(Enums.Colour.White) },
                { "b8", new Knight(Enums.Colour.Black) }
            };

            Board board = new Board(boardPosition);

            Move move = new Move(board.GetSquareByNotation("e7"), board.GetSquareByNotation("e8"));
            move.PromotionPiece = new Queen(Enums.Colour.White);
            AlgebraicNotationGenerator generator = new AlgebraicNotationGenerator(board, move);
            string notation = generator.Generate();
            Assert.IsTrue(notation == "e8=Q", "Incorrect notation for white pawn promotion");

            move = new Move(board.GetSquareByNotation("d2"), board.GetSquareByNotation("d1"));
            move.PromotionPiece = new Bishop(Enums.Colour.White);
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "d1=B", "Incorrect notation for black pawn promotion");

            move = new Move(board.GetSquareByNotation("a7"), board.GetSquareByNotation("b8"));
            move.PromotionPiece = new Rook(Enums.Colour.White);
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "axb8=R");
        }

        [TestMethod]
        public void TestCheckNotation()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>()
            {
                { "e1", new King(Enums.Colour.White) },
                { "f3", new Pawn(Enums.Colour.Black) },
                { "a7", new Queen(Enums.Colour.Black) },
                { "b4", new Knight(Enums.Colour.Black) },
                { "d3", new Pawn(Enums.Colour.White) }
            };

            Board board = new Board(boardPosition);

            Move move = new Move(board.GetSquareByNotation("f3"), board.GetSquareByNotation("f2"));
            AlgebraicNotationGenerator generator = new AlgebraicNotationGenerator(board, move);
            string notation = generator.Generate();
            Assert.IsTrue(notation == "f2+");

            move = new Move(board.GetSquareByNotation("a7"), board.GetSquareByNotation("a1"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Qa1+");

            move = new Move(board.GetSquareByNotation("b4"), board.GetSquareByNotation("d3"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Nxd3+");
        }

        [TestMethod]
        public void TestDisambiguation()
        {
            //Testing disambiguation when file and rank of subject pieces differ
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>()
            {
                { "c3", new Knight(Enums.Colour.White) },
                { "g5", new Knight(Enums.Colour.White) },
                { "c5", new Knight(Enums.Colour.Black) } //Ensure that colour is not causing an issue
            };

            Board board = new Board(boardPosition);

            Move move = new Move(board.GetSquareByNotation("c3"), board.GetSquareByNotation("e4"));
            AlgebraicNotationGenerator generator = new AlgebraicNotationGenerator(board, move);
            string notation = generator.Generate();
            Assert.IsTrue(notation == "Nce4", "Incorrect notation: disambiguation when file and rank differ (Knight)" );

            move = new Move(board.GetSquareByNotation("g5"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Nge4", "Incorrect notation: disambiguation when file and rank differ (Knight)");

            move = new Move(board.GetSquareByNotation("c5"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Ne4", "Incorrect notation: disambiguation when file and rank differ (Knight)");

            //Testing disambiguation when file only differs
            boardPosition = new Dictionary<string, Piece>()
            {
                { "c3", new Knight(Enums.Colour.White) },
                { "g3", new Knight(Enums.Colour.White) },
                { "c5", new Knight(Enums.Colour.Black) } //Ensure that colour is not causing an issue
            };

            board = new Board(boardPosition);

            move = new Move(board.GetSquareByNotation("c3"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Nce4", "Incorrect notation: disambiguation when file differs (Knight)");

            move = new Move(board.GetSquareByNotation("g3"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Nge4", "Incorrect notation: disambiguation when file differs (Knight)");


            //Testing disambiguation when rank only differs
            boardPosition = new Dictionary<string, Piece>()
            {
                { "c3", new Knight(Enums.Colour.White) },
                { "c5", new Knight(Enums.Colour.White) },
                { "g3", new Knight(Enums.Colour.Black) } //Ensure that colour is not causing an issue
            };

            board = new Board(boardPosition);

            move = new Move(board.GetSquareByNotation("c3"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "N3e4", "Incorrect notation: disambiguation when rank differs (Knight)");

            move = new Move(board.GetSquareByNotation("c5"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "N5e4", "Incorrect notation: disambiguation when rank differs (Knight)");

            //Testing disambiguation when file or rank would be ambiguous
            boardPosition = new Dictionary<string, Piece>()
            {
                { "c3", new Knight(Enums.Colour.White) },
                { "c5", new Knight(Enums.Colour.White) },
                { "g5", new Knight(Enums.Colour.White) } 
            };

            board = new Board(boardPosition);

            move = new Move(board.GetSquareByNotation("c5"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Nc5e4", "Incorrect notation: disambiguation when file and rank alone are insufficient to disambiguate (Knight)");

            //testing diambiguation with capture
            boardPosition = new Dictionary<string, Piece>()
            {
                { "c3", new Knight(Enums.Colour.White) },
                { "c5", new Knight(Enums.Colour.White) },
                { "g5", new Knight(Enums.Colour.White) },
                { "e4", new Pawn(Enums.Colour.Black) },
            };

            board = new Board(boardPosition);

            move = new Move(board.GetSquareByNotation("c5"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Nc5xe4", "Incorrect notation: disambiguation with capture");

            //Test disambiguation with chack
            boardPosition = new Dictionary<string, Piece>()
            {
                { "c3", new Knight(Enums.Colour.White) },
                { "c5", new Knight(Enums.Colour.White) },
                { "g5", new Knight(Enums.Colour.White) },
                { "d6", new King(Enums.Colour.Black) },
            };

            board = new Board(boardPosition);

            move = new Move(board.GetSquareByNotation("c5"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Nc5e4+", "Incorrect notation: disambiguation with check");

            //Testing disambiguation when file only differs (Queen)
            boardPosition = new Dictionary<string, Piece>()
            {
                { "e1", new Queen(Enums.Colour.White) },
                { "a4", new Queen(Enums.Colour.White) },
                { "h7", new Queen(Enums.Colour.Black) } //Ensure that colour is not causing an issue
            };

            board = new Board(boardPosition);

            move = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Qee4", "Incorrect notation: disambiguation when file differs (Queen)");

            move = new Move(board.GetSquareByNotation("a4"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Qae4", "Incorrect notation: disambiguation when file differs (Queen)");

            //Testing disambiguation when rank only differs (Queen)
            boardPosition = new Dictionary<string, Piece>()
            {
                { "e1", new Queen(Enums.Colour.White) },
                { "e8", new Queen(Enums.Colour.White) },
            };

            board = new Board(boardPosition);

            move = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Q1e4", "Incorrect notation: disambiguation when rank differs (Queen)");

            move = new Move(board.GetSquareByNotation("e8"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Q8e4", "Incorrect notation: disambiguation when rank differs");

            //Testing disambiguation when file or rank would be ambiguous (Queen)
            boardPosition = new Dictionary<string, Piece>()
            {
                { "e1", new Queen(Enums.Colour.White) },
                { "e7", new Queen(Enums.Colour.White) },
                { "h7", new Queen(Enums.Colour.White) }
            };

            board = new Board(boardPosition);

            move = new Move(board.GetSquareByNotation("e7"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Qe7e4", "Incorrect notation: disambiguation when file and rank alone are insufficient to disambiguate (Queen)");

            //Testing disambiguation when file or rank would be ambiguous (Bishop)
            boardPosition = new Dictionary<string, Piece>()
            {
                { "c6", new Bishop(Enums.Colour.White) },
                { "g6", new Bishop(Enums.Colour.White) },
                { "g2", new Bishop(Enums.Colour.White) }
            };

            board = new Board(boardPosition);

            move = new Move(board.GetSquareByNotation("g6"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "Bg6e4", "Incorrect notation: disambiguation when file and rank alone are insufficient to disambiguate (Bishop)");

            //Testing disambiguation when file is ambiguous (Rook)
            boardPosition = new Dictionary<string, Piece>()
            {
                { "e1", new Rook(Enums.Colour.White) },
                { "e8", new Rook(Enums.Colour.White) },
            };

            board = new Board(boardPosition);

            move = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("e4"));
            generator = new AlgebraicNotationGenerator(board, move);
            notation = generator.Generate();
            Assert.IsTrue(notation == "R1e4", "Incorrect notation: disambiguation when file and rank alone are insufficient to disambiguate (Queen)");
        }
    }
}
