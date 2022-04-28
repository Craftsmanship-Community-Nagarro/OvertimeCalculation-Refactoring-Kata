using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overtime
{
    public class Assignment
    {
        public Assignment(bool isUnionized, TimeSpan duration)
        {
            this.isUnionized = isUnionized;
            this.duration = duration;
        }

        public bool isUnionized { get; set; }
        public TimeSpan duration { get; set; }


        public override string ToString()
        {
            return $"{nameof(Assignment)} -> isUnionized: {isUnionized}, duration: {duration}";
        }
    }
}
