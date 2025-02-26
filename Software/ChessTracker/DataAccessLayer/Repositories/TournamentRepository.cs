using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class TournamentRepository : Repository<Tournament>
    {
        public TournamentRepository() : base(new ChessModel())
        {
        }

        public override int Update(Tournament entity, bool saveChanges = true)
        {
            var tournament = Entities.SingleOrDefault(t => t.tournament_id == entity.tournament_id);
            if (tournament == null)
                return 0;

            tournament.date = entity.date;
            tournament.time = entity.time;
            tournament.place = entity.place;
            tournament.type = entity.type;
            tournament.numberOfRounds = entity.numberOfRounds;
            tournament.judge_id = entity.judge_id;
            tournament.federation_id = entity.federation_id;

            return saveChanges ? SaveChanges() : 0;
        }

        public List<Tournament> GetUpcomingTournaments()
        {
            var tournaments = Entities.Where(t => t.date >= DateTime.Now).ToList();

            Console.WriteLine($"Broj dohvaćenih turnira: {tournaments.Count}");

            return tournaments;
        }

        public List<Tournament> GetAllTournaments()
        {
            var tournaments = (from t in Entities
                               select new
                               {
                                   t.tournament_id,
                                   t.place,
                                   t.date,
                                   t.time
                               })
                              .AsEnumerable() 
                              .Select(t => new Tournament
                              {
                                  tournament_id = t.tournament_id,
                                  place = t.place,
                                  date = t.date,
                                  time = t.time
                              })
                              .ToList();

            return tournaments;
        }

    }
}
