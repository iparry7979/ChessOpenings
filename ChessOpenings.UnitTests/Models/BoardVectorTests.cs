using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessOpenings.Models;
using ChessOpenings.Pieces;

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
    }
}
