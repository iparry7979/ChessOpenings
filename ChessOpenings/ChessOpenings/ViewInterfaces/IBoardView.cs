using ChessOpenings.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.ViewInterfaces
{
    public interface IBoardView
    {
        void SelectSquare(Square sq);
        void UnselectSquare(Square s);
        void DrawBoard(bool inverted);
        void UpdateOpeningName(string openingName);
        Stream ParseOpenings();
        void UpdateOpeningList(List<Opening> openings);
    }
}
