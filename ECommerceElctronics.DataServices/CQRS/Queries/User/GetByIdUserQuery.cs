using ECommerceElctronics.DataServices.ResultPattern;
using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.DataServices.CQRS.Queries.UserFolder
{
    public record GetByIdUserQuery(int UserIdRequest) : IRequest<Result<GetUserDetailsResponse>>;
}
