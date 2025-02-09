namespace InterviewManagementSystem.Application.Shared.Helpers
{
    public static class UserStatusHelper
    {
        private static readonly string ActiveText = "Active";
        private static readonly string InActiveText = "In-Active";


        public static string GetUserStatusText(bool? isActive)
        {
            return (isActive.GetValueOrDefault() ? ActiveText : InActiveText).Trim();
        }
    }
}
