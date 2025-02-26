using DataAccessLayer.Repositories;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PlayerResultService
    {
        public List<PlayerResult2> GetPlayerResultsByTournamentId(int tournamentId)
        {
            using (var repo = new PlayerResultRepository())
            {
                return repo.GetPlayerResultsByTournamentId(tournamentId);
            }
        }

        public int AddPlayerResult(PlayerResult playerResult, out string message)
        {
            using (var repo = new PlayerResultRepository())
            {
                int affectedRows = repo.Add(playerResult);
                if (affectedRows > 0)
                {
                    message = "";
                    return affectedRows;
                }
                else
                {
                    message = "Došlo je do pogreške prilikom unosa rezultata igrača";
                    return affectedRows;
                }
            }
        }
    }
}
