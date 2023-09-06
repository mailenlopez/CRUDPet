using Application.Common.Exceptions;
using Application.Pet.Commands.UpdatePet;
using Application.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using WebApi.Controllers;
using Assert = NUnit.Framework.Assert;

namespace UnitTest.Application
{
    [TestFixture]
    public class UpdatePetCommandHandlerTest
    {
        private UpdatePetCommandHandler _handler;
        private Mock<IPetRepository> _petRepository;
        private Mock<UpdatePetCommand> _updatePetCommand;
        private CancellationToken _cancellationToken;


        public UpdatePetCommandHandlerTest()
        {
            _cancellationToken = new CancellationToken();
            _petRepository = new Mock<IPetRepository>();
            _handler = new UpdatePetCommandHandler(_petRepository.Object);
        }

        [Fact]
        public async Task when_updatePet_then_success()
        {
            //Arrange
            _updatePetCommand = new Mock<UpdatePetCommand>();
            _petRepository
                .Setup(c => c.GetByIdAsync(1))
                .ReturnsAsync(new Pet());

            _updatePetCommand.Object.Id = 1;
            _updatePetCommand.Object.Name = "firu";
            _updatePetCommand.Object.Description = "lorem ipsum";

            //Action
            await _handler.Handle(_updatePetCommand.Object, _cancellationToken);

            //Assert
            _petRepository.Verify(d => d.UpdateAsync(It.IsAny<Pet>()), Times.Exactly(1));
        }

        [Fact]
        [ExpectedException(typeof (NotFoundException))]
        public async Task when_updatePet_then_fail()
        {
            //Arrange
            _updatePetCommand = new Mock<UpdatePetCommand>();
            _petRepository
                .Setup(c => c.GetByIdAsync(1))
                .ReturnsAsync((Pet?)null);

            _updatePetCommand.Object.Id = 1;

            //Assert
            var x = Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_updatePetCommand.Object, _cancellationToken));
            _petRepository.Verify(d => d.AddAsync(It.IsAny<Pet>()), Times.Exactly(0));
        }
    }
}
