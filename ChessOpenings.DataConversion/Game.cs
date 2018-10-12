using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessOpenings.DataConversion
{
    public class Game
    {
        private List<string> moves = new List<string>();
        public float Result { get; set; }
        private string gameString;

        public Game(string gameString)
        {
            this.gameString = gameString;
            if (gameString.Contains("1/2-1/2"))
            {
                Result = 0.5f;
            }
            else if (gameString.Contains("1-0"))
            {
                Result = 1;
            }
            else if (gameString.Contains("0-1"))
            {
                Result = 0;
            }
            else
            {
                Result = -1;
            }
            ExtractMoves();
        }

        public override string ToString()
        {
            string winningString = "";
            if (Result == 1) winningString = "White wins";
            if (Result == 0.5) winningString = "Draw";
            if (Result == 0) winningString = "Black wins";
            string rtn = moves.Count() + " moves - " + winningString;
            return rtn;
        }

        public bool EmploysOpening(List<string> openingSequence)
        {
            bool rtn = true;
            for (int i = 0; i < openingSequence.Count(); i++)
            {
                if (i < moves.Count())
                {
                    if (!openingSequence[i].Equals(moves[i]))
                    {
                        rtn = false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return rtn;
        }

        private void ExtractMoves()
        {
            string strippedString = Regex.Replace(gameString, "\\[.*\\]", "");
            strippedString = Regex.Replace(strippedString, "\\$\\d", "");
            strippedString = Regex.Replace(strippedString, "\\d*\\.", "");
            strippedString = Regex.Replace(strippedString, "1/2-1/2", "");
            strippedString = Regex.Replace(strippedString, "1-0", "");
            strippedString = Regex.Replace(strippedString, "0-1", "");

            string[] movesArray = Regex.Split(strippedString, "\\s");

            for (int i = 0; i < movesArray.Length; i++)
            {
                string currentString = movesArray[i];
                if (currentString.Trim().Length != 0)
                {
                    moves.Add(currentString);
                }
            }
        }
    }
}
