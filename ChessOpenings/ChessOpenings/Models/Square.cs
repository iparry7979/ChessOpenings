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
        public Colour Colour { get; set; }
        public Piece Piece { get; set; }
        public char File { get; set; }
        public byte Rank { get; set; }
        public string Notation
        {
            get
            {
                return File.ToString() + Rank.ToString();
            }
        }

        public Square(Piece p, Colour c)
        {
            Piece = p;
            Colour = c;
        }

        public override string ToString()
        {
            return File + Rank + " " + Piece.ToString();
        }

        public bool ContainsPiece()
        {
            return Piece != null;
        }

        public override bool Equals(object obj)
        {
            if (obj is Square)
            {
                if (this.Notation == ((Square)obj).Notation)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
