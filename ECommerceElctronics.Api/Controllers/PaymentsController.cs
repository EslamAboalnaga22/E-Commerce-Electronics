
using ECommerceElctronics.DataServices.Services;
using ECommerceElctronics.Entities.Dtos.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ECommerceElctronics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IStripeServices _stripeServices;

        public PaymentsController(IStripeServices stripeServices)
        {
            _stripeServices = stripeServices;
        }

        [HttpPost("Customer")]
        public async Task<IActionResult> CraeteCustomerAsync(CreateCustomerBankAccountResource customer)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var response = await _stripeServices.CreateCustomer(customer);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }
        [HttpPost("Charge")]
        public async Task<IActionResult> CraeteChargeAsync(CreateChargeResource charge)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _stripeServices.CreateCharge(charge);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

    }
}
