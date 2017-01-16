using Application.Data.Infrastructure;
using Application.Data.Repositories;
using Application.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public interface IClientService
    {
        Client Add(Client client);

        void Save();
    }
    public class ClientService : IClientService
    {
        private IClientRepository _clientRepository;
        private IUnitOfWork _unitOfWork;

        public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            this._clientRepository = clientRepository;
            this._unitOfWork = unitOfWork;
        }

        public Client Add(Client client)
        {
            var clientResult = _clientRepository.Add(client);
            return clientResult;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
