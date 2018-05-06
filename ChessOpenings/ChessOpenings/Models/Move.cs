using ChessOpenings.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Models
{
    public class Move
    {
        public Square FromSquare { get; set; }
        public Square ToSquare { get; set; }
        public Piece PieceTaken { get; set; }
        public string AlgebraicNotation { get; set; }

    }
}
