using Application.User.Queries.LoginUser;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetUser
{
    public class GetUserMapper : Profile
    {
        public GetUserMapper()
        {
            CreateMap<Domain.Entities.User, GetUserDto>();
        }
    }
}
