using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessOpenings.Models;
using ChessOpenings.Pieces;
using System.Collections.Generic;

namespace ChessOpenings.UnitTests
{
    [TestClass]
    public class BoardVectorTests
    {
        [TestMethod]
        public void TestEqualsOverride()
        {
            Bishop b = new Bishop(Enums.Colour.White);
            Knight n = new Knight(Enums.Colour.White);
            BoardVector emptyVector = new BoardVector();
            BoardVector testVectorLength1 = new BoardVector
            {
                Sequence = new Square[]
                {
                    new Square(b, Enums.Colour.White)
                    {
                        File = 'a',
                        Rank = 1
                    },
                }
            };
            BoardVector testVectorLength2 = new BoardVector
            {
                Sequence = new Square[]
                {
                    new Square(b, Enums.Colour.White)
                    {
                        File = 'a',
                        Rank = 1
                    },
                    new Square(b, Enums.Colour.White)
                    {
                        File = 'b',
                        Rank = 2
                    },
                }
            };

            BoardVector equalVector = new BoardVector
            {
                Sequence = new Square[]
               {
                    new Square(n, Enums.Colour.White)
                    {
                        File = 'a',
                        Rank = 1
                    },
                    new Square(n, Enums.Colour.White)
                    {
                        File = 'b',
                        Rank = 2
                    },
               }
            };

            BoardVector unEqualVector = new BoardVector
            {
                Sequence = new Square[]
                {
                    new Square(b, Enums.Colour.White)
                    {
                        File = 'c',
                        Rank = 4
                    },
                    new Square(b, Enums.Colour.White)
                    {
                        File = 'e',
                        Rank = 7
                    },
                }
            };

            //RunTests

            //null object not equal
            Assert.IsFalse(testVectorLength1.Equals(null));

            //Different length vectors not equal
            Assert.IsFalse(testVectorLength1.Equals(testVectorLength2));

            //Vectors of same size but different values are not equal
            Assert.IsFalse(testVectorLength2.Equals(unEqualVector));

            //Vectors containing squares of same rank and file are equal (different piece)
            Assert.IsTrue(testVectorLength2.Equals(equalVector));

            //Empty vectors are equal
            Assert.IsTrue(emptyVector.Equals(emptyVector));
        }

        [TestMethod]
        public void TestFirstOccupiedSquare()
        {
            Bishop b = new Bishop(Enums.Colour.White);
            Knight k = new Knight(Enums.Colour.Black);
            BoardVector testVector = GetTestVector(3);
            testVector.AddPiece(b, testVector.Count - 1);
            Square occupiedSquare = testVector.GetFirstOccupiedSquare();
            Assert.IsTrue(occupiedSquare.Notation == "a3", "Incorrect square returned");
            Assert.IsTrue(occupiedSquare.Piece == b, "Incorrect Piece");

            testVector = GetTestVector(4);
            testVector.AddPiece(b, testVector.Count - 1);
            testVector.AddPiece(k, testVector.Count - 2);
            occupiedSquare = testVector.GetFirstOccupiedSquare();
            Assert.IsTrue(occupiedSquare.Notation == "a3", "Incorrect square returned");
            Assert.IsTrue(occupiedSquare.Piece == k, "Incorrect piece");

            testVector = GetTestVector(4);
            occupiedSquare = testVector.GetFirstOccupiedSquare();
            Assert.IsNull(occupiedSquare, "Empty vector did not return null");
        }

        [TestMethod]
        public void TestGetSquaresContainingPiece()
        {
            Knight blackKnight = new Knight(Enums.Colour.White);
            Knight whiteKnight = new Knight(Enums.Colour.Black);
            Bishop blackBishop = new Bishop(Enums.Colour.Black);

            BoardVector testVector = GetTestVector(6);

            testVector.AddPiece(blackKnight, 0);
            testVector.AddPiece(whiteKnight, testVector.Count - 1);

            List<Square> result = testVector.GetSquaresContainingPiece(blackKnight, false);

            //Test correct results when ignoring colour
            Assert.IsTrue(result.Count == 1, "Array is unexpected size");
            Assert.IsTrue(result[0].Notation == testVector.Sequence[0].Notation, "Wrong square returned");

            result = testVector.GetSquaresContainingPiece(blackKnight, true);

            //Test correct results when not ignoring colour
            Assert.IsTrue(result.Count == 2, "Array is unexpected size");
            Assert.IsTrue(result[0].Notation == testVector.Sequence[0].Notation, "Wrong square returned");
            Assert.IsTrue(result[1].Notation == testVector.Sequence[testVector.Count - 1].Notation, "Wrong square returned");

            result = testVector.GetSquaresContainingPiece(blackBishop, false);

            //Test correct results when different piece used
            Assert.IsTrue(result == null || result.Count == 0);

            testVector = GetTestVector(2);

            result = testVector.GetSquaresContainingPiece(blackBishop, false);

            //Test correct results when all squares empty
            Assert.IsTrue(result == null || result.Count == 0);

            testVector = GetTestVector(0);

            result = testVector.GetSquaresContainingPiece(blackBishop, false);

            //Test zero vector
            Assert.IsTrue(result == null || result.Count == 0);
        }

        public BoardVector GetTestVector(int length, bool ordered = true)
        {
            BoardVector testVector = new BoardVector();
            for (int i = 0; i < length; i++)
            {
                testVector.AddSquare(new Square(null, Enums.Colour.White)
                {
                    File = 'a',
                    Rank = (byte)(i + 1)
                });
            }
            return testVector;
        }


    }
}
