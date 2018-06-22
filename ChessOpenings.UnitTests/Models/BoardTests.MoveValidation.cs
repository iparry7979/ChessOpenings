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
    public partial class BoardTests
    {
        [TestMethod]
        public void SimpleValidationTests()
        {
            //fails when from square == to square
            Board board = new Board();
            Move m = new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("e2"));
            Assert.IsFalse(board.ValidateMove(m));

            //Fails when from square contains no piece
            m = new Move(board.GetSquareByNotation("e4"), board.GetSquareByNotation("e5"));
            Assert.IsFalse(board.ValidateMove(m));

            //Fails when piece is wrong colour
            m = new Move(board.GetSquareByNotation("e7"), board.GetSquareByNotation("e5"));
            Assert.IsFalse(board.ValidateMove(m));

            //Fails when move is null
            Assert.IsFalse(board.ValidateMove(null));

            //Fails when destination squares piece is same colour as moving piece
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "h1", new Rook(Enums.Colour.White) },
                { "h2", new Bishop(Enums.Colour.White) }
            };

            board = new Board(boardPosition);
            m = new Move(board.GetSquareByNotation("h1"), board.GetSquareByNotation("h2"));
            Assert.IsFalse(board.ValidateMove(m));
        }

        [TestMethod]
        public void TestCastlingValidation()
        {
            //PGN for Test situations
            string kingSideCastleValid = "1. e4 e5 2. Nf3 Nf6 3. Bc4 Bc5";
            string queenSideCastleValid = "1. d4 d5 2. Nc3 Nc6 3. Bf4 Bf5 4. Qd3 Qd6";
            string invalidKingSideCastleKingMoved = "1. e4 e5 2. Nf3 Nf6 3. Bc4 Bc5 4. Ke2 Ke7 5. Ke1 Ke8";
            string invalidKingSideCastleRookMoved = "1. e4 e5 2. Nf3 Nf6 3. Bc4 Bc5 4. O-O Rg8 5. a3 Rh8 6. h3";
            string invalidQueenSideCastleKingMoved = "1. d4 d5 2. Nc3 Nc6 3. Bf4 Bf5 4. Qd3 Qd6 5. Kd2 Kd7 6. Ke1 Ke8 7. a3";
            string invalidQueenSideCastleRookMoved = "1. d4 d5 2. Nc3 Nc6 3. Bf4 Bf5 4. Qd3 Qd6 5. Rb1 Rb8 6. Ra1, Ra8";
            string invalidKingSideCastleAcrossCheck = "1. e4 e5 2. f4 b6 3. Nf3 Ba6 4. g3 h6 5. Bg2 d6";
            string invalidQueenSideCastleAcrossCheck = "1. Nf3 d5 2. Ng5 Bf5 3. Ne6 Nc6 4. h3 Qd6 5. a3";
            string invalidKingSideCastleIntoCheck = "1. e4 Nc6 2. Nf3 Ne5 3. Bc4 Nxf3";

            //Valid King side castle both colours
            Board board = new Board(kingSideCastleValid);

            Move whiteCastleKingSide = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("g1"));
            Assert.IsTrue(board.ValidateMove(whiteCastleKingSide));
            board.MakeMove(whiteCastleKingSide);
            Move blackCastleKingSide = new Move(board.GetSquareByNotation("e8"), board.GetSquareByNotation("g8"));
            Assert.IsTrue(board.ValidateMove(blackCastleKingSide));

            //Valid Queen side castle both colours

            board = new Board(queenSideCastleValid);
            Move whiteQueenSideCastle = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("c1"));
            Assert.IsTrue(board.ValidateMove(whiteQueenSideCastle));
            board.MakeMove(whiteQueenSideCastle);
            Move blackQueenSideCastle = new Move(board.GetSquareByNotation("e8"), board.GetSquareByNotation("c8"));
            Assert.IsTrue(board.ValidateMove(blackQueenSideCastle));

            //Invalid castle - King side - King moved - white only

            board = new Board(invalidKingSideCastleKingMoved);
            whiteCastleKingSide = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("g1"));
            Assert.IsFalse(board.ValidateMove(whiteCastleKingSide));

            //Invalid castle - King side - Rook moved - black only

            board = new Board(invalidKingSideCastleRookMoved);
            blackCastleKingSide = new Move(board.GetSquareByNotation("e8"), board.GetSquareByNotation("g8"));
            Assert.IsFalse(board.ValidateMove(blackCastleKingSide));

            //Invalid castle - Queen side - King moved - black only

            board = new Board(invalidQueenSideCastleKingMoved);
            blackQueenSideCastle = new Move(board.GetSquareByNotation("e8"), board.GetSquareByNotation("c8"));
            Assert.IsFalse(board.ValidateMove(blackQueenSideCastle));

            //Invalid castle - Queen side - Rook moved - white only

            board = new Board(invalidQueenSideCastleRookMoved);
            whiteQueenSideCastle = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("c1"));
            Assert.IsFalse(board.ValidateMove(whiteQueenSideCastle));

            //Invalid castle - King side - Across check - white only
            board = new Board(invalidKingSideCastleAcrossCheck);
            whiteCastleKingSide = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("g1"));
            Assert.IsFalse(board.ValidateMove(whiteCastleKingSide));

            //Invalid castle - Queen side - Across check - black only
            board = new Board(invalidQueenSideCastleAcrossCheck);
            blackQueenSideCastle = new Move(board.GetSquareByNotation("e8"), board.GetSquareByNotation("c8"));
            Assert.IsFalse(board.ValidateMove(blackQueenSideCastle));

            //Invalid castle - King side - Into check - white only
            board = new Board(invalidKingSideCastleIntoCheck);
            whiteCastleKingSide = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("g1"));
            Assert.IsFalse(board.ValidateMove(whiteCastleKingSide));
        }

        [TestMethod]
        public void TestMoveResultsInCheck()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "a8", new King(Enums.Colour.Black) },
                { "b5", new Bishop(Enums.Colour.White) },
                { "h1", new King(Enums.Colour.White) },
                { "h5", new Knight(Enums.Colour.Black) }
            };
            Board board = new Board(boardPosition);

            Move m1 = new Move(board.GetSquareByNotation("b5"), board.GetSquareByNotation("d7"));
            Assert.IsTrue(board.ValidateMove(m1));
            board.MakeMove(m1);

            Move m3 = new Move(board.GetSquareByNotation("h5"), board.GetSquareByNotation("g7"));
            Assert.IsTrue(board.ValidateMove(m3));

            boardPosition = new Dictionary<string, Piece>
            {
                { "a8", new King(Enums.Colour.Black) },
                { "c6", new Bishop(Enums.Colour.White) },
                { "b7", new Rook(Enums.Colour.Black) },
                { "h1", new King(Enums.Colour.White) }
            };

            board = new Board(boardPosition);

            Move m2 = new Move(board.GetSquareByNotation("b7"), board.GetSquareByNotation("b1"));

            Assert.IsFalse(board.ValidateMove(m2));

            //King on king

            boardPosition = new Dictionary<string, Piece>
            {
                { "a1", new King(Enums.Colour.Black) },
                { "c1", new King(Enums.Colour.White) },
            };

            board = new Board(boardPosition);

            Move m4 = new Move(board.GetSquareByNotation("c1"), board.GetSquareByNotation("a2"));
            Assert.IsFalse(board.ValidateMove(m4));
            board.MakeMove(new Move(board.GetSquareByNotation("c1"), board.GetSquareByNotation("c2")));

            Move m5 = new Move(board.GetSquareByNotation("a1"), board.GetSquareByNotation("a2"));
            Assert.IsTrue(board.ValidateMove(m5));

            Move m6 = new Move(board.GetSquareByNotation("a1"), board.GetSquareByNotation("b1"));
            Assert.IsFalse(board.ValidateMove(m6));

            boardPosition = new Dictionary<string, Piece>
            {
                { "a1", new King(Enums.Colour.White) },
                { "h2", new Rook(Enums.Colour.Black) }
            };

            board = new Board(boardPosition);
            Move m7 = new Move(board.GetSquareByNotation("a1"), board.GetSquareByNotation("a2"));
            Assert.IsFalse(board.ValidateMove(m7));

            boardPosition = new Dictionary<string, Piece>
            {
                { "c6", new Bishop(Enums.Colour.Black) },
                { "a8", new King(Enums.Colour.White) },
                { "h1", new Rook(Enums.Colour.White) }
            };

            board = new Board(boardPosition);

            Move m8 = new Move(board.GetSquareByNotation("h1"), board.GetSquareByNotation("h2"));
            Move m9 = new Move(board.GetSquareByNotation("a8"), board.GetSquareByNotation("a7"));
            Assert.IsFalse(board.ValidateMove(m8));
            Assert.IsTrue(board.ValidateMove(m9));
        }

        [TestMethod]
        public void TestPawnAdvanceValidation()
        {
            //valid advance
            Board board = new Board();
            Move whiteAdvanceDouble = new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("e4"));
            Move whiteAdvanceSingle = new Move(board.GetSquareByNotation("d2"), board.GetSquareByNotation("d3"));
            Assert.IsTrue(board.ValidateMove(whiteAdvanceDouble));
            Assert.IsTrue(board.ValidateMove(whiteAdvanceSingle));
            board.MakeMove(whiteAdvanceSingle);

            Move blackAdvanceDouble = new Move(board.GetSquareByNotation("e7"), board.GetSquareByNotation("e5"));
            Move blackAdvanceSingle = new Move(board.GetSquareByNotation("d7"), board.GetSquareByNotation("d6"));
            Assert.IsTrue(board.ValidateMove(blackAdvanceDouble));
            Assert.IsTrue(board.ValidateMove(blackAdvanceSingle));
            board.MakeMove(blackAdvanceSingle);

            Move secondAdvance = new Move(board.GetSquareByNotation("d3"), board.GetSquareByNotation("d4"));
            Assert.IsTrue(board.ValidateMove(secondAdvance));

            //Invalid - Ridiculously invalid pawn move

            board = new Board();

            Move alwaysInvalidPawnMoveWhite = new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("a5"));
            Assert.IsFalse(board.ValidateMove(alwaysInvalidPawnMoveWhite));

            board.MakeMove(new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("e4")));

            Move alwaysInvalidPawnMoveBlack = new Move(board.GetSquareByNotation("e7"), board.GetSquareByNotation("e8"));
            Assert.IsFalse(board.ValidateMove(alwaysInvalidPawnMoveBlack));

            //Invalid advance - Double after first move

            board = new Board();

            Move white1 = new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("e4"));
            board.MakeMove(white1);
            Move black1 = new Move(board.GetSquareByNotation("a7"), board.GetSquareByNotation("a6"));
            board.MakeMove(black1);
            Move white2 = new Move(board.GetSquareByNotation("e4"), board.GetSquareByNotation("e6"));
            Assert.IsFalse(board.ValidateMove(white2));
            board.MakeMove(new Move(board.GetSquareByNotation("e4"), board.GetSquareByNotation("e5")));
            Move black2 = new Move(board.GetSquareByNotation("a6"), board.GetSquareByNotation("a4"));
            Assert.IsFalse(board.ValidateMove(black2));

            //Invalid advance - Destination square occupied

            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "e4", new Pawn(Enums.Colour.White) },
                { "e5", new Pawn(Enums.Colour.Black) },
                { "a2", new Pawn(Enums.Colour.White) },
                { "a4", new Pawn(Enums.Colour.White) },
                { "h7", new Pawn(Enums.Colour.Black) },
                { "h5", new Pawn(Enums.Colour.White) }
            };

            board = new Board(boardPosition);
            Move whiteSingleAdvanceOccupied = new Move(board.GetSquareByNotation("e4"), board.GetSquareByNotation("e5"));
            Move whiteDoubleAdvanceOccupied = new Move(board.GetSquareByNotation("a2"), board.GetSquareByNotation("a4"));
            Assert.IsFalse(board.ValidateMove(whiteSingleAdvanceOccupied));
            Assert.IsFalse(board.ValidateMove(whiteDoubleAdvanceOccupied));

            board.ChangeTurn();

            Move blackSingleAdvanceOccupied = new Move(board.GetSquareByNotation("e5"), board.GetSquareByNotation("e4"));
            Move blackDoubleAdvanceOccupied = new Move(board.GetSquareByNotation("h7"), board.GetSquareByNotation("h5"));
            Assert.IsFalse(board.ValidateMove(blackSingleAdvanceOccupied));
            Assert.IsFalse(board.ValidateMove(blackDoubleAdvanceOccupied));

            //Invalid advance - Path obstructed

            boardPosition = new Dictionary<string, Piece>
            {
                { "e2", new Pawn(Enums.Colour.White) },
                { "e3", new Pawn(Enums.Colour.White) },
                { "h7", new Pawn(Enums.Colour.Black) },
                { "h6", new Pawn(Enums.Colour.White) }
            };

            board = new Board(boardPosition);

            Move whiteAdvancePathBlocked = new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("e4"));
            Assert.IsFalse(board.ValidateMove(whiteAdvancePathBlocked));

            board.ChangeTurn();
            Move blackAdvancePathBlocked = new Move(board.GetSquareByNotation("h7"), board.GetSquareByNotation("h5"));
            Assert.IsFalse(board.ValidateMove(blackAdvancePathBlocked));
        }

        [TestMethod]
        public void TestPawnCaptureValidation()
        {
            //Valid Capture
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "e4", new Pawn(Enums.Colour.White) },
                { "d5", new Pawn(Enums.Colour.Black) },
                { "c4", new Rook(Enums.Colour.White) },
                { "f5", new Knight(Enums.Colour.Black) }
            };

            Board board = new Board(boardPosition);

            Move whiteCaptureLeft = new Move(board.GetSquareByNotation("e4"), board.GetSquareByNotation("d5"));
            Move whiteCaptureRight = new Move(board.GetSquareByNotation("e4"), board.GetSquareByNotation("f5"));
            Assert.IsTrue(board.ValidateMove(whiteCaptureLeft));
            Assert.IsTrue(board.ValidateMove(whiteCaptureRight));

            board.ChangeTurn();
            Move blackCaptureLeft = new Move(board.GetSquareByNotation("d5"), board.GetSquareByNotation("e4"));
            Move blackCaptureRight = new Move(board.GetSquareByNotation("d5"), board.GetSquareByNotation("c4"));
            Assert.IsTrue(board.ValidateMove(blackCaptureLeft));
            Assert.IsTrue(board.ValidateMove(blackCaptureRight));

            //Invalid capture - square unnocupied

            board = new Board();

            Move whiteCaptureUnoccupied = new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("f3"));
            Assert.IsFalse(board.ValidateMove(whiteCaptureUnoccupied));
            board.ChangeTurn();
            Move blackCaptureUnoccupied = new Move(board.GetSquareByNotation("a7"), board.GetSquareByNotation("b6"));
            Assert.IsFalse(board.ValidateMove(blackCaptureUnoccupied));

            //Invalid capture - square occupied by own piece

            boardPosition = new Dictionary<string, Piece>
            {
                { "e4", new Pawn(Enums.Colour.White) },
                { "d5", new Pawn(Enums.Colour.White) },
                { "c3", new Pawn(Enums.Colour.Black) },
                { "f2", new Knight(Enums.Colour.Black) }
            };

            board = new Board(boardPosition);

            Move whiteCaptureOccupiedByOwnColour = new Move(board.GetSquareByNotation("e4"), board.GetSquareByNotation("d5"));
            Assert.IsFalse(board.ValidateMove(whiteCaptureOccupiedByOwnColour));

            board.ChangeTurn();
            Move blackCaptureOccupiedByOwnColour = new Move(board.GetSquareByNotation("c3"), board.GetSquareByNotation("f2"));
            Assert.IsFalse(board.ValidateMove(blackCaptureOccupiedByOwnColour));

            //en passant positive and negative

            boardPosition = new Dictionary<string, Piece>
            {
                { "e2", new Pawn(Enums.Colour.White) },
                { "d4", new Pawn(Enums.Colour.Black) },
                { "a2", new Pawn(Enums.Colour.White) },
            };

            board = new Board(boardPosition);

            board.MakeMove(new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("e4")));
            Move validEnpassant = new Move(board.GetSquareByNotation("d4"), board.GetSquareByNotation("e3"));
            Assert.IsTrue(board.ValidateMove(validEnpassant));

            board = new Board(boardPosition);

            board.MakeMove(new Move(board.GetSquareByNotation("a2"), board.GetSquareByNotation("a4")));
            Move inValidEnpassant = new Move(board.GetSquareByNotation("d4"), board.GetSquareByNotation("e3"));
            Assert.IsFalse(board.ValidateMove(inValidEnpassant));
        }

        [TestMethod]
        public void TestBishopMoveValidation()
        {
            //valid moves

            Board board = new Board("1. e4 e5");

            Move whiteValidBishopMove = new Move(board.GetSquareByNotation("f1"), board.GetSquareByNotation("a6"));
            Assert.IsTrue(board.ValidateMove(whiteValidBishopMove));
            board.MakeMove(whiteValidBishopMove);

            Move blackValidBishopMove = new Move(board.GetSquareByNotation("f8"), board.GetSquareByNotation("a3"));
            Assert.IsTrue(board.ValidateMove(blackValidBishopMove));
            board.MakeMove(blackValidBishopMove);

            Move whiteValidBishopCapture = new Move(board.GetSquareByNotation("a6"), board.GetSquareByNotation("b7"));
            Assert.IsTrue(board.ValidateMove(whiteValidBishopCapture));
            board.MakeMove(whiteValidBishopCapture);

            Move blackValidBishopCapture = new Move(board.GetSquareByNotation("a3"), board.GetSquareByNotation("b2"));
            Assert.IsTrue(board.ValidateMove(blackValidBishopCapture));

            //Invalid move - not on correct vectors

            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "h1", new Bishop(Enums.Colour.White) },
                { "h8", new Bishop(Enums.Colour.Black) }
            };

            board = new Board(boardPosition);
            Move invalidBishopLinearVector = new Move(board.GetSquareByNotation("h1"), board.GetSquareByNotation("h4"));
            Assert.IsFalse(board.ValidateMove(invalidBishopLinearVector));

            board.ChangeTurn();
            Move invalidBishopMoveLikeKnight = new Move(board.GetSquareByNotation("h8"), board.GetSquareByNotation("g6"));
            Assert.IsFalse(board.ValidateMove(invalidBishopMoveLikeKnight));

            //Invalid move - path blocked

            boardPosition = new Dictionary<string, Piece>
            {
                { "h1", new Bishop(Enums.Colour.White) },
                { "h8", new Bishop(Enums.Colour.Black) },
                { "g2", new Pawn(Enums.Colour.Black) },
                { "a1", new Rook(Enums.Colour.Black) }
            };

            board = new Board(boardPosition);

            Move invalidBishopPathBlockedWhite = new Move(board.GetSquareByNotation("h1"), board.GetSquareByNotation("a8"));
            Assert.IsFalse(board.ValidateMove(invalidBishopPathBlockedWhite));
            board.ChangeTurn();
            Move invalidBishopPathBlockedBlack = new Move(board.GetSquareByNotation("h8"), board.GetSquareByNotation("a1"));
            Assert.IsFalse(board.ValidateMove(invalidBishopPathBlockedBlack));
        }

        [TestMethod]
        public void TestRookMoveValidation()
        {
            //Valid moves

            Board board = new Board("1. h4 h5");

            Move whiteValidRookMove = new Move(board.GetSquareByNotation("h1"), board.GetSquareByNotation("h3"));
            Assert.IsTrue(board.ValidateMove(whiteValidRookMove));
            board.MakeMove(whiteValidRookMove);

            Move blackValidRookMove = new Move(board.GetSquareByNotation("h8"), board.GetSquareByNotation("h6"));
            Assert.IsTrue(board.ValidateMove(blackValidRookMove));
            board.MakeMove(blackValidRookMove);

            board.MakeMove(new Move(board.GetSquareByNotation("h3"), board.GetSquareByNotation("g3"))); 
            board.MakeMove(new Move(board.GetSquareByNotation("h6"), board.GetSquareByNotation("f6"))); //move rooks into position to test capture

            Move whiteValidRookCapture = new Move(board.GetSquareByNotation("g3"), board.GetSquareByNotation("g7"));
            Assert.IsTrue(board.ValidateMove(whiteValidRookCapture));
            board.MakeMove(whiteValidRookCapture);

            Move blackValidRookCapture = new Move(board.GetSquareByNotation("f6"), board.GetSquareByNotation("f2"));
            Assert.IsTrue(board.ValidateMove(blackValidRookCapture));

            //Invalid move - not on correct vector

            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "h1", new Rook(Enums.Colour.White) },
                { "h8", new Rook(Enums.Colour.Black) }
            };

            board = new Board(boardPosition);
            Move invalidRookDiagonalVector = new Move(board.GetSquareByNotation("h1"), board.GetSquareByNotation("g2"));
            Assert.IsFalse(board.ValidateMove(invalidRookDiagonalVector));

            board.ChangeTurn();
            Move invalidRookMoveLikeKnight = new Move(board.GetSquareByNotation("h8"), board.GetSquareByNotation("g6"));
            Assert.IsFalse(board.ValidateMove(invalidRookMoveLikeKnight));

            //Invalid move - path blocked

            boardPosition = new Dictionary<string, Piece>
            {
                { "h1", new Rook(Enums.Colour.White) },
                { "h8", new Rook(Enums.Colour.Black) },
                { "b1", new Pawn(Enums.Colour.Black) },
                { "h7", new Rook(Enums.Colour.Black) }
            };

            board = new Board(boardPosition);

            Move invalidRookPathBlockedWhite = new Move(board.GetSquareByNotation("h1"), board.GetSquareByNotation("a1"));
            Assert.IsFalse(board.ValidateMove(invalidRookPathBlockedWhite));
            board.ChangeTurn();
            Move invalidRookPathBlockedBlack = new Move(board.GetSquareByNotation("h8"), board.GetSquareByNotation("h1"));
            Assert.IsFalse(board.ValidateMove(invalidRookPathBlockedBlack));
        }

        [TestMethod]
        public void TestQueenMoveValidation()
        {
            //Valid moves

            Board board = new Board("1. e4 e5");

            Move whiteValidQueenMove = new Move(board.GetSquareByNotation("d1"), board.GetSquareByNotation("g4"));
            Assert.IsTrue(board.ValidateMove(whiteValidQueenMove));
            board.MakeMove(whiteValidQueenMove);

            Move blackValidQueenMove = new Move(board.GetSquareByNotation("d8"), board.GetSquareByNotation("h4"));
            Assert.IsTrue(board.ValidateMove(blackValidQueenMove));
            board.MakeMove(blackValidQueenMove);

            Move whiteValidQueenCapture = new Move(board.GetSquareByNotation("g4"), board.GetSquareByNotation("g7"));
            Assert.IsTrue(board.ValidateMove(whiteValidQueenCapture));
            board.MakeMove(whiteValidQueenCapture);

            Move blackValidQueenCapture = new Move(board.GetSquareByNotation("h4"), board.GetSquareByNotation("h2"));
            Assert.IsTrue(board.ValidateMove(blackValidQueenCapture));

            //Invalid move - not on correct vector

            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "h1", new Queen(Enums.Colour.White) },
                { "h8", new Queen(Enums.Colour.Black) }
            };

            board.ChangeTurn();
            Move invalidQueenMoveIncorrectVector = new Move(board.GetSquareByNotation("h1"), board.GetSquareByNotation("g8"));
            Assert.IsFalse(board.ValidateMove(invalidQueenMoveIncorrectVector));

            //Invalid move - path blocked

            boardPosition = new Dictionary<string, Piece>
            {
                { "h1", new Queen(Enums.Colour.White) },
                { "b1", new Pawn(Enums.Colour.Black) },
                { "h7", new Rook(Enums.Colour.Black) },
                { "f3", new Bishop(Enums.Colour.White) }
            };

            board = new Board(boardPosition);

            Move invalidQueenPathBlocked1 = new Move(board.GetSquareByNotation("h1"), board.GetSquareByNotation("a1"));
            Assert.IsFalse(board.ValidateMove(invalidQueenPathBlocked1));
            Move invalidQueenPathBlocked2 = new Move(board.GetSquareByNotation("h1"), board.GetSquareByNotation("h8"));
            Assert.IsFalse(board.ValidateMove(invalidQueenPathBlocked2));
            Move invalidQueenPathBlocked3 = new Move(board.GetSquareByNotation("h1"), board.GetSquareByNotation("a8"));
            Assert.IsFalse(board.ValidateMove(invalidQueenPathBlocked3));
        }

        [TestMethod]
        public void TestKnightMoveValidation()
        {
            //Valid moves

            Board board = new Board("1. e4 e5");

            Move whiteValidKnightMove = new Move(board.GetSquareByNotation("g1"), board.GetSquareByNotation("f3"));
            Assert.IsTrue(board.ValidateMove(whiteValidKnightMove));
            board.MakeMove(whiteValidKnightMove);

            Move blackValidKnightMove = new Move(board.GetSquareByNotation("g8"), board.GetSquareByNotation("f6"));
            Assert.IsTrue(board.ValidateMove(blackValidKnightMove));
            board.MakeMove(blackValidKnightMove);

            Move whiteValidKnightCapture = new Move(board.GetSquareByNotation("f3"), board.GetSquareByNotation("e5"));
            Assert.IsTrue(board.ValidateMove(whiteValidKnightCapture));
            board.MakeMove(whiteValidKnightCapture);

            Move blackValidKnightCapture = new Move(board.GetSquareByNotation("f6"), board.GetSquareByNotation("e4"));
            Assert.IsTrue(board.ValidateMove(blackValidKnightCapture));

            //Invalid moves - wrong vector and dest occupied

            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "e4", new Knight(Enums.Colour.White) },
                { "f2", new Knight(Enums.Colour.White) },
                { "h1", new King(Enums.Colour.Black) }
            };

            board = new Board(boardPosition);

            Move knightInvalidVector = new Move(board.GetSquareByNotation("e4"), board.GetSquareByNotation("f5"));
            Assert.IsFalse(board.ValidateMove(knightInvalidVector));

            Move knightDestinationOccupied = new Move(board.GetSquareByNotation("e4"), board.GetSquareByNotation("f2"));
            Assert.IsFalse(board.ValidateMove(knightDestinationOccupied));
        }

        [TestMethod]
        public void TestKingMoveValidation()
        {
            //Valid moves

            Board board = new Board("1. e4 e5");

            Move whiteValidKingMove = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("e2"));
            Assert.IsTrue(board.ValidateMove(whiteValidKingMove));
            board.MakeMove(whiteValidKingMove);

            Move blackValidKingMove = new Move(board.GetSquareByNotation("e8"), board.GetSquareByNotation("e7"));
            Assert.IsTrue(board.ValidateMove(blackValidKingMove));
            board.MakeMove(blackValidKingMove);

            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "e1", new King(Enums.Colour.White) },
                { "e8", new King(Enums.Colour.Black) },
                { "f1", new Bishop(Enums.Colour.Black) },
                { "e7", new Pawn(Enums.Colour.White) }
            };

            board = new Board(boardPosition);

            Move whiteValidKingCapture = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("f1"));
            Assert.IsTrue(board.ValidateMove(whiteValidKingCapture));
            board.MakeMove(whiteValidKingCapture);

            Move blackValidKingCapture = new Move(board.GetSquareByNotation("e8"), board.GetSquareByNotation("e7"));
            Assert.IsTrue(board.ValidateMove(blackValidKingCapture));

            //Invalid moves - wrong vector and dest occupied

            boardPosition = new Dictionary<string, Piece>
            {
                { "e1", new King(Enums.Colour.White) },
                { "e2", new Knight(Enums.Colour.White) },
                { "f8", new Queen(Enums.Colour.Black)  }
            };

            board = new Board(boardPosition);

            Move kingInvalidVector = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("e3"));
            Assert.IsFalse(board.ValidateMove(kingInvalidVector));

            Move kingDestinationOccupied = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("e2"));
            Assert.IsFalse(board.ValidateMove(kingDestinationOccupied));

            Move kingMoveIntoCheck = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("f1"));
            Assert.IsFalse(board.ValidateMove(kingMoveIntoCheck));
        }
    }
}
