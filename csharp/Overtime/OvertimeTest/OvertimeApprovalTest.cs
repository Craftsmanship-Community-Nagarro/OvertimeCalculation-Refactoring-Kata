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
        public void TestOvertimeCalculation()
        {
            var briefing = new Briefing(true, true, true, true);
            var assignment = new Assignment(true, TimeSpan.FromMinutes(1));

            var overtime = CompensationCalculator.calculateOvertime(1.0m, assignment, briefing);

            Approvals.Verify(overtime);

            //TODO Implement Test
            //Single Verification           -> Approvals.Verify(...);
            //Combinated Verification       -> CombinationApprovals.VerifyAllCombinations(...);
        }
    }
}