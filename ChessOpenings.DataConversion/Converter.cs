using ChessOpenings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.DataConversion
{
    public class Converter
    {
        static void Main(string[] args)
        {
            String text = System.IO.File.ReadAllText("../../DataFiles/scid.eco");
            Console.WriteLine(text);
            
        }
    }
}
