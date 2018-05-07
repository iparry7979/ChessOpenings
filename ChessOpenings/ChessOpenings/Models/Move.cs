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
        public Square FromSquare {
            get
            {
                return _fromSquare;
            }
            set
            {
                _fromSquare = value;
                SubjectPiece = value.Piece;
            }
        }
        private Square _fromSquare;

        public Square ToSquare { get; set; }
        public Piece SubjectPiece { get; set; }
        public Piece PieceTaken { get; set; }
        public Piece PromotionPiece { get; set; }
        public string AlgebraicNotation { get; set; }

        public string FromSquareNotation()
        {
            return GenerateSquareNotation(FromSquare);
        }

        public string ToSquareNotation()
        {
            return GenerateSquareNotation(ToSquare);
        }

        private string GenerateSquareNotation(Square s)
        {
            string rtn = "";
            rtn += s.File.ToString() + s.Rank.ToString();
            return rtn;
        }

        public string SquareEnabledForEnPassant()
        {
            if (SubjectPiece is Pawn)
            {
                if (SubjectPiece.colour == Enums.Colour.White)
                {
                    if (FromSquare.Rank == 2 && ToSquare.Rank == 4)
                    {
                        return FromSquare.File.ToString() + 3;
                    }
                }
                else
                {
                    if (FromSquare.Rank == 7 && ToSquare.Rank == 5)
                    {
                        return FromSquare.File.ToString() + 6;
                    }
                }
            }
            return null;
        }
    }
}
