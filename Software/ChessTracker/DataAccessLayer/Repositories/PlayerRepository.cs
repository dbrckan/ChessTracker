using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class PlayerRepository : Repository<Player>
    {
        public PlayerRepository() : base(new ChessModel())
        {
            
        }

        public virtual IQueryable<Player> GetAll(int clubId)
        {
            var query = from e in Entities where e.club_id == clubId select e;
            return query;
        }
        /*Autor: Nika Antolić*/
        public override int Update(Player entity, bool saveChanges = true)
        {
            var existingPlayer = Entities.Find(entity.player_id);
            if(existingPlayer != null)
            {
                existingPlayer.firstName = entity.firstName;
                existingPlayer.lastName = entity.lastName;
                existingPlayer.dateOfBirth = entity.dateOfBirth;
                existingPlayer.contact = entity.contact;
                existingPlayer.rating = entity.rating;
                existingPlayer.gender = entity.gender;
                existingPlayer.username = entity.username;
                existingPlayer.password = entity.password;
                existingPlayer.status_id = entity.status_id;
                return Context.SaveChanges();
            }
            return 0;
        }
        /*Autor: Nika Antolić*/
        public bool UsernameExists(string username)
        {
            return Entities.Any(e => e.username == username);
        }
        public Player GetPlayerByUsername(string username)
        {
            return Entities.SingleOrDefault(p => p.username == username);
        }


        public List<Player> GetAllPlayers()
        {
            var players = (from p in Entities
                           join c in Context.Club on p.club_id equals c.club_id 
                           select new
                           {
                               p.player_id,
                               p.firstName,
                               p.lastName,
                               p.username,
                               p.gender, 
                               p.rating,
                               p.contact,
                               ClubName = c.name 
                           })
                          .AsEnumerable() 
                          .Select(p => new Player
                          {
                              player_id = p.player_id,
                              firstName = p.firstName,
                              lastName = p.lastName,
                              username = p.username,
                              gender = p.gender,
                              rating = p.rating, 
                              contact = p.contact,
                              Club = new Club { name = p.ClubName } 
                          })
                          .ToList();

            return players;
        }

        public List<Player> GetPlayersByTournamentId(int tournamentId)
        {
            var players = (from p in Entities
                           join co in Context.ChangeOfRating on p.player_id equals co.player_id
                           join gr in Context.GameRecord on co.record_id equals gr.record_id
                           join g in Context.Game on gr.game_id equals g.game_id
                           join r in Context.Round on g.round_id equals r.round_id
                           join t in Context.Tournament on r.tournament_id equals t.tournament_id
                           where t.tournament_id == tournamentId
                           select p)
                          .ToList();
            return players;
        }

        /*Autor: Nika Antolić*/
        public Player GetById(int playerId)
        {
            return Context.Player.FirstOrDefault(p => p.player_id == playerId);
        }

        public List<Player> GetPlayersInTournament(int tournamentId)
        {
            return (from p in Context.Player
                    join r in Context.RegistrationForTournament on p.player_id equals r.player_id
                    where r.tournament_id == tournamentId
                    select p).ToList();
            
        }
    }
}
