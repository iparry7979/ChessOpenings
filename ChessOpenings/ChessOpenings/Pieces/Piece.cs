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

        //protected abstract Square[] DeterminePath(Square start, Square end);
    }
}
