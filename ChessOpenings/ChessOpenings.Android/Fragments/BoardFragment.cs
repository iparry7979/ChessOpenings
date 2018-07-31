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
using Java.IO;
using System.IO;
using ChessOpenings.Droid.Adapters;
using Android.Support.V7.RecyclerView;
using Android.Support.V7.CardView;
using Android.Support.V7.Widget;

namespace ChessOpenings.Droid.Fragments
{
    public class BoardFragment : Fragment, IBoardView
    {
        private GridLayout boardTable { get; set; }
        private LinearLayout boardLayout { get; set; }
        private LinearLayout tileLayout { get; set; }
        private LinearLayout secondaryLinesLayout { get; set; }
        private TextView openingName;
        private ListView lvNextMoves;
        public BoardController boardController { get; set; }
        private View view;
        private RecyclerView recyclerView;
        private RecyclerView.LayoutManager rvLayoutManager;
        private OpeningAdapter rvAdapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.boardView, container, false);
            boardLayout = view.FindViewById<LinearLayout>(Resource.Id.boardLayout);
            tileLayout = view.FindViewById<LinearLayout>(Resource.Id.tileView);
            secondaryLinesLayout = view.FindViewById<LinearLayout>(Resource.Id.tile_scroll_view);
            openingName = view.FindViewById<TextView>(Resource.Id.openingName);
            lvNextMoves = view.FindViewById<ListView>(Resource.Id.next_move_list);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            rvLayoutManager = new LinearLayoutManager(this.Activity);
            recyclerView.SetLayoutManager(rvLayoutManager);
            boardController = new BoardController(this);
            boardController.DrawBoard();
            boardController.UpdateOpening();
            boardController.UpdateOpeningList();
            InitialiseButtons();
            return view;
        }

        public void DrawBoard(bool inverted)
        {
            
            boardLayout.RemoveAllViews();
            boardTable = new SquareGridLayout(this.Activity);
            boardTable.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            boardTable.RowCount = 8;
            boardTable.ColumnCount = 8;
            BuildBoard(boardTable, inverted);
            boardLayout.AddView(boardTable);
        }

        private void BuildBoard(GridLayout boardGrid, bool inverted = false)
        {
            //Builds the Table Layout from the Board object
            Board board = boardController.Board;

            if (!inverted)
            {
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
            else
            {
                for (int i = 0; i < board.squaresArray.GetLength(0); i++)
                {
                    for (int j = board.squaresArray.GetLength(1) -1; j >= 0; j--)
                    {
                        View squareLayout = BuildSquare(board.squaresArray[i, j]);
                        squareLayout.Click += SquareClicked;
                        boardGrid.AddView(squareLayout);
                    }
                }
            }
        }

        public View BuildSquare(Square squareModel)
        {
            SquareView rtn = new SquareView(this.Activity, squareModel); //SquareView extends ImageView
            rtn.UpdateBackgroundColor(); //set square color
            if (squareModel.Piece != null)
            {
                rtn.DrawPiece();
            }

            GridLayout.LayoutParams param = new GridLayout.LayoutParams(new ViewGroup.LayoutParams(50, 50));

            //set weight of each row and column to ensure equal size
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

        public void OpeningTileClicked(object sender, EventArgs args)
        {
            if (sender is TileLayout)
            {
                boardController.MakeMove(((TileLayout)sender).MoveNotation);
            }
        }

        public void OpeningCardClicked(object sender, Opening opening)
        {
            if (opening != null)
            {
                boardController.MakeMove(opening.lastMove);
            }
        }

        public void BackClicked(object sender, EventArgs args)
        {
            boardController.GoBackOneMove();
        }

        public void ResetClicked(object sender, EventArgs args)
        {
            boardController.ResetBoard();
        }

        public void FlipBoardClicked(object sender, EventArgs args)
        {
            boardController.FlipBoard();
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
            if (s != null)
            {
                s.UnSelectSquare();
            }
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

        public void InitialiseButtons()
        {
            ImageButton backOneButton = view.FindViewById<ImageButton>(Resource.Id.back_one_move_button);
            ImageButton resetButton = view.FindViewById<ImageButton>(Resource.Id.reset_board_button);
            ImageButton flipBoardButton = view.FindViewById<ImageButton>(Resource.Id.flip_board_button);

            backOneButton.Click += BackClicked;
            resetButton.Click += ResetClicked;
            flipBoardButton.Click += FlipBoardClicked;
        }

        public Stream ParseOpenings()
        {
            string s = System.Environment.CurrentDirectory;
            Stream stream = null;
            try
            {
                stream = Activity.Assets.Open("openings.xml");
            }
            catch (Java.IO.FileNotFoundException e)
            {
                return null;
            }
            return stream;
        }

        public void UpdateOpeningName(string name)
        {
            openingName.Text = name;
        }

        public void UpdateOpeningList(List<Opening> openings)
        {
            /*bool firstOpeningFlag = true;
            tileLayout.RemoveAllViews();
            secondaryLinesLayout.RemoveAllViews();
            for(int i = 0; i < openings.Count(); i++)
            {
                if (firstOpeningFlag)
                {
                    TileLayout t = new TileLayout(this.Activity, openings[i], false, boardController.Board.Turn);
                    t.Click += OpeningTileClicked;
                    tileLayout.AddView(t);
                }
                else
                {
                    TileLayout t = new TileLayout(this.Activity, openings[i], true, boardController.Board.Turn);
                    Color c = new Color();
                    int colourOffset = (127 / openings.Count() - 1) * i;
                    c.G = (byte)(127 - colourOffset);
                    c.R = (byte)(127 + colourOffset);
                    c.B = 0;
                    c.A = 255;
                    t.TileColour = c;
                    secondaryLinesLayout.AddView(t);
                    t.Click += OpeningTileClicked;
                }
                firstOpeningFlag = false;
            }*/

            rvAdapter = new OpeningAdapter(openings);
            rvAdapter.ItemClick += OpeningCardClicked;
            rvAdapter.Turn = boardController.Board.Turn;
            recyclerView.SetAdapter(rvAdapter);
                
            
            
        }
    }
}