using Application.User.Commands.CreateUser;
using Application.User.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (response != 1)
            throw new Exception("The user could not been created.");

        LoginUserQuery loginUserRequest = new LoginUserQuery
        {
            Email = request.Email,
            Password = request.Password
        };

        var user = await _mediator.Send("")
        return Ok(response);
    }
}