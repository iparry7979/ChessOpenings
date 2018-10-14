using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.DataConversion
{
    public class PgnReader
    {
        public static string PATH = "E:/";
        public static string FILENAME = "ficsgamesdb_201801_standard2000_nomovetimes_1543578.pgn";
        public StreamReader reader;
        public List<Game> games = new List<Game>();

        public List<Game> GetGamesList()
        {
            reader = new StreamReader(PATH + FILENAME);

            string currentGame = "";

            string currentLine = reader.ReadLine();
            while (currentLine != null)
            {
                currentGame += currentLine + "\n";
                if (currentLine.EndsWith("1/2-1/2") || currentLine.EndsWith("1-0") || currentLine.EndsWith("0-1"))
                {
                    games.Add(new Game(currentGame));
                    currentGame = "";
                }
                currentLine = reader.ReadLine();
            }

            return games;
        }
    }
}
