using ApprovalTests;
using ApprovalTests.Combinations;
using ApprovalTests.Reporters;
using Overtime;
using System;
using System.Collections.Generic;
using Xunit;
using System.Text.Json;
using Moq;

namespace OvertimeTest;

[UseReporter(typeof(VisualStudioReporter))]
public class OvertimeApprovalTest
{
    [Fact]
    public void TestOvertimeCalculation()
    {
        var briefing = new Briefing(true, true, true, true);
        var assignment = new Assignment(true, TimeSpan.FromMinutes(1));

        var overtime = CompensationCalculator.calculateOvertime(1.0m, assignment, briefing);
        Approvals.Verify(Formatter.Format(overtime));
    }

    /// <remarks>
    /// Note that the <see cref="Func{T, TResult}"/> resultFormatter parameter 
    /// of the <see cref="CombinationApprovals.VerifyAllCombinations"/> method
    /// formats only the result, but not the input. 
    /// Use inheritance to format the input by overriding the <see cref="object.ToString"/> method.
    /// </remarks>
    [Fact]
    public void TestOvertimeCalculationCombinations()
    {
        var hoursOvertimeTotals = new List<decimal>() { 1.0m, 3.5m, 6.8m };
        var briefings = new List<Briefing>() {
            new BriefingExtension(true, true, true, true),
            new BriefingExtension(false, true, true, true),
            new BriefingExtension(false, false, true, true) };
        var assignments = new List<Assignment>() {
            new AssignmentExtension(true, TimeSpan.FromMinutes(1)),
            new AssignmentExtension(true, TimeSpan.FromMinutes(2)),
            new AssignmentExtension(true, TimeSpan.FromMinutes(5)) };

        CombinationApprovals.VerifyAllCombinations(CompensationCalculator.calculateOvertime, Formatter.Format, hoursOvertimeTotals, assignments, briefings);
    }

    /// <remarks>
    /// Note that the <see cref="Func{T, TResult}"/> resultFormatter parameter 
    /// of the <see cref="CombinationApprovals.VerifyAllCombinations"/> method
    /// formats only the result, but not the input. 
    /// Use mocks to format the input by setting up the <see cref="object.ToString"/> method.
    /// </remarks>
    [Fact]
    public void TestOvertimeCalculationCombinationsWithMocks()
    {
        var hoursOvertimeTotals = new List<decimal>() { 1.0m, 3.5m, 6.8m };
        var briefings = new List<Briefing>() {
            GetMocked<Briefing>(true, true, true, true),
            GetMocked<Briefing>(false, true, true, true),
            GetMocked<Briefing>(false, false, true, true) };
        var assignments = new List<Assignment>() {
            GetMocked<Assignment>(true, TimeSpan.FromMinutes(1)),
            GetMocked<Assignment>(true, TimeSpan.FromMinutes(2)),
            GetMocked<Assignment>(true, TimeSpan.FromMinutes(5)) };

        CombinationApprovals.VerifyAllCombinations(CompensationCalculator.calculateOvertime, Formatter.Format, hoursOvertimeTotals, assignments, briefings);
    }

    private static Mock<T> GetMock<T>(params object[] args) where T : class
    {
        var mock = new Mock<T>(args);
        mock.Setup(x => x.ToString()).Returns(Formatter.Format(mock.Object));
        return mock;
    }

    private static T GetMocked<T>(params object[] args) where T : class => GetMock<T>(args).Object;
}

/// <summary>
/// Formatter for approval tests for <see cref=" CompensationCalculator.calculateOvertime"/>
/// </summary>
internal static class Formatter
{
    internal static string Format(object arg) => arg switch
    {
        null => "",
        Assignment a => $"{nameof(Assignment)} -> {JsonSerializer.Serialize(a)}",
        Briefing b => $"{nameof(Briefing)} -> {JsonSerializer.Serialize(b)}",
        Overtime.Overtime o => $"{nameof(Overtime.Overtime)} -> {JsonSerializer.Serialize(o)}",
        _ => $"{arg.GetType().Name} -> {JsonSerializer.Serialize(arg)}"
    };
}

/// <summary>
/// Extends the <see cref="Assignment"/> to override the public <see cref="ToString"/> method
/// </summary>
internal class AssignmentExtension : Assignment
{
    internal AssignmentExtension(bool isUnionized, TimeSpan duration) : base(isUnionized, duration)
    {
    }

    public override string ToString() => Formatter.Format(this);
}

/// <summary>
/// Extends the <see cref="Briefing"/> to override the public <see cref="ToString"/> method
/// </summary>
internal class BriefingExtension : Briefing
{
    internal BriefingExtension(bool Watcode, bool Z3, bool Foreign, bool Hbmo) : base(Watcode, Z3, Foreign, Hbmo)
    {
    }

    public override string ToString() => Formatter.Format(this);
}