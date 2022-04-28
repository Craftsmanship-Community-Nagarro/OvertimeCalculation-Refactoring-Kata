package codingdojo;

import org.approvaltests.Approvals;
import org.approvaltests.combinations.CombinationApprovals;
import org.approvaltests.reporters.JunitReporter;
import org.approvaltests.reporters.QuietReporter;
import org.approvaltests.reporters.UseReporter;
import org.approvaltests.reporters.linux.DiffMergeReporter;
import org.junit.jupiter.api.Test;
import org.lambda.functions.Function3;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.math.BigDecimal;
import java.time.Duration;
import java.time.temporal.ChronoUnit;
import java.time.temporal.TemporalUnit;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class CompensationCalculatorTest {
  @Test
  void shouldReturnValidOvertime() {
    BigDecimal hoursOvertimeTotal = BigDecimal.valueOf(1);
    Assignment assignment = new Assignment(true, Duration.of(1, ChronoUnit.HOURS));
    Briefing briefing = new Briefing(true, true, true, true);

    Overtime overtime = CompensationCalculator.calculateOvertime(hoursOvertimeTotal, assignment, briefing);

    Approvals.verify(overtime);
  }

  @Test
  void shouldReturnAllValidCombinationForOvertime() {
    Briefing briefing = new Briefing(true, true, true, true);

    BigDecimal[] possibleHoursOvertimeTotal = new BigDecimal[]{
        BigDecimal.valueOf(1),
        BigDecimal.valueOf(2),
        BigDecimal.valueOf(5)
    };
    Assignment[] possibleAssignments = new Assignment[]{
        new Assignment(true, Duration.of(1, ChronoUnit.HOURS)),
        new Assignment(false, Duration.of(1, ChronoUnit.HOURS))
    };
    Briefing[] possibleBriefings = new Briefing[]{
      new Briefing(true, true, true, true),
      new Briefing(true, true, true, false),
      new Briefing(true, true, false, false),
      new Briefing(true, false, false, false),
      new Briefing(false, false, false, false),
    };



    Function3<BigDecimal, Assignment, Briefing, Overtime> calculateOvertimeFunction =
        CompensationCalculator::calculateOvertime;
    CombinationApprovals.verifyAllCombinations(calculateOvertimeFunction, possibleHoursOvertimeTotal,
        possibleAssignments, possibleBriefings);
  }

//  Briefing[] generateCombinationsForBriefings() {
//    List<List<Boolean>> combinations = new ArrayList<>();
//
//    byte test = 0b0001;
////    byte test2 = test + 1;
//
//  }
}
