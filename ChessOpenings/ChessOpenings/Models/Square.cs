using ChessOpenings.Enums;
using ChessOpenings.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Models
{
    public class Square
    {
        Colour Colour { get; set; }
        Piece Piece { get; set; }
        char File { get; set; }
        byte Rank { get; set; }

        public Square(Piece p, Colour c)
        {
            Piece = p;
            Colour = c;
        }

        public override string ToString()
        {
            return File + Rank + " " + Piece.ToString();
        }
    }
}
