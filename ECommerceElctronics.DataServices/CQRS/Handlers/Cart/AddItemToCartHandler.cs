using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.CartFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.CartFolder
{
    public class AddItemToCartHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddItemToCartCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<bool>> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetById(request.AddItemCart.ProductId);

            if (product == null)
                return Result<bool>.Failure(new("Product Not Found", "Readed"));

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

            return Result<bool>.Success(true);
        }
    }
}
