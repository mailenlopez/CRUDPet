using Application.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        protected IUserRepository _userRepository;
        protected readonly IUnitOfWork _unitOfWork;
        public CreateUserCommandHandler(IUserRepository userRepo, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.User();
            user.Name = request.Name;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Email = request.Email;

            return await _userRepository.AddAsync(user);
        }
    }
}
