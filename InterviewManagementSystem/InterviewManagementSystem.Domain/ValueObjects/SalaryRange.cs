namespace InterviewManagementSystem.Domain.ValueObjects
{
    public sealed record SalaryRange
    {
        public decimal From { get; private set; }
        public decimal To { get; private set; }


        public SalaryRange(decimal from, decimal to)
        {
            From = from;
            To = to;
        }

    }
}
