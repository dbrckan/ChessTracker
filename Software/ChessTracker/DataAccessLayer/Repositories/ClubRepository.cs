using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Autor: David Brckan

namespace DataAccessLayer.Repositories
{
    public class ClubRepository : Repository<Club>
    {
        public ClubRepository() : base(new ChessModel()) { }

        public bool SaveClub(Club club, out string message)
        {
            if (Entities.Any(c => c.username == club.username))
            {
                message = "Korisničko ime već postoji.";
                return false;
            }

            try
            {
                Entities.Add(club);
                Context.SaveChanges();
                message = "Klub je uspješno dodan!";
                return true;
            }
            catch
            {
                message = "Došlo je do greške prilikom spremanja kluba.";
                return false;
            }
        }

        public override int Update(Club entity, bool saveChanges = true)
        {
            var existingClub = Entities.SingleOrDefault(c => c.club_id == entity.club_id);
            if (existingClub != null)
            {
                existingClub.name = entity.name;
                existingClub.address = entity.address;
                existingClub.contact = entity.contact;
                existingClub.username = entity.username;
                existingClub.password = entity.password;
                existingClub.dateOfEstablishment = entity.dateOfEstablishment;
                existingClub.federation_id = entity.federation_id;

                return saveChanges ? SaveChanges() : 0;
            }
            return 0;
        }

        public string GetClubNameById(int clubId)
        {
            var club = Entities.Find(clubId);
            return club != null ? club.name : null;
        }

        public virtual IQueryable<ClubStatistic> GetClubStatistics()
        {
            var query = from club in Context.Club
                        select new ClubStatistic
                        {
                            Name = club.name,
                            FoundedDate = club.dateOfEstablishment,
                            Address = club.address,
                            ActivePlayersCount = club.Player.Count(p => p.status_id == 1),
                            InactivePlayersCount = club.Player.Count(p => p.status_id == 2)
                        };
            return query;
        }

        public List<Club> GetClubsByTournamentId(int tournamentId)
        {
            var query = from club in Context.Club
                        join p in Context.Player on club.club_id equals p.club_id
                        join co in Context.ChangeOfRating on p.player_id equals co.player_id
                        join gr in Context.GameRecord on co.record_id equals gr.record_id
                        join g in Context.Game on gr.game_id equals g.game_id
                        join r in Context.Round on g.round_id equals r.round_id
                        join t in Context.Tournament on r.tournament_id equals t.tournament_id
                        where t.tournament_id == tournamentId
                        select club;
            return query.Distinct().ToList();
        }
    }
}
