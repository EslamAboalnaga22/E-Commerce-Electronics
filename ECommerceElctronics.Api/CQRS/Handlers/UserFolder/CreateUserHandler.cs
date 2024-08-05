using AutoMapper;
using ECommerceElctronics.Api.CQRS.Commands.UserFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.UserFolder
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = request.UserRequest;

            await _unitOfWork.Users.Add(result);
            await _unitOfWork.CompleteAsync();

            if (result == null)
                return null;

            return result;
        }
    }
}
