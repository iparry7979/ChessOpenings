using ChessOpenings.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Models
{
    public partial class Board
    {
        public bool ValidateMove(Move move)
        {
            if (move == null)
            {
                return false;
            }
            if (move.FromSquare == move.ToSquare)
            {
                return false;
            }
            if (move.SubjectPiece == null)
            {
                return false;
            }
            if (move.SubjectPiece.colour != Turn)
            {
                return false;
            }

            if (move.ToSquare.ContainsPiece())
            {
                if (move.ToSquare.Piece.colour == Turn)
                {
                    return false;
                }
            }

            //Check

            if (MoveResultsInCheck(move))
            {
                return false;
            }

            //Castling
            
            if (!ValidateCastling(move))
            {
                return false;
            }

            //Rook/Bishop/Queen correct unobstructed vector

            if (move.SubjectPiece is Pawn)
            {
                if (!ValidatePawnAction(move))
                {
                    return false;
                }
            }

            else if (move.SubjectPiece is Bishop || move.SubjectPiece is Rook || move.SubjectPiece is Queen)
            {
                if (!ValidateCorrectBishopQueenRookVector(move))
                {
                    return false;
                }
            }
            
            else if (move.SubjectPiece is Knight || move.SubjectPiece is King)
            {
                if (!(move.IsKingSideCastle() || move.IsQueenSideCastle()))
                {
                    if (!ValidateCorrectKnightKingVector(move))
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        private bool ValidateCastling(Move move)
        {
            if (move.IsKingSideCastle())
            {
                if (Turn == Enums.Colour.White)
                {
                    if (!WhiteCanCastleKingSide)
                    {
                        return false;
                    }
                    if (GetSquareByNotation("f1").ContainsPiece() || GetSquareByNotation("g1").ContainsPiece())
                    {
                        return false;
                    }
                    if (SquareIsUnderAttack(GetSquareByNotation("f1"), Enums.Colour.Black)) //Can't castle across check
                    {
                        return false;
                    }
                }
                if (Turn == Enums.Colour.Black)
                {
                    if (!BlackCanCastleKingSide)
                    {
                        return false;
                    }
                    if (GetSquareByNotation("f8").ContainsPiece() || GetSquareByNotation("g8").ContainsPiece())
                    {
                        return false;
                    }
                    if (SquareIsUnderAttack(GetSquareByNotation("f8"), Enums.Colour.White)) //Can't castle across check
                    {
                        return false;
                    }
                }
            }
            if (move.IsQueenSideCastle())
            {
                if (Turn == Enums.Colour.White)
                {
                    if (!WhiteCanCastleQueenSide)
                    {
                        return false;
                    }
                    if (GetSquareByNotation("d1").ContainsPiece() || GetSquareByNotation("c1").ContainsPiece() || GetSquareByNotation("b1").ContainsPiece())
                    {
                        return false;
                    }
                    if (SquareIsUnderAttack(GetSquareByNotation("d1"), Enums.Colour.Black)) //Can't castle across check
                    {
                        return false;
                    }
                }

                if (Turn == Enums.Colour.Black)
                {
                    if (!BlackCanCastleQueenSide)
                    {
                        return false;
                    }
                    if (GetSquareByNotation("d8").ContainsPiece() || GetSquareByNotation("c8").ContainsPiece() || GetSquareByNotation("b8").ContainsPiece())
                    {
                        return false;
                    }
                    if (SquareIsUnderAttack(GetSquareByNotation("d8"), Enums.Colour.White)) //Can't castle across check
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool MoveResultsInCheck(Move move)
        {
            if (Turn == Enums.Colour.White)
            {
                if (WhiteKingIsInCheck(move))
                {
                    return true;
                }
            }

            if (Turn == Enums.Colour.Black)
            {
                if (BlackKingIsInCheck(move))
                {
                    return true;
                }
            }

            return false;
        }

        private bool ValidateCorrectBishopQueenRookVector(Move move)
        {
            BoardVector[] vectors = null;

            if (move.SubjectPiece is Bishop)
            {
                vectors = GetDiagonalVectorsFromSquare(move.FromSquare);
            }
            else if (move.SubjectPiece is Rook)
            {
                vectors = GetLinearVectorsFromSquare(move.FromSquare);
            }
            else if (move.SubjectPiece is Queen)
            {
                vectors = GetAllRadialVectors(move.FromSquare);
            }

            BoardVector candidateVector = null;
            foreach (BoardVector vector in vectors)
            {
                if (vector.Contains(move.ToSquare))
                {
                    candidateVector = vector;
                }
            }

            if (candidateVector == null)
            {
                return false;
            }

            return !candidateVector.PathIsObstructed(move.ToSquare);
        }

        private bool ValidateCorrectKnightKingVector(Move move)
        {
            BoardVector vector = null;
            if (move.SubjectPiece is Knight)
            {
                vector = GetKnightConnections(move.FromSquare);
            }
            else if (move.SubjectPiece is King)
            {
                vector = GetKingVector(move.FromSquare);
            }
            if (vector.Contains(move.ToSquare))
            {
                return true;
            }
            return false;
        }

        private bool ValidatePawnAction(Move move)
        {
            BoardVector advanceVector = GetPawnAdvanceVector(move.FromSquare, move.SubjectPiece.colour);
            BoardVector attackVector = GetPawnAttackVectors(move.FromSquare, move.SubjectPiece.colour);

            if (advanceVector.Contains(move.ToSquare)) //pawn advance
            {
                if (move.ToSquare.ContainsPiece())
                {
                    return false;
                }
                if (advanceVector.PathIsObstructed(move.ToSquare))
                {
                    return false;
                }
            }
            else if (attackVector.Contains(move.ToSquare)) //pawn capture
            {
                if (!(move.ToSquare == enPassantAllowedSquare))
                {
                    if (!move.ToSquare.ContainsPiece()) //cannot move diagonal to empty square (unless enapassant)
                    {
                        return false;
                    }
                    else if (move.ToSquare.Piece.colour == move.SubjectPiece.colour) //cannot take own piece
                    {
                        return false;
                    }
                }
                
            }
            else
            {
                return false;
            }
            if (move.ToSquare.Rank == 8 || move.ToSquare.Rank == 1)
            {
                if (move.PromotionPiece == null)
                {
                    return false;
                }
                if (move.PromotionPiece is Pawn)
                {
                    return false;
                }
                if (move.PromotionPiece.colour != move.SubjectPiece.colour)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
