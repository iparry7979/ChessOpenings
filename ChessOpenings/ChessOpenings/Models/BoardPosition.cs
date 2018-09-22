using ChessOpenings.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Models
{
    public class BoardPosition
    {
        public Dictionary<string, Piece> Position { get; set; }

        public BoardPosition(Dictionary<string, Piece> position)
        {
            Position = position;
        }

        public int GenerateComparisonValue()
        {
            

            string square;
            Piece piece;
            int comparisonValue = 0;
            foreach (KeyValuePair<string, Piece> kvp in Position)
            {
                square = kvp.Key;
                piece = kvp.Value;
                int pieceMultipler = GetPieceMultiplier(piece);
                int squareMultiplier = GetSquareMultiplier(square);
                comparisonValue += pieceMultipler * squareMultiplier;
            }
            return comparisonValue;
        }

        private int GetPieceMultiplier(Piece p)
        {
            if (p is Pawn)
            {
                return 1;
            }
            if (p is Bishop)
            {
                return 10;
            }
            if (p is Knight)
            {
                return 50;
            }
            if (p is Rook)
            {
                return 100;
            }
            if (p is Queen)
            {
                return 500;
            }
            if (p is King)
            {
                return 1000;
            }
            return 1;
        }

        private int GetSquareMultiplier(string square)
        {
            if (square.Length == 2)
            {
                return square[0] * square[1];
            }
            return 1;
        }

        public override bool Equals(object obj)
        {
            if (obj is BoardPosition)
            {
                BoardPosition comparitor = (BoardPosition)obj;
                if (this.GenerateComparisonValue() == comparitor.GenerateComparisonValue())
                {
                    bool rtn = true;
                    foreach (KeyValuePair<string, Piece> kvp in Position)
                    {
                        if (comparitor.Position.ContainsKey(kvp.Key))
                        {
                            Piece p;
                            if (comparitor.Position.TryGetValue(kvp.Key, out p))
                            {
                                if (!(p.Equals(kvp.Value)))
                                {
                                    rtn = false;
                                }
                            }
                            else
                            {
                                rtn = false;
                            }
                        }
                    }
                    return rtn;
                }
            }
            return false;
        }
    }
}
