using ChessOpenings.Enums;
using ChessOpenings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Colour c) : base(c) { }

        public override bool ValidateMove(Square start, Square end, Board board)
        {
            return true;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
