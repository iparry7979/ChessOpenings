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
        public bool WhiteCanCastleKingSide
        {
            get
            {
                foreach (Move m in gameHistory)
                {
                    if (m.FromSquare.Notation == "e1" || m.FromSquare.Notation == "h1")
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public bool WhiteCanCastleQueenSide
        {
            get
            {
                foreach (Move m in gameHistory)
                {
                    if (m.FromSquare.Notation == "e1" || m.FromSquare.Notation == "a1")
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public bool BlackCanCastleKingSide
        {
            get
            {
                foreach(Move m in gameHistory)
                {
                    if (m.FromSquare.Notation == "e8" || m.FromSquare.Notation == "h8")
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public bool BlackCanCastleQueenSide
        {
            get
            {
                foreach (Move m in gameHistory)
                {
                    if (m.FromSquare.Notation == "e8" || m.FromSquare.Notation == "h1")
                    {
                        return false;
                    }
                }
                return true;
            }
        }

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

        public Board(Dictionary<string, Piece> position)
        {
            Turn = Enums.Colour.White;
            gameHistory = new Stack<Move>();
            Square[,] board = new Square[8, 8];
            for (int rank = 0; rank < 8; rank++)
            {
                for (int file = 0; file < 8; file++)
                {
                    Colour sc = (file + rank) % 2 == 0 ? Colour.Black : Colour.White;

                    Square currentSquare = new Square(null, sc);

                    currentSquare.File = (char)(file + 97); //convert int to relevant character
                    currentSquare.Rank = (byte)(rank + 1);

                    Piece p;

                    position.TryGetValue(currentSquare.Notation, out p);

                    currentSquare.Piece = p;

                    board[rank, file] = currentSquare;
                }
            }
            squaresArray = board;
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

        public bool MakeMove(Move move, bool generateNotaion = true)
        {
            if (generateNotaion)
            {
                AlgebraicNotationGenerator notationGenerator = new AlgebraicNotationGenerator(this, move);
                move.AlgebraicNotation = notationGenerator.Generate();
            }
            bool valid = true;
            valid = ValidateMove(move);
            if (valid)
            {
                //Update the board with move
                if (move.ToSquare.ContainsPiece())
                {
                    move.PieceTaken = move.ToSquare.Piece;
                }
                //Castling
                if (move.IsKingSideCastle())
                {
                    if (Turn == Enums.Colour.White)
                    {
                        Square rookSourceSquare = GetSquareByNotation("h1");
                        Square rookDestinationSquare = GetSquareByNotation("f1");
                        rookDestinationSquare.Piece = rookSourceSquare.Piece;
                        rookSourceSquare.Piece = null;
                    }
                    else
                    {
                        Square rookSourceSquare = GetSquareByNotation("h8");
                        Square rookDestinationSquare = GetSquareByNotation("f8");
                        rookDestinationSquare.Piece = rookSourceSquare.Piece;
                        rookSourceSquare.Piece = null;
                    }
                }
                if (move.IsQueenSideCastle())
                {
                    if (Turn == Enums.Colour.White)
                    {
                        Square rookSourceSquare = GetSquareByNotation("a1");
                        Square rookDestinationSquare = GetSquareByNotation("d1");
                        rookDestinationSquare.Piece = rookSourceSquare.Piece;
                        rookSourceSquare.Piece = null;
                    }
                    else
                    {
                        Square rookSourceSquare = GetSquareByNotation("a8");
                        Square rookDestinationSquare = GetSquareByNotation("d8");
                        rookDestinationSquare.Piece = rookSourceSquare.Piece;
                        rookSourceSquare.Piece = null;
                    }
                }
                //En passant
                if (enPassantAllowedSquare != null && enPassantAllowedSquare.Equals(move.ToSquare) && move.SubjectPiece is Pawn)
                {
                    Move lastMove = GetLastMove();
                    move.PieceTaken = lastMove.ToSquare.Piece;
                    lastMove.ToSquare.Piece = null;
                }
                move.ToSquare.Piece = move.FromSquare.Piece;
                move.FromSquare.Piece = null;
                if (move.IsPromotion())
                {
                    move.ToSquare.Piece = move.PromotionPiece;
                }
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
                lastMove.FromSquare.Piece = lastMove.SubjectPiece;
                lastMove.ToSquare.Piece = lastMove.PieceTaken;

                if (lastMove.IsKingSideCastle())
                {
                    if (lastMove.SubjectPiece.colour == Enums.Colour.White)
                    {
                        Square rookSourceSquare = GetSquareByNotation("f1");
                        Square rookDestinationSquare = GetSquareByNotation("h1");
                        rookDestinationSquare.Piece = rookSourceSquare.Piece;
                        rookSourceSquare.Piece = null;
                    }
                    else
                    {
                        Square rookSourceSquare = GetSquareByNotation("f8");
                        Square rookDestinationSquare = GetSquareByNotation("h8");
                        rookDestinationSquare.Piece = rookSourceSquare.Piece;
                        rookSourceSquare.Piece = null;
                    }
                }
                if (lastMove.IsQueenSideCastle())
                {
                    if (lastMove.SubjectPiece.colour == Enums.Colour.White)
                    {
                        Square rookSourceSquare = GetSquareByNotation("d1");
                        Square rookDestinationSquare = GetSquareByNotation("a1");
                        rookDestinationSquare.Piece = rookSourceSquare.Piece;
                        rookSourceSquare.Piece = null;
                    }
                    else
                    {
                        Square rookSourceSquare = GetSquareByNotation("d8");
                        Square rookDestinationSquare = GetSquareByNotation("a8");
                        rookDestinationSquare.Piece = rookSourceSquare.Piece;
                        rookSourceSquare.Piece = null;
                    }
                }
                ChangeTurn();
                gameHistory.Pop();
                if (enPassantAllowedSquare != null && enPassantAllowedSquare.Equals(lastMove.ToSquare) && lastMove.SubjectPiece is Pawn)
                {
                    lastMove.ToSquare.Piece = null;
                    GetLastMove().ToSquare.Piece = GetLastMove().SubjectPiece;
                }
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

        public List<string> GetAllMovesByNotation()
        {
            List<String> moves = new List<string>();
            foreach (Move m in gameHistory)
            {
                moves.Add(m.AlgebraicNotation);
            }
            moves.Reverse();
            return moves;
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

        public List<Square> GetSquaresContainingPiece(string pieceNotation, Enums.Colour colour)
        {
            Piece p = null;
            switch(pieceNotation)
            {
                case "P":
                    p = new Pawn(colour);
                    break;
                case "N":
                    p = new Knight(colour);
                    break;
                case "B":
                    p = new Bishop(colour);
                    break;
                case "R":
                    p = new Rook(colour);
                    break;
                case "Q":
                    p = new Queen(colour);
                    break;
                case "K":
                    p = new King(colour);
                    break;
                default:
                    p = null;
                    break;
            }
            return GetSquaresContainingPiece(p);
        }

        public List<Square> GetSquaresContainingPiece(Piece p)
        {
            if (p == null)
            {
                return null;
            }

            List<Square> rtn = new List<Square>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (squaresArray[i, j].ContainsPiece() && squaresArray[i, j].Piece.Equals(p))
                    {
                        rtn.Add(squaresArray[i, j]);
                    }
                }
            }
            return rtn;
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

        public BoardVector GetKingVector(string squareNotation)
        {
            if (squareNotation != null)
            {
                return GetKingVector(GetSquareByNotation(squareNotation));
            }
            return null;
        }

        public BoardVector GetKingVector(Square subjectSquare)
        {
            BoardVector rtn = new BoardVector();
            if (subjectSquare == null)
            {
                return null;
            }
            char file = subjectSquare.File;
            byte rank = subjectSquare.Rank;

            List<Square> squares = new List<Square>();
            squares.Add(GetSquare((char)(file + 1), (byte)(rank + 1)));
            squares.Add(GetSquare((char)(file + 1), rank));
            squares.Add(GetSquare((char)(file + 1), (byte)(rank - 1)));
            squares.Add(GetSquare((char)(file - 1), (byte)(rank + 1)));
            squares.Add(GetSquare((char)(file - 1), rank));
            squares.Add(GetSquare((char)(file - 1), (byte)(rank - 1)));
            squares.Add(GetSquare(file, (byte)(rank + 1)));
            squares.Add(GetSquare(file, (byte)(rank - 1)));

            foreach (Square s in squares)
            {
                if (s != null)
                {
                    rtn.AddSquare(s);
                }
            }
            return rtn;
        }

        public BoardVector GetPawnAttackVectors(string squareNotation, Enums.Colour oppositionPieceColour)
        {
            if (squareNotation != null)
            {
                return GetPawnAttackVectors(GetSquareByNotation(squareNotation), oppositionPieceColour);
            }
            return null;
        }

        public BoardVector GetPawnAttackVectors(Square subjectSquare, Enums.Colour oppositionPieceColour)
        {
            BoardVector rtn = new BoardVector();
            if (subjectSquare == null)
            {
                return null;
            }
            char file = subjectSquare.File;
            byte rank = subjectSquare.Rank;

            List<Square> squares = new List<Square>();
            if (oppositionPieceColour == Enums.Colour.White)
            {
                squares.Add(GetSquare((char)(file - 1), (byte)(rank - 1)));
                squares.Add(GetSquare((char)(file + 1), (byte)(rank - 1)));
            }
            else
            {
                squares.Add(GetSquare((char)(file - 1), (byte)(rank + 1)));
                squares.Add(GetSquare((char)(file + 1), (byte)(rank + 1)));
            }

            foreach(Square s in squares)
            {
                if (s != null)
                {
                    rtn.AddSquare(s);
                }
            }
            return rtn;
        }

        public bool WhiteKingIsInCheck()
        {
            List<Square> sList = GetSquaresContainingPiece("K", Enums.Colour.White);
            if (sList.Count() > 0)
            {
                Square s = sList[0];
                return SquareIsUnderAttack(s, Enums.Colour.Black);
            }
            return false;
        }

        public bool WhiteKingIsInCheck(Move preceedingMove)
        {
            MakeMove(preceedingMove, false);
            bool rtn = WhiteKingIsInCheck();
            GoBackOneMove();
            return rtn;
        }

        public bool BlackKingIsInCheck()
        {
            List<Square> sList = GetSquaresContainingPiece("K", Enums.Colour.Black);
            if (sList.Count() > 0)
            {
                Square s = sList[0];
                return SquareIsUnderAttack(s, Enums.Colour.White);
            }
            return false;
        }

        public bool BlackKingIsInCheck(Move preceedingMove)
        {
            MakeMove(preceedingMove, false);
            bool rtn = BlackKingIsInCheck();
            GoBackOneMove();
            return rtn;
        }

        public bool IsEitherKingInCheck()
        {
            return WhiteKingIsInCheck() || BlackKingIsInCheck();
        }

        public bool IsEitherKingInCheck(Move preceedingMove)
        {
            MakeMove(preceedingMove, false);
            bool rtn = IsEitherKingInCheck();
            GoBackOneMove();
            return rtn;
        }

        public bool SquareIsUnderAttack(Square s, Enums.Colour oppositionPieceColour)
        {
            BoardVector[] diagonals = GetDiagonalVectorsFromSquare(s);
            BoardVector[] linears = GetLinearVectorsFromSquare(s);
            BoardVector knightVector = GetKnightConnections(s);
            BoardVector kingVector = GetKingVector(s);
            BoardVector pawnVector = GetPawnAttackVectors(s, oppositionPieceColour);

            foreach (BoardVector d in diagonals)
            {
                if (d.GetFirstPiece() is Bishop && d.GetFirstPiece().colour == oppositionPieceColour)
                {
                    return true;
                }
                if (d.GetFirstPiece() is Queen && d.GetFirstPiece().colour == oppositionPieceColour)
                {
                    return true;
                }
            }

            foreach (BoardVector l in linears)
            {
                if (l.GetFirstPiece() is Rook && l.GetFirstPiece().colour == oppositionPieceColour)
                {
                    return true;
                }
                if (l.GetFirstPiece() is Queen && l.GetFirstPiece().colour == oppositionPieceColour)
                {
                    return true;
                }
            }

            if (knightVector.GetSquaresContainingPiece(new Knight(oppositionPieceColour), false).Count > 0)
            {
                return true;
            }

            if (kingVector.GetSquaresContainingPiece(new King(oppositionPieceColour), false).Count > 0)
            {
                return true;
            }

            if (pawnVector.GetSquaresContainingPiece(new Pawn(oppositionPieceColour), false).Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
