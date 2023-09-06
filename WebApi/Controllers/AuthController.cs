using Microsoft.AspNetCore.Mvc;
using MediatR;
using Domain.Entities;
using System.Threading;
using Application.Pet.Queries.GetPets;
using Application.User.Queries.LoginUser;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<LoginUserDto>> Login(LoginUserQuery request, CancellationToken cancellationToken)
        {
             var response = await _mediator.Send(request, cancellationToken);

             if(string.IsNullOrEmpty(response.Token))
                return BadRequest("The attempted login has failed.");

            return Ok(response);
        }
    }
}
