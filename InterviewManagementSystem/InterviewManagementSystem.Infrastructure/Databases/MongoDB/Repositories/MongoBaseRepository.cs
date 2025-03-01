using InterviewManagementSystem.Domain.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace InterviewManagementSystem.Infrastructure.Databases.MongoDB.Repositories;




public class UserRepo : MongoBaseRepository<User>
{
    public UserRepo(IMongoDatabase database) : base(database, "Col")
    {
    }

    public override Task<User> GetByIdAsync(string id)
    {
        return base.GetByIdAsync(id);
    }
}




public class MongoBaseRepository<TDocument> : IMongoRepository<TDocument> where TDocument : class
{

    private readonly IMongoCollection<TDocument> _collection;

    public MongoBaseRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<TDocument>(collectionName);
    }

    public virtual async Task<TDocument> GetByIdAsync(string id)
    {
        return await _collection.Find(Builders<TDocument>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TDocument>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> filter)
    {
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task InsertOneAsync(TDocument document)
    {
        await _collection.InsertOneAsync(document);
    }

    public async Task ReplaceOneAsync(string id, TDocument document)
    {
        await _collection.ReplaceOneAsync(Builders<TDocument>.Filter.Eq("_id", id), document);
    }

    public async Task DeleteOneAsync(string id)
    {
        await _collection.DeleteOneAsync(Builders<TDocument>.Filter.Eq("_id", id));
    }
}
