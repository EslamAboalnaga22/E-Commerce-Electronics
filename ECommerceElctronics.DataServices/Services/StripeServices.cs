using ECommerceElctronics.Entities.Dtos.Payment;
using Stripe;

namespace ECommerceElctronics.DataServices.Services
{
    public class StripeServices : IStripeServices
    {
        private readonly TokenService _tokenService;
        private readonly CustomerService _customerService;
        private readonly ChargeService _chargeService;

        public StripeServices(TokenService tokenService, CustomerService customerService, ChargeService chargeService)
        {
            _tokenService = tokenService;
            _customerService = customerService;
            _chargeService = chargeService;
        }

        public async Task<CustomerBankAccountResource> CreateCustomer(CreateCustomerBankAccountResource resource)
        {
            var options = new TokenCreateOptions
            {
                BankAccount = new TokenBankAccountOptions
                {
                    Country = "US",
                    Currency = "usd",
                    AccountHolderName = "Jenny Rosen",
                    AccountHolderType = "individual",
                    RoutingNumber = "110000000",
                    AccountNumber = "000123456789",
                },
            };

            var token = await _tokenService.CreateAsync(options);

            CustomerCreateOptions customerOpions = new()
            {
                Email = resource.Email,
                Name = resource.Name,   
                Phone =  resource.Phone,
                Source = token.Id
            };

            var customer = await _customerService.CreateAsync(customerOpions);

            return new CustomerBankAccountResource { CustomerID = customer.Id, Email = customer.Email, Name = customer.Name, Address = customer.Phone }; 
        }

        public async Task<ChargeResource> CreateCharge(CreateChargeResource resource)
        {
            ChargeCreateOptions chargeOptions = new()
            {
                Currency = resource.Currency,
                Amount = resource.Amount,
                ReceiptEmail = resource.ReceiptEmail,
                Customer = resource.CustomerId,
                Description = resource.Description,
            };

            var charge = await _chargeService.CreateAsync(chargeOptions);

            return new ChargeResource {ChargeId = charge.Id, Currency = charge.Currency,Amount = charge.Amount, CustomerId = charge.CustomerId, ReceiptEmail = charge.ReceiptEmail, Description = charge.Description};
        }
    }
}
