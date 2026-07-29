using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.UserFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.UserFolder
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<GetUserDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetUserDetailsResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Users.GetAll();

            var result = _mapper.Map<IEnumerable<GetUserDetailsResponse>>(users);

            return result;
        }
    }
}
