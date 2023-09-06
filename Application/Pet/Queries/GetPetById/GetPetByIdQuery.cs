using MediatR;

namespace Application.Pet.Queries.GetPetById
{
    public sealed record GetPetByIdQuery(int id) : IRequest<GetPetByIdDto>;
}
