using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Entities
{
    public class Pair2
    {
        public int PairId { get; set; }
        public string Pair { get; set; }

        public int Player1Id { get; set; }

        public int Player2Id { get; set; }
    }
}
