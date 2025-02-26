using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GameRecordRepository : Repositories.Repository<GameRecord>
    {
        public GameRecordRepository() : base(new ChessModel())
        {
        }

        public override int Update(GameRecord entity, bool saveChanges = true)
        {
            var gameRecord = Entities.Find(entity.record_id);
            if (gameRecord != null)
            {
                gameRecord.result = entity.result;
                return Context.SaveChanges();
            }
            return 0;
        }

        /*Autor: Nika Antolić*/
        public int CountGamesPlayedByPlayerId(int playerId)
        {
            return Context.GameHistory.Count(gh => gh.player_id == playerId);
        }

        public List<GameRecord2> GetGameRecordsByRoundId(int roundId, int tournamentId)
        {
            var query = from gr in Context.GameRecord
                        join g in Context.Game on gr.game_id equals g.game_id
                        where g.round_id == roundId
                        join r in Context.Round on g.round_id equals r.round_id
                        where r.tournament_id == tournamentId
                        join p in Context.Pairs on gr.pair_id equals p.pair_id
                        join p1 in Context.Player on p.player1_id equals p1.player_id
                        join p2 in Context.Player on p.player2_id equals p2.player_id
                        select new GameRecord2
                        {
                            Id = gr.record_id,
                            Number = g.number,
                            Player1Name = p1.firstName + " " + p1.lastName,
                            Player2Name = p2.firstName + " " + p2.lastName,
                            GameResult = gr.result,
                        };
            return query.ToList();
        }

        public bool IsGameRecordExist(int pairId)
        {
            return Context.GameRecord.Any(gr => gr.pair_id == pairId);
        }

        public GameRecord2 GetGameRecordById(int recordId)
        {
            var query = from gr in Context.GameRecord
                        join g in Context.Game on gr.game_id equals g.game_id
                        where gr.record_id == recordId
                        join p in Context.Pairs on gr.pair_id equals p.pair_id
                        join p1 in Context.Player on p.player1_id equals p1.player_id
                        join p2 in Context.Player on p.player2_id equals p2.player_id
                        select new GameRecord2
                        {
                            Id = recordId,
                            Number = g.number,
                            Player1Name = p1.firstName + " " + p1.lastName,
                            Player2Name = p2.firstName + " " + p2.lastName,
                            GameResult = gr.result,
                            Player1Id = p.player1_id,
                            Player2Id = p.player2_id,
                        };
            return query.FirstOrDefault();
        }

        public decimal GetScoreByPlayerId(int playerId, int tournamentId)
        {
            var query = from gr in Context.GameRecord
                        join g in Context.Game on gr.game_id equals g.game_id
                        join r in Context.Round on g.round_id equals r.round_id
                        where r.tournament_id == tournamentId
                        join p in Context.Pairs on gr.pair_id equals p.pair_id
                        where p.player1_id == playerId || p.player2_id == playerId
                        select new
                        {
                            gr.result,
                            Player1Id = p.player1_id,
                            Player2Id = p.player2_id
                        };

            decimal score = 0;

            foreach (var record in query)
            {
                if (record.result == "1" && record.Player1Id == playerId)
                {
                    score++;
                }
                else if (record.result == "2" && record.Player2Id == playerId)
                {
                    score++;
                }
                else if (record.result == "/")
                {
                    score += 0.5m;
                }
            }

            return score;
        }

        public decimal GetScoreByPlayerIdAndClubId(int playerId, int clubId, int tournamentId)
        {
            var query = from gr in Context.GameRecord
                        join g in Context.Game on gr.game_id equals g.game_id
                        join r in Context.Round on g.round_id equals r.round_id
                        where r.tournament_id == tournamentId
                        join p in Context.Pairs on gr.pair_id equals p.pair_id
                        where (p.player1_id == playerId || p.player2_id == playerId)
                        join pl1 in Context.Player on p.player1_id equals pl1.player_id
                        join pl2 in Context.Player on p.player2_id equals pl2.player_id
                        where pl1.club_id == clubId || pl2.club_id == clubId
                        select new
                        {
                            gr.result,
                            Player1Id = p.player1_id,
                            Player2Id = p.player2_id
                        };
            decimal score = 0;
            foreach (var record in query)
            {
                if (record.result == "1" && record.Player1Id == playerId)
                {
                    score++;
                }
                else if (record.result == "2" && record.Player2Id == playerId)
                {
                    score++;
                }
                else if (record.result == "/")
                {
                    score += 0.5m;
                }
            }
            return score;
        }
    }
}
