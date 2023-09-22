using Application.Repositories;
using Application.User.Queries.LoginUser;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserDto>
    {
        readonly IUserRepository _userRepository;
        readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<GetUserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            var userDto = new GetUserDto();

            var user = await _userRepository.GetByEmail(query.Email, cancellationToken);

            if (user != null)
            {
                userDto = _mapper.Map<GetUserDto>(user);
            }

            return userDto;
        }
    }
}
