using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class TournamentService
    {
        private readonly TournamentRepository _tournamentRepository;

        public TournamentService()
        {
            _tournamentRepository = new TournamentRepository();
        }

        public List<Tournament> GetAllTournaments()
        {
            return _tournamentRepository.GetAll().ToList();
        }

        public List<Tournament> GetUpcomingTournaments()
        {
            return _tournamentRepository.GetUpcomingTournaments();
        }

        public bool AddTournament(Tournament tournament, out string message)
        {
            int affectedRows = _tournamentRepository.Add(tournament);
            if (affectedRows > 0)
            {
                message = "";
                return true;
            }
            else
            {
                message = "Došlo je do pogreške prilikom dodavanja turnira.";
                return false;
            }
        }

        public bool UpdateTournament(Tournament tournament, out string message)
        {
            int affectedRows = _tournamentRepository.Update(tournament);
            if (affectedRows > 0)
            {
                message = "";
                return true;
            }
            else
            {
                message = "Došlo je do greške prilikom ažuriranja turnira.";
                return false;
            }
        }

        public void DeleteTournament(Tournament selectedTournamentTournament)
        {
            _tournamentRepository.Remove(selectedTournamentTournament);
        }
    }
}
