using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUserRepository : IBaseRepository<Domain.Entities.User>
    {
        Task<Domain.Entities.User> GetByEmail(string email, CancellationToken cancellationToken);
    }
}
