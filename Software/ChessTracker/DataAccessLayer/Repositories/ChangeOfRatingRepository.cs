using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Autor: Nika Antolić*/

namespace DataAccessLayer.Repositories
{
    public class ChangeOfRatingRepository : Repository<ChangeOfRating>
    {
        public ChangeOfRatingRepository() : base(new ChessModel())
        {
            
        }
        public override int Update(ChangeOfRating entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public void DeleteByRecordId(int recordId)
        {
            var changes = Context.ChangeOfRating.Where(c => c.record_id == recordId).ToList();

            foreach (var change in changes)
            {
                Context.ChangeOfRating.Remove(change);
            }

            Context.SaveChanges(); 
        }

        public int GetTournamentIdByPlayerId(int playerId)
        {
            var query = from p in Context.Player
                        join co in Context.ChangeOfRating on p.player_id equals co.player_id
                        join gr in Context.GameRecord on co.record_id equals gr.record_id
                        join g in Context.Game on gr.game_id equals g.game_id
                        join r in Context.Round on g.round_id equals r.round_id
                        where p.player_id == playerId
                        select r.tournament_id;

            return query.FirstOrDefault();
        }
        public List<ChangeOfRating> GetRatingChangesForPlayerInTournament(int playerId)
        {
            var tournamentId = GetTournamentIdByPlayerId(playerId);

            var ratingChanges = from co in Context.ChangeOfRating
                                join gr in Context.GameRecord on co.record_id equals gr.record_id
                                join g in Context.Game on gr.game_id equals g.game_id
                                join r in Context.Round on g.round_id equals r.round_id
                                where co.player_id == playerId && r.tournament_id == tournamentId
                                select co;

            return ratingChanges.ToList();
        }

        public decimal CalculateTotalRatingChangeForPlayer(int playerId)
        {
            var ratingChanges = GetRatingChangesForPlayerInTournament(playerId);

            decimal totalChange = (decimal)ratingChanges.Sum(co => co.change);

            return totalChange;
        }

        public void UpdatePlayerRatingsAtEndOfTournament(int tournamentId)
        {
            var playersInTournament = from p in Context.Player
                                      join co in Context.ChangeOfRating on p.player_id equals co.player_id
                                      join gr in Context.GameRecord on co.record_id equals gr.record_id
                                      join g in Context.Game on gr.game_id equals g.game_id
                                      join r in Context.Round on g.round_id equals r.round_id
                                      where r.tournament_id == tournamentId
                                      select p;

            foreach (var player in playersInTournament.Distinct())
            {
                decimal totalChange = CalculateTotalRatingChangeForPlayer(player.player_id);

                var playerToUpdate = Context.Player.FirstOrDefault(p => p.player_id == player.player_id);
                if (playerToUpdate != null)
                {
                    playerToUpdate.rating += totalChange; 
                }
            }

            Context.SaveChanges();
        }


    }
}
