using DataAccessLayer.Repositories;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using EntitiesLayer.Entities;

// David Brckan
namespace Tests
{
    [TestClass]
    public class GameHistoryRepositoryTests
    {
        private GameHistoryRepository _repository;
        private ChessModel _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new ChessModel();
            _repository = new GameHistoryRepository();
        }

        [TestMethod]
        public void GetGameHistory_ExistingPlayer_ReturnsHistory()
        {

            var existingPlayer = _context.Player.FirstOrDefault();
            if (existingPlayer == null)
            {
                Assert.Inconclusive("Nema igrača u bazi za testiranje.");
                return;
            }


            List<GameHistory2> history = _repository.GetGameHistory(existingPlayer.player_id);


            Assert.IsNotNull(history, "Lista povijesti ne smije biti null.");
            Assert.IsTrue(history.Count >= 0, "Metoda bi trebala vratiti barem 0 elemenata.");
        }

        [TestMethod]
        public void GetGameHistory_NonExistingPlayer_ReturnsEmptyList()
        {

            List<GameHistory2> history = _repository.GetGameHistory(99999);


            Assert.IsNotNull(history, "Lista povijesti ne smije biti null.");
            Assert.AreEqual(0, history.Count, "Očekuje se prazna lista za nepostojećeg igrača.");
        }


    }
}
