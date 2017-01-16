using Application.Data.Infrastructure;
using Application.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {

    }
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(IDbFactory dbFactory) :base (dbFactory)
        {
            
        }

        public Client FindClient(string clientId)
        {
            var client = DbContext.Clients.Find(clientId);
            return client;
        }
    }

    
}
