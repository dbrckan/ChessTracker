using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class PlayerResultRepository : Repository<PlayerResult>
    {
        public PlayerResultRepository() : base(new ChessModel())
        {
        }

        public List<PlayerResult2> GetPlayerResultsByTournamentId(int tournamentId)
        {
            var query = (from pr in Context.PlayerResult
                         join p in Context.Player on pr.player_id equals p.player_id
                         where pr.tournament_id == tournamentId
                         orderby pr.score descending
                         select new { pr, p })
                         .ToList();

            var result = query.Select((x, index) => new PlayerResult2
            {
                PlayerId = x.pr.player_id,
                Position = index + 1,
                FirstName = x.p.firstName,
                LastName = x.p.lastName,
                Score = x.pr.score
            }).ToList();

            return result;
        }

        public override int Update(PlayerResult entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
