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
        public int Count
        {
            get
            {
                return Sequence.Length;
            }
        }

        public BoardVector()
        {
            Sequence = new Square[0];       
        }

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

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is BoardVector))
            {
                return false;
            }

            BoardVector comparator = obj as BoardVector;
            if (comparator.Sequence == null && this.Sequence ==null)
            {
                return true;
            }

            if (comparator.Sequence == null || this.Sequence == null)
            {
                return false;
            }

            if (comparator.Sequence.Length != this.Sequence.Length)
            {
                return false;
            }

            for (int i = 0; i < this.Sequence.Length; i++)
            {
                if (!(this.Sequence[i].Rank == comparator.Sequence[i].Rank && this.Sequence[i].File == comparator.Sequence[i].File))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Contains(Square s)
        {
            for (int i = 0; i < Sequence.Length; i++)
            {
                if (Sequence[i].Notation == s.Notation)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
