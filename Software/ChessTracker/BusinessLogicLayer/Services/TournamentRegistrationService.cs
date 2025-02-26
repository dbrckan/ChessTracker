using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Data.Entity.Infrastructure;

//Autor: David Brckan

namespace BusinessLogicLayer.Services
{
    public class TournamentRegistrationService
    {
        private readonly TournamentRegistrationRepository _repository;

        public TournamentRegistrationService()
        {
            _repository = new TournamentRegistrationRepository();
        }

        public bool RegisterPlayerForTournament(int playerId, int tournamentId, out string message)
        {
            bool success = _repository.RegisterPlayer(playerId, tournamentId, out message);

            if (success)
            {
                Debug.WriteLine($"[SUCCESS] Igrač {playerId} uspješno prijavljen na turnir {tournamentId}.");
            }
            else
            {
                Debug.WriteLine($"[ERROR] Neuspješna prijava igrača {playerId} na turnir {tournamentId}.");
            }

            return success;
        }

        public List<Tournament> GetUpcomingTournaments()
        {
            return _repository.GetUpcomingTournaments();
        }

        public List<Club> GetRegisteredClubs(int tournamentId)
        {
            return _repository.GetRegisteredClubs(tournamentId);
        }
        
        public List<Player> GetRegisteredPlayers(int tournamentId)
        {
            return _repository.GetRegisteredPlayers(tournamentId);
        }
        public bool RegisterClubForTournament(int clubId, int tournamentId, List<int> playerIds, out string message)
        {
            return _repository.RegisterClub(clubId, tournamentId, playerIds, out message);
        }
    }
}
