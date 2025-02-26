using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class PairsRepository : Repository<Pairs>
    {
        public PairsRepository() : base(new ChessModel())
        {
        }

        public override int Update(Pairs entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public Pair2 GetPairByGameId(int gameId)
        {
            var pairs = from p in Context.Pairs
                        where p.game_id == gameId
                        join p1 in Context.Player on p.player1_id equals p1.player_id
                        join p2 in Context.Player on p.player2_id equals p2.player_id
                        select new Pair2
                        {
                            PairId = p.pair_id,
                            Pair = p1.firstName + " " + p1.lastName + " vs " + p2.firstName + " " + p2.lastName,
                            Player1Id = p1.player_id,
                            Player2Id = p2.player_id
                        };

            return pairs.FirstOrDefault();
        }
    }
}
