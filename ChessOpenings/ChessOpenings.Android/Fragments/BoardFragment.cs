using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ChessOpenings.ViewInterfaces;
using ChessOpenings.Controllers;
using ChessOpenings.Models;
using ChessOpenings.Droid.Views;
using Android.Graphics;

namespace ChessOpenings.Droid.Fragments
{
    public class BoardFragment : Fragment, IBoardView
    {
        private GridLayout boardTable { get; set; }
        private LinearLayout boardLayout { get; set; }
        private BoardController boardController;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            boardController = new BoardController(this);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.boardView, container, false);
            boardLayout = view.FindViewById<LinearLayout>(Resource.Id.boardLayout);
            DrawBoard();
            return view;
        }

        public void DrawBoard()
        {
            boardLayout.RemoveAllViews();
            boardTable = new SquareGridLayout(this.Activity);
            boardTable.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            boardTable.RowCount = 8;
            boardTable.ColumnCount = 8;

            BuildBoard(boardTable);

            boardLayout.AddView(boardTable);
        }

        private void BuildBoard(GridLayout boardGrid)
        {
            //Builds the Table Layout from the Board object
            Board board = boardController.Board;

            for (int i = board.squaresArray.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < board.squaresArray.GetLength(1); j++)
                {
                    View squareLayout = BuildSquare(board.squaresArray[i, j]);
                    squareLayout.Click += SquareClicked;
                    boardGrid.AddView(squareLayout);
                }
            }
        }

        public View BuildSquare(Square squareModel)
        {
            SquareView rtn = new SquareView(this.Activity, squareModel);
            rtn.UpdateBackgroundColor();
            if (squareModel.Piece != null)
            {
                rtn.Text = squareModel.Piece.ToString() + squareModel.File + squareModel.Rank;
                rtn.SetTextColor(squareModel.Piece.colour == Enums.Colour.White ? Color.LightGray : Color.DarkGray);
            }

            GridLayout.LayoutParams param = new GridLayout.LayoutParams();
            param.RowSpec = GridLayout.InvokeSpec(GridLayout.Undefined, 1f);
            param.ColumnSpec = GridLayout.InvokeSpec(GridLayout.Undefined, 1f);
            rtn.LayoutParameters = param;
            return rtn;
        }

        public void SquareClicked(object sender, EventArgs args)
        {
            SquareView tappedSquare = null;
            if (sender is SquareView)
            {
                tappedSquare = (SquareView)sender;
            }
            if (tappedSquare == null)
            {
                return;
            }
            boardController.SquareTapped(tappedSquare.squareModel);
        }

        public void SelectSquare(Square sq)
        {
            SquareView s = GetSquareView(sq);


            SelectSquare(s);

            if (s != null)
            {
                s.SelectSquare();
            }
        }

        private void SelectSquare(SquareView s)
        {
            s.SelectSquare();
        }

        public void UnselectSquare(Square sq)
        {
            SquareView s = GetSquareView(sq);
            s.UnSelectSquare();
        }

        public SquareView GetSquareView(Square sq)
        {
            for (int i = 0; i < boardTable.ChildCount; i++)
            {
                View v = boardTable.GetChildAt(i);
                if (v is SquareView)
                {
                    if (((SquareView)v).squareModel.Equals(sq))
                    {
                        return (SquareView)v;
                    }
                }
            }
            return null;
        }
    }
}