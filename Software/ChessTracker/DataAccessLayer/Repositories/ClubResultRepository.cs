using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ClubResultRepository: Repository<ClubResult>
    {
        public ClubResultRepository() : base(new ChessModel())
        {
        }

        public override int Update(ClubResult entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }

        public List<ClubResult> GetClubResults(int tournamentId)
        {
            var clubResults = Entities.Where(cr => cr.tournament_id == tournamentId).ToList();
            return clubResults;
        }

        public List<ClubResult2> GetClubResultsByTournamentId(int tournamentId)
        {
            var query = (from cr in Context.ClubResult
                         join c in Context.Club on cr.club_id equals c.club_id
                         where cr.tournament_id == tournamentId
                         orderby cr.score descending
                         select new { cr, c })
                         .ToList();

            var result = query.Select((x, index) => new ClubResult2
            {
                ClubId = x.cr.club_id,
                Position = index + 1,
                ClubName = x.c.name,
                Score = x.cr.score
            }).ToList();

            return result;
        }



        public int UpdateClubScore(int clubId, int tournamentId, decimal score)
        {
            var clubResult = Entities.FirstOrDefault(cr => cr.club_id == clubId && cr.tournament_id == tournamentId);
            if (clubResult != null)
            {
                clubResult.score = score;
                return Context.SaveChanges();
            }
            return 0;
        }
    }
}
