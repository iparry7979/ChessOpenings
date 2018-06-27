using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChessOpenings.Models
{
    public class Opening
    {
        public string Name { get; set; }

        public string lastMove { get; set; }

        public string Id { get; set; }

        public double Frequency { get; set; }

        public double SuccessRate { get; set; }

        public string ShortName
        {
            get
            {
                Regex reg = new Regex(@"(:|,)\s *\d");
                Match m = reg.Match(Name);

                return Name.Substring(0, m.Success ? m.Index : Name.Length);
            }
        }

        // public List<Opening> ChildOpenings { get; set; }

    }
}
