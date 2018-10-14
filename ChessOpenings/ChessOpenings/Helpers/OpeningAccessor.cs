using ChessOpenings.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ChessOpenings.Helpers
{
    public class OpeningAccessor
    {
        public XDocument XmlDocument { get; set; }

        public OpeningAccessor(XDocument document)
        {
            XmlDocument = document;
        }

        public Opening GetOpening(List<string> moves)
        {
            XElement thisOpening = GetOpeningElement(moves);

            if (thisOpening != null)
            {
                Opening result = ElementToOpening(thisOpening);
                if (moves.Count > 0)
                {
                    if (moves[moves.Count - 1] != result.lastMove)
                    {
                        Board board = new Board(moves);
                        BoardPosition position = board.ToBoardPosition();
                        int comparisonValue = position.GenerateComparisonValue();
                        List<Opening> candidateOpenings = GetOpeningsForComparisonValue(comparisonValue);
                        foreach (Opening candidate in candidateOpenings)
                        {
                            Board candidateBoard = new Board(candidate.Moves);
                            BoardPosition candidatePosition = candidateBoard.ToBoardPosition();
                            if (position.Equals(candidatePosition))
                            {
                                result = candidate;
                            }
                        }
                    }
                }
                return result;
            }
            return null;
        }

        public List<Opening> GetChildrenOfOpening(List<string> moves)
        {
            Opening opening = GetOpening(moves);
            XElement thisOpening = GetOpeningElement(opening.Moves);
            List<Opening> childOpenings = new List<Opening>();
            if (thisOpening.Attribute("Name").Value == "Start" || moves.LastOrDefault() == thisOpening.Attribute("Move").Value)
            {
                List<XElement> children = thisOpening.Elements().ToList();
                foreach (XElement child in children)
                {
                    childOpenings.Add(ElementToOpening(child));
                }
                childOpenings = childOpenings.OrderByDescending(o => o.Frequency).ToList();
            }
            return childOpenings;
        }

        private Opening ElementToOpening(XElement element, bool includeMoveList = false)
        {
            if (element == null) return null;
            Opening rtn = new Opening();
            rtn.Name = element.Attribute("Name")?.Value;
            rtn.lastMove = element.Attribute("Move")?.Value;
            rtn.Id = element.Name.LocalName;

            string freq = element.Attribute("Frequency")?.Value;
            string succRate = element.Attribute("Success_Rate")?.Value;
            string count = element.Attribute("Count")?.Value;

            rtn.Frequency = Convert.ToDouble(freq, CultureInfo.InvariantCulture);
            rtn.SuccessRate = Convert.ToDouble(succRate, CultureInfo.InvariantCulture);
            rtn.Count = Convert.ToInt32(count, CultureInfo.InvariantCulture);

            if (includeMoveList)
            {
                rtn.Moves = BuildMoveList(element);
            }

            return rtn;
        }

        private List<string> BuildMoveList(XElement element)
        {
            List<string> moves = new List<string>();
            XElement currentElement = element;
            while (currentElement != null)
            {
                if (!string.IsNullOrEmpty(currentElement.Attribute("Move")?.Value))
                {
                    moves.Add(currentElement.Attribute("Move").Value);
                }
                currentElement = currentElement.Parent;
            }
            moves.Reverse();
            return moves;
        }

        private XElement GetOpeningElement(List<string> moves)
        {
            XElement thisOpening = XmlDocument.Root;
            if (moves == null || moves.Count() == 0)
            {
                return thisOpening;
            }
            foreach (string move in moves)
            {
                List<XElement> children = thisOpening.Elements().ToList();
                if (children.Count() > 0)
                {
                    foreach (XElement child in children)
                    {
                        if (child.Attribute("Move").Value == move)
                        {
                            thisOpening = child;
                        }
                    }
                }
            }
            return thisOpening;
        }

        private List<Opening> GetOpeningsForComparisonValue(int comparisonValue)
        {
            List<Opening> rtn = new List<Opening>();
            XElement root = XmlDocument.Root;
            GetOpeningsForComparisonValueRecursive(root, comparisonValue, rtn);
            return rtn;

        }

        private void GetOpeningsForComparisonValueRecursive(XElement currentElement, int comparisonValue, List<Opening> openingList)
        {
            string openingValue = currentElement.Attribute("ComparisonValue")?.Value;
            if (!string.IsNullOrEmpty(openingValue))
            {
                int cv = 0;
                if (int.TryParse(openingValue, out cv))
                {
                    if (cv == comparisonValue)
                    {
                        openingList.Add(ElementToOpening(currentElement, true));
                    }
                }
            }
            List<XElement> children = currentElement.Elements().ToList();
            foreach (XElement e in children)
            {
                GetOpeningsForComparisonValueRecursive(e, comparisonValue, openingList);
            }
        }
    }
}
