using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

//Autor: David Brckan

namespace BusinessLogicLayer.Services
{
    public class ClubService
    {
        private readonly ClubRepository _clubRepository;

        public ClubService()
        {
            _clubRepository = new ClubRepository();
        }

        public bool AddClub(Club club, out string message)
        {
            return _clubRepository.SaveClub(club, out message);
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        /*Autor: Nika Antolić*/
        public List<Club> GetAllClubs()
        {
            return _clubRepository.GetAll().ToList();
        }
        /*Autor: Nika Antolić*/
        public List<ClubStatistic> GetClubStatisticsReport()
        {
            return _clubRepository.GetClubStatistics().ToList();
        }

        public List<Club> GetClubsByTournamentId(int  tournamentId)
        {
            return _clubRepository.GetClubsByTournamentId(tournamentId);
        }
    }

}
