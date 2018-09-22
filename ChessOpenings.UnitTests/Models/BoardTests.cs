using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessOpenings.Models;
using ChessOpenings.Pieces;
using System.Collections.Generic;
using ChessOpenings.Helpers;

namespace ChessOpenings.UnitTests
{
    [TestClass]
    public partial class BoardTests
    {
        public Enums.Colour White
        {

            get
            {
                return Enums.Colour.White;
            }
        }
        public Bishop b
        {
            get
            {
                return new Bishop(White);
            }
        }
        public Rook r
        {
            get
            {
                return new Rook(White);
            }
        }

        public BoardVector TopLeft
        {
            get
            {
                return new BoardVector(new Square[]
                {
                    new Square(b, White)
                    {
                        File = 'd',
                        Rank = 5
                    },
                    new Square(b, White)
                    {
                        File = 'c',
                        Rank = 6
                    },
                    new Square(b, White)
                    {
                        File = 'b',
                        Rank = 7
                    },
                     new Square(b, White)
                    {
                        File = 'a',
                        Rank = 8
                    },
                });
            }
        }

        public BoardVector BottomLeft
        {
            get
            {
                return new BoardVector(new Square[]
                {
                     new Square(b, White)
                    {
                        File = 'd',
                        Rank = 3
                    },
                    new Square(b, White)
                    {
                        File = 'c',
                        Rank = 2
                    },
                    new Square(b, White)
                    {
                        File = 'b',
                        Rank = 1
                    },
                });
            }
        }

        public BoardVector BottomRight
        {
            get
            {
                return new BoardVector(new Square[]
                {
                    new Square(b, White)
                    {
                        File = 'f',
                        Rank = 3
                    },
                    new Square(b, White)
                    {
                        File = 'g',
                        Rank = 2
                    },
                    new Square(b, White)
                    {
                        File = 'h',
                        Rank = 1
                    },
                });
            }
        }

        public BoardVector TopRight
        {
            get
            {
                return new BoardVector(new Square[]
                {
                    new Square(b, White)
                    {
                        File = 'f',
                        Rank = 5
                    },
                    new Square(b, White)
                    {
                        File = 'g',
                        Rank = 6
                    },
                    new Square(b, White)
                    {
                        File = 'h',
                        Rank = 7
                    },
                });
            }
        }

        public BoardVector Top
        {
            get
            {
                return new BoardVector(new Square[]
                {
                    new Square(b, White)
                    {
                        File = 'e',
                        Rank = 5
                    },
                    new Square(b, White)
                    {
                        File = 'e',
                        Rank = 6
                    },
                    new Square(b, White)
                    {
                        File = 'e',
                        Rank = 7
                    },
                     new Square(b, White)
                    {
                        File = 'e',
                        Rank = 8
                    },
                });
            }
        }

        public BoardVector Left
        {
            get
            {
                return new BoardVector(new Square[]
                {
                    new Square(b, White)
                    {
                        File = 'd',
                        Rank = 4
                    },
                    new Square(b, White)
                    {
                        File = 'c',
                        Rank = 4
                    },
                    new Square(b, White)
                    {
                        File = 'b',
                        Rank = 4
                    },
                    new Square(b, White)
                    {
                        File = 'a',
                        Rank = 4
                    }
                });
            }
        }

        public BoardVector Bottom
        {
            get
            {
                return new BoardVector(new Square[]
                {
                    new Square(b, White)
                    {
                        File = 'e',
                        Rank = 3
                    },
                    new Square(b, White)
                    {
                        File = 'e',
                        Rank = 2
                    },
                    new Square(b, White)
                    {
                        File = 'e',
                        Rank = 1
                    },
                });
            }
        }

        public BoardVector Right
        {
            get
            {
                return new BoardVector(new Square[]
                {
                   new Square(b, White)
                    {
                        File = 'f',
                        Rank = 4
                    },
                    new Square(b, White)
                    {
                        File = 'g',
                        Rank = 4
                    },
                    new Square(b, White)
                    {
                        File = 'h',
                        Rank = 4
                    },
                });
            }
        }

        [TestMethod]
        public void TestConstructorWithPosition()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>()
            {
                { "a1", new Rook(Enums.Colour.White)},
                { "e1", new King(Enums.Colour.White)},
                { "g1", new Knight(Enums.Colour.White)},
                { "e2", new Pawn(Enums.Colour.White)},
                { "a8", new Rook(Enums.Colour.Black)},
                { "e8", new King(Enums.Colour.Black)},
                { "g8", new Knight(Enums.Colour.Black)},
                { "e7", new Pawn(Enums.Colour.Black)}
            };
            Board testBoard = new Board(boardPosition);

            Square a1 = testBoard.GetSquareByNotation("a1");
            Square e1 = testBoard.GetSquareByNotation("e1");
            Square g1 = testBoard.GetSquareByNotation("g1");
            Square e2 = testBoard.GetSquareByNotation("e2");
            Square a8 = testBoard.GetSquareByNotation("a8");
            Square e8 = testBoard.GetSquareByNotation("e8");
            Square g8 = testBoard.GetSquareByNotation("g8");
            Square e7 = testBoard.GetSquareByNotation("e7");

            //Test Initialisation

            Assert.IsTrue(testBoard.enPassantAllowedSquare == null);
            Assert.IsTrue(testBoard.Turn == Enums.Colour.White);
            PrivateObject p = new PrivateObject(testBoard);
            var hist = p.GetFieldOrProperty("gameHistory");
            Assert.IsTrue(hist != null);
            Assert.IsTrue(testBoard.WhiteCanCastleKingSide);
            Assert.IsTrue(testBoard.WhiteCanCastleQueenSide);
            Assert.IsTrue(testBoard.BlackCanCastleKingSide);
            Assert.IsTrue(testBoard.BlackCanCastleQueenSide);

            //Test squares not null

            Assert.IsNotNull(a1);
            Assert.IsNotNull(e1);
            Assert.IsNotNull(g1);
            Assert.IsNotNull(e2);
            Assert.IsNotNull(a8);
            Assert.IsNotNull(e8);
            Assert.IsNotNull(g8);
            Assert.IsNotNull(e7);

            //Test squares are correct colour

            Assert.IsTrue(a1.Colour == Enums.Colour.Black);
            Assert.IsTrue(e1.Colour == Enums.Colour.Black);
            Assert.IsTrue(g1.Colour == Enums.Colour.Black);
            Assert.IsTrue(e2.Colour == Enums.Colour.White);
            Assert.IsTrue(a8.Colour == Enums.Colour.White);
            Assert.IsTrue(e8.Colour == Enums.Colour.White);
            Assert.IsTrue(g8.Colour == Enums.Colour.White);
            Assert.IsTrue(e7.Colour == Enums.Colour.Black);

            //Test correct pieces

            Assert.IsTrue(a1.Piece is Rook && a1.Piece.colour == Enums.Colour.White);
            Assert.IsTrue(e1.Piece is King && e1.Piece.colour == Enums.Colour.White);
            Assert.IsTrue(g1.Piece is Knight && g1.Piece.colour == Enums.Colour.White);
            Assert.IsTrue(e2.Piece is Pawn && e2.Piece.colour == Enums.Colour.White);
            Assert.IsTrue(a8.Piece is Rook && a8.Piece.colour == Enums.Colour.Black);
            Assert.IsTrue(e8.Piece is King && e8.Piece.colour == Enums.Colour.Black);
            Assert.IsTrue(g8.Piece is Knight && g8.Piece.colour == Enums.Colour.Black);
            Assert.IsTrue(e7.Piece is Pawn && e7.Piece.colour == Enums.Colour.Black);

            //Test remaining squares are empty

            List<Square> occupiedSquares = new List<Square> { a1, e1, g1, e2, a8, e8, g8, e7 };
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Square currentSquare = testBoard.squaresArray[i, j];
                    Assert.IsTrue(occupiedSquares.Contains(currentSquare) == currentSquare.ContainsPiece());
                }
            }
        }

        #region Test Moves
        [TestMethod]
        public void TestMakeMove()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "a1", new Rook(Enums.Colour.White)},
                { "e1", new King(Enums.Colour.White)},
                { "g3", new Knight(Enums.Colour.White)},
                { "h1", new Rook(Enums.Colour.White)},
                { "e2", new Pawn(Enums.Colour.White)},
                { "a8", new Rook(Enums.Colour.Black)},
                { "e8", new King(Enums.Colour.Black)},
                { "f5", new Knight(Enums.Colour.Black)},
                { "h8", new Rook(Enums.Colour.Black)},
                { "e7", new Pawn(Enums.Colour.Black)}
            };

            Board testBoard = new Board(boardPosition);

            Move move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("e2");
            move.ToSquare = testBoard.GetSquareByNotation("e4");
            testBoard.MakeMove(move);
            Assert.IsFalse(testBoard.GetSquareByNotation("e2").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("e4").Piece is Pawn);
            Assert.IsTrue(testBoard.Turn == Enums.Colour.Black);
            Assert.IsTrue(move.AlgebraicNotation == "e4");
            PrivateObject priv = new PrivateObject(testBoard);
            Stack<Move> hist = (Stack<Move>)(priv.GetFieldOrProperty("gameHistory"));
            Assert.IsTrue(hist.Peek() == move);

            move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("f5");
            move.ToSquare = testBoard.GetSquareByNotation("g3");
            testBoard.MakeMove(move);
            Assert.IsFalse(testBoard.GetSquareByNotation("f5").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("g3").Piece is Knight && testBoard.GetSquareByNotation("g3").Piece.colour == Enums.Colour.Black);
            Assert.IsTrue(testBoard.Turn == Enums.Colour.White);
            Assert.IsTrue(move.AlgebraicNotation == "Nxg3");
            hist = (Stack<Move>)(priv.GetFieldOrProperty("gameHistory"));
            Assert.IsTrue(hist.Peek() == move);

            //Test Castling

            testBoard = new Board(boardPosition);

            move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("e1");
            move.ToSquare = testBoard.GetSquareByNotation("g1");
            testBoard.MakeMove(move);
            Assert.IsFalse(testBoard.GetSquareByNotation("e1").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("g1").Piece is King);
            Assert.IsTrue(testBoard.GetSquareByNotation("f1").Piece is Rook);
            Assert.IsFalse(testBoard.GetSquareByNotation("h1").ContainsPiece());

            move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("e8");
            move.ToSquare = testBoard.GetSquareByNotation("g8");
            testBoard.MakeMove(move);
            Assert.IsFalse(testBoard.GetSquareByNotation("e8").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("g8").Piece is King);
            Assert.IsTrue(testBoard.GetSquareByNotation("f8").Piece is Rook);
            Assert.IsFalse(testBoard.GetSquareByNotation("h8").ContainsPiece());

            testBoard = new Board(boardPosition);

            move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("e1");
            move.ToSquare = testBoard.GetSquareByNotation("c1");
            testBoard.MakeMove(move);
            Assert.IsFalse(testBoard.GetSquareByNotation("e1").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("c1").Piece is King);
            Assert.IsTrue(testBoard.GetSquareByNotation("d1").Piece is Rook);
            Assert.IsFalse(testBoard.GetSquareByNotation("a1").ContainsPiece());

            testBoard = new Board(boardPosition);
            testBoard.ChangeTurn();

            move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("e8");
            move.ToSquare = testBoard.GetSquareByNotation("c8");
            testBoard.MakeMove(move);
            Assert.IsFalse(testBoard.GetSquareByNotation("e8").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("c8").Piece is King);
            Assert.IsTrue(testBoard.GetSquareByNotation("d8").Piece is Rook);
            Assert.IsFalse(testBoard.GetSquareByNotation("a8").ContainsPiece());

            //Test Enpassant

            boardPosition = new Dictionary<string, Piece>
            {
                {"e7", new Pawn(Enums.Colour.Black) },
                {"f5", new Pawn(Enums.Colour.White) }
            };

            testBoard = new Board(boardPosition);

            testBoard.ChangeTurn();

            move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("e7");
            move.ToSquare = testBoard.GetSquareByNotation("e5");
            testBoard.MakeMove(move);

            Move move1 = new Move();
            move1.FromSquare = testBoard.GetSquareByNotation("f5");
            move1.ToSquare = testBoard.GetSquareByNotation("e6");
            testBoard.MakeMove(move1);

            Assert.IsFalse(testBoard.GetSquareByNotation("e5").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("e6").Piece.colour == Enums.Colour.White);

            boardPosition = new Dictionary<string, Piece>
            {
                {"e7", new Pawn(Enums.Colour.Black) },
                {"f5", new Pawn(Enums.Colour.White) },
                {"h6", new Rook(Enums.Colour.White) }
            };

            testBoard = new Board(boardPosition);
            testBoard.ChangeTurn();

            move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("e7");
            move.ToSquare = testBoard.GetSquareByNotation("e5");
            testBoard.MakeMove(move);

            move1 = new Move();
            move1.FromSquare = testBoard.GetSquareByNotation("h6");
            move1.ToSquare = testBoard.GetSquareByNotation("e6");
            testBoard.MakeMove(move1);

            Assert.IsTrue(testBoard.GetSquareByNotation("e5").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("e6").Piece is Rook);

            //Test Promotion

            boardPosition = new Dictionary<string, Piece>
            {
                {"e7", new Pawn(Enums.Colour.White) }
            };

            testBoard = new Board(boardPosition);

            move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("e7");
            move.ToSquare = testBoard.GetSquareByNotation("e8");
            move.PromotionPiece = new Queen(Enums.Colour.White);
            testBoard.MakeMove(move);
            Assert.IsFalse(testBoard.GetSquareByNotation("e7").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("e8").Piece is Queen);
            Assert.IsTrue(testBoard.Turn == Enums.Colour.Black);
        }

        [TestMethod]
        public void TestGoBackOneMove()
        {
            Board testBoard = new Board();

            //Test standard move

            Move move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("e2");
            move.ToSquare = testBoard.GetSquareByNotation("e4");
            testBoard.MakeMove(move);
            testBoard.GoBackOneMove();
            Assert.IsTrue(testBoard.GetSquareByNotation("e2").Piece is Pawn);
            Assert.IsFalse(testBoard.GetSquareByNotation("e4").ContainsPiece());

            //Test capture

            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "a1", new Bishop(Enums.Colour.White)},
                { "h8", new Knight(Enums.Colour.Black)},
            };

            testBoard = new Board(boardPosition);
            move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("a1");
            move.ToSquare = testBoard.GetSquareByNotation("h8");
            testBoard.MakeMove(move);
            testBoard.GoBackOneMove();
            Assert.IsTrue(testBoard.GetSquareByNotation("a1").Piece is Bishop);
            Assert.IsTrue(testBoard.GetSquareByNotation("h8").Piece is Knight);

            //Test castling

            boardPosition = new Dictionary<string, Piece>
            {
                { "a1", new Rook(Enums.Colour.White)},
                { "e1", new King(Enums.Colour.White)},
                { "h1", new Rook(Enums.Colour.White)},
                { "a8", new Rook(Enums.Colour.Black)},
                { "e8", new King(Enums.Colour.Black)},
                { "h8", new Rook(Enums.Colour.Black)},
            };

            testBoard = new Board(boardPosition);

            move = new Move(); // White King Side
            move.FromSquare = testBoard.GetSquareByNotation("e1");
            move.ToSquare = testBoard.GetSquareByNotation("g1");
            testBoard.MakeMove(move);
            testBoard.GoBackOneMove();
            Assert.IsFalse(testBoard.GetSquareByNotation("g1").ContainsPiece());
            Assert.IsFalse(testBoard.GetSquareByNotation("f1").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("e1").Piece is King);
            Assert.IsTrue(testBoard.GetSquareByNotation("h1").Piece is Rook);

            testBoard = new Board(boardPosition);

            move = new Move(); // Black King Side
            move.FromSquare = testBoard.GetSquareByNotation("e1");
            move.ToSquare = testBoard.GetSquareByNotation("d1");
            Move move1 = new Move();
            move1.FromSquare = testBoard.GetSquareByNotation("e8");
            move1.ToSquare = testBoard.GetSquareByNotation("g8");
            testBoard.MakeMove(move);
            testBoard.MakeMove(move1);
            testBoard.GoBackOneMove();
            Assert.IsFalse(testBoard.GetSquareByNotation("g8").ContainsPiece());
            Assert.IsFalse(testBoard.GetSquareByNotation("f8").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("e8").Piece is King);
            Assert.IsTrue(testBoard.GetSquareByNotation("h8").Piece is Rook);

            testBoard = new Board(boardPosition);

            move = new Move(); // White Queen Side
            move.FromSquare = testBoard.GetSquareByNotation("e1");
            move.ToSquare = testBoard.GetSquareByNotation("c1");
            testBoard.MakeMove(move);
            testBoard.GoBackOneMove();
            Assert.IsFalse(testBoard.GetSquareByNotation("c1").ContainsPiece());
            Assert.IsFalse(testBoard.GetSquareByNotation("d1").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("e1").Piece is King);
            Assert.IsTrue(testBoard.GetSquareByNotation("a1").Piece is Rook);

            testBoard = new Board(boardPosition);

            move = new Move(); // Black Queen Side
            move.FromSquare = testBoard.GetSquareByNotation("e1");
            move.ToSquare = testBoard.GetSquareByNotation("g1");
            move1 = new Move();
            move1.FromSquare = testBoard.GetSquareByNotation("e8");
            move1.ToSquare = testBoard.GetSquareByNotation("c8");
            testBoard.MakeMove(move);
            testBoard.MakeMove(move1);
            testBoard.GoBackOneMove();
            Assert.IsFalse(testBoard.GetSquareByNotation("c8").ContainsPiece());
            Assert.IsFalse(testBoard.GetSquareByNotation("d8").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("e8").Piece is King);
            Assert.IsTrue(testBoard.GetSquareByNotation("a8").Piece is Rook);

            //Test Enpassant

            boardPosition = new Dictionary<string, Piece>
            {
                {"e7", new Pawn(Enums.Colour.Black) },
                {"f5", new Pawn(Enums.Colour.White) },
            };

            testBoard = new Board(boardPosition);
            testBoard.ChangeTurn();

            move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("e7");
            move.ToSquare = testBoard.GetSquareByNotation("e5");

            move1 = new Move();
            move1.FromSquare = testBoard.GetSquareByNotation("f5");
            move1.ToSquare = testBoard.GetSquareByNotation("e6");

            testBoard.MakeMove(move);
            testBoard.MakeMove(move1);
            testBoard.GoBackOneMove();

            Assert.IsFalse(testBoard.GetSquareByNotation("e7").ContainsPiece());
            Assert.IsFalse(testBoard.GetSquareByNotation("e6").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("e5").Piece is Pawn);
            Assert.IsTrue(testBoard.GetSquareByNotation("f5").Piece is Pawn);

            //Test Promotion

            boardPosition = new Dictionary<string, Piece>
            {
                {"e7", new Pawn(Enums.Colour.White) }
            };

            testBoard = new Board(boardPosition);

            move = new Move();
            move.FromSquare = testBoard.GetSquareByNotation("e7");
            move.ToSquare = testBoard.GetSquareByNotation("e8");
            move.PromotionPiece = new Queen(Enums.Colour.White);
            testBoard.MakeMove(move);
            testBoard.GoBackOneMove();
            Assert.IsFalse(testBoard.GetSquareByNotation("e8").ContainsPiece());
            Assert.IsTrue(testBoard.GetSquareByNotation("e7").Piece is Pawn);
        }

        #endregion
        [TestMethod]
        public void TestGetDiagonalVectors()
        {
            Square square = new Square(new Bishop(Enums.Colour.White), Enums.Colour.White)
            {
                File = 'e',
                Rank = 4
            };

            Board board = new Board();
            BoardVector[] boardVector = board.GetDiagonalVectorsFromSquare(square);

            Assert.IsTrue(boardVector != null, "Board Vector is null");
            Assert.IsTrue(boardVector.Length == 4, "Incorrect Array Length");

            //All diagonal vectors included in array
            Assert.IsTrue(VectorArrayContainsVector(boardVector, TopLeft), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, BottomLeft), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, BottomRight), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, TopRight), "Expected Vector Not Contained");

            BoardVector[] boardVector2 = board.GetDiagonalVectorsFromSquare("h1");
            Assert.IsTrue(boardVector2.Length == 4);

            BoardVector emptyVector = new BoardVector();
            Assert.IsTrue(VectorArrayContainsVector(boardVector2, emptyVector));

        }

        [TestMethod]
        public void TestGetLinearVectors()
        {
            Board board = new Board();
            BoardVector[] boardVector = board.GetLinearVectorsFromSquare("e4");

            Assert.IsTrue(boardVector != null, "Board Vector is null");
            Assert.IsTrue(boardVector.Length == 4, "Incorrect Array Length");

            //All linear vectors included in array
            Assert.IsTrue(VectorArrayContainsVector(boardVector, Top), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, Left), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, Bottom), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, Right), "Expected Vector Not Contained");

            BoardVector[] boardVector2 = board.GetLinearVectorsFromSquare("h4");
            Assert.IsTrue(boardVector2.Length == 4);

            BoardVector emptyVector = new BoardVector();
            Assert.IsTrue(VectorArrayContainsVector(boardVector2, emptyVector));
        }

        public bool VectorArrayContainsVector(BoardVector[] array, BoardVector vector)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(vector))
                {
                    return true;
                }
            }
            return false;
        }

        [TestMethod]
        public void TestAllRadialVectors()
        {
            Board board = new Board();
            BoardVector[] boardVector = board.GetAllRadialVectors("e4");

            Assert.IsTrue(boardVector != null, "Board Vector is null");
            Assert.IsTrue(boardVector.Length == 8, "Incorrect Array Length");

            //All linear vectors included in array
            Assert.IsTrue(VectorArrayContainsVector(boardVector, Top), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, Left), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, Bottom), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, Right), "Expected Vector Not Contained");

            //All diagonal vectors included in array
            Assert.IsTrue(VectorArrayContainsVector(boardVector, TopLeft), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, BottomLeft), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, BottomRight), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, TopRight), "Expected Vector Not Contained");

            BoardVector[] boardVector2 = board.GetLinearVectorsFromSquare("h4");
            Assert.IsTrue(boardVector2.Length == 4);

            BoardVector emptyVector = new BoardVector();
            Assert.IsTrue(VectorArrayContainsVector(boardVector2, emptyVector));
        }

        [TestMethod]
        public void TestKnightConnections()
        {
            Board board = new Board();
            BoardVector knightConnections = board.GetKnightConnections("e4");

            Assert.IsTrue(knightConnections != null, "Board Vector is null");
            Assert.IsTrue(knightConnections.Count == 8, "Incorrect Array Length");

            BoardVector e4Connections = new BoardVector(new Square[]
            {
                new Square(null, White)
                {
                    File = 'd',
                    Rank = 6
                },
                new Square(null, White)
                {
                    File = 'c',
                    Rank = 5
                },
                new Square(null, White)
                {
                    File = 'c',
                    Rank = 3
                },
                new Square(null, White)
                {
                    File = 'd',
                    Rank = 2
                },
                 new Square(null, White)
                {
                    File = 'f',
                    Rank = 2
                },
                  new Square(null, White)
                {
                    File = 'g',
                    Rank = 3
                },
                   new Square(null, White)
                {
                    File = 'g',
                    Rank = 5
                },
                 new Square(null, White)
                {
                    File = 'f',
                    Rank = 6
                },
            });

            for (int i = 0; i < e4Connections.Count; i++)
            {
                Assert.IsTrue(knightConnections.Contains(e4Connections.Sequence[i]), "Vector missing square");
            }

            knightConnections = board.GetKnightConnections("h1");

            Assert.IsTrue(knightConnections != null, "Board Vector is null");
            Assert.IsTrue(knightConnections.Count == 2, "Incorrect Array Length");

            BoardVector h1Connections = new BoardVector(new Square[]
            {
                new Square(null, White)
                {
                    File = 'f',
                    Rank = 2
                },
                new Square(null, White)
                {
                    File = 'g',
                    Rank = 3
                }
            });

            for (int i = 0; i < h1Connections.Count; i++)
            {
                Assert.IsTrue(knightConnections.Contains(h1Connections.Sequence[i]), "Vector missing square");
            }
        }
        [TestMethod]
        public void TestKingVector()
        {
            Board board = new Board();
            BoardVector kingConnections = board.GetKingVector("e4");

            Assert.IsTrue(kingConnections != null, "Board Vector is null");
            Assert.IsTrue(kingConnections.Count == 8, "Incorrect Array Length");

            BoardVector e4Connections = new BoardVector(new Square[]
            {
                new Square(null, White)
                {
                    File = 'd',
                    Rank = 5
                },
                new Square(null, White)
                {
                    File = 'e',
                    Rank = 5
                },
                new Square(null, White)
                {
                    File = 'f',
                    Rank = 3
                },
                new Square(null, White)
                {
                    File = 'd',
                    Rank = 4
                },
                 new Square(null, White)
                {
                    File = 'f',
                    Rank = 4
                },
                  new Square(null, White)
                {
                    File = 'd',
                    Rank = 3
                },
                   new Square(null, White)
                {
                    File = 'e',
                    Rank = 3
                },
                 new Square(null, White)
                {
                    File = 'f',
                    Rank = 3
                },
            });

            for (int i = 0; i < e4Connections.Count; i++)
            {
                Assert.IsTrue(kingConnections.Contains(e4Connections.Sequence[i]), "Vector missing square");
            }

            kingConnections = board.GetKingVector("h1");

            Assert.IsTrue(kingConnections != null, "Board Vector is null");
            Assert.IsTrue(kingConnections.Count == 3, "Incorrect Array Length");

            BoardVector h1Connections = new BoardVector(new Square[]
            {
                new Square(null, White)
                {
                    File = 'g',
                    Rank = 1
                },
                new Square(null, White)
                {
                    File = 'g',
                    Rank = 2
                },
                new Square(null, White)
                {
                    File = 'h',
                    Rank = 2
                }
            });

            for (int i = 0; i < h1Connections.Count; i++)
            {
                Assert.IsTrue(kingConnections.Contains(h1Connections.Sequence[i]), "Vector missing square");
            }
        }

        [TestMethod]
        public void testPawnAttackVectors()
        {
            //Reverse vectors
            Board b = new Board();
            BoardVector pawnAttackVector = b.GetReversePawnAttackVectors("e4", Enums.Colour.White);
            Assert.IsTrue(pawnAttackVector != null, "Board Vector is null");
            Assert.IsTrue(pawnAttackVector.Count == 2, "Incorrect Array Length");

            BoardVector e4Connections = new BoardVector(new Square[]
            {
                new Square(null, White)
                {
                    File = 'd',
                    Rank = 3
                },
                new Square(null, White)
                {
                    File = 'f',
                    Rank = 3
                },
            });

            for (int i = 0; i < e4Connections.Count; i++)
            {
                Assert.IsTrue(pawnAttackVector.Contains(e4Connections.Sequence[i]), "Vector missing square");
            }

            pawnAttackVector = b.GetReversePawnAttackVectors("e4", Enums.Colour.Black);
            Assert.IsTrue(pawnAttackVector != null, "Board Vector is null");
            Assert.IsTrue(pawnAttackVector.Count == 2, "Incorrect Array Length");

            BoardVector e4BlackConnections = new BoardVector(new Square[]
            {
                new Square(null, White)
                {
                    File = 'd',
                    Rank = 5
                },
                new Square(null, White)
                {
                    File = 'f',
                    Rank = 5
                },
            });

            for (int i = 0; i < e4BlackConnections.Count; i++)
            {
                Assert.IsTrue(pawnAttackVector.Contains(e4BlackConnections.Sequence[i]), "Vector missing square");
            }

            pawnAttackVector = b.GetReversePawnAttackVectors("h8", Enums.Colour.White);
            Assert.IsTrue(pawnAttackVector != null, "Board Vector is null");
            Assert.IsTrue(pawnAttackVector.Count == 1, "Incorrect Array Length");

            BoardVector h8WhiteConnections = new BoardVector(new Square[]
            {
                new Square(null, White)
                {
                    File = 'g',
                    Rank = 7
                },
            });

            for (int i = 0; i < h8WhiteConnections.Count; i++)
            {
                Assert.IsTrue(pawnAttackVector.Contains(h8WhiteConnections.Sequence[i]), "Vector missing square");
            }

            pawnAttackVector = b.GetReversePawnAttackVectors("h8", Enums.Colour.Black);
            Assert.IsTrue(pawnAttackVector == null || pawnAttackVector.Count == 0, "Board Vector is not null");

            //Forward Vectors

            b = new Board();
            pawnAttackVector = b.GetPawnAttackVectors("e4", Enums.Colour.White);
            Assert.IsTrue(pawnAttackVector != null, "Board Vector is null");
            Assert.IsTrue(pawnAttackVector.Count == 2, "Incorrect Array Length");

            e4Connections = new BoardVector(new Square[]
            {
                new Square(null, White)
                {
                    File = 'd',
                    Rank = 5
                },
                new Square(null, White)
                {
                    File = 'f',
                    Rank = 5
                },
            });

            for (int i = 0; i < e4Connections.Count; i++)
            {
                Assert.IsTrue(pawnAttackVector.Contains(e4Connections.Sequence[i]), "Vector missing square");
            }

            pawnAttackVector = b.GetPawnAttackVectors("e4", Enums.Colour.Black);
            Assert.IsTrue(pawnAttackVector != null, "Board Vector is null");
            Assert.IsTrue(pawnAttackVector.Count == 2, "Incorrect Array Length");

            e4BlackConnections = new BoardVector(new Square[]
            {
                new Square(null, White)
                {
                    File = 'd',
                    Rank = 3
                },
                new Square(null, White)
                {
                    File = 'f',
                    Rank = 3
                },
            });

            for (int i = 0; i < e4BlackConnections.Count; i++)
            {
                Assert.IsTrue(pawnAttackVector.Contains(e4BlackConnections.Sequence[i]), "Vector missing square");
            }

            pawnAttackVector = b.GetPawnAttackVectors("h7", Enums.Colour.White);
            Assert.IsTrue(pawnAttackVector != null, "Board Vector is null");
            Assert.IsTrue(pawnAttackVector.Count == 1, "Incorrect Array Length");

            BoardVector h7WhiteConnections = new BoardVector(new Square[]
            {
                new Square(null, White)
                {
                    File = 'g',
                    Rank = 8
                },
            });

            for (int i = 0; i < h7WhiteConnections.Count; i++)
            {
                Assert.IsTrue(pawnAttackVector.Contains(h7WhiteConnections.Sequence[i]), "Vector missing square");
            }

            pawnAttackVector = b.GetPawnAttackVectors("h8", Enums.Colour.White);
            Assert.IsTrue(pawnAttackVector == null || pawnAttackVector.Count == 0, "Board Vector is not null");
        }

        [TestMethod]
        public void TestPawnAdvanceVector()
        {
            //Reverse vectors
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "a2", new Pawn(Enums.Colour.White) },
                { "a5", new Pawn(Enums.Colour.White) },
                { "h7", new Pawn(Enums.Colour.Black) },
                { "g2", new Pawn(Enums.Colour.Black) }
            };

            Board board = new Board(boardPosition);

            BoardVector v1 = board.GetReversePawnAdvanceVector(board.GetSquareByNotation("a4"), Enums.Colour.White);
            Assert.IsTrue(v1.Count == 2);
            Assert.IsTrue(v1.Sequence[0].Notation == "a3");
            Assert.IsTrue(v1.Sequence[1].Notation == "a2");

            v1 = board.GetReversePawnAdvanceVector(board.GetSquareByNotation("a6"), Enums.Colour.White);
            Assert.IsTrue(v1.Count == 1);
            Assert.IsTrue(v1.Sequence[0].Notation == "a5");

            v1 = board.GetReversePawnAdvanceVector(board.GetSquareByNotation("h5"), Enums.Colour.Black);
            Assert.IsTrue(v1.Count == 2);
            Assert.IsTrue(v1.Sequence[0].Notation == "h6");
            Assert.IsTrue(v1.Sequence[1].Notation == "h7");

            v1 = board.GetReversePawnAdvanceVector(board.GetSquareByNotation("g1"), Enums.Colour.Black);
            Assert.IsTrue(v1.Count == 1);
            Assert.IsTrue(v1.Sequence[0].Notation == "g2");

            //Forward vectors

            v1 = board.GetPawnAdvanceVector(board.GetSquareByNotation("a2"), Enums.Colour.White);
            Assert.IsTrue(v1.Count == 2);
            Assert.IsTrue(v1.Sequence[0].Notation == "a3");
            Assert.IsTrue(v1.Sequence[1].Notation == "a4");

            v1 = board.GetPawnAdvanceVector(board.GetSquareByNotation("a5"), Enums.Colour.White);
            Assert.IsTrue(v1.Count == 1);
            Assert.IsTrue(v1.Sequence[0].Notation == "a6");

            v1 = board.GetPawnAdvanceVector(board.GetSquareByNotation("h7"), Enums.Colour.Black);
            Assert.IsTrue(v1.Count == 2);
            Assert.IsTrue(v1.Sequence[0].Notation == "h6");
            Assert.IsTrue(v1.Sequence[1].Notation == "h5");

            v1 = board.GetPawnAdvanceVector(board.GetSquareByNotation("g2"), Enums.Colour.Black);
            Assert.IsTrue(v1.Count == 1);
            Assert.IsTrue(v1.Sequence[0].Notation == "g1");
        }

        [TestMethod]
        public void TestSquareUnderAttack_PositiveFlows()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "b1", new Bishop(Enums.Colour.White) }
            };
            Board board = new Board(boardPosition);
            Square e4 = board.GetSquareByNotation("e4");
            Assert.IsTrue(board.SquareIsUnderAttack(e4, Enums.Colour.White));
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.Black));

            board.GetSquareByNotation("c2").Piece = new Rook(Enums.Colour.White);
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.White));

            boardPosition = new Dictionary<string, Piece>
            {
                { "a4", new Rook(Enums.Colour.White) }
            };
            board = new Board(boardPosition);
            e4 = board.GetSquareByNotation("e4");
            Assert.IsTrue(board.SquareIsUnderAttack(e4, Enums.Colour.White));
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.Black));

            board.GetSquareByNotation("b4").Piece = new Pawn(Enums.Colour.White);
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.White));

            boardPosition = new Dictionary<string, Piece>
            {
                { "h7", new Queen(Enums.Colour.White) }
            };
            board = new Board(boardPosition);
            e4 = board.GetSquareByNotation("e4");
            Assert.IsTrue(board.SquareIsUnderAttack(e4, Enums.Colour.White));
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.Black));

            board.GetSquareByNotation("g6").Piece = new Knight(Enums.Colour.White);
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.White));

            boardPosition = new Dictionary<string, Piece>
            {
                { "c5", new Knight(Enums.Colour.White) }
            };
            board = new Board(boardPosition);
            e4 = board.GetSquareByNotation("e4");
            Assert.IsTrue(board.SquareIsUnderAttack(e4, Enums.Colour.White));
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.Black));

            board.GetSquareByNotation("d4").Piece = new Bishop(Enums.Colour.White);
            board.GetSquareByNotation("d5").Piece = new Knight(Enums.Colour.White);
            Assert.IsTrue(board.SquareIsUnderAttack(e4, Enums.Colour.White));

            boardPosition = new Dictionary<string, Piece>
            {
                { "d3", new Pawn(Enums.Colour.White) }
            };
            board = new Board(boardPosition);
            e4 = board.GetSquareByNotation("e4");
            Assert.IsTrue(board.SquareIsUnderAttack(e4, Enums.Colour.White));
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.Black));

            board.GetSquareByNotation("d3").Piece.colour = Enums.Colour.Black;
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.Black));

            boardPosition = new Dictionary<string, Piece>
            {
                { "d5", new Pawn(Enums.Colour.Black) }
            };
            board = new Board(boardPosition);
            e4 = board.GetSquareByNotation("e4");
            Assert.IsTrue(board.SquareIsUnderAttack(e4, Enums.Colour.Black));
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.White));

            board.GetSquareByNotation("d5").Piece.colour = Enums.Colour.White;
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.White));

            boardPosition = new Dictionary<string, Piece>
            {
                { "e5", new King(Enums.Colour.White) }
            };
            board = new Board(boardPosition);
            e4 = board.GetSquareByNotation("e4");
            Assert.IsTrue(board.SquareIsUnderAttack(e4, Enums.Colour.White));
            Assert.IsFalse(board.SquareIsUnderAttack(e4, Enums.Colour.Black));
        }

        [TestMethod]
        public void TestGetSquaresContainingPiece()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "h7", new Queen(Enums.Colour.White) }
            };

            Board board = new Board(boardPosition);

            List<Square> result = board.GetSquaresContainingPiece(new Queen(Enums.Colour.White));
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].Notation == "h7");

            boardPosition = new Dictionary<string, Piece>
            {
                { "h7", new Queen(Enums.Colour.White) },
                { "a2", new Queen(Enums.Colour.White) }
            };

            board = new Board(boardPosition);

            result = board.GetSquaresContainingPiece(new Queen(Enums.Colour.White));
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(board.GetSquareByNotation("h7")) && result.Contains(board.GetSquareByNotation("a2")));

            boardPosition = new Dictionary<string, Piece>
            {
                { "h7", new Queen(Enums.Colour.Black) },
                { "a2", new Queen(Enums.Colour.Black) }
            };

            board = new Board(boardPosition);

            result = board.GetSquaresContainingPiece(new Queen(Enums.Colour.White));
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void TestKingCheckMethods()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "h7", new King(Enums.Colour.White) },
                { "d7", new Rook(Enums.Colour.Black) },
                { "e4", new King(Enums.Colour.Black) }
            };

            Board board = new Board(boardPosition);

            Assert.IsTrue(board.WhiteKingIsInCheck());
            Assert.IsTrue(board.IsEitherKingInCheck());
            Assert.IsFalse(board.BlackKingIsInCheck());

            boardPosition = new Dictionary<string, Piece>
            {
                { "h7", new King(Enums.Colour.White) },
                { "d3", new Rook(Enums.Colour.Black) },
                { "e4", new King(Enums.Colour.Black) }
            };

            board = new Board(boardPosition);

            Move move = new Move();
            move.FromSquare = board.GetSquareByNotation("d3");
            move.ToSquare = board.GetSquareByNotation("d7");

            Assert.IsFalse(board.WhiteKingIsInCheck());
            Assert.IsFalse(board.IsEitherKingInCheck());
            Assert.IsTrue(board.WhiteKingIsInCheck(move));
            Assert.IsTrue(board.IsEitherKingInCheck(move));
            Assert.IsFalse(board.BlackKingIsInCheck(move));
            //check that the move has been reversed
            Assert.IsTrue(board.GetSquareByNotation("d3").Piece is Rook);
            Assert.IsFalse(board.GetSquareByNotation("d7").ContainsPiece());
        }

        [TestMethod]
        public void TestGetAllMovesByNotation()
        {
            Board board = new Board();

            List<string> result = board.GetAllMovesByNotation();

            Assert.IsTrue(result.Count == 0);

            Move m = new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("e4"));

            board.MakeMove(m);

            result = board.GetAllMovesByNotation();

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0] == "e4");

            Move m1 = new Move(board.GetSquareByNotation("b8"), board.GetSquareByNotation("c6"));

            board.MakeMove(m1);

            result = board.GetAllMovesByNotation();

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result[0] == "e4");
            Assert.IsTrue(result[1] == "Nc6");
        }

        [TestMethod]
        public void TestPGNGeneration()
        {
            Board board = new Board();
            string result = board.ToPGN();
            Assert.IsTrue(result == "");

            board = new Board();

            Move m = new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("e4"));
            board.MakeMove(m);
            result = board.ToPGN();
            Assert.IsTrue(result == "1. e4");

            board = new Board();
            m = new Move(board.GetSquareByNotation("e2"), board.GetSquareByNotation("e4"));
            board.MakeMove(m);
            Move m1 = new Move(board.GetSquareByNotation("e7"), board.GetSquareByNotation("e5"));
            board.MakeMove(m1);
            Move m2 = new Move(board.GetSquareByNotation("g1"), board.GetSquareByNotation("f3"));
            board.MakeMove(m2);
            Move m3 = new Move(board.GetSquareByNotation("b8"), board.GetSquareByNotation("c6"));
            board.MakeMove(m3);
            Move m4 = new Move(board.GetSquareByNotation("f1"), board.GetSquareByNotation("b5"));
            board.MakeMove(m4);
            Move m5 = new Move(board.GetSquareByNotation("a7"), board.GetSquareByNotation("a6"));
            board.MakeMove(m5);
            Move m6 = new Move(board.GetSquareByNotation("b5"), board.GetSquareByNotation("a4"));
            board.MakeMove(m6);
            Move m7 = new Move(board.GetSquareByNotation("g8"), board.GetSquareByNotation("f6"));
            board.MakeMove(m7);
            Move m8 = new Move(board.GetSquareByNotation("e1"), board.GetSquareByNotation("g1"));
            board.MakeMove(m8);

            result = board.ToPGN();

            Assert.IsTrue(result == "1. e4 e5 2. Nf3 Nc6 3. Bb5 a6 4. Ba4 Nf6 5. O-O");
        }

        [TestMethod]
        public void TestGetBoardPosition()
        {
            Dictionary<string, Piece> boardPosition = new Dictionary<string, Piece>
            {
                { "e4", new Pawn(Enums.Colour.White) },
                { "e8", new King(Enums.Colour.Black) }
            };

            Board board = new Board(boardPosition);

            Dictionary<string, Piece> result = board.GetBoardPosition();
            Assert.IsTrue(result.Count == 2);
            Piece p1;
            Piece p2;
            result.TryGetValue("e4", out p1);
            result.TryGetValue("e8", out p2);
            Assert.IsTrue(p1.GetPieceNotation() == "WP");
            Assert.IsTrue(p2.GetPieceNotation() == "BK");
        }

        [TestMethod]
        public void Constructor_PortableGameNotation()
        {
            string start = "";
            string morphyDefence = "1. e4 e5 2. Nf3 Nc6 3. Bb5 a6 4. Ba4 Nf6";
            string queensGambitWithComments = "[tag1 \"xx\"] [tag2 \"yy\"] 1. d4 d5 {this is a comment} 2. c4 $44 dxc4";

            Dictionary<string, Piece> morphyDefencePosition = new Dictionary<string, Piece>
            {
                { "a1", new Rook(Enums.Colour.White) },
                { "b1", new Knight(Enums.Colour.White) },
                { "c1", new Bishop(Enums.Colour.White) },
                { "d1", new Queen(Enums.Colour.White) },
                { "e1", new King(Enums.Colour.White) },
                { "a4", new Bishop(Enums.Colour.White) },
                { "f3", new Knight(Enums.Colour.White) },
                { "h1", new Rook(Enums.Colour.White) },
                { "a2", new Pawn(Enums.Colour.White) },
                { "b2", new Pawn(Enums.Colour.White) },
                { "c2", new Pawn(Enums.Colour.White) },
                { "d2", new Pawn(Enums.Colour.White) },
                { "e4", new Pawn(Enums.Colour.White) },
                { "f2", new Pawn(Enums.Colour.White) },
                { "g2", new Pawn(Enums.Colour.White) },
                { "h2", new Pawn(Enums.Colour.White) },

                { "a8", new Rook(Enums.Colour.Black) },
                { "c6", new Knight(Enums.Colour.Black) },
                { "c8", new Bishop(Enums.Colour.Black) },
                { "d8", new Queen(Enums.Colour.Black) },
                { "e8", new King(Enums.Colour.Black) },
                { "f8", new Bishop(Enums.Colour.Black) },
                { "f6", new Knight(Enums.Colour.Black) },
                { "h8", new Rook(Enums.Colour.Black) },
                { "a6", new Pawn(Enums.Colour.Black) },
                { "b7", new Pawn(Enums.Colour.Black) },
                { "c7", new Pawn(Enums.Colour.Black) },
                { "d7", new Pawn(Enums.Colour.Black) },
                { "e5", new Pawn(Enums.Colour.Black) },
                { "f7", new Pawn(Enums.Colour.Black) },
                { "g7", new Pawn(Enums.Colour.Black) },
                { "h7", new Pawn(Enums.Colour.Black) },
            };

            Dictionary<string, Piece> queensGambitPosition = new Dictionary<string, Piece>
            {
                { "a1", new Rook(Enums.Colour.White) },
                { "b1", new Knight(Enums.Colour.White) },
                { "c1", new Bishop(Enums.Colour.White) },
                { "d1", new Queen(Enums.Colour.White) },
                { "e1", new King(Enums.Colour.White) },
                { "f1", new Bishop(Enums.Colour.White) },
                { "g1", new Knight(Enums.Colour.White) },
                { "h1", new Rook(Enums.Colour.White) },
                { "a2", new Pawn(Enums.Colour.White) },
                { "b2", new Pawn(Enums.Colour.White) },
                { "d4", new Pawn(Enums.Colour.White) },
                { "e2", new Pawn(Enums.Colour.White) },
                { "f2", new Pawn(Enums.Colour.White) },
                { "g2", new Pawn(Enums.Colour.White) },
                { "h2", new Pawn(Enums.Colour.White) },

                { "a8", new Rook(Enums.Colour.Black) },
                { "b8", new Knight(Enums.Colour.Black) },
                { "c8", new Bishop(Enums.Colour.Black) },
                { "d8", new Queen(Enums.Colour.Black) },
                { "e8", new King(Enums.Colour.Black) },
                { "f8", new Bishop(Enums.Colour.Black) },
                { "g8", new Knight(Enums.Colour.Black) },
                { "h8", new Rook(Enums.Colour.Black) },
                { "a7", new Pawn(Enums.Colour.Black) },
                { "b7", new Pawn(Enums.Colour.Black) },
                { "c7", new Pawn(Enums.Colour.Black) },
                { "c4", new Pawn(Enums.Colour.Black) },
                { "e7", new Pawn(Enums.Colour.Black) },
                { "f7", new Pawn(Enums.Colour.Black) },
                { "g7", new Pawn(Enums.Colour.Black) },
                { "h7", new Pawn(Enums.Colour.Black) },
            };

            Board board = new Board(start);

            Dictionary<string, Piece> result = board.GetBoardPosition();

            Assert.IsTrue(CompareBoardPositions(result, Constants.startPosition));

            board = new Board(morphyDefence);

            result = board.GetBoardPosition();

            Assert.IsTrue(CompareBoardPositions(result, morphyDefencePosition));

            Assert.IsTrue(board.ToPGN() == morphyDefence); 

            board = new Board(queensGambitWithComments);

            result = board.GetBoardPosition();

            Assert.IsTrue(CompareBoardPositions(result, queensGambitPosition));
        }

        [TestMethod]
        public void TestToBoardPosition()
        {
            Board board = new Board();
            BoardPosition position = board.ToBoardPosition();
            Piece p;
            Assert.IsTrue(position.Position.Count == 32);
            if (position.Position.TryGetValue("a1", out p))
            {
                Assert.IsTrue(p.GetPieceNotation() == "WR", "Wrong piece at a1");
            }
            else
            {
                Assert.Fail("No piece at a1");
            }
            if (position.Position.TryGetValue("b2", out p))
            {
                Assert.IsTrue(p.GetPieceNotation() == "WP", "Wrong piece at b2");
            }
            else
            {
                Assert.Fail("No piece at b2");
            }
            if (position.Position.TryGetValue("e8", out p))
            {
                Assert.IsTrue(p.GetPieceNotation() == "BK", "Wrong piece at e8");
            }
            else
            {
                Assert.Fail("No piece at e8");
            }
            if (position.Position.TryGetValue("e7", out p))
            {
                Assert.IsTrue(p.GetPieceNotation() == "BP", "Wrong piece at e7");
            }
            else
            {
                Assert.Fail("No piece at e7");
            }
            Assert.IsFalse(position.Position.ContainsKey("a3"));

        }

        private bool CompareBoardPositions(Dictionary<string, Piece> p1, Dictionary<string, Piece> p2)
        {
            bool equal = true;
            foreach(KeyValuePair<string, Piece> square in p1)
            {
                if (p2.ContainsKey(square.Key))
                {
                    if (!p2[square.Key].Equals(square.Value))
                    {
                        equal = false;
                    }
                }
                else
                {
                    equal = false;
                }
            }
            return equal;
        }
    }    
}
