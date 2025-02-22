namespace InterviewManagementSystem.Infrastructure.Databases.Cloudinary
{
    public sealed class CloudinarySetting
    {
        public string? CloudName { get; set; } = string.Empty;
        public string? FolderName { get; set; } = string.Empty;
        public string? ApiKey { get; set; } = string.Empty;
        public string? ApiSecret { get; set; } = string.Empty;
    }
}
