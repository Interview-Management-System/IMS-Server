﻿namespace InterviewManagementSystem.Infrastructure.Databases.MongoDB
{
    public sealed class MongoDbSetting
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}
