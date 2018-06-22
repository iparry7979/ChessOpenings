using ChessOpenings.Helpers;
using ChessOpenings.Models;
using ChessOpenings.Pieces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.UnitTests.Helpers
{
    [TestClass]
    public class AlgebraicNotationParserTests
    {
        [TestMethod]
        public void TestExtractPiece()
        {
            //Test Castle
            AlgebraicNotationParser parser = new AlgebraicNotationParser("O-O", Enums.Colour.Black, null);
            Piece p = parser.ExtractPiece();
            Assert.IsTrue(p is King && p.colour == Enums.Colour.Black);

            parser = new AlgebraicNotationParser("O-O-O", Enums.Colour.Black, null);
            p = parser.ExtractPiece();
            Assert.IsTrue(p is King && p.colour == Enums.Colour.Black);

            //Test Pawn

            parser = new AlgebraicNotationParser("e4", Enums.Colour.White, null);
            p = parser.ExtractPiece();
            Assert.IsTrue(p is Pawn && p.colour == Enums.Colour.White);

            parser = new AlgebraicNotationParser("e4xf5", Enums.Colour.White, null);
            p = parser.ExtractPiece();
            Assert.IsTrue(p is Pawn && p.colour == Enums.Colour.White);

            //Test Knight

            parser = new AlgebraicNotationParser("Nd3", Enums.Colour.White, null);
            p = parser.ExtractPiece();
            Assert.IsTrue(p is Knight && p.colour == Enums.Colour.White);

            //Test Bishop

            parser = new AlgebraicNotationParser("Bxe6+", Enums.Colour.White, null);
            p = parser.ExtractPiece();
            Assert.IsTrue(p is Bishop && p.colour == Enums.Colour.White);

            //Test Rook

            parser = new AlgebraicNotationParser("Ref4", Enums.Colour.White, null);
            p = parser.ExtractPiece();
            Assert.IsTrue(p is Rook && p.colour == Enums.Colour.White);

            //Test Promotion

            parser = new AlgebraicNotationParser("e8=Q", Enums.Colour.White, null);
            p = parser.ExtractPiece();
            Assert.IsTrue(p is Pawn && p.colour == Enums.Colour.White);
        }

        [TestMethod]
        public void TestExtractDestinationSquare()
        {
            AlgebraicNotationParser parser = new AlgebraicNotationParser("O-O", Enums.Colour.Black, null);
            string square = parser.ExtractDestinationSquare();
            Assert.IsTrue(square == "g8");

            parser = new AlgebraicNotationParser("O-O-O", Enums.Colour.Black, null);
            square = parser.ExtractDestinationSquare();
            Assert.IsTrue(square == "c8");

            parser = new AlgebraicNotationParser("O-O", Enums.Colour.White, null);
            square = parser.ExtractDestinationSquare();
            Assert.IsTrue(square == "g1");

            parser = new AlgebraicNotationParser("O-O-O", Enums.Colour.White, null);
            square = parser.ExtractDestinationSquare();
            Assert.IsTrue(square == "c1");

            parser = new AlgebraicNotationParser("Ne4", Enums.Colour.White, null);
            square = parser.ExtractDestinationSquare();
            Assert.IsTrue(square == "e4");

            parser = new AlgebraicNotationParser("Bxc6+", Enums.Colour.Black, null);
            square = parser.ExtractDestinationSquare();
            Assert.IsTrue(square == "c6");

            parser = new AlgebraicNotationParser("g3", Enums.Colour.Black, null);
            square = parser.ExtractDestinationSquare();
            Assert.IsTrue(square == "g3");
        }

        [TestMethod]
        public void TestExtractPromotionPiece()
        {
            AlgebraicNotationParser parser = new AlgebraicNotationParser("e1=Q", Enums.Colour.Black, null);
            Piece p = parser.ExtractPromotionPiece();
            Assert.IsTrue(p is Queen && p.colour == Enums.Colour.Black);

            parser = new AlgebraicNotationParser("exd1=N", Enums.Colour.Black, null);
            p = parser.ExtractPromotionPiece();
            Assert.IsTrue(p is Knight && p.colour == Enums.Colour.Black);

            parser = new AlgebraicNotationParser("exd1", Enums.Colour.Black, null);
            p = parser.ExtractPromotionPiece();
            Assert.IsNull(p);
        }

        [TestMethod]
        public void TestGetMoves_SimpleMoves()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "e3", new Knight(Enums.Colour.White) },
                { "h1", new Bishop(Enums.Colour.White) },
                { "b3", new Queen(Enums.Colour.White) },
                { "d8", new Rook(Enums.Colour.White) },
                { "d4", new Pawn(Enums.Colour.White) },
                { "h2", new Pawn(Enums.Colour.White) },
                { "e5", new King(Enums.Colour.White) }
            };

            Board board = new Board(boardPosition);

            AlgebraicNotationParser parser = new AlgebraicNotationParser("Nd5", Enums.Colour.White, board);
            Move m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "e3");
            Assert.IsTrue(m.ToSquare.Notation == "d5");
            Assert.IsTrue(m.SubjectPiece is Knight);

            parser = new AlgebraicNotationParser("Bd5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "h1");
            Assert.IsTrue(m.ToSquare.Notation == "d5");
            Assert.IsTrue(m.SubjectPiece is Bishop);

            parser = new AlgebraicNotationParser("Qd5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "b3");
            Assert.IsTrue(m.ToSquare.Notation == "d5");
            Assert.IsTrue(m.SubjectPiece is Queen);

            parser = new AlgebraicNotationParser("Rd5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "d8");
            Assert.IsTrue(m.ToSquare.Notation == "d5");
            Assert.IsTrue(m.SubjectPiece is Rook);

            parser = new AlgebraicNotationParser("d5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "d4");
            Assert.IsTrue(m.ToSquare.Notation == "d5");
            Assert.IsTrue(m.SubjectPiece is Pawn);

            parser = new AlgebraicNotationParser("Kd5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "e5");
            Assert.IsTrue(m.ToSquare.Notation == "d5");
            Assert.IsTrue(m.SubjectPiece is King);

            parser = new AlgebraicNotationParser("h4", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "h2");
            Assert.IsTrue(m.ToSquare.Notation == "h4");
            Assert.IsTrue(m.SubjectPiece is Pawn);
        }

        [TestMethod]
        public void TestGetMove_Negatives()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "e3", new Knight(Enums.Colour.White) },
                { "h1", new Bishop(Enums.Colour.White) },
                { "b3", new Queen(Enums.Colour.White) },
                { "d8", new Rook(Enums.Colour.White) },
                { "d4", new Pawn(Enums.Colour.White) },
                { "h2", new Pawn(Enums.Colour.Black) },
                { "e5", new King(Enums.Colour.White) },
                { "a6", new Pawn(Enums.Colour.White) },
                { "a7", new Pawn(Enums.Colour.Black) }
            };

            Board board = new Board(boardPosition);

            AlgebraicNotationParser parser = new AlgebraicNotationParser("Ng7", Enums.Colour.White, board);
            Move m = parser.GetMove();
            Assert.IsNull(m);

            //Pawn advance when not valid
            parser = new AlgebraicNotationParser("d6", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsNull(m);

            //Wrong colour
            parser = new AlgebraicNotationParser("h3", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsNull(m);
        }

        [TestMethod]
        public void TestGetMove_Captures()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "e5", new Bishop(Enums.Colour.White) },
                { "c7", new Bishop(Enums.Colour.Black) },
                { "c4", new Knight(Enums.Colour.Black) },
                { "f6", new Pawn(Enums.Colour.Black) },
                { "f4", new Pawn(Enums.Colour.Black) },
                { "e8", new Rook(Enums.Colour.White) }
            };

            Board board = new Board(boardPosition);

            AlgebraicNotationParser parser = new AlgebraicNotationParser("Bxe5", Enums.Colour.Black, board);
            Move m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "c7");
            Assert.IsTrue(m.ToSquare.Notation == "e5");
            Assert.IsTrue(m.SubjectPiece is Bishop);

            parser = new AlgebraicNotationParser("Nxe5", Enums.Colour.Black, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "c4");
            Assert.IsTrue(m.ToSquare.Notation == "e5");
            Assert.IsTrue(m.SubjectPiece is Knight);

            parser = new AlgebraicNotationParser("fxe5", Enums.Colour.Black, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "f6");
            Assert.IsTrue(m.ToSquare.Notation == "e5");
            Assert.IsTrue(m.SubjectPiece is Pawn);

            //Same colour piece take should fail
            parser = new AlgebraicNotationParser("Rxe5", Enums.Colour.Black, board);
            m = parser.GetMove();
            Assert.IsNull(m);
        }

        [TestMethod]
        public void TestGetMove_Disambiguations()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "c4", new Knight(Enums.Colour.White) },
                { "f7", new Knight(Enums.Colour.White) },
                { "c3", new Knight(Enums.Colour.White) },
                { "c1", new Knight(Enums.Colour.White) }
            };

            Board board = new Board(boardPosition);

            AlgebraicNotationParser parser = new AlgebraicNotationParser("Ne5", Enums.Colour.White, board);
            Move m = parser.GetMove();
            Assert.IsNull(m); //should be null as no disambiguation is provided

            parser = new AlgebraicNotationParser("Nce5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "c4");
            Assert.IsTrue(m.ToSquare.Notation == "e5");
            Assert.IsTrue(m.SubjectPiece is Knight);

            parser = new AlgebraicNotationParser("Nfe5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "f7");
            Assert.IsTrue(m.ToSquare.Notation == "e5");
            Assert.IsTrue(m.SubjectPiece is Knight);

            parser = new AlgebraicNotationParser("N1a2", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "c1");
            Assert.IsTrue(m.ToSquare.Notation == "a2");
            Assert.IsTrue(m.SubjectPiece is Knight);

            parser = new AlgebraicNotationParser("N3a2", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "c3");
            Assert.IsTrue(m.ToSquare.Notation == "a2");
            Assert.IsTrue(m.SubjectPiece is Knight);

            parser = new AlgebraicNotationParser("Nca2", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsNull(m); //Insufficient disambiguation provided

            //Test double ambiguation
            boardPosition = new Dictionary<string, Piece>
            {
                { "c4", new Knight(Enums.Colour.White) },
                { "c6", new Knight(Enums.Colour.White) },
                { "g6", new Knight(Enums.Colour.White) }
            };

            board = new Board(boardPosition);

            parser = new AlgebraicNotationParser("Nce5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsNull(m); //Insufficient disambiguation provided

            parser = new AlgebraicNotationParser("N6e5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsNull(m); //Insufficient disambiguation provided

            parser = new AlgebraicNotationParser("Nge5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "g6");
            Assert.IsTrue(m.ToSquare.Notation == "e5");
            Assert.IsTrue(m.SubjectPiece is Knight);

            parser = new AlgebraicNotationParser("N4e5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "c4");
            Assert.IsTrue(m.ToSquare.Notation == "e5");
            Assert.IsTrue(m.SubjectPiece is Knight);

            parser = new AlgebraicNotationParser("Nc6e5", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "c6");
            Assert.IsTrue(m.ToSquare.Notation == "e5");
            Assert.IsTrue(m.SubjectPiece is Knight);
        }

        [TestMethod]
        public void TestGetMove_Castling()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "e1", new King(Enums.Colour.White) },
                { "a1", new Rook(Enums.Colour.White) },
                { "h1", new Rook(Enums.Colour.White) },
                { "e8", new King(Enums.Colour.Black) },
                { "a8", new Rook(Enums.Colour.Black) },
                { "h8", new Rook(Enums.Colour.Black) }
            };

            Board board = new Board(boardPosition);

            AlgebraicNotationParser parser = new AlgebraicNotationParser("O-O", Enums.Colour.White, board);
            Move m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "e1");
            Assert.IsTrue(m.ToSquare.Notation == "g1");
            Assert.IsTrue(m.SubjectPiece is King);

            parser = new AlgebraicNotationParser("O-O-O", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "e1");
            Assert.IsTrue(m.ToSquare.Notation == "c1");
            Assert.IsTrue(m.SubjectPiece is King);

            parser = new AlgebraicNotationParser("O-O", Enums.Colour.Black, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "e8");
            Assert.IsTrue(m.ToSquare.Notation == "g8");
            Assert.IsTrue(m.SubjectPiece is King);

            parser = new AlgebraicNotationParser("O-O-O", Enums.Colour.Black, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "e8");
            Assert.IsTrue(m.ToSquare.Notation == "c8");
            Assert.IsTrue(m.SubjectPiece is King);
        }

        [TestMethod]
        public void TestGetMove_Promotion()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "e7", new Pawn(Enums.Colour.White) },
                { "a7", new Pawn(Enums.Colour.White) },
                { "b7", new Pawn(Enums.Colour.White) },
                { "a8", new Bishop(Enums.Colour.Black) }
            };

            Board board = new Board(boardPosition);

            AlgebraicNotationParser parser = new AlgebraicNotationParser("e8=R", Enums.Colour.White, board);
            Move m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "e7");
            Assert.IsTrue(m.ToSquare.Notation == "e8");
            Assert.IsTrue(m.SubjectPiece is Pawn);
            Assert.IsTrue(m.PromotionPiece is Rook);

            parser = new AlgebraicNotationParser("bxa8=Q", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "b7");
            Assert.IsTrue(m.ToSquare.Notation == "a8");
            Assert.IsTrue(m.SubjectPiece is Pawn);
            Assert.IsTrue(m.PromotionPiece is Queen);
        }

        [TestMethod]
        public void TestGetMove_Other()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "h8", new King(Enums.Colour.Black) },
                { "a7", new Rook(Enums.Colour.White) },
                { "b6", new Rook(Enums.Colour.White) },
                { "h6", new Bishop(Enums.Colour.White) }
            };

            Board board = new Board(boardPosition);

            //Check
            AlgebraicNotationParser parser = new AlgebraicNotationParser("Bg7+", Enums.Colour.White, board);
            Move m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "h6");
            Assert.IsTrue(m.ToSquare.Notation == "g7");
            Assert.IsTrue(m.SubjectPiece is Bishop);

            //check mate
            parser = new AlgebraicNotationParser("Rb8#", Enums.Colour.White, board);
            m = parser.GetMove();
            Assert.IsTrue(m.FromSquare.Notation == "b6");
            Assert.IsTrue(m.ToSquare.Notation == "b8");
            Assert.IsTrue(m.SubjectPiece is Rook);
        }

        [TestMethod]
        public void TestExtractDisambiguationString()
        {
            AlgebraicNotationParser parser = new AlgebraicNotationParser("e4", Enums.Colour.White, null);
            string result = parser.ExtractDisambiguationString("e4");
            Assert.IsNull(result);

            parser = new AlgebraicNotationParser("Ne4", Enums.Colour.White, null);
            result = parser.ExtractDisambiguationString("e4");
            Assert.IsNull(result);

            parser = new AlgebraicNotationParser("Nxe4", Enums.Colour.White, null);
            result = parser.ExtractDisambiguationString("e4");
            Assert.IsNull(result);

            parser = new AlgebraicNotationParser("fxe4", Enums.Colour.White, null);
            result = parser.ExtractDisambiguationString("e4");
            Assert.IsNull(result);

            parser = new AlgebraicNotationParser("Nfe4", Enums.Colour.White, null);
            result = parser.ExtractDisambiguationString("e4");
            Assert.IsTrue(result == "f");

            parser = new AlgebraicNotationParser("Nfxe4", Enums.Colour.White, null);
            result = parser.ExtractDisambiguationString("e4");
            Assert.IsTrue(result == "f");

            parser = new AlgebraicNotationParser("N5e4", Enums.Colour.White, null);
            result = parser.ExtractDisambiguationString("e4");
            Assert.IsTrue(result == "5");

            parser = new AlgebraicNotationParser("N5xe4", Enums.Colour.White, null);
            result = parser.ExtractDisambiguationString("e4");
            Assert.IsTrue(result == "5");

            parser = new AlgebraicNotationParser("Nf2e4", Enums.Colour.White, null);
            result = parser.ExtractDisambiguationString("e4");
            Assert.IsTrue(result == "f2");

            parser = new AlgebraicNotationParser("Nf2xe4+", Enums.Colour.White, null);
            result = parser.ExtractDisambiguationString("e4");
            Assert.IsTrue(result == "f2");
        }
    }
}
