using AutoMapper;
using ECommerceElctronics.Api.CQRS.Queries.UserFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Handlers.UserFolder
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserQuery, GetUserDetailsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetUserDetailsResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetById(request.UserIdRequest);

            if (user == null)
                return null;

            var result = _mapper.Map<GetUserDetailsResponse>(user);

            return result;
        }
    }
}
