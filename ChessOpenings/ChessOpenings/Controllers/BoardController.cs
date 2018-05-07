using ChessOpenings.Models;
using ChessOpenings.ViewInterfaces;
using System.Xml.Linq;
using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace ChessOpenings.Controllers
{
    public class BoardController
    {
        public IBoardView View;
        public Square selectedSquare;
        public Move NextMove;
        public Board Board;
        public Enums.BoardOrientation Orientation;
        public XDocument doc;

        private const string OPENINGDATAFILE = "openings.xml";

        public BoardController(IBoardView view)
        {
            View = view;
            Board = new Board();
            NextMove = new Move();
            selectedSquare = null;
            Orientation = Enums.BoardOrientation.Standard;
            doc = XDocument.Load(View.ParseOpenings());
            Board.InitialiseBoard();
        }

        public void SquareTapped(Square tappedSquare)
        {
            if (selectedSquare == null)
            {
                if (tappedSquare.Piece != null && tappedSquare.Piece.colour == Board.Turn)
                {
                    NextMove.FromSquare = tappedSquare;
                    selectedSquare = tappedSquare;
                    View.SelectSquare(tappedSquare);
                }
            }
            else
            {
                if (tappedSquare == selectedSquare)
                {
                    selectedSquare = null;
                    View.UnselectSquare(tappedSquare);
                    return;
                }
                NextMove.ToSquare = tappedSquare;
                Board.MakeMove(NextMove);
                DrawBoard();
                View.UnselectSquare(selectedSquare);
                selectedSquare = null;
                NextMove = new Move();
            }
        }

        public void GoBackOneMove()
        {
            Board.GoBackOneMove();
            View.UnselectSquare(selectedSquare);
            selectedSquare = null;
            NextMove = new Move();
            DrawBoard();
        }

        public void ResetBoard()
        {
            selectedSquare = null;
            NextMove = new Move();
            Board = new Board();
            DrawBoard();
        }

        public void FlipBoard()
        {
            if (Orientation == Enums.BoardOrientation.Standard)
            {
                Orientation = Enums.BoardOrientation.Inverted;
            }
            else
            {
                Orientation = Enums.BoardOrientation.Standard;
            }
            DrawBoard();
        }

        public void DrawBoard()
        {
            View.DrawBoard(Orientation == Enums.BoardOrientation.Inverted);
        }
    }
}
