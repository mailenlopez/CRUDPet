using Application.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        protected IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }
        public async Task<int> Handle(CreateUserCommand request, 
            CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByEmail(request.Email, cancellationToken) != null)
                throw new ArgumentException(
                    "There is already an account asociated to this email.");

            var user = new Domain.Entities.User
            {
                Name = request.Name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Email = request.Email
            };

            return await _userRepository.AddAsync(user);
        }
    }
}
