using ChessOpenings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessOpenings.DataConversion
{
    public class TransitionOpening
    {
        public string Code { get; set; }
        public string Name { get; set;}
        public int ComparisonValue { get; set; }
        private List<string> _moves;
        public List<String> Moves
        {
            get
            {
                return _moves;
            }
        }

        public TransitionOpening()
        {
            _moves = new List<string>();
        }

        public void AddMove(string move)
        {
            _moves.Add(move);
        }

        public static TransitionOpening FromString(string openingString)
        {
            TransitionOpening rtn = new TransitionOpening();
            if (openingString == null || openingString.Length == 0)
            {
                return rtn;
            }
            int currentPosition = 0;
            StringBuilder codeString = new StringBuilder();

            //extract code
            while (openingString[currentPosition] != ' ')
            {
                codeString.Append(openingString[currentPosition]);
                currentPosition++;
                if (openingString.Length == currentPosition)
                {
                    rtn.Code = codeString.ToString();
                    return rtn;
                }
            }
            rtn.Code = codeString.ToString();

            //extract opening name
            int quoteCount = 0;
            StringBuilder openingName = new StringBuilder();

            while (quoteCount < 2)
            {
                if (quoteCount == 0)
                {
                    if (openingString[currentPosition] == '\"')
                        quoteCount++;
                }
                else if (quoteCount == 1)
                {
                    if (openingString[currentPosition] == '\"')
                    {
                        quoteCount++;
                    }
                    else
                    {
                        openingName.Append(openingString[currentPosition]);
                    }
                }
                currentPosition++;
                if (openingString.Length == currentPosition)
                {
                    rtn.Name = openingName.ToString();
                    return rtn;
                }
            }
            rtn.Name = openingName.ToString();

            //Extract Moves
            string moveString = openingString.Substring(currentPosition).Trim();
            string[] movesArray = moveString.Split(' ');
            foreach(string currentMove in movesArray)
            {
                if (currentMove.Length > 0 && currentMove[0] != '*')
                {
                    string[] splitByPeriod = currentMove.Split('.');
                    string notation = splitByPeriod[splitByPeriod.Length - 1];
                    if (notation != null && notation.Length > 0)
                    {
                        rtn.AddMove(notation);
                    }
                }
            }

            //Generate Comparison Value
            Board board = new Board(rtn.Moves);
            BoardPosition position = board.ToBoardPosition();
            int comparisonValue = position.GenerateComparisonValue();
            rtn.ComparisonValue = comparisonValue;

            return rtn;
        }
    }
}
