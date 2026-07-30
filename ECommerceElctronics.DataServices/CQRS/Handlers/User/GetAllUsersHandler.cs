using AutoMapper;
using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.DataServices.CQRS.Queries.UserFolder;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Handlers.UserFolder
{
    public class GetAllUsersHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<GetUserDetailsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IEnumerable<GetUserDetailsResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Users.GetAll();

            if (users == null)
                return Result<IEnumerable<GetUserDetailsResponse>>.Failure(new("No Users Found", "Readed"));
           
            var result = _mapper.Map<IEnumerable<GetUserDetailsResponse>>(users);

            return Result<IEnumerable<GetUserDetailsResponse>>.Success(result);
        }
    }
}
