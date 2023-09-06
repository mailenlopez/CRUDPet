using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pet.Queries.GetPetById
{
    public class GetPetByIdMapper : Profile
    {
        public GetPetByIdMapper()
        {
            CreateMap<Domain.Entities.Pet, GetPetByIdDto>();
        }
    }
}
