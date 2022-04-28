using ApprovalTests;
using ApprovalTests.Combinations;
using ApprovalTests.Reporters;
using Overtime;
using System;
using System.Collections.Generic;
using Xunit;

namespace OvertimeTest
{
    [UseReporter(typeof(VisualStudioReporter))]
    public class OvertimeApprovalTest
    {
        [Fact]
        public void TestOvertimeCalculation()
        {
            var briefing = new Briefing(true, true, true, true);
            var assignment = new Assignment(true, TimeSpan.FromMinutes(1));

            var overtime = CompensationCalculator.calculateOvertime(1.0m, assignment, briefing);

            Approvals.Verify(overtime);
        }

        [Fact]
        public void TestOvertimeCalculationCombinations()
        {
            var hoursOvertimeTotals = new List<decimal>() { 1.0m, 3.5m, 6.8m };
            var briefings = new List<Briefing>() { 
                new Briefing(true, true, true, true), 
                new Briefing(false, true, true, true), 
                new Briefing(false, false, true, true) };
            var assignments = new List<Assignment>() {
                new Assignment(true, TimeSpan.FromMinutes(1)),
                new Assignment(true, TimeSpan.FromMinutes(2)),
                new Assignment(true, TimeSpan.FromMinutes(5)) };

            CombinationApprovals.VerifyAllCombinations(CompensationCalculator.calculateOvertime, hoursOvertimeTotals, assignments, briefings);
        }
    }
}