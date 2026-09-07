namespace backend.Models
{

    public enum BudgetPeriodType
    {
        Weekly,
        Monthly,
        Yearly,
        Custom
    }
    public class Budget
    {
        public int Id { get; set; }
        public decimal Limit { get; set; }
        public BudgetPeriodType PeriodType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

    }

    public static class BudgetPeriodCalculator
    {
        public static (DateTime PeriodStart, DateTime PeriodEnd) GetCurrentPeriod(Budget budget, DateTime now)
        {
            return budget.PeriodType switch
            {
                BudgetPeriodType.Monthly => GetCurrentMonthlyPeriod(budget.StartDate, now),
                BudgetPeriodType.Yearly => GetCurrentYearlyPeriod(budget.StartDate, now),
                BudgetPeriodType.Custom => (budget.StartDate, budget.EndDate ?? now),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static (DateTime, DateTime) GetCurrentMonthlyPeriod(DateTime startDate, DateTime now)
        {
            int monthsElapsed = ((now.Year - startDate.Year) * 12) + now.Month - startDate.Month;
            if (now.Day < startDate.Day) monthsElapsed--;

            DateTime periodStart = startDate.AddMonths(monthsElapsed);
            DateTime periodEnd = periodStart.AddMonths(1).AddTicks(-1);

            return (periodStart, periodEnd);
        }

        private static (DateTime, DateTime) GetCurrentYearlyPeriod(DateTime startDate, DateTime now)
        {
            int yearsElapsed = now.Year - startDate.Year;
            if (now < startDate.AddYears(yearsElapsed)) yearsElapsed--;

            DateTime periodStart = startDate.AddYears(yearsElapsed);
            DateTime periodEnd = periodStart.AddYears(1).AddTicks(-1);

            return (periodStart, periodEnd);
        }
    }
}
