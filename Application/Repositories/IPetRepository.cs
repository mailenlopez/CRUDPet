namespace Application.Repositories
{
    public interface IPetRepository : IBaseRepository<Domain.Entities.Pet>
    {
        Task<IEnumerable<Domain.Entities.Pet>> GetPetByNameAsync(string name, CancellationToken cancellationToken);
    }
}
