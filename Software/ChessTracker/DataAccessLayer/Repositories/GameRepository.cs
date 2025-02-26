using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GameRepository: Repository<Game>
    {
        public GameRepository() : base(new ChessModel())
        {
        }

        public override int Update(Game entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public List<Game> GetGamesByRoundId(int roundId)
        {
            var games = Entities.Where(g => g.round_id == roundId).ToList();
            return games;
        }
    }
}
