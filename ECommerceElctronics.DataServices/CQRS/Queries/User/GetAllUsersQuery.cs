using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.UserFolder
{
    public record GetAllUsersQuery : IRequest<Result<IEnumerable<GetUserDetailsResponse>>>;
}
