using DataAccessLayer.Repositories;
using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class TournamentRegistrationTests
    {
        private TournamentRegistrationRepository _repository;
        private ChessModel _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new ChessModel();
            _repository = new TournamentRegistrationRepository();
        }

        [TestMethod]
        public void RegisterPlayer_ValidPlayer_ReturnsSuccess()
        {

            int playerId = 15;
            int tournamentId = 2;
            string message;

           
            bool result = _repository.RegisterPlayer(playerId, tournamentId, out message);

           
            Assert.IsTrue(result, "Očekuje se da igrač bude uspješno prijavljen na turnir.");
            Assert.AreEqual("Igrač je uspješno prijavljen na turnir.", message);
        }

        [TestMethod]
        public void RegisterPlayer_AlreadyRegistered_ReturnsWarning()
        {

            int playerId = 1;
            int tournamentId = 1;
            string message;

           
            _repository.RegisterPlayer(playerId, tournamentId, out message);

          
            bool result = _repository.RegisterPlayer(playerId, tournamentId, out message);

            
            Assert.IsFalse(result, "Očekuje se da prijava ne uspije jer je igrač već prijavljen.");
            Assert.AreEqual("Igrač je već prijavljen na turnir.", message);
        }

        [TestMethod]
        public void RegisterClub_AlreadyRegistered_ReturnsWarning()
        {

            int clubId = 1;
            int tournamentId = 1;
            List<int> playerIds = _context.Player.Where(p => p.club_id == clubId).Select(p => p.player_id).Take(3).ToList();
            string message;

           
            _repository.RegisterClub(clubId, tournamentId, playerIds, out message);

           
            bool result = _repository.RegisterClub(clubId, tournamentId, playerIds, out message);

            
            Assert.IsFalse(result, "Očekuje se da prijava ne uspije jer je klub već prijavljen.");
            Assert.AreEqual("Klub je već prijavljen na turnir.", message);
        }

        [TestMethod]
        public void RegisterClub_NoPlayers_ReturnsError()
        {
           
            int clubId = _context.Club.Select(c => c.club_id).FirstOrDefault();
            int tournamentId = _context.Tournament.Select(t => t.tournament_id).FirstOrDefault();
            List<int> playerIds = new List<int>(); 
            string message;

           
            bool result = _repository.RegisterClub(clubId, tournamentId, playerIds, out message);

          
            Assert.IsFalse(result, "Očekuje se da prijava ne uspije jer nema igrača za prijavu.");
            Assert.AreEqual("Greška: Nema igrača za prijavu u klubu.", message);
        }
    }
}
