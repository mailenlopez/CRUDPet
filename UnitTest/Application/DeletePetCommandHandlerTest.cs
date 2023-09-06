using Application.Pet.Commands.DeletePet;
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
    public class DeletePetCommandHandlerTest
    {
        private DeletePetCommandHandler _handler;
        private Mock<IPetRepository> _petRepository;
        private Mock<DeletePetCommand> _deletePetCommand;
        private CancellationToken _cancellationToken;


        public DeletePetCommandHandlerTest()
        {
            _cancellationToken = new CancellationToken();
            _petRepository = new Mock<IPetRepository>();
            
            _petRepository
                .Setup(c => c.DeleteAsync(1));
            _handler = new DeletePetCommandHandler(_petRepository.Object);
        }

        [Fact]
        public async Task when_deletePet_then_success()
        {
            //Arrange
            _petRepository
                .Setup(c => c.GetByIdAsync(1))
                .ReturnsAsync(new Pet());
            _deletePetCommand = new Mock<DeletePetCommand>(1);

            //Action
            var response = await _handler.Handle(_deletePetCommand.Object, _cancellationToken);

            //Assert
            Assert.IsTrue(response.IsDeleted);
            _petRepository.Verify(d => d.DeleteAsync(1), Times.Exactly(1));
        }

        [Fact]
        public async Task when_deletePet_then_fail()
        {
            //Arrange
            _petRepository
                .Setup(c => c.GetByIdAsync(1))
                .ReturnsAsync((Pet)null);
            _deletePetCommand = new Mock<DeletePetCommand>(1);

            //Action
            var response = await _handler.Handle(_deletePetCommand.Object, _cancellationToken);

            //Assert
            Assert.IsFalse(response.IsDeleted);
            _petRepository.Verify(d => d.DeleteAsync(1), Times.Exactly(0));
        }
    }
}
