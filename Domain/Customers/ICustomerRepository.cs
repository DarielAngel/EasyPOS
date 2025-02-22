namespace Domain.Customers;

public interface ICustomerRepository {
    Task<Customer> GetByAsync(CustomerId id);

    Task Add(Customer customer);
}