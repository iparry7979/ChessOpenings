using ChessOpenings.Enums;
using ChessOpenings.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.Models
{
    public class Board
    {
        public Square[,] squaresArray { get; }
        public Enums.Colour Turn { get; set; }
        private Stack<Move> gameHistory { get; set; }

        public Board()
        {
            Turn = Enums.Colour.White;
            gameHistory = new Stack<Move>();
            squaresArray = InitialiseBoard();
        }

        public Square[,] InitialiseBoard()
        {
            Square[,] board = new Square[8, 8];
            for (int rank = 0; rank < 8; rank++)
            {
                for (int file = 0; file < 8; file++)
                {
                    Piece p = null;
                    if (rank == 0)
                    {
                        Colour c = Colour.White;
                        switch (file)
                        {
                            case 0:
                            case 7:
                                p = new Rook(c);
                                break;
                            case 1:
                            case 6:
                                p = new Knight(c);
                                break;
                            case 2:
                            case 5:
                                p = new Bishop(c);
                                break;
                            case 3:
                                p = new Queen(c);
                                break;
                            case 4:
                                p = new King(c);
                                break;
                        }
                    }
                    else if (rank == 1)
                    {
                        Colour c = Colour.White;
                        p = new Pawn(c);
                    }
                    else if (rank == 6)
                    {
                        Colour c = Colour.Black;
                        p = new Pawn(c);
                    }
                    else if (rank == 7)
                    {
                        Colour c = Colour.Black;
                        switch (file)
                        {
                            case 0:
                            case 7:
                                p = new Rook(c);
                                break;
                            case 1:
                            case 6:
                                p = new Knight(c);
                                break;
                            case 2:
                            case 5:
                                p = new Bishop(c);
                                break;
                            case 3:
                                p = new Queen(c);
                                break;
                            case 4:
                                p = new King(c);
                                break;
                        }
                    }

                    Colour sc = (file + rank) % 2 == 0 ? Colour.White : Colour.Black;

                    Square currentSquare = new Square(p, sc);

                    currentSquare.File = (char)(file + 97); //convert int to relevant character
                    currentSquare.Rank = (byte)(rank + 1);

                    board[rank, file] = currentSquare;
                }
            }

            return board;
        }

        public bool MakeMove(Move move)
        {
            bool valid = true;
            valid = ValidateMove(move);
            if (valid)
            {
                //Update the board with move
                if (move.ToSquare.ContainsPiece())
                {
                    move.PieceTaken = move.ToSquare.Piece;
                }
                move.ToSquare.Piece = move.FromSquare.Piece;
                move.FromSquare.Piece = null;
                ChangeTurn();
                gameHistory.Push(move);
            }
            return valid;
        }

        public Move GoBackOneMove()
        {
            Move lastMove = null;
            if (gameHistory.Count > 0)
            {
                lastMove = gameHistory.Peek();
                lastMove.FromSquare.Piece = lastMove.ToSquare.Piece;
                lastMove.ToSquare.Piece = lastMove.PieceTaken;
                ChangeTurn();
                gameHistory.Pop();
            }
            return lastMove;
        }

        public bool ValidateMove(Move move)
        {
            if (move.FromSquare == move.ToSquare)
            {
                return false;
            }
            return true;
        }

        public void ChangeTurn()
        {
            if (Turn == Enums.Colour.White)
            {
                Turn = Enums.Colour.Black;
                return;
            }
            Turn = Enums.Colour.White;
        }
    }
}
