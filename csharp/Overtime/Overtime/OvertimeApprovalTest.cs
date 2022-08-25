using ApprovalTests;
using ApprovalTests.Combinations;
using ApprovalTests.Reporters;
using Overtime;
using System;
using Xunit;

namespace OvertimeTest
{
    [UseReporter(typeof(VisualStudioReporter))]
    public class OvertimeApprovalTest
    {
        [Fact]
        [UseReporter(typeof(CodeCompareReporter))]
        public void TestOvertimeCalculation()
        {
            int[] assignmentHours = { 0, 5, 6 };
            int[] overtimeTotalHours = { 0, 1, 10, 12, 17 };
            bool[] allcomb = { true, false };

            CombinationApprovals.VerifyAllCombinations(doCalculateOvertime, assignmentHours, overtimeTotalHours, allcomb, allcomb, allcomb, allcomb, allcomb);

        }

        private Overtime.OvertimeTime doCalculateOvertime(int assignmentHours, int overtimeTotalHours, bool isUnionized, bool watcode, bool z3, bool foreign, bool hbmo)
        {
            Assignment assignment = new Assignment(isUnionized, TimeSpan.FromHours(assignmentHours));
            Briefing briefing = new Briefing(watcode, z3, foreign, hbmo);
            Decimal overtimeTotal = Convert.ToDecimal(overtimeTotalHours);
            return CompensationCalculator.calculateOvertime(overtimeTotal, assignment, briefing);
        }

    }
}