using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Commands.UserFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.UserFolder
{
    public class CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateUserCommand, Result<User>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = request.UserRequest;


            if (result == null)
                return Result<User>.Failure(new("Something Wrong In Data", "Created"));

            await _unitOfWork.Users.Add(result);
            await _unitOfWork.CompleteAsync();

            return Result<User>.Success(result);
        }
    }
}
