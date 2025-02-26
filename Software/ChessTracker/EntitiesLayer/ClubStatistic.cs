using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Autor: Nika Antolić*/

namespace EntitiesLayer
{
    public class ClubStatistic
    {
        public string Name { get; set; }
        public DateTime FoundedDate { get; set; }
        public string Address { get; set; }
        public int ActivePlayersCount { get; set; }
        public int InactivePlayersCount { get; set; }
    }
}
