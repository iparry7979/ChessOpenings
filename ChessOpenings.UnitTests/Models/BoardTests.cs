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

            Assert.IsTrue(boardVector != null, "Board Vector is null");
            Assert.IsTrue(boardVector.Length == 4, "Incorrect Array Length");

            Enums.Colour white = Enums.Colour.White;
            Bishop b = new Bishop(white);

            BoardVector topLeft = new BoardVector(new Square[]
                {
                    new Square(b, white)
                    {
                        File = 'd',
                        Rank = 5
                    },
                    new Square(b, white)
                    {
                        File = 'c',
                        Rank = 6
                    },
                    new Square(b, white)
                    {
                        File = 'b',
                        Rank = 7
                    },
                     new Square(b, white)
                    {
                        File = 'a',
                        Rank = 8
                    },
                });

            BoardVector bottomLeft = new BoardVector(new Square[]
               {
                    new Square(b, white)
                    {
                        File = 'd',
                        Rank = 3
                    },
                    new Square(b, white)
                    {
                        File = 'c',
                        Rank = 2
                    },
                    new Square(b, white)
                    {
                        File = 'b',
                        Rank = 1
                    },
               });

            BoardVector bottomRight = new BoardVector(new Square[]
              {
                    new Square(b, white)
                    {
                        File = 'f',
                        Rank = 3
                    },
                    new Square(b, white)
                    {
                        File = 'g',
                        Rank = 2
                    },
                    new Square(b, white)
                    {
                        File = 'h',
                        Rank = 1
                    },
              });

            BoardVector topRight = new BoardVector(new Square[]
              {
                    new Square(b, white)
                    {
                        File = 'f',
                        Rank = 5
                    },
                    new Square(b, white)
                    {
                        File = 'g',
                        Rank = 6
                    },
                    new Square(b, white)
                    {
                        File = 'h',
                        Rank = 7
                    },
              });

            //All diagonal vectors included in array
            Assert.IsTrue(VectorArrayContainsVector(boardVector, topLeft), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, bottomLeft), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, bottomRight), "Expected Vector Not Contained");
            Assert.IsTrue(VectorArrayContainsVector(boardVector, topRight), "Expected Vector Not Contained");

            Square square2 = new Square(b, white)
            {
                File = 'h',
                Rank = 1
            };

            BoardVector[] boardVector2 = board.GetDiagonalsFromSquare(square2);
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
    }
}
