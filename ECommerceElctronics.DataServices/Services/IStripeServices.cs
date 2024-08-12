using ECommerceElctronics.Entities.Dtos.Payment;

namespace ECommerceElctronics.DataServices.Services
{
    public interface IStripeServices
    {
        Task<CustomerBankAccountResource> CreateCustomer(CreateCustomerBankAccountResource resource);
        Task<ChargeResource> CreateCharge(CreateChargeResource resource);
    }
}
