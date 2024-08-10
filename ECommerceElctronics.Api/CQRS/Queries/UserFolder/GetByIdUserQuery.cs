using ECommerceElctronics.Entities.Dtos.Responses;
using MediatR;

namespace ECommerceElctronics.Api.CQRS.Queries.UserFolder
{
    public class GetByIdUserQuery : IRequest<GetUserDetailsResponse>
    {
        public int UserIdRequest { get; set; }

        public GetByIdUserQuery(int userIdRequest)
        {
            UserIdRequest = userIdRequest;
        }
    }
}
