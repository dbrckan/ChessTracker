using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class GameRecordService
    {
        private GameRecordRepository gameRecordRepository = new GameRecordRepository();
        
        private ChangeOfRatingRepository changeOfRatingRepository = new ChangeOfRatingRepository();
        private PlayerRepository playerRepository = new PlayerRepository();
        private const decimal KFactor = 30;
        public int CountGamesPlayedByPlayerId(int playerId)
        {
            return gameRecordRepository.CountGamesPlayedByPlayerId(playerId);
        }

        public List<GameRecord2> GetGameRecordsByRoundId(int roundId, int tournamentId)
        {
            return gameRecordRepository.GetGameRecordsByRoundId(roundId, tournamentId);
        }

        public GameRecord2 GetGameRecordById(int id)
        {
            return gameRecordRepository.GetGameRecordById(id);
        }

        public bool AddGameRecord(GameRecord gameRecord, out string message)
        {
            if (gameRecordRepository.IsGameRecordExist((int)gameRecord.pair_id))
            {
                message = "Evidencija partije pod ovim ID-jem već postoji";
                return false;
            }
            using (var repo = new GameRecordRepository())
            {
                int affectedRows = repo.Add(gameRecord);
                if (affectedRows > 0)
                {
                    message = "";
                    return true;
                }
                else
                {
                    message = "Došlo je do pogreške prilikom unosa evidencije";
                    return false;
                }
            }

        }

        public bool UpdateGameRecord(GameRecord gameRecord, out string message)
        {
            using (var repo = new GameRecordRepository())
            {
                int affectedRows = repo.Update(gameRecord);
                if (affectedRows > 0)
                {
                    message = "";
                    return true;
                }
                else
                {
                    message = "Došlo je do pogreške prilikom ažuriranja evidencije";
                    return false;
                }
            }
        }

        public decimal GetScoreByPlayerId(int playerId, int tournamentId)
        {
            return gameRecordRepository.GetScoreByPlayerId(playerId, tournamentId);
        }

        public decimal GetScoreByPlayerIdAndClubId(int playerId, int clubId, int tournamentId)
        {
            return gameRecordRepository.GetScoreByPlayerIdAndClubId(playerId, clubId, tournamentId);
        }
        /*Autor: Nika Antolić*/
        public void UpdateChangeOfRating(GameRecord gameRecord, Player player1, Player player2)
        {
            decimal actualScorePlayer1 = 0;
            decimal actualScorePlayer2 = 0;

            switch (gameRecord.result.ToString())
            {
                case "1":
                    actualScorePlayer1 = 1;
                    actualScorePlayer2 = 0;
                    break;
                case "2":
                    actualScorePlayer1 = 0;
                    actualScorePlayer2 = 1;
                    break;
                case "/":
                    actualScorePlayer1 = 0.5m;
                    actualScorePlayer2 = 0.5m;
                    break;
                default:
                    throw new ArgumentException("Neispravan rezultat");
            }

            decimal expectedScorePlayer1 = 1m / (1m + (decimal)Math.Pow(10, (double)(player2.rating - player1.rating) / 400));
            decimal expectedScorePlayer2 = 1m / (1m + (decimal)Math.Pow(10, (double)(player1.rating - player2.rating) / 400));

            decimal ratingChangePlayer1 = KFactor * (actualScorePlayer1 - expectedScorePlayer1);
            decimal ratingChangePlayer2 = KFactor * (actualScorePlayer2 - expectedScorePlayer2);

            SaveRatingChange(player1.player_id, gameRecord.record_id, ratingChangePlayer1);
            SaveRatingChange(player2.player_id, gameRecord.record_id, ratingChangePlayer2);
        }

        private void SaveRatingChange(int playerId, int recordId, decimal change)
        {
            var ratingChange = new ChangeOfRating
            {
                player_id = playerId,
                record_id = recordId,
                change = change
            };
            changeOfRatingRepository.Add(ratingChange);
        }

        public void UpdateRatingChanges(GameRecord gameRecord)
        {
            var gameRecord2 = GetGameRecordById(gameRecord.record_id);

            if (gameRecord2 != null)
            {
                var player1 = playerRepository.GetById(gameRecord2.Player1Id);
                var player2 = playerRepository.GetById(gameRecord2.Player2Id);

                if (player1 != null && player2 != null)
                {
                    DeleteExistingRatingChanges(gameRecord.record_id);
                    UpdateChangeOfRating(gameRecord, player1, player2);
                }
                else
                {
                    throw new ArgumentException("Jedan ili oba igrača nisu pronađena.");
                }
            }
            else
            {
                throw new ArgumentException("GameRecord s navedenim ID-jem nije pronađen");
            }
        }

        private void DeleteExistingRatingChanges(int recordId)
        {
            changeOfRatingRepository.DeleteByRecordId(recordId);
        }

        public void UpdatePlayerRatingsAtEndOfTournament(int tournamentId)
        {
            changeOfRatingRepository.UpdatePlayerRatingsAtEndOfTournament(tournamentId);
        }
    }
}
