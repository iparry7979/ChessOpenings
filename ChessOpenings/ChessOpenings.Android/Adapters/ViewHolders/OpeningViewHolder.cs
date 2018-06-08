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

namespace ChessOpenings.Droid.Adapters
{
    public class OpeningViewHolder : Java.Lang.Object
    {
        public TextView Name;
        public TextView lastMove;
        public TextView Id;
    }
}