using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ChessOpenings.Models;
using ChessOpenings.ViewInterfaces;
using Android.Graphics;
using ChessOpenings.Droid.Views;

namespace ChessOpenings.Droid
{
    [Activity(Label = "ChessOpenings.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainScreen : Activity, IMainScreen
    {

        public Board Board { get; set; }
        public Move NextMove { get; set; }
        private GridLayout boardTable {get; set;}
        private LinearLayout boardLayout { get; set; }
        //private bool fromSquareSet;
        private SquareView selectedSquare;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            Board = new Board();

            Board.InitialiseBoard();

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            NextMove = new Move();
            selectedSquare = null;

            boardLayout = FindViewById<LinearLayout>(Resource.Id.boardLayout);

            boardTable = new SquareGridLayout(this);
            boardTable.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            boardTable.SetBackgroundColor(Color.Red); //Remove once layout complete
            boardTable.RowCount = 8;
            boardTable.ColumnCount = 8;

            BuildBoard(boardTable);

            boardLayout.AddView(boardTable);
        }

        private void DrawBoard()
        {
            
            boardTable = new SquareGridLayout(this);
            boardTable.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            boardTable.SetBackgroundColor(Color.Red); //Remove once layout complete
            boardTable.RowCount = 8;
            boardTable.ColumnCount = 8;

            BuildBoard(boardTable);

            boardLayout.AddView(boardTable);
        }

        private void BuildBoard(GridLayout boardGrid)
        {
            //Builds the Table Layout from the Board object

            for (int i = Board.squaresArray.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < Board.squaresArray.GetLength(1); j++)
                {
                    View squareLayout = BuildSquare(Board.squaresArray[i, j]);
                    squareLayout.Click += SquareClicked;
                    boardGrid.AddView(squareLayout);
                }
            }
        }

        public View BuildSquare(Square squareModel)
        {
            SquareView rtn = new SquareView(this, squareModel);
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
            if (selectedSquare == null)
            {
                if (tappedSquare.ContainsPiece())
                {
                    NextMove.FromSquare = tappedSquare.squareModel;
                    SelectSquare(tappedSquare);
                }
            }
            else
            {
                NextMove.ToSquare = tappedSquare.squareModel;
                Board.MakeMove(NextMove);
                DrawBoard();
                UnselectSelectedSquare();
                NextMove = new Move();
            }
        }

        private void SelectSquare(SquareView s)
        {
            UnselectSelectedSquare();
            s.SelectSquare();
            selectedSquare = s;
        }

        private void UnselectSelectedSquare()
        {
            if (selectedSquare != null)
            {
                selectedSquare.UnSelectSquare();
                selectedSquare = null;
            }
        }
    }
}


