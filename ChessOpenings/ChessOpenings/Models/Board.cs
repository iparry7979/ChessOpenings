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

        public Board()
        {
            squaresArray = InitialiseBoard();
        }

        public Square[,] InitialiseBoard()
        {
            Square[,] board = new Square[8, 8];
            for (int file = 0; file < 8; file++)
            {
                for (int rank = 0; rank < 8; rank++)
                {
                    Piece p = null;
                    if (file == 0)
                    {
                        Colour c = Colour.Black;
                        switch (rank)
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
                    else if (file == 1)
                    {
                        Colour c = Colour.Black;
                        p = new Pawn(c);
                    }
                    else if (file == 6)
                    {
                        Colour c = Colour.White;
                        p = new Pawn(c);
                    }
                    else if (file == 7)
                    {
                        Colour c = Colour.White;
                        switch (rank)
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

                    board[file, rank] = currentSquare;
                }
            }

            return board;
        }
    }
}
