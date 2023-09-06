using Application.Pet.Queries.GetPets;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Pet.Queries.GetPetById
{
    public class GetPetByIdHandler : IRequestHandler<GetPetByIdQuery, GetPetByIdDto>
    {
        readonly IMapper _mapper;
        readonly IPetRepository _petRepository;

        public GetPetByIdHandler(IPetRepository petRepository, IMapper mapper)
        {
            _mapper = mapper;
            _petRepository = petRepository;
        }

        public async Task<GetPetByIdDto> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
        {
            var pet = await _petRepository.GetByIdAsync(request.id);
            return _mapper.Map<GetPetByIdDto>(pet);
        }
    }
}
