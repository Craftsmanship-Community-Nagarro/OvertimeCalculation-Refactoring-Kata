using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overtime
{
    public class Briefing
    {
        public Briefing(bool Watcode, bool Z3, bool Foreign, bool Hbmo)
        {
            this.Watcode = Watcode;
            this.Z3 = Z3;
            this.Foreign = Foreign;
            this.Hbmo = Hbmo;
        }

        public bool Watcode { get; set; }
        public bool Z3 { get; set; }
        public bool Foreign { get; set; }
        public bool Hbmo { get; set; }
    }
}
