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

namespace ChessOpenings.Droid.Views
{
    class SquareGridLayout : GridLayout
    {
        public SquareGridLayout(Context context) : base(context)
        {

        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            int parentWidth = MeasureSpec.GetSize(widthMeasureSpec);
            int parentHeight = MeasureSpec.GetSize(heightMeasureSpec);
            bool useWidth = parentWidth <= parentHeight;
            if (useWidth)
            {
                base.OnMeasure(widthMeasureSpec, widthMeasureSpec);
                return;
            }
            base.OnMeasure(heightMeasureSpec, heightMeasureSpec);
        }
    }
}