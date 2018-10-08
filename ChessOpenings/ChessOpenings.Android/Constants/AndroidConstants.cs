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

namespace ChessOpenings.Droid
{
    public static class AndroidConstants
    {
        public static int WhitePawnResource = Resource.Drawable.YellowP;
        public static int WhiteBishopResource = Resource.Drawable.YellowB;
        public static int WhiteKnightResource = Resource.Drawable.YellowN;
        public static int WhiteRookResource = Resource.Drawable.YellowR;
        public static int WhiteQueenResource = Resource.Drawable.YellowQ;
        public static int WhiteKingResource = Resource.Drawable.YellowK;

        public static int BlackPawnResource = Resource.Drawable.BrownP;
        public static int BlackBishopResource = Resource.Drawable.BrownB;
        public static int BlackKnightResource = Resource.Drawable.BrownN;
        public static int BlackRookResource = Resource.Drawable.BrownR;
        public static int BlackQueenResource = Resource.Drawable.BrownQ;
        public static int BlackKingResource = Resource.Drawable.BrownK;

        public static Dictionary<string, int> PieceResources = new Dictionary<string, int>()
        {
            {"WK", WhiteKingResource},
            {"WQ", WhiteQueenResource},
            {"WR", WhiteRookResource},
            {"WN", WhiteKnightResource},
            {"WB", WhiteBishopResource},
            {"WP", WhitePawnResource},
            {"BK", BlackKingResource},
            {"BQ", BlackQueenResource},
            {"BR", BlackRookResource},
            {"BN", BlackKnightResource},
            {"BB", BlackBishopResource},
            {"BP", BlackPawnResource}
        };
    }
}