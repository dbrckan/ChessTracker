using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class GameHistory2
    {
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public string GameResult { get; set; }
        public int RoundNumber { get; set; }
        public string TournamentName { get; set; }
        public DateTime TournamentDate { get; set; }
    }
}
