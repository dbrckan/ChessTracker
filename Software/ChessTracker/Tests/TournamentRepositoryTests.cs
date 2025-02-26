using DataAccessLayer.Repositories;
using DataAccessLayer;
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
    public class TournamentRepositoryTests
    {
        private TournamentRepository _repository;
        private ChessModel _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new ChessModel();
            _repository = new TournamentRepository();
        }

        [TestMethod]
        public void Update_ExistingTournament_UpdatesSuccessfully()
        {

            var existingTournament = _context.Tournament.FirstOrDefault();
            if (existingTournament == null)
            {
                Assert.Inconclusive("Nema turnira u bazi za testiranje.");
                return;
            }

            var updatedTournament = new Tournament
            {
                tournament_id = existingTournament.tournament_id,
                date = existingTournament.date.AddDays(1),
                time = existingTournament.time,
                place = "Zagreb",
                type = "Simultanka",
                numberOfRounds = existingTournament.numberOfRounds + 1,
                judge_id = existingTournament.judge_id,
                federation_id = existingTournament.federation_id
            };


            int result = _repository.Update(updatedTournament);


            Assert.AreEqual(1, result, "Očekuje se da je jedan redak ažuriran.");

            var dbTournament = _context.Tournament.Find(existingTournament.tournament_id);
            Assert.IsNotNull(dbTournament, "Turnir bi trebao postojati u bazi.");
            Assert.AreEqual("Zagreb", dbTournament.place);
            Assert.AreEqual("Simultanka", dbTournament.type);
        }

        [TestMethod]
        public void Update_NonExistingTournament_ReturnsZero()
        {

            var nonExistingTournament = new Tournament
            {
                tournament_id = 99999,
                date = DateTime.Now,
                time = new TimeSpan(12, 0, 0),
                place = "Neko mjesto",
                type = "Brzi Šah",
                numberOfRounds = 5,
                judge_id = 1,
                federation_id = 1
            };


            int result = _repository.Update(nonExistingTournament);


            Assert.AreEqual(0, result, "Očekuje se da nema ažuriranih redaka.");
        }

        [TestMethod]
        public void GetUpcomingTournaments_ReturnsOnlyFutureTournaments()
        {

            List<Tournament> upcomingTournaments = _repository.GetUpcomingTournaments();

            Assert.IsNotNull(upcomingTournaments, "Lista turnira ne smije biti null.");
            Assert.IsTrue(upcomingTournaments.All(t => t.date >= DateTime.Now), "Svi dohvaćeni turniri trebaju biti u budućnosti.");
        }

        [TestMethod]
        public void GetAllTournaments_ReturnsAllTournaments()
        {

            List<Tournament> allTournaments = _repository.GetAllTournaments();


            Assert.IsNotNull(allTournaments, "Lista turnira ne smije biti null.");
            Assert.IsTrue(allTournaments.Count > 0, "Očekuje se da postoji barem jedan turnir u bazi.");
        }
    }
}
