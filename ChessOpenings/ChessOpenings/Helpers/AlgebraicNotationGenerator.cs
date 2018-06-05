using ChessOpenings.Models;
using ChessOpenings.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Helpers
{
    public class AlgebraicNotationGenerator
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

        public AlgebraicNotationGenerator(Board board, Move move)
        {
            SubjectPiece = move.SubjectPiece.ToString();
            SourceSquare = move.FromSquare.Notation;
            DestinationSquare = move.ToSquare.Notation;
            Capture = false;
            //Check = false;
            Promotion = "";
            KingSideCastle = false;
            QueenSideCastle = false;
            Board = board;
            Move = move;
            Piece = move.SubjectPiece;
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
                List<Square> ambiguousSquares = new List<Square>();
                foreach (BoardVector d in diagonals)
                {
                    Square s = d.GetFirstOccupiedSquare();
                    if (s != null && !(s.Equals(Move.FromSquare)))
                    {
                        if (s.Piece is Bishop)
                        {
                            ambiguousSquares.Add(s);
                        }
                    }
                }
                if (ambiguousSquares.Count() > 0)
                {
                    if (!FileAmbiguityExists(Move.FromSquare, ambiguousSquares))
                    {
                        return Move.FromSquare.File.ToString();
                    }
                    if (!RankAmbiguityExists(Move.FromSquare, ambiguousSquares))
                    {
                        return Move.FromSquare.Rank.ToString();
                    }
                    return Move.FromSquare.File.ToString() + Move.FromSquare.Rank.ToString();
                }
            }
            else if (Piece is Rook)
            {
                BoardVector[] linears = Board.GetLinearVectorsFromSquare(Move.ToSquare);
                List<Square> ambiguousSquares = new List<Square>();
                foreach (BoardVector l in linears)
                {
                    Square s = l.GetFirstOccupiedSquare();
                    if (s != null && !(s.Equals(Move.FromSquare)))
                    {
                        if (s.Piece is Rook)
                        {
                            ambiguousSquares.Add(s);
                        }
                    }
                }
                if (ambiguousSquares.Count() > 0)
                {
                    if (!FileAmbiguityExists(Move.FromSquare, ambiguousSquares))
                    {
                        return Move.FromSquare.File.ToString();
                    }
                    if (!RankAmbiguityExists(Move.FromSquare, ambiguousSquares))
                    {
                        return Move.FromSquare.Rank.ToString();
                    }
                    return Move.FromSquare.File.ToString() + Move.FromSquare.Rank.ToString();
                }
            }
            else if (Piece is Queen)
            {
                BoardVector[] radials = Board.GetAllRadialVectors(Move.ToSquare);
                List<Square> ambiguousSquares = new List<Square>();
                foreach (BoardVector r in radials)
                {
                    Square s = r.GetFirstOccupiedSquare();
                    if (s != null && !(s.Equals(Move.FromSquare)))
                    {
                        if (s.Piece is Queen)
                        {
                            ambiguousSquares.Add(s);
                        }
                    }
                }
                if (ambiguousSquares.Count() > 0)
                {
                    if (!FileAmbiguityExists(Move.FromSquare, ambiguousSquares))
                    {
                        return Move.FromSquare.File.ToString();
                    }
                    if (!RankAmbiguityExists(Move.FromSquare, ambiguousSquares))
                    {
                        return Move.FromSquare.Rank.ToString();
                    }
                    return Move.FromSquare.File.ToString() + Move.FromSquare.Rank.ToString();
                }
            }
            else if (Piece is Knight)
            {
                BoardVector knightVector = Board.GetKnightConnections(Move.ToSquare);
                List<Square> knightSquares = knightVector.GetSquaresContainingPiece(Piece, false);
                if (knightSquares.Count >= 2)
                {
                    if (!FileAmbiguityExists(Move.FromSquare, knightSquares))
                    {
                        return Move.FromSquare.File.ToString();
                    }
                    if (!RankAmbiguityExists(Move.FromSquare, knightSquares))
                    {
                        return Move.FromSquare.Rank.ToString();
                    }
                    return Move.FromSquare.File.ToString() + Move.FromSquare.Rank.ToString();
                }
            }
            return "";
        }

        private bool FileAmbiguityExists(Square subjectSquare, List<Square> ambiguousSquares)
        {
            foreach(Square s in ambiguousSquares)
            {
                if (!(s.Equals(subjectSquare)))
                {
                    if (s.File == subjectSquare.File)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool RankAmbiguityExists(Square subjectSquare, List<Square> ambiguousSquares)
        {
            foreach(Square s in ambiguousSquares)
            {
                if (!(s.Equals(subjectSquare)))
                {
                    if (s.Rank == subjectSquare.Rank)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool MoveCausesCheck()
        {
            if (Piece.colour == Enums.Colour.White)
            {
                return Board.BlackKingIsInCheck(Move);
            }
            else
            {
                return Board.WhiteKingIsInCheck(Move);
            }
        }
    }
}
