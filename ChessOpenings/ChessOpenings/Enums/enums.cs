using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Enums
{
    public enum Colour
    {
        White,
        Black
    }

    enum Material
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King,
        None
    }

    public enum BoardOrientation
    {
        Standard,
        Inverted
    }
}
