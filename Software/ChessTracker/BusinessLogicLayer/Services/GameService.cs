using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class GameService
    {
        private GameRepository gameRepository = new GameRepository();

        public List<Game> GetGamesByRoundId(int roundId)
        {
            return gameRepository.GetGamesByRoundId(roundId);
        }

        internal Game Add(Game newGame)
        {
            gameRepository.Add(newGame);
            return newGame;
        }
    }
}
