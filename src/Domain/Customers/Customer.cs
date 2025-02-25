using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Customers;

public sealed class Customer : AgregateRoot {
    public Customer()
    {
    }
    public Customer(CustomerId id, string name, string lastname, string email, PhoneNumber phoneNumber, Address address, bool active)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Lastname = lastname ?? throw new ArgumentNullException(nameof(lastname));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Active = active;
    }

    public CustomerId Id { get; private set;}

    public string Name {get; private set;} = string.Empty;

    public string Lastname {get; private set;} = string.Empty;    

    public string FullName => $"{Name} {Lastname}".Trim();

    public string Email {get; private set;} = string.Empty;

    public PhoneNumber PhoneNumber {get; private set;}

    public Address Address {get; private set;}

    public bool Active {get; set;}

    public static Customer UpdateCustomer(Guid id, string name, string lastname, string email, PhoneNumber phoneNumber, Address address, bool active) {
        return new Customer(new CustomerId(id), name, lastname, email, phoneNumber, address, active);
    }
}