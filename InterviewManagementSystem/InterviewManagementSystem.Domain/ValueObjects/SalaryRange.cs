using InterviewManagementSystem.Domain.Exceptions;

namespace InterviewManagementSystem.Domain.ValueObjects
{
    public sealed record SalaryRange
    {
        public decimal From { get; private set; }

        public decimal To { get; private set; }



        public SalaryRange()
        {

        }


        private SalaryRange(decimal from, decimal to)
        {
            From = from;
            To = to;
        }


        public static SalaryRange Create(decimal from, decimal to)
        {
            //CheckValidSalaryRange(from, to);
            return new SalaryRange(from, to);
        }



        private static void CheckValidSalaryRange(decimal from, decimal to)
        {
            if (from <= 0)
                throw new InvalidSalaryRange("Salary 'From' must be greater than 0.");

            if (to <= 0)
                throw new InvalidSalaryRange("Salary 'To' must be greater than 0.");

            if (from >= to)
                throw new InvalidSalaryRange("Minimum salary must be less than maximum salary.");

            if (from > decimal.MaxValue || to > decimal.MaxValue)
                throw new InvalidSalaryRange("Salaries must not exceed the maximum decimal value.");
        }
    }
}
