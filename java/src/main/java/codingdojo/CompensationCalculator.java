package codingdojo;

import java.math.BigDecimal;
import java.time.Duration;

public class CompensationCalculator {

  public final static BigDecimal MAX_OVERTIME_HOURS_RATE_1 = BigDecimal.TEN;
  public static final int THRESHOLD_OVERTIME_HOURS_RATE_2 = 6;

  public static Overtime calculateOvertime(BigDecimal hoursOvertimeTotal, Assignment assignment, Briefing briefing) {

    if (isOverTimeWithTotal(hoursOvertimeTotal, assignment, briefing)) {
      return new Overtime(hoursOvertimeTotal, BigDecimal.ZERO);
    }

    if (hoursOvertimeTotal.compareTo(BigDecimal.ZERO) < 1) {
      return new Overtime(BigDecimal.ZERO, BigDecimal.ZERO);
    }

    final BigDecimal hoursOvertimeRate2 = hoursOvertimeTotal.subtract(MAX_OVERTIME_HOURS_RATE_1);

    if (assignment.isUnionized()) {
      BigDecimal threshold = calculateThreshold(assignment);
      return new Overtime(MAX_OVERTIME_HOURS_RATE_1, hoursOvertimeRate2.min(threshold));
    }

    return new Overtime(MAX_OVERTIME_HOURS_RATE_1, hoursOvertimeRate2);
  }

  private static boolean isOverTimeWithTotal(BigDecimal hoursOvertimeTotal, Assignment assignment, Briefing briefing) {
    return isOvertime(assignment, briefing) || hoursOvertimeTotal.compareTo(MAX_OVERTIME_HOURS_RATE_1) < 1;
  }

  private static boolean isOvertime(Assignment assignment, Briefing briefing) {
    boolean isWatcodeUnion = briefing.watcode() && assignment.isUnionized();
    boolean isWatcodeNonUnionForeign = briefing.watcode() && !assignment.isUnionized() && briefing.foreign();
    return (!briefing.watcode() && !briefing.z3() && !assignment.isUnionized())
            || (briefing.hbmo() && assignment.isUnionized())
            || isWatcodeNonUnionForeign
            || isWatcodeUnion
            || (briefing.foreign() && !assignment.isUnionized());
  }

  private static BigDecimal calculateThreshold(Assignment assignment) {
    Duration remainder = assignment.duration().minusHours(CompensationCalculator.THRESHOLD_OVERTIME_HOURS_RATE_2);
    if (remainder.isNegative()) {
      return BigDecimal.valueOf(assignment.duration().toSeconds() / 3600);
    }
    return BigDecimal.valueOf(CompensationCalculator.THRESHOLD_OVERTIME_HOURS_RATE_2);
  }

}
