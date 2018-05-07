using ChessOpenings.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Models
{
    public class BoardVector
    {
        public Square[] Sequence { get; set; }

        public BoardVector() { }

        public BoardVector(Square[] sequence)
        {
            Sequence = sequence;
        }

        public void AddSquare(Square square)
        {
            if (Sequence.Length == 0)
            {
                Sequence = new Square[] { square };
            }
            else
            {
                List<Square> squareList = Sequence.ToList();
                squareList.Add(square);
                Sequence = squareList.ToArray();
            }
        }

        public Piece GetFirstPiece()
        {
            if (Sequence == null) return null;
            for (int i = 0; i < Sequence.Length; i++)
            {
                if (Sequence[i].ContainsPiece())
                {
                    return Sequence[i].Piece;
                }
            }
            return null;
        }
    }
}
