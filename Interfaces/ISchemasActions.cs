namespace GraphQL_API_X_clone.Interfaces;

public interface ISchemasActions <T, Tinput> where T : class
{
    Task<ICollection<T>> GetAsync();
    Task<T> GetByIdAsync(string id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(string id,Tinput entity);
    Task DeleteAsync(string id);
}

