using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class RoundService
    {
        private RoundRepository roundRepository = new RoundRepository();

        public List<Round> GetRoundsByTournamentId(int tournamentId)
        {
            return roundRepository.GetRoundsByTournamentId(tournamentId);
        }
        public Round Add(Round round)
        {
            roundRepository.Add(round);
            return round;
        }
    }
}
