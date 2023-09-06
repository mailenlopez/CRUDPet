using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Pet.Queries.GetPets
{
    public class GetPetsQueryHandler : IRequestHandler<GetPetsQuery, IEnumerable<GetPetsDto>>
    {
        readonly IPetRepository _petRepository;
        readonly IMapper _mapper;
        public GetPetsQueryHandler(IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetPetsDto>> Handle(GetPetsQuery request, CancellationToken cancellationToken)
        {
            var pets = await _petRepository.GetAllAsync();
            return _mapper.Map<List<GetPetsDto>>(pets);
        }
    }
}
