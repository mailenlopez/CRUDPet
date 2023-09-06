using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.LoginUser
{
    public class LoginUserMapper : Profile
    {
        public LoginUserMapper()
        {
            CreateMap<LoginUserQuery, Domain.Entities.User>();
        }
    }
}
