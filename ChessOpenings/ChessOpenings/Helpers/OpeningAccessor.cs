using ChessOpenings.Models;
using System;
using System.Collections.Generic;
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

            if (thisOpening != null && thisOpening.Name != "Start")
            {
                return ElementToOpening(thisOpening);
            }
            return null;
        }

        public List<Opening> GetChildrenOfOpening(List<string> moves)
        {
            XElement thisOpening = GetOpeningElement(moves);
            List<XElement> children = thisOpening.Elements().ToList();
            List<Opening> childOpenings = new List<Opening>();
            foreach (XElement child in children)
            {
                childOpenings.Add(ElementToOpening(child));
            }
            return childOpenings;
        }

        private Opening ElementToOpening(XElement element)
        {
            if (element == null) return null;
            Opening rtn = new Opening();
            rtn.Name = element.Attribute("Name").Value;
            rtn.lastMove = element.Attribute("Move").Value;
            rtn.Id = element.Name.LocalName;
            return rtn;
        }

        private XElement GetOpeningElement(List<string> moves)
        {
            XElement thisOpening = XmlDocument.Root;
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
    }
}
