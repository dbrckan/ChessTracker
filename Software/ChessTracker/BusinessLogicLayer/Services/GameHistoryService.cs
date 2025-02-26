using DataAccessLayer.Repositories;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class GameHistoryService
    {
        private GameHistoryRepository gameHistoryRepository = new GameHistoryRepository();

        public List<GameHistory2> GetGameHistory(int playerId)
        {
            return gameHistoryRepository.GetGameHistory(playerId);
        }

        public bool AddGameHistory(GameHistory gameHistory, out string message)
        {
            using (var repo = new GameHistoryRepository())
            {
                int affectedRows = repo.Add(gameHistory);
                if (affectedRows > 0)
                {
                    message = "";
                    return true;
                }
                else
                {
                    message = "Došlo je do pogreške prilikom unosa evidenciju u povijest partija";
                    return false;
                }
            }
        }
    }
}
