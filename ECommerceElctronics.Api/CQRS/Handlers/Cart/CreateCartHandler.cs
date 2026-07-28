using AutoMapper;
using ECommerceElctronics.Api.CQRS.Handlers.CartFolder;
using ECommerceElctronics.Api.CQRS.Commands.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;
using ECommerceElctronics.Entities.Dtos.Requests;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceElctronics.Api.CQRS.Handlers.CartFolder
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, Cart>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCartHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Cart> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var Cartresult = request.CartRequest;

            if (Cartresult == null)
                return null;

            Cart cart = new()
            {
                UserId = Cartresult.UserId,
                Date = Cartresult.Date
            };

            await _unitOfWork.Carts.Add(cart);

            var result = await _unitOfWork.CompleteAsync();

            if (result)
            {
                foreach (var item in Cartresult.OrdersRequest)
                {
                    Order order = new()
                    {
                        ProductId = item.ProductId,
                        Quantitiy = item.Quantitiy,
                        UserId = cart.UserId,
                        CartId = cart.Id
                    };
                    await _unitOfWork.Orders.Add(order);
                }

                await _unitOfWork.CompleteAsync();
            }
            //TODO
            // 
            // Delete the cart added

            return cart;
        }
    }
}
