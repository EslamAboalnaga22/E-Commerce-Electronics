using AutoMapper;
using ECommerceElctronics.DataServices.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceElctronics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
        protected readonly IMapper _mapper = mapper;
        protected readonly IMediator _mediator = mediator;
    }
}
