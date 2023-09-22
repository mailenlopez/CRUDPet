using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetUser
{
    public class GetUserQuery: IRequest<GetUserDto>
    {
        public string Email { get; set; }
    }
}
