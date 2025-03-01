using System.Linq.Expressions;

namespace InterviewManagementSystem.Domain.Interfaces
{
    public interface IMongoRepository<TDocument> where TDocument : class
    {
        Task<TDocument> GetByIdAsync(string id);
        Task<IEnumerable<TDocument>> GetAllAsync();
        Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> filter);
        Task InsertOneAsync(TDocument document);
        Task ReplaceOneAsync(string id, TDocument document);
        Task DeleteOneAsync(string id);
    }
}
