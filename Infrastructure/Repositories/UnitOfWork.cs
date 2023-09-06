using Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IServiceProvider _serviceProvider;
        public UnitOfWork(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
        }

        public IPetRepository PetRepository => _serviceProvider.GetService<IPetRepository>()!;

        public IUserRepository UserRepository => _serviceProvider.GetService<IUserRepository>()!;
        //TODO: Add Begin, BeginAsync, Commit,
    }
}
