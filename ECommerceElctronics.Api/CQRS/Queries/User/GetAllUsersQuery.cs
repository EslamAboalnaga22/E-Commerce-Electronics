using ECommerceElctronics.Entities.Dtos.Responses;
using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.UserFolder
{
    public class GetAllUsersQuery : IRequest<IEnumerable<GetUserDetailsResponse>>
    {
    }
}
