using Application.Common.Exceptions;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pet.Commands.UpdatePet
{
    public class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand>
    {
        protected IPetRepository _petRepository;
        public UpdatePetCommandHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public async Task Handle(UpdatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = await _petRepository.GetByIdAsync(request.Id);

            if (pet == null)
                throw new NotFoundException();

            pet.Name = request.Name;
            pet.Description = request.Description;
            pet.Lineage = request.Type;
            
            await _petRepository.UpdateAsync(pet!);
        }
    }
}
