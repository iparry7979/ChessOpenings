using ChessOpenings.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Helpers
{
    public static class Constants
    {
        public static Dictionary<string, Piece> startPosition = new Dictionary<string, Piece>
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
            { "c2", new Pawn(Enums.Colour.White) },
            { "d2", new Pawn(Enums.Colour.White) },
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
            { "d7", new Pawn(Enums.Colour.Black) },
            { "e7", new Pawn(Enums.Colour.Black) },
            { "f7", new Pawn(Enums.Colour.Black) },
            { "g7", new Pawn(Enums.Colour.Black) },
            { "h7", new Pawn(Enums.Colour.Black) },
        };
    }
}
