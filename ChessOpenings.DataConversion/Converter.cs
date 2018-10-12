using ChessOpenings.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ChessOpenings.DataConversion
{
    public class Converter
    {
        public static StreamReader reader;
        public static string path = "E:/ChessOpenings/ChessOpenings.DataConversion/DataFiles/";
        public static string inputFile = "scid.eco";
        public static string outputFile = "openings2.xml";
        public static string scidDataFile = "E:/ChessOpenings/ChessOpenings.DataConversion/DataFiles/scid.eco";
        public static string currentLine;
        public static List<TransitionOpening> allOpenings;
        public static List<Game> gameList;
        public static XmlDocument doc;
        public static XmlElement root;
        static void Main(string[] args)
        {
            Init();
            int count = 1;
            while (currentLine != null)
            {
                
                if (count % 100 == 0)
                {
                    Console.WriteLine("Reading item " + count);
                }
                string currentOpeningString = GetNextOpening();
                TransitionOpening opening = TransitionOpening.FromString(currentOpeningString);
                allOpenings.Add(opening);
                count++;
            }

            XmlElement root = ConvertToXml(allOpenings);
            Console.WriteLine("Determining Opening Frequencies...");
            DetermineOpeningFrequencies(root, new List<string>());
            Console.WriteLine("Done determining frequencies");
            doc.AppendChild(root);
            XDocument.Parse(doc.OuterXml).Save(path + outputFile);
        }

        static string GetNextOpening()
        {
            string rtn = "";
            try
            {
                bool done = false;
                while (!done && currentLine != null)
                {
                    if (currentLine.Trim().EndsWith("*"))
                    {
                        done = true;
                    }
                    if (!currentLine.StartsWith("#") && currentLine.Trim().Length > 0)
                    {
                        rtn = rtn + currentLine;
                    }
                    currentLine = reader.ReadLine();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return rtn;
        }

        static void Init()
        {
            doc = new XmlDocument();
            allOpenings = new List<TransitionOpening>();
            reader = new StreamReader(path + inputFile);
            currentLine = reader.ReadLine();
            PgnReader r = new PgnReader();
            Console.WriteLine("Reading Games...");
            gameList = r.GetGamesList();
            Console.WriteLine("Done reading games");
        }

        static XmlElement ConvertToPositionXml(List<TransitionOpening> openings)
        {
            XmlElement root = doc.CreateElement("openings");
            foreach (TransitionOpening opening in openings)
            {
                if (openings.IndexOf(opening) % 100 == 0)
                {
                    Console.WriteLine("Processing item " + openings.IndexOf(opening) + " of " + openings.Count());
                }
                Board board = new Board(opening.Moves);
                BoardPosition position = board.ToBoardPosition();
                int comparisonValue = position.GenerateComparisonValue();
                XmlElement openingElement = doc.CreateElement("Opening");
                openingElement.SetAttribute("Code", opening.Code);
                openingElement.SetAttribute("ComparisonValue", comparisonValue.ToString());
                root.AppendChild(openingElement);
            }
            return root;
        }

        static XmlElement ConvertToXml(List<TransitionOpening> openings)
        {
            root = doc.CreateElement("Start");
            root.SetAttribute("Name", "Start");
            int openingCount = 1;
            foreach (TransitionOpening currentOpening in openings)
            {
                AddOpening(currentOpening);
                if (openingCount % 100 == 0)
                {
                    Console.WriteLine("Converting opening " + openingCount + " to XML");
                }
                openingCount++;
            }
            return root;
        }

        static void AddOpening(TransitionOpening opening)
        {
            XmlElement currentElement = root;
            foreach(string move in opening.Moves)
            {
                XmlNodeList eList = currentElement.ChildNodes;
                XmlElement e = GetChildForMove(eList, move);
                if (e == null)
                {
                    XmlElement child = doc.CreateElement(opening.Code);
                    child.SetAttribute("Move", move);
                    child.SetAttribute("Name", opening.Name);
                    child.SetAttribute("ComparisonValue", opening.ComparisonValue.ToString());
                    currentElement.AppendChild(child);
                }
                else
                {
                    currentElement = e;
                }
            }
        }

        static XmlElement GetChildForMove(XmlNodeList eList, string move)
        {
            foreach (XmlNode node in eList)
            {
                XmlElement e = node as XmlElement;
                if (e.GetAttribute("Move") == move)
                {
                    return e;
                }
            }
            return null;
        }

        static void DetermineOpeningFrequencies(XmlElement element, List<string> moves)
        {
            if (element.GetAttribute("Move") != "")
            {
                moves.Add(element.GetAttribute("Move"));
            }

            XmlNodeList children = element.ChildNodes;
            if (children.Count == 0) return; //base case

            List<int> gameCountList = new List<int>();
            List<float> successRateList = new List<float>();

            foreach (XmlNode node in children)
            {
                XmlElement e = node as XmlElement;
                float successRate = 0;
                List<string> movesForChild = new List<string>();
                movesForChild.AddRange(moves);
                movesForChild.Add(e.GetAttribute("Move"));
                int count = 0;
                float successCount = 0;
                foreach (Game g in gameList)
                {
                    if (g.EmploysOpening(movesForChild))
                    {
                        count++;
                        successCount += g.Result;
                    }
                }
                gameCountList.Add(count);

                if (count > 0)
                {
                    successRate = successCount / (float)count;
                }
                successRateList.Add(successRate);
            }

            int total = 0;
            foreach (int i in gameCountList)
            {
                total += i;
            }
            for (int i = 0; i < children.Count; i++)
            {
                double proportion = 0;
                if (total != 0)
                {
                    proportion = (double)(gameCountList[i]) / (double)total;
                }
                float successRate = successRateList[i];
                int gameCount = gameCountList[i];
                XmlElement currentChild = children[i] as XmlElement;
                currentChild.SetAttribute("Frequency", proportion.ToString());
                currentChild.SetAttribute("Success_Rate", successRate.ToString());
                currentChild.SetAttribute("Count", gameCount.ToString());
            }
            //Recursive case
            foreach (XmlNode child in children)
            {
                List<string> movesCopy = new List<string>();
                movesCopy.AddRange(moves);
                DetermineOpeningFrequencies(child as XmlElement, movesCopy);
            }
        }
    }
}
