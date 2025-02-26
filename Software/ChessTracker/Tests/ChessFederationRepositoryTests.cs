using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// David Brckan

namespace Tests
{
    [TestClass]
    public class ChessFederationRepositoryTests
    {
        private ChessFederationRepository _repository;
        private ChessModel _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new ChessModel();
            _repository = new ChessFederationRepository();
        }

        [TestMethod]
        public void GetAllFederations_ReturnsFederationsList()
        {

            List<ChessFederation> federations = _repository.GetAllFederations();


            Assert.IsNotNull(federations, "Lista saveza ne smije biti null.");
            Assert.IsTrue(federations.Count >= 0, "Metoda bi trebala vratiti barem 0 elemenata.");
        }

        [TestMethod]
        public void GetAllFederations_ContainsExpectedData()
        {

            var federation = _context.ChessFederation.FirstOrDefault();
            if (federation == null)
            {
                Assert.Inconclusive("Nema saveza u bazi za testiranje.");
                return;
            }


            List<ChessFederation> federations = _repository.GetAllFederations();


            Assert.IsTrue(federations.Any(f => f.federation_id == federation.federation_id),
                          "Lista saveza bi trebala sadržavati barem jedan postojeći savez.");
        }
    }
}
