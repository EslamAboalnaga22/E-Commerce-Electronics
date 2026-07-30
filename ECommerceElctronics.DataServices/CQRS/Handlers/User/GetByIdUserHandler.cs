using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.UserFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.UserFolder
{
    public class GetByIdUserHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByIdUserQuery, Result<GetUserDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<GetUserDetailsResponse>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetById(request.UserIdRequest);

            if (user == null)
                return Result<GetUserDetailsResponse>.Failure(new("No User Found", "Readed"));

            var result = _mapper.Map<GetUserDetailsResponse>(user);

            return Result<GetUserDetailsResponse>.Success(result);
        }
    }
}
