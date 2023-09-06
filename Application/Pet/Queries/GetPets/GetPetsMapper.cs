using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pet.Queries.GetPets
{
    public class GetPetsMapper : Profile
    {
        public GetPetsMapper() 
        {
            CreateMap<GetPetsDto, Domain.Entities.Pet>();
            CreateMap<Domain.Entities.Pet, GetPetsDto>();
        }
    }
}
