﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InterviewManagementSystem.Infrastructure.Databases.MongoDB.Repositories
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
