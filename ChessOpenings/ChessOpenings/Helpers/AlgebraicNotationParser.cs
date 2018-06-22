using ChessOpenings.Models;
using ChessOpenings.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Helpers
{
    public class AlgebraicNotationParser
    {
        private string Notation;
        private Enums.Colour Colour;
        private Board Board;
        public AlgebraicNotationParser(string notation, Enums.Colour colour, Board board)
        {
            Notation = notation;
            Colour = colour;
            Board = board;
        }

        public Piece ExtractPiece()
        {
            if (IsCastle())
            {
                return new King(Colour);
            }
            foreach (char c in Notation)
            {
                if (c == '=')
                {
                    return new Pawn(Colour);
                }
                if (Char.IsUpper(c))
                {
                    return Piece.CreatePiece(c.ToString(), Colour);
                }
            }
            return new Pawn(Colour);
        }

        public string ExtractDestinationSquare()
        {
            if (IsCastle())
            {
                if (Notation == "O-O")
                {
                    if (Colour == Enums.Colour.White)
                    {
                        return "g1";
                    }
                    return "g8";
                }
                if (Colour == Enums.Colour.White)
                {
                    return "c1";
                }
                return "c8";
            }
            string n = Notation.Substring(0);
            while (!Char.IsDigit(n[n.Length - 1]))
            {
                n = n.Substring(0, n.Length - 1);
            }
            return n.Substring(n.Length - 2);
        }

        public Square ExtractSourceSquare()
        {
            if (Board == null) return null;
            Square destinationSquare = Board.GetSquareByNotation(ExtractDestinationSquare());
            Piece piece = ExtractPiece();
            List<Square> candidateSquares = GetCandidateSquares(piece, destinationSquare); //gets squares containing pieces that could move to destination square
            if (candidateSquares.Count == 0)
            {
                return null;
            }
            if (candidateSquares.Count == 1)
            {
                return candidateSquares[0];
            }
            //if this point is reached disambiguation is required
            string disambiguationString = ExtractDisambiguationString(destinationSquare.Notation);
            if (disambiguationString == null) return null;
            List<Square> disambiguatedSquares = new List<Square>();
            foreach(Square s in candidateSquares)
            {
                if (disambiguationString.Length == 2)
                {
                    if (s.Notation == disambiguationString)
                    {
                        return s;
                    }
                }
                else if (char.IsLower(disambiguationString[0]))
                {
                    if (s.File == disambiguationString[0])
                    {
                        disambiguatedSquares.Add(s);
                    }
                }
                else if (char.IsDigit(disambiguationString[0]))
                {
                    byte rank;
                    Byte.TryParse(disambiguationString[0].ToString(), out rank);
                    if (rank == s.Rank)
                    {
                        disambiguatedSquares.Add(s);
                    }
                }
            }
            if (disambiguatedSquares.Count() == 1)
            {
                return disambiguatedSquares[0];
            }
            return null;
        }

        public Piece ExtractPromotionPiece()
        {
            bool useNextChar = false;
            foreach (char c in Notation)
            {
                if (useNextChar)
                {
                    return Piece.CreatePiece(c.ToString(), Colour);
                }
                if (c == '=')
                {
                    useNextChar = true;
                }
            }
            return null;
        }

        public bool IsCastle()
        {
            return Notation == "O-O" || Notation == "O-O-O";
        }

        public Move GetMove()
        {
            Move rtn = new Move();
            if (IsCastle())
            {
                if (Notation == "O-O")
                {
                    if (Colour == Enums.Colour.White)
                    {
                        if (Board.WhiteCanCastleKingSide)
                        {
                            return new Move(Board.GetSquareByNotation("e1"), Board.GetSquareByNotation("g1"));
                        }
                        return null;
                    }
                    if (Board.BlackCanCastleKingSide)
                    {
                        return new Move(Board.GetSquareByNotation("e8"), Board.GetSquareByNotation("g8"));
                    }
                    return null;
                }
                if (Colour == Enums.Colour.White)
                {
                    if (Board.WhiteCanCastleQueenSide)
                    {
                        return new Move(Board.GetSquareByNotation("e1"), Board.GetSquareByNotation("c1"));
                    }
                    return null;
                }
                if (Board.BlackCanCastleQueenSide)
                {
                    return new Move(Board.GetSquareByNotation("e8"), Board.GetSquareByNotation("c8"));
                }
                return null;
            }

            string dSquareNotation = ExtractDestinationSquare();
            Square destinationSquare = Board.GetSquareByNotation(dSquareNotation);
            Square sourceSquare = ExtractSourceSquare();
            Piece subjectPiece = ExtractPiece();
            Piece promotionPiece = ExtractPromotionPiece();

            if (destinationSquare == null || sourceSquare == null || subjectPiece == null)
            {
                return null;
            }

            rtn = new Move(sourceSquare, destinationSquare);
            if (promotionPiece != null)
            {
                rtn.PromotionPiece = promotionPiece;
            }
            
            return rtn;
        }

        private List<Square> GetCandidateSquares(Piece p, Square destinationSquare)
        {
            List<Square> rtnList = new List<Square>();
            if (p is Pawn)
            {
                if (Notation.Substring(0, 2) == destinationSquare.Notation) //pawn advance
                {
                    BoardVector v = Board.GetReversePawnAdvanceVector(destinationSquare, p.colour);
                    Square candidate = v.GetFirstOccupiedSquare();
                    if (candidate != null)
                    {
                        if (candidate.Piece is Pawn && candidate.Piece.colour == Colour)
                        {
                            rtnList.Add(candidate);
                        }
                    }
                }
                else //pawn capture
                {
                    char sourceFile = Notation[0];
                    BoardVector v = Board.GetReversePawnAttackVectors(destinationSquare, p.colour);
                    foreach(Square s in v.Sequence)
                    {
                        if (s.File == sourceFile)
                        {
                            if (s.Piece is Pawn && s.Piece.colour == Colour)
                            {
                                rtnList.Add(s);
                            }
                        }
                    }
                }
            }

            else if (p is Bishop)
            {
                BoardVector[] diagonals = Board.GetDiagonalVectorsFromSquare(destinationSquare);
                foreach(BoardVector vector in diagonals)
                {
                    Square candidate = vector.GetFirstOccupiedSquare();
                    if (candidate != null)
                    {
                        if (candidate.Piece is Bishop && candidate.Piece.colour == Colour)
                        {
                            rtnList.Add(candidate);
                        }
                    }
                }
            }

            else if (p is Rook)
            {
                BoardVector[] linears = Board.GetLinearVectorsFromSquare(destinationSquare);
                foreach (BoardVector vector in linears)
                {
                    Square candidate = vector.GetFirstOccupiedSquare();
                    if (candidate != null)
                    {
                        if (candidate.Piece is Rook && candidate.Piece.colour == Colour)
                        {
                            rtnList.Add(candidate);
                        }
                    }
                }
            }

            else if (p is Queen)
            {
                BoardVector[] radials = Board.GetAllRadialVectors(destinationSquare);
                foreach (BoardVector vector in radials)
                {
                    Square candidate = vector.GetFirstOccupiedSquare();
                    if (candidate != null)
                    {
                        if (candidate.Piece is Queen && candidate.Piece.colour == Colour)
                        {
                            rtnList.Add(candidate);
                        }
                    }
                }
            }

            else if (p is Knight)
            {
                BoardVector knightVector = Board.GetKnightConnections(destinationSquare);
                rtnList.AddRange(knightVector.GetSquaresContainingPiece(p, false));
            }

            else if (p is King)
            {
                BoardVector kingConnections = Board.GetKingVector(destinationSquare);
                rtnList.AddRange(kingConnections.GetSquaresContainingPiece(p, false));
            }

            return rtnList;
        }

        public string ExtractDisambiguationString(string destinationSquare)
        {
            List<char> files = new List<char>{ 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            List<char> ranks = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8' };
            try
            {
                int destIndex = Notation.IndexOf(destinationSquare);
                if (Char.IsUpper(Notation[0]))
                {
                    for (int i = 1; i < destIndex; i++)
                    {
                        if (ranks.Contains(Notation[i]))
                        {
                            return Notation[i].ToString();
                        }
                        if (files.Contains(Notation[i]))
                        {
                            if (ranks.Contains(Notation[i+1]))
                            {
                                return Notation[i].ToString() + Notation[i + 1].ToString();
                            }
                            return Notation[i].ToString();
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException) { }
            return null;
        }
    }
}
