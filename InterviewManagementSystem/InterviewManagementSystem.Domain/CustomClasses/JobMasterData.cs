namespace InterviewManagementSystem.Domain.CustomClasses
{
    public struct JobMasterData
    {
        public decimal To { get; set; }
        public decimal From { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public short[] LevelIdList { get; set; }
        public short[] SkillIdList { get; set; }
        public short[] BenefitIdList { get; set; }

    }
}
