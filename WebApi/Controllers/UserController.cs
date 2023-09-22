using Application.User.Commands.CreateUser;
using Application.User.Queries.GetUser;
using Application.User.Queries.LoginUser;
using Domain.Entities;
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
    public async Task<ActionResult<User>> Create(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (response != 1)
            throw new Exception("The user could not been created.");

        GetUserQuery getUserQuery = new GetUserQuery
        {
            Email = request.Email
        };

        var user = await _mediator.Send(getUserQuery, cancellationToken);
        return Ok(user);
    }
}