using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Requests;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.CartFolder
{
    public class AddItemToCartHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddItemToCartCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetById(request.AddItemCart.ProductId);

            if (product == null)
                throw new Exception("Product Not Found");

            var cart = await _unitOfWork.Carts.GetCartByUserId(request.AddItemCart.UserId);

            if (cart == null)
            {
                cart = new()
                {
                    UserId = request.AddItemCart.UserId,
                };

                await _unitOfWork.Carts.Add(cart);
                await _unitOfWork.CompleteAsync();
            }

            var item = cart.Items.FirstOrDefault(x => x.ProductId == request.AddItemCart.ProductId);

            if (item == null)
            {
                cart.Items.Add(new()
                {
                    ProductId = request.AddItemCart.ProductId,
                    Quantity = request.AddItemCart.Quentity,
                    UnitPrice = product.Price
                });
                await _unitOfWork.CompleteAsync();
            }
            else
            {
                item.Quantity += request.AddItemCart.Quentity;
                await _unitOfWork.CompleteAsync();
            }

            return true;
        }
    }
}
