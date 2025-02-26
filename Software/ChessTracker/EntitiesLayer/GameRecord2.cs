using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class GameRecord2
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public string GameResult { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
    }
}
