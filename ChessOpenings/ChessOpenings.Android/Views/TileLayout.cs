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
using ChessOpenings.Pieces;
using ChessOpenings.Models;
using ChessOpenings.Helpers;
using Android.Graphics;

namespace ChessOpenings.Droid.Views
{
    public class TileLayout : LinearLayout
    {
        private bool compact { get; set; }

        private ImageView ivPieceImageIcon;
        private TextView tvDestinationSquare;
        private TextView tvOpeningName;
        private TextView tvWinRate;
        private LinearLayout tile;

        public string MoveNotation { get; set; }

        public Color TileColour
        {
            set
            {
                tile.SetBackgroundColor(value);
            }
        }
        /*public TileLayout(Context context, Piece piece, double frequency, double winRate, bool compact = false) : base(context)
        {
            this.compact = compact;
            int resource = compact ? Resource.Layout.next_move_tile_view_compact : Resource.Layout.next_move_tile_view;
            Inflate(context, resource, this);
            ivPieceImageIcon = FindViewById<ImageView>(Resource.Id.piece_image);
            tvDestinationSquare = FindViewById<TextView>(Resource.Id.tv_destination_square);
            tvOpeningName = FindViewById<TextView>(Resource.Id.tv_next_opening_name);
            tvWinRate = FindViewById<TextView>(Resource.Id.winrate);
        }*/

        public TileLayout(Context context, Opening opening, bool compact, Enums.Colour turn) : base(context)
        {
            this.compact = compact;
            int resource = compact ? Resource.Layout.next_move_tile_view_compact : Resource.Layout.next_move_tile_view;
            Inflate(context, resource, this);
            ivPieceImageIcon = FindViewById<ImageView>(Resource.Id.piece_image);
            tvDestinationSquare = FindViewById<TextView>(Resource.Id.tv_destination_square);
            tvOpeningName = FindViewById<TextView>(Resource.Id.tv_next_opening_name);
            tvWinRate = FindViewById<TextView>(Resource.Id.winrate);
            tile = FindViewById<LinearLayout>(Resource.Id.tile);

            MoveNotation = opening.lastMove;

            AlgebraicNotationParser anp = new AlgebraicNotationParser(opening.lastMove, turn, null);
            Piece subjectPiece = anp.ExtractPiece();

            ivPieceImageIcon.SetImageResource(GetPieceIconResource(subjectPiece));
            tvDestinationSquare.Text = anp.ExtractDestinationSquare();
            tvOpeningName.Text = opening.ShortName;
            double successPercentage = Math.Round(opening.SuccessRate, 2) * 100;
            tvWinRate.Text = successPercentage.ToString() + "%";
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            if (!compact)
            {
                int parentWidth = MeasureSpec.GetSize(widthMeasureSpec);
                int parentHeight = MeasureSpec.GetSize(heightMeasureSpec);
                int size = parentWidth > parentHeight ? parentHeight : parentWidth;
                int finalMeasureSpec = MeasureSpec.MakeMeasureSpec(size, MeasureSpecMode.Exactly);
                base.OnMeasure(finalMeasureSpec, finalMeasureSpec);
            }
            else
            {
                base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
                int size = MeasureSpec.GetSize(heightMeasureSpec);
                int finalMeasureSpec = MeasureSpec.MakeMeasureSpec(size, MeasureSpecMode.Exactly);
                base.OnMeasure(finalMeasureSpec, finalMeasureSpec);
            }
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
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
    }
}