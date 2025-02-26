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
    public class PairsService
    {
        private readonly GameRepository _gameRepository = new GameRepository();
        private readonly RoundRepository _roundRepository = new RoundRepository();
        private readonly PairsRepository _pairsRepository = new PairsRepository();
        private readonly PlayerRepository _playerRepository = new PlayerRepository();
        private readonly RoundService _roundService = new RoundService();
        private readonly GameService _gameService = new GameService();

        public Pair2 GetPairByGameId(int roundId)
        {
            return _pairsRepository.GetPairByGameId(roundId);
        }

        public bool GeneratePairsForNextRound(int tournamentId, int roundId)
        {
            List<Player> players = _playerRepository.GetPlayersInTournament(tournamentId);
            if (players.Count < 2)
            {
                return false; 
            }
            
            Random rand = new Random();
            players = players.OrderBy(p => rand.Next()).ToList();
            
            List<Round> rounds = _roundRepository.GetRoundsByTournamentId(tournamentId);
            int nextRoundNumber = rounds.Count == 0 ? 1 : rounds.Max(r => r.number) + 1;
            
            
            for (int i = 0; i < players.Count; i += 2)
            {
                if (i + 1 >= players.Count) break; 

                Game newGame = new Game
                {
                    number = (i / 2) + 1,
                    round_id = roundId,
                };
                Game newGameId = _gameService.Add(newGame); 
                
                Pairs newPair = new Pairs
                {
                    player1_id = players[i].player_id,
                    player2_id = players[i + 1].player_id,
                    game_id = newGameId.game_id
                };
                _pairsRepository.Add(newPair);
              
            }

            return true;
        }
    }
}