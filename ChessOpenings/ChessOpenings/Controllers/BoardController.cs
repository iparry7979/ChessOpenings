﻿using ChessOpenings.Models;
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
using ChessOpenings.Helpers;

namespace ChessOpenings.Controllers
{
    public class BoardController
    {
        public IBoardView BoardView;
        public Square selectedSquare;
        public Move NextMove;
        public Board Board;
        public Enums.BoardOrientation Orientation;
        public OpeningAccessor OAccessor;

        private const string OPENINGDATAFILE = "openings.xml";

        public BoardController(IBoardView boardView/*, IOpeningInformationView openingView*/)
        {
            BoardView = boardView;
            //OpeningView = openingView;
            Board = new Board();
            NextMove = new Move();
            selectedSquare = null;
            Orientation = Enums.BoardOrientation.Standard;
            XDocument doc = XDocument.Load(BoardView.ParseOpenings());
            OAccessor = new OpeningAccessor(doc);
            Board.InitialiseBoard();
            UpdateOpening();
            UpdateOpeningList();
        }

        public void SquareTapped(Square tappedSquare)
        {
            if (selectedSquare == null)
            {
                if (tappedSquare.Piece != null && tappedSquare.Piece.colour == Board.Turn)
                {
                    NextMove.FromSquare = tappedSquare;
                    selectedSquare = tappedSquare;
                    BoardView.SelectSquare(tappedSquare);
                }
            }
            else
            {
                if (tappedSquare == selectedSquare)
                {
                    selectedSquare = null;
                    BoardView.UnselectSquare(tappedSquare);
                    return;
                }
                NextMove.ToSquare = tappedSquare;
                Board.MakeMove(NextMove);
                DrawBoard();
                BoardView.UnselectSquare(selectedSquare);
                UpdateOpening();
                UpdateOpeningList();
                selectedSquare = null;
                NextMove = new Move();
            }
        }

        public void GoBackOneMove()
        {
            Board.GoBackOneMove();
            BoardView.UnselectSquare(selectedSquare);
            UpdateOpening();
            UpdateOpeningList();
            selectedSquare = null;
            NextMove = new Move();
            DrawBoard();
        }

        public void ResetBoard()
        {
            selectedSquare = null;
            NextMove = new Move();
            Board = new Board();
            UpdateOpening();
            UpdateOpeningList();
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
            BoardView.DrawBoard(Orientation == Enums.BoardOrientation.Inverted);
        }

        public void UpdateOpening()
        {
            BoardView.UpdateOpeningName(GetOpening().Name);
        }

        public void UpdateOpeningList()
        {
            BoardView.UpdateOpeningList(GetNextOpenings());
        }

        public Opening GetOpening()
        {
            return OAccessor.GetOpening(Board.GetAllMovesByNotation());
        }

        public List<Opening> GetNextOpenings()
        {
            return OAccessor.GetChildrenOfOpening(Board.GetAllMovesByNotation());
        }
    }
}
