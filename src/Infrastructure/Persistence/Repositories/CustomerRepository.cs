using Domain.Customers;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persiistence.Repository;

public class CustomerRepository : ICustomerRepository
{

    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(Customer customer) => await _context.Customers.AddAsync(customer);

    public async Task Delete(Customer id)
    {
        _context.Customers.Remove(id);

        await Task.CompletedTask;
    }

    public async Task<bool> ExistAsync(CustomerId id)
    {
        return await _context.Customers.AnyAsync(c => c.Id == id);
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(CustomerId id)
    {
        return await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task Update(Customer id)
    {
        _context.Customers.Update(id);

        await Task.CompletedTask;
    }

}