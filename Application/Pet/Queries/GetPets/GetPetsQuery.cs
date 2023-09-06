using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pet.Queries.GetPets
{
    public record GetPetsQuery : IRequest<IEnumerable<GetPetsDto>>
    {
    }
}
