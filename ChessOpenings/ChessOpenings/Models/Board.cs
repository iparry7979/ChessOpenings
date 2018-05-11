using ChessOpenings.Enums;
using ChessOpenings.Helpers;
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

        public Square enPassantAllowedSquare
        {
            get
            {
                if (GetLastMove() == null)
                {
                    return null;
                }
                string squareNotation = GetLastMove().SquareEnabledForEnPassant();
                return GetSquareByNotation(squareNotation);
            }
        }

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

                    Colour sc = (file + rank) % 2 == 0 ? Colour.Black : Colour.White;

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
            AlgebraicNotationGenerator notationGenerator = new AlgebraicNotationGenerator(this, move, move.FromSquare.Piece);
            move.AlgebraicNotation = notationGenerator.Generate();
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

        public Move GetLastMove()
        {
            if (gameHistory.Count() > 0)
            {
                return gameHistory.Peek();
            }
            return null;
        }

        public Square GetSquare(char file, byte rank)
        {
            return GetSquareByNotation(file.ToString() + rank.ToString());
        }

        public Square GetSquareByNotation(string notation)
        {
            //validate
            if (notation == null)
            {
                return null;
            }
            if (notation.Length != 2)
            {
                return null;
            }
            if (!(Char.IsLetter(notation[0]) && Char.IsNumber(notation[1])))
            {
                return null;
            }
            int file;
            if (Char.IsLower(notation[0]))
            {
                file = notation[0] - 97;
            }
            else if (Char.IsUpper(notation[0]))
            {
                file = notation[0] - 65;
            }
            file = notation[0] - 97;
            int rank;
            if (!int.TryParse(notation[1].ToString(), out rank))
            {
                return null;
            }
            rank--; //convert for 0 starting array
            if (rank >= 0 && rank <= 7 && file >= 0 && file <= 7)
            {
                return squaresArray[rank, file];
            }
            return null;
        }

        public BoardVector[] GetDiagonalVectorsFromSquare(string squareNotation)
        {
            if (squareNotation != null)
            {
                return GetDiagonalVectorsFromSquare(GetSquareByNotation(squareNotation));
            }
            return null;
        }
        public BoardVector[] GetDiagonalVectorsFromSquare(Square subjectSquare)
        {
            if (subjectSquare == null)
            {
                return null;
            }
            List<BoardVector> vectorList = new List<BoardVector>();
            char file = subjectSquare.File;
            byte rank = subjectSquare.Rank;

            BoardVector bottomLeft = new BoardVector();
            while (file >= 'a' && rank >= 1)
            {
                if (rank != subjectSquare.Rank && file != subjectSquare.File)
                {
                    bottomLeft.AddSquare(GetSquare(file, rank));
                }
                file--;
                rank--;
            }
            vectorList.Add(bottomLeft);

            file = subjectSquare.File;
            rank = subjectSquare.Rank;
            BoardVector bottomRight = new BoardVector();
            while (file <= 'h' && rank >= 1)
            {
                if (rank != subjectSquare.Rank && file != subjectSquare.File)
                {
                    bottomRight.AddSquare(GetSquare(file, rank));
                }
                file++;
                rank--;
            }
            vectorList.Add(bottomRight);

            file = subjectSquare.File;
            rank = subjectSquare.Rank;
            BoardVector topRight = new BoardVector();
            while (file <= 'h' && rank <= 8)
            {
                if (rank != subjectSquare.Rank && file != subjectSquare.File)
                {
                    topRight.AddSquare(GetSquare(file, rank));
                }
                file++;
                rank++;
                
            }
            vectorList.Add(topRight);

            file = subjectSquare.File;
            rank = subjectSquare.Rank;
            BoardVector topLeft = new BoardVector();
            while (file >= 'a' && rank <= 8)
            {
                if (rank != subjectSquare.Rank && file != subjectSquare.File)
                {
                    topLeft.AddSquare(GetSquare(file, rank));
                }
                file--;
                rank++;
            }
            vectorList.Add(topLeft);

            return vectorList.ToArray();
        }

        public BoardVector[] GetLinearVectorsFromSquare(string squareNotation)
        {
            if (squareNotation != null)
            {
                return GetLinearVectorsFromSquare(GetSquareByNotation(squareNotation));
            }
            return null;
        }

        public BoardVector[] GetLinearVectorsFromSquare(Square subjectSquare)
        {
            if (subjectSquare == null)
            {
                return null;
            }
            List<BoardVector> vectorList = new List<BoardVector>();
            char file = subjectSquare.File;
            byte rank = subjectSquare.Rank;

            BoardVector bottom = new BoardVector();
            while (rank >= 1)
            {
                if (rank != subjectSquare.Rank)
                {
                    bottom.AddSquare(GetSquare(file, rank));
                }
                rank--;
            }
            vectorList.Add(bottom);

            file = subjectSquare.File;
            rank = subjectSquare.Rank;
            BoardVector right = new BoardVector();
            while (file <= 'h')
            {
                if (file != subjectSquare.File)
                {
                    right.AddSquare(GetSquare(file, rank));
                }
                file++;
            }
            vectorList.Add(right);

            file = subjectSquare.File;
            rank = subjectSquare.Rank;
            BoardVector top = new BoardVector();
            while (rank <= 8)
            {
                if (rank != subjectSquare.Rank)
                {
                    top.AddSquare(GetSquare(file, rank));
                }
                rank++;

            }
            vectorList.Add(top);

            file = subjectSquare.File;
            rank = subjectSquare.Rank;
            BoardVector left = new BoardVector();
            while (file >= 'a')
            {
                if (file != subjectSquare.File)
                {
                    left.AddSquare(GetSquare(file, rank));
                }
                file--;
            }
            vectorList.Add(left);

            return vectorList.ToArray();
        }

        public BoardVector[] GetAllRadialVectors(string squareNotation)
        {
            if (squareNotation != null)
            {
                return GetAllRadialVectors(GetSquareByNotation(squareNotation));
            }
            return null;
        }

        public BoardVector[] GetAllRadialVectors(Square subjectSquare)
        {
            BoardVector[] diagonals = GetDiagonalVectorsFromSquare(subjectSquare);
            BoardVector[] linears = GetLinearVectorsFromSquare(subjectSquare);
            List<BoardVector> allVectors = diagonals.ToList<BoardVector>();
            allVectors.AddRange(linears.ToList<BoardVector>());
            return allVectors.ToArray();
        }

        public BoardVector GetKnightConnections(string squareNotation)
        {
            if (squareNotation != null)
            {
                return GetKnightConnections(GetSquareByNotation(squareNotation));
            }
            return null;
        }

        public BoardVector GetKnightConnections(Square subjectSquare)
        {
            BoardVector rtn = new BoardVector();
            if (subjectSquare == null)
            {
                return null;
            }
            char file = subjectSquare.File;
            byte rank = subjectSquare.Rank;

            List<Square> squares = new List<Square>();
            squares.Add(GetSquare((char)(file + 2), (byte)(rank - 1)));
            squares.Add(GetSquare((char)(file + 2), (byte)(rank + 1)));
            squares.Add(GetSquare((char)(file - 2), (byte)(rank - 1)));
            squares.Add(GetSquare((char)(file - 2), (byte)(rank + 1)));
            squares.Add(GetSquare((char)(file + 1), (byte)(rank - 2)));
            squares.Add(GetSquare((char)(file + 1), (byte)(rank + 2)));
            squares.Add(GetSquare((char)(file - 1), (byte)(rank - 2)));
            squares.Add(GetSquare((char)(file - 1), (byte)(rank + 2)));

            foreach(Square s in squares)
            {
                if (s != null)
                {
                    rtn.AddSquare(s);
                }
            }
            return rtn;
        }
    }
}
