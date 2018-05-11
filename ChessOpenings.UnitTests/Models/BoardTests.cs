using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessOpenings.Models;
using ChessOpenings.Pieces;

namespace ChessOpenings.UnitTests
{
    [TestClass]
    public class BoardTests
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
    }    
}
