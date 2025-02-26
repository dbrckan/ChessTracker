using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ChessFederationService
    {
        private readonly ChessFederationRepository _federationRepository;

        public ChessFederationService()
        {
            _federationRepository = new ChessFederationRepository();
        }

        public List<ChessFederation> GetAllFederations()
        {
            return _federationRepository.GetAllFederations();
        }
    }
}
