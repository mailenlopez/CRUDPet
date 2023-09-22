using Application.Pet.Commands.CreatePet;
using Application.Repositories;
using Application.User.Commands.CreateUser;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace UnitTest.Application
{
    [TestFixture]
    public class CreateUserCommandHandlerTest
    {
        [Test]
        public async Task AddAsync_WithValidUser_ShouldCreateAnUser()
        {
            //Arrange

            var _fakeUserRepo = new Mock<IUserRepository>();

            _fakeUserRepo.Setup(x => x.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(1);

            CreateUserCommand userCommand = new CreateUserCommand
            {
                Name = "Mailen",
                Email = "mailen@m.com",
                Password = "test123"
            };

            CancellationToken _cancellationToken = new CancellationToken();
            var sut = new CreateUserCommandHandler(_fakeUserRepo.Object);

            //Act
            var result = await sut.Handle(userCommand, _cancellationToken);

            //Assert
            Assert.AreEqual(result, 1);
            _fakeUserRepo.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);

        }

    }
}
