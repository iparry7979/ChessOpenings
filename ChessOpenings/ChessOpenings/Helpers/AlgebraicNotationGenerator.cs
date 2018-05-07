using ChessOpenings.Models;
using ChessOpenings.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Helpers
{
    class AlgebraicNotationGenerator
    {
        private string SubjectPiece;
        private string DisambiguationString;
        private string DestinationSquare;
        private string SourceSquare;
        private bool Capture;
        private bool Check;
        private string Promotion;
        private bool KingSideCastle;
        private bool QueenSideCastle;
        private string Disambiguation;

        private Board Board;
        private Move Move;
        private Piece Piece;

        public string Notation;

        public AlgebraicNotationGenerator(Board board, Move move, Piece subjectPiece)
        {
            SubjectPiece = subjectPiece.ToString();
            SourceSquare = move.FromSquare.Notation;
            DestinationSquare = move.ToSquare.Notation;
            Capture = false;
            Check = false;
            Promotion = "";
            KingSideCastle = false;
            QueenSideCastle = false;
            Board = board;
            Move = move;
            Piece = subjectPiece;
        }

        private string BuildString()
        {
            if (KingSideCastle) return "O-O";
            if (QueenSideCastle) return "O-O-O";
            string rtn = "";
            if (SubjectPiece != "P")
            {
                rtn += SubjectPiece;
            }
            rtn += SourceNotation;
            if (Capture)
            {
                if (SubjectPiece == "P")
                {
                    rtn += SourceSquare[0].ToString();
                }
                rtn += "x";
            }
            rtn += DestinationSquare;
            if (Promotion.Length > 0)
            {
                rtn += "=" + Promotion;
            }
            if (Check)
            {
                rtn += "+";
            }
            return rtn;
        }

        public string Generate()
        {
            KingSideCastle = IsKingSideCastle();
            QueenSideCastle = IsQueenSideCastle();
            Capture = IsCapture();
            Promotion = PromotionString();
            return BuildString();
        }

        private bool IsCapture()
        {
            //TODO: Allow for En Passant
            if (Move.ToSquare.ContainsPiece())
            {
                return true;
            }
            //Check for en passant
            Square enPassantSquare = Board.enPassantAllowedSquare;
            if (enPassantSquare != null)
            {
                if (DestinationSquare == enPassantSquare.Notation && Move.SubjectPiece is Pawn)
                {
                    EnPassant = true;
                    return true;
                }
            }
            return false;
        }

        private string PromotionString()
        {
            if (SubjectPiece == "P")
            {
                if (Move.ToSquare.Rank == 1 || Move.ToSquare.Rank == 8)
                {
                    return Move.PromotionPiece.ToString();
                }
            }
            return "";
        }

        private bool IsKingSideCastle()
        {
            if (Piece is King)
            {
                if (Piece.colour == Enums.Colour.White)
                {
                    if (Move.FromSquare.Notation == "e1" && Move.ToSquare.Notation == "g1")
                    {
                        return true;
                    }
                }
                else
                {
                    if (Move.FromSquare.Notation == "e8" && Move.ToSquare.Notation == "g8")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsQueenSideCastle()
        {
            if (Piece is King)
            {
                if (Piece.colour == Enums.Colour.White)
                {
                    if (Move.FromSquare.Notation == "e1" && Move.ToSquare.Notation == "c1")
                    {
                        return true;
                    }
                }
                else
                {
                    if (Move.FromSquare.Notation == "e8" && Move.ToSquare.Notation == "c8")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
