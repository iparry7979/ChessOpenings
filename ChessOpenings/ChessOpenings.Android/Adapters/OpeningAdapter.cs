using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using ChessOpenings.Models;
using ChessOpenings.Helpers;
using ChessOpenings.Pieces;

namespace ChessOpenings.Droid.Adapters
{
    public class OpeningAdapter : RecyclerView.Adapter
    {
        public List<Opening> Openings;
        public EventHandler<Opening> ItemClick;
        public Enums.Colour Turn;

        public OpeningAdapter(List<Opening> openings)
        {
            Openings = openings;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.opening_card_view, parent, false);

            RVOpeningViewHolder vh = new RVOpeningViewHolder(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RVOpeningViewHolder vh = holder as RVOpeningViewHolder;
            vh.OpeningName.Text = Openings[position].ShortName;

            AlgebraicNotationParser anp = new AlgebraicNotationParser(Openings[position].lastMove, Turn, null);
            Piece subjectPiece = anp.ExtractPiece();

            vh.PieceImage.SetImageResource(GetPieceIconResource(subjectPiece));
            vh.SquareNotation.Text = anp.ExtractDestinationSquare();

            double whiteWinRate = Math.Round(Openings[position].SuccessRate, 2) * 100;
            double blackWinRate = 100 - whiteWinRate;
            vh.WinRate.Text = Turn == Enums.Colour.White ? whiteWinRate.ToString() + "%" : blackWinRate.ToString() + "%";
            vh.WinRate.Text += " - " + Openings[position].Count + " games";
        }

        public void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, Openings[position]);
        }

        private int GetPieceIconResource(Piece piece)
        {
            if (piece.colour == Enums.Colour.White)
            {
                if (piece is King)
                {
                    return (Resource.Drawable.icon_king_white);
                }
                if (piece is Queen)
                {
                    return (Resource.Drawable.icon_queen_white);
                }
                if (piece is Rook)
                {
                    return (Resource.Drawable.icon_rook_white);
                }
                if (piece is Bishop)
                {
                    return (Resource.Drawable.icon_bishop_white);
                }
                if (piece is Knight)
                {
                    return (Resource.Drawable.icon_knight_white);
                }
                if (piece is Pawn)
                {
                    return (Resource.Drawable.icon_pawn_white);
                }
            }
            else
            {
                if (piece is King)
                {
                    return (Resource.Drawable.icon_king_black);
                }
                if (piece is Queen)
                {
                    return (Resource.Drawable.icon_queen_black);
                }
                if (piece is Rook)
                {
                    return (Resource.Drawable.icon_rook_black);
                }
                if (piece is Bishop)
                {
                    return (Resource.Drawable.icon_bishop_black);
                }
                if (piece is Knight)
                {
                    return (Resource.Drawable.icon_knight_black);
                }
                if (piece is Pawn)
                {
                    return (Resource.Drawable.icon_pawn_black);
                }
            }
            return -1;
        }

        public override int ItemCount => Openings.Count();

    }
}