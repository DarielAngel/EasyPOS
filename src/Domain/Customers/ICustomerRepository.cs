namespace Domain.Customers;

public interface ICustomerRepository {
    Task<Customer?> GetByIdAsync(CustomerId id);
    Task<bool> ExistAsync(CustomerId id);
    Task<List<Customer>> GetAllAsync();
    Task Delete(Customer id);
    Task Update(Customer id);
    Task Add(Customer customer);

}