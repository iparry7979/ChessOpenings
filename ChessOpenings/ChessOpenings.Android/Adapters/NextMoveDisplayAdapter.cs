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
using ChessOpenings.Models;

namespace ChessOpenings.Droid.Adapters
{
    public class NextMoveDisplayAdapter : BaseAdapter<Opening>
    {
        List<Opening> NextOpenings;

        public NextMoveDisplayAdapter(List<Opening> openings)
        {
            NextOpenings = openings;
        }

        public override Opening this[int position]
        {
            get
            {
                return NextOpenings[position];
            }
        }

        public override int Count
        {
            get
            {
                return NextOpenings.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.next_move_table_row, parent, false);

                var move = view.FindViewById<TextView>(Resource.Id.move_notation);
                var name = view.FindViewById<TextView>(Resource.Id.opening_name);

                view.Tag = new OpeningViewHolder { Name = name, lastMove = move };
            }

            var holder = (OpeningViewHolder)view.Tag;
            holder.Name.Text = NextOpenings[position].Name;
            holder.lastMove.Text = NextOpenings[position].lastMove;

            return view;
        }
    }
}