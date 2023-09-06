using Application.Repositories;
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

namespace Application.User.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserDto>
    {
        readonly IUserRepository _userRepository;
        readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public LoginUserQueryHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<LoginUserDto> Handle(LoginUserQuery query, CancellationToken cancellationToken)
        {
            var userToken = new LoginUserDto();

            var user = _mapper.Map<Domain.Entities.User>(query);

            var existEmail = await _userRepository.GetByEmail(user.Email, cancellationToken);

            if(existEmail != null)
            {
                var isValidPassword = BCrypt.Net.BCrypt.Verify(query.Password, existEmail.PasswordHash);
                if (isValidPassword)
                {
                    userToken.Token = CreateToken(existEmail);
                }
            }

            return userToken;
        }

        private string CreateToken(Domain.Entities.User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                null,
                null,
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return jwt;
        }
    }
}
