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
    public class ClubResultService
    {
        public int UpdateClubScore(int clubId, int tournamentId, decimal score, out string message)
        {
            using (var repo = new ClubResultRepository())
            {
                int affectedRows = repo.UpdateClubScore(clubId, tournamentId, score);
                if (affectedRows > 0)
                {
                    message = "";
                    return affectedRows;
                }
                else
                {
                    message = "Došlo je do pogreške prilikom izračuna rezultata kluba";
                    return affectedRows;
                }
            }
        }

        public List<ClubResult2> GetClubResultsByTournamentId(int tournamentId)
        {
            using (var repo = new ClubResultRepository())
            {
                return repo.GetClubResultsByTournamentId(tournamentId);
            }
        }

        public int AddClubResult(ClubResult clubResult, out string message)
        {
            using (var repo = new ClubResultRepository())
            {
                int affectedRows = repo.Add(clubResult);
                if (affectedRows > 0)
                {
                    message = "";
                    return affectedRows;
                }
                else
                {
                    message = "Došlo je do pogreške prilikom unosa rezultata kluba";
                    return affectedRows;
                }
            }
        }
    }
}
