using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Autor: Nika Antolić*/

namespace BusinessLogicLayer.Services
{
    public class PlayerService
    {
        private PlayerRepository playerRepository = new PlayerRepository();
        private GameRecordRepository gameRecordRepository = new GameRecordRepository();
        public List<Player> GetPlayers(int clubId)
        {
            return playerRepository.GetAll(clubId).ToList();
        }
        public bool AddPlayer(Player player, out string message)
        {
            if (playerRepository.UsernameExists(player.username))
            {
                message = "Korisničko ime već postoji";
                return false;
            }
            using(var repo = new PlayerRepository())
            {
                int affectedRows = repo.Add(player);
                if(affectedRows > 0)
                {
                    message = "";
                    return true;
                }
                else
                {
                    message = "Došlo je do pogreške prilikom dodavanja igrača";
                    return false;
                }
            }
        }
        public bool UpdatePlayer(Player player, out string message)
        {
            if (string.IsNullOrEmpty(player.firstName) ||
            string.IsNullOrEmpty(player.lastName) ||
            string.IsNullOrEmpty(player.contact) ||
            player.dateOfBirth == DateTime.MinValue) 
            {
                message = "Sva polja moraju biti popunjena.";
                return false;
            }

            int affectedRows = playerRepository.Update(player);
            if (affectedRows > 0)
            {
                message = "";
                return true;
            }
            else
            {
                message = "Došlo je do greške prilikom ažuriranja igrača.";
                return false;
            }
        }


        public List<Player> GetAllPlayers()
        {
            return playerRepository.GetAllPlayers();
        }

        public List<PlayerStatistic> GetAllPlayerStatistic()
        {
            var players = playerRepository.GetAll();
            var playerStatistics = new List<PlayerStatistic>();
            foreach (var player in players)
            {
                int gamesPlayed = gameRecordRepository.CountGamesPlayedByPlayerId(player.player_id);
                playerStatistics.Add(new PlayerStatistic
                {
                    FullName = $"{player.firstName} {player.lastName}",
                    Rating = (decimal)player.rating,
                    GamesPlayed = gamesPlayed
                });
            }
            return playerStatistics;
        }

        public List<RegisteredPlayerStatistic> GetRegisteredPlayerStatistics(int clubId)
        {
            var players = playerRepository.GetAll(clubId).ToList();
            var clubRepository = new ClubRepository();

            var playerStatistics = players.Select(player => new RegisteredPlayerStatistic
            {
                FirstName = player.firstName,
                LastName = player.lastName,
                Rating = (decimal)player.rating,
                Club = clubRepository.GetClubNameById(player.club_id)
            }).ToList();

            return playerStatistics;

        }

        public List<Player> GetPlayersByTournamentId(int tournamentId)
        {
            return playerRepository.GetPlayersByTournamentId(tournamentId);
        }
      
        public Player GetPlayerById(int playerId)
        {
            return playerRepository.GetById(playerId);
        }

    }
}
