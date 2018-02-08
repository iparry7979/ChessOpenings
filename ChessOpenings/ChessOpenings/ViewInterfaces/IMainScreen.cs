using ChessOpenings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.ViewInterfaces
{
    public interface IMainScreen
    {
        Board Board { get; set; }
    }
}
