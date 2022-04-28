using ApprovalTests;
using ApprovalTests.Combinations;
using ApprovalTests.Reporters;
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
            //TODO Implement Test
            //Single Verification           -> Approvals.Verify(...);
            //Combinated Verification       -> CombinationApprovals.VerifyAllCombinations(...);
        }
    }
}