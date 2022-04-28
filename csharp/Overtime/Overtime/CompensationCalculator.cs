using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overtime
{
    public class CompensationCalculator
    {
        public static Decimal MAX_OVERTIME_HOURS_RATE_1 = 10;
        public static int THRESHOLD_OVERTIME_HOURS_RATE_2 = 6;

        public static Overtime calculateOvertime(decimal hoursOvertimeTotal, Assignment assignment, Briefing briefing)
        {
            decimal hoursOvertimeRate1 = 0;
            decimal hoursOvertimeRate2 = 0;

            bool isWatcodeUnion = briefing.Watcode && assignment.isUnionized;
            bool isWatcodeNonUnionForeign = briefing.Watcode && !assignment.isUnionized && briefing.Foreign;

            if (
                    (!briefing.Watcode && !briefing.Z3 && !assignment.isUnionized)
                            || (briefing.Hbmo && assignment.isUnionized)
                            || isWatcodeNonUnionForeign
                            || isWatcodeUnion
                            || (briefing.Foreign && !assignment.isUnionized)
            )
            {
                hoursOvertimeRate1 = hoursOvertimeTotal;
            }
            else
            {
                if (Decimal.Compare(hoursOvertimeTotal, 0) < 1)
                {
                    return new Overtime(hoursOvertimeRate1, hoursOvertimeRate2);
                }
                else if (Decimal.Compare(hoursOvertimeTotal, MAX_OVERTIME_HOURS_RATE_1) < 1)
                {
                    hoursOvertimeRate1 = hoursOvertimeTotal;
                }
                else
                {
                    hoursOvertimeRate1 = MAX_OVERTIME_HOURS_RATE_1;
                    hoursOvertimeRate2 = Decimal.Subtract(hoursOvertimeTotal,MAX_OVERTIME_HOURS_RATE_1);
                    if (assignment.isUnionized)
                    {
                        decimal threshold = calculateThreshold(assignment, THRESHOLD_OVERTIME_HOURS_RATE_2);
                        hoursOvertimeRate2 = hoursOvertimeRate2 > threshold ? threshold : hoursOvertimeRate2;
                    }
                }
            }

            return new Overtime(hoursOvertimeRate1, hoursOvertimeRate2);
        }

        private static decimal calculateThreshold(Assignment assignment, long threshold)
        {
            TimeSpan remainder = assignment.duration.Subtract(TimeSpan.FromHours(threshold));
            if (remainder.TotalHours > 0)
            {
                return Convert.ToDecimal(assignment.duration.TotalSeconds / 3600 );
            }
            return Convert.ToDecimal(threshold);
        }
    }
}
