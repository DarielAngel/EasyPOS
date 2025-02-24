using ErrorOr;

namespace Domain.DomainErrors;

public static partial class Errors {
    public static class Customer {
        public static Error PhoneNumberWithBadFormat =>
            Error.Validation("Customer.PhoneNumber", "Phone number has not valid format.");

        public static Error AddressWithBadFormat =>
            Error.Validation("Customer.Address", "Address is not valid.");

        public static Error CustomerByIdNotFound => 
            Error.Validation("Customer.NotFound", "The customer with the provide Id has not found.");

        public static Error CustomerHasNotUpdate(Error e) => Error.Validation(e.Code, e.Description);

        public static Error CustomerWithBadFormat => 
            Error.Validation("Customer.BadFormat", "Customer has not valid format.");

    }
}