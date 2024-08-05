using ECommerceElctronics.Entities.Models;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.UserFolder
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}
