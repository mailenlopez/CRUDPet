using MediatR;

namespace Application.Pet.Commands.DeletePet
{
    public record DeletePetCommand(int Id) : IRequest<DeletePetCommandResponse>;
}
