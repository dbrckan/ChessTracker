using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RoundRepository: Repository<Round>
    {
        public RoundRepository() : base(new ChessModel())
        {
        }

        public override int Update(Round entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public List<Round> GetRoundsByTournamentId(int tournamentId)
        {
            var rounds = Entities.Where(r => r.tournament_id == tournamentId).ToList();
            return rounds;
        }
    }
}
