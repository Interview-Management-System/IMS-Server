namespace InterviewManagementSystem.Application.Shared.Helpers
{
    public static class GenderHelper
    {

        private static readonly string MaleText = "Male";
        private static readonly string FemaleText = "Female";


        public static string GetGenderText(bool isMale)
        {
            return (isMale ? MaleText : FemaleText).Trim();
        }
    }
}
