using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GameHistoryRepository : Repository<GameHistory>
    {
        public GameHistoryRepository() : base(new ChessModel())
        {
        }

        public override int Update(GameHistory entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public List<GameHistory2> GetGameHistory(int playerId)
        {
            var query = from gh in Context.GameHistory
                        where gh.player_id == playerId
                        join gr in Context.GameRecord on gh.record_id equals gr.record_id
                        join pr in Context.Pairs on gr.pair_id equals pr.pair_id
                        join p1 in Context.Player on pr.player1_id equals p1.player_id
                        join p2 in Context.Player on pr.player2_id equals p2.player_id
                        join g in Context.Game on gr.game_id equals g.game_id
                        join r in Context.Round on g.round_id equals r.round_id
                        join t in Context.Tournament on r.tournament_id equals t.tournament_id

                        select new GameHistory2
                        {
                            Player1Name = p1.firstName + " " + p1.lastName,
                            Player2Name = p2.firstName + " " + p2.lastName,
                            GameResult = gr.result,
                            RoundNumber = r.number,
                            TournamentName = t.name,
                            TournamentDate = t.date
                        };

            return query.ToList();
        }
    }
  }
