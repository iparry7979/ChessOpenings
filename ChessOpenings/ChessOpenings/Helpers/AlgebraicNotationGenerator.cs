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
            //Check = false;
            Promotion = "";
            KingSideCastle = false;
            QueenSideCastle = false;
            Board = board;
            Move = move;
            Piece = subjectPiece;
            Check = MoveCausesCheck();
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
            rtn += Disambiguation;
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
            Notation = rtn;
            return rtn;
        }

        public string Generate()
        {
            KingSideCastle = IsKingSideCastle();
            QueenSideCastle = IsQueenSideCastle();
            Capture = IsCapture();
            Promotion = PromotionString();
            Disambiguation = GenerateDisambiguationString();
            return BuildString();
        }

        private bool IsCapture()
        {
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
            return Move.IsKingSideCastle();
        }

        private bool IsQueenSideCastle()
        {
            return Move.IsQueenSideCastle();
        }

        private string GenerateDisambiguationString()
        {
            if (Piece is Bishop)
            {
                BoardVector[] diagonals = Board.GetDiagonalVectorsFromSquare(Move.ToSquare);
                foreach(BoardVector d in diagonals)
                {
                    Square s = d.GetFirstOccupiedSquare();
                    if (s != null && !(s.Equals(Move.FromSquare)))
                    {
                        if (s.Piece is Bishop)
                        {
                            if (s.File != Move.FromSquare.File)
                            {
                                return Move.FromSquare.File.ToString();
                            }
                            else
                            {
                                if (s.Rank != Move.FromSquare.Rank)
                                {
                                    return Move.FromSquare.Rank.ToString();
                                }
                            }
                        }
                    }
                }
            }
            else if (Piece is Rook)
            {
                BoardVector[] linears = Board.GetLinearVectorsFromSquare(Move.ToSquare);
                foreach (BoardVector l in linears)
                {
                    Square s = l.GetFirstOccupiedSquare();
                    if (s != null && !(s.Equals(Move.FromSquare)))
                    {
                        if (s.Piece is Rook)
                        {
                            if (s.File != Move.FromSquare.File)
                            {
                                return Move.FromSquare.File.ToString();
                            }
                            else
                            {
                                if (s.Rank != Move.FromSquare.Rank)
                                {
                                    return Move.FromSquare.Rank.ToString();
                                }
                            }
                        }
                    }
                }
            }
            else if (Piece is Queen)
            {
                BoardVector[] radials = Board.GetAllRadialVectors(Move.ToSquare);
                foreach (BoardVector r in radials)
                {
                    Square s = r.GetFirstOccupiedSquare();
                    if (s != null && !(s.Equals(Move.FromSquare)))
                    {
                        if (s.Piece is Queen)
                        {
                            if (s.File != Move.FromSquare.File)
                            {
                                return Move.FromSquare.File.ToString();
                            }
                            else
                            {
                                if (s.Rank != Move.FromSquare.Rank)
                                {
                                    return Move.FromSquare.Rank.ToString();
                                }
                            }
                        }
                    }
                }
            }
            else if (Piece is Knight)
            {
                BoardVector knightVector = Board.GetKnightConnections(Move.ToSquare);
                List<Square> knightSquares = knightVector.GetSquaresContainingPiece(Piece, false);
                if (knightSquares.Count == 2)
                {
                    foreach (Square s in knightSquares)
                    {
                        if (s != Move.FromSquare)
                        {
                            if (s.File != Move.FromSquare.File)
                            {
                                return Move.FromSquare.File.ToString();
                            }
                            else
                            {
                                if (s.Rank != Move.FromSquare.Rank)
                                {
                                    return Move.FromSquare.Rank.ToString();
                                }
                            }
                        }
                    }
                }
            }
            return "";
        }

        public bool MoveCausesCheck()
        {
            return false;
        }
    }
}
