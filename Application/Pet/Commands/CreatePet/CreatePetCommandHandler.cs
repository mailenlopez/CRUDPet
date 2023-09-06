using Application.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pet.Commands.CreatePet
{
    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, int>
    {
        protected IPetRepository _petRepository;
        public CreatePetCommandHandler(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }
        public async Task<int> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = new Domain.Entities.Pet();
            pet.Name = request.Name;
            pet.Description = request.Description;
            pet.Lineage = request.Type;

            return await _petRepository.AddAsync(pet);
        }
    }
}
