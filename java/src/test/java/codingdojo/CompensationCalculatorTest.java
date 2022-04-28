package codingdojo;

import org.approvaltests.combinations.CombinationApprovals;
import org.junit.jupiter.api.Test;

import java.math.BigDecimal;
import java.time.Duration;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class CompensationCalculatorTest {


  @Test
  void testCalculateOvertime() {

    BigDecimal[] hoursOverTimeTotals = new BigDecimal[]{BigDecimal.ZERO, BigDecimal.ONE, new BigDecimal(-1), new BigDecimal(10), new BigDecimal(11)};
    Boolean[] booleans = new Boolean[]{Boolean.TRUE, Boolean.FALSE};
    Duration[] durations = new Duration[]{Duration.ofHours(1), Duration.ofHours(2), Duration.ofHours(8), Duration.ZERO, Duration.ofHours(-1)};

    CombinationApprovals.verifyAllCombinations(this::callComputeOvertime, hoursOverTimeTotals, booleans, durations, booleans, booleans, booleans, booleans);
  }

  private Overtime callComputeOvertime(BigDecimal hoursOverTimeTotal, boolean unionized, Duration duration, boolean watcode, boolean z3, boolean foreign, boolean hbmo) {
    Assignment assignment = new Assignment(unionized, duration);
    Briefing briefing = new Briefing(watcode, z3, foreign, hbmo);

    return CompensationCalculator.calculateOvertime(hoursOverTimeTotal, assignment, briefing);
  }

}
