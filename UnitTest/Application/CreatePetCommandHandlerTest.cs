using Application.Pet.Commands.CreatePet;
using Application.Repositories;
using Domain.Entities;
using MediatR;
using Moq;
using NUnit.Framework;
using WebApi.Controllers;
using Assert = NUnit.Framework.Assert;

namespace UnitTest.Application
{
    [TestFixture]
    public class CreatePetCommandHandlerTest
    {
        private CreatePetCommandHandler _handler;
        private Mock<IPetRepository> _petRepository;
        private Mock<CreatePetCommand> _createPetCommand;
        private CancellationToken _cancellationToken;

       
        public CreatePetCommandHandlerTest()
        {
            _cancellationToken = new CancellationToken();
            _petRepository = new Mock<IPetRepository>();
            _petRepository
                .Setup(c => c.AddAsync(It.IsAny<Pet>()))
                .ReturnsAsync(1);
            _handler = new CreatePetCommandHandler(_petRepository.Object);
        }

        [Fact]
        public async Task when_createPet_then_success()
        {
            //Arrange
            _createPetCommand = new Mock<CreatePetCommand>();

            _createPetCommand.Object.Name = "firu";
            _createPetCommand.Object.Description = "lorem ipsum";

            //Action
            var response = await _handler.Handle(_createPetCommand.Object, _cancellationToken);
           
            //Assert
            Assert.AreEqual(response, 1); 
            _petRepository.Verify(d => d.AddAsync(It.IsAny<Pet>()), Times.Exactly(1));       
        }
    }
}
