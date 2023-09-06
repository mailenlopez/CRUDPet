using Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<LoginUserDto>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
