public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Product> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task AddAsync(Product product)
    {
        return _repository.AddAsync(product);
    }

    public Task UpdateAsync(Product product)
    {
        return _repository.UpdateAsync(product);
    }

    public Task DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }
}