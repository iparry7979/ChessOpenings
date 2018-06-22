using ChessOpenings.Enums;
using ChessOpenings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Pieces
{
    public abstract class Piece
    {
        public Colour colour { get; set; }

        public Piece(Colour c)
        {
            colour = c;
        }

        public string GetPieceNotation()
        {
            if (colour == Colour.White) return "W" + ToString();
            return "B" + ToString();
        }

        public abstract bool ValidateMove(Square start, Square end, Board board);

        public abstract override string ToString();

        public override bool Equals(object obj)
        {
            if (obj is Piece)
            {
                return ((Piece)obj).GetPieceNotation() == GetPieceNotation();
            }
            return false;
        }

        public static Piece CreatePiece(string pieceNotation, Enums.Colour colour)
        {
            switch (pieceNotation)
            {
                case "P":
                    return new Pawn(colour);
                case "N":
                    return new Knight(colour);
                case "B":
                    return new Bishop(colour);
                case "R":
                    return new Rook(colour);
                case "Q":
                    return new Queen(colour);
                case "K":
                    return new King(colour);
                default:
                    return null;
            }
        }
    }
}
