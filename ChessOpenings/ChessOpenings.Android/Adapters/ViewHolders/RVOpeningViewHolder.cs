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

namespace ChessOpenings.Droid.Adapters
{
    public class RVOpeningViewHolder : RecyclerView.ViewHolder
    {
        public TextView OpeningName;
        public ImageView PieceImage;
        public ImageView ArrowImage;
        public TextView SquareNotation;
        public TextView WinRate;
        public RVOpeningViewHolder (View itemView, Action<int> listener) : base(itemView)
        {
            OpeningName = itemView.FindViewById<TextView>(Resource.Id.tv_opening_text);
            PieceImage = itemView.FindViewById<ImageView>(Resource.Id.piece_image);
            ArrowImage = itemView.FindViewById<ImageView>(Resource.Id.arrow);
            SquareNotation = itemView.FindViewById<TextView>(Resource.Id.tv_destination_square);
            WinRate = itemView.FindViewById<TextView>(Resource.Id.tv_win_rate);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }
}