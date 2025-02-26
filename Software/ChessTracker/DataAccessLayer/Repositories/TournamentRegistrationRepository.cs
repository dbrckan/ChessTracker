using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;

//Autor: David Brckan

namespace DataAccessLayer.Repositories
{
    public class TournamentRegistrationRepository
    {
        private readonly ChessModel _context;

        public TournamentRegistrationRepository()
        {
            _context = new ChessModel();
        }

        public List<Club> GetRegisteredClubs(int tournamentId)
        {
            var query = from r in _context.RegistrationForTournament
                join c in _context.Club on r.club_id equals c.club_id
                where r.tournament_id == tournamentId
                select c;

            return query.Distinct().ToList();
        }
        
        public List<Player> GetRegisteredPlayers(int tournamentId)
        {
            var query = from r in _context.RegistrationForTournament
                join p in _context.Player on r.player_id equals p.player_id
                where r.tournament_id == tournamentId
                select p;

            return query.Distinct().ToList();
        }

        public bool RegisterPlayer(int playerId, int tournamentId, out string message)
        {
            Debug.WriteLine($"[INFO] Početak registracije igrača {playerId} na turnir {tournamentId}.");

           
            bool alreadyRegistered = _context.RegistrationForTournament
                .Any(r => r.player_id == playerId && r.tournament_id == tournamentId);

            if (alreadyRegistered)
            {
                message = $"Igrač je već prijavljen na turnir.";
                Debug.WriteLine($"[WARNING] {message}");
                return false;
            }

            
            var playerClub = _context.Player
                .Where(p => p.player_id == playerId)
                .Select(p => p.club_id)
                .FirstOrDefault();

            if (playerClub == 0)
            {
                message = $"Greška: Klub za odabranog igrača nije pronađen.";
                Debug.WriteLine($"[ERROR] {message}");
                return false;
            }

            
            string sqlInsert = @"
        INSERT INTO RegistrationForTournament (registerByClub, player_id, club_id, tournament_id) 
        VALUES (@p0, @p1, @p2, @p3);
    ";

            try
            {
                _context.Database.ExecuteSqlCommand(
                    sqlInsert,
                    "Ne",          
                    playerId,      
                    playerClub,   
                    tournamentId   
                );

                message = $"Igrač je uspješno prijavljen na turnir.";
                Debug.WriteLine($"[SUCCESS] {message}");
                return true;
            }
            catch (Exception ex)
            {
                message = $"Neuspješna prijava igrača na turnir: {ex.Message}";
                Debug.WriteLine($"[ERROR] {message}");
                return false;
            }
        }



        public List<Tournament> GetUpcomingTournaments()
        {
            return _context.Tournament
                .Where(t => t.date >= DateTime.Now)
                .ToList();
        }

        public bool RegisterClub(int clubId, int tournamentId, List<int> playerIds, out string message)
        {
            Debug.WriteLine($"[INFO] Početak registracije kluba {clubId} na turnir {tournamentId} s {playerIds.Count} igrača.");

           
                foreach (int playerId in playerIds)
                {
                    var player = _context.Player
                        .Where(p => p.player_id == playerId)
                        .Select(p => new { p.status_id, p.firstName, p.lastName })
                        .FirstOrDefault();

                    if (player.status_id == 2)
                    {
                        message = $"Igrač {player.firstName} {player.lastName} je deaktiviran i ne može se prijaviti na turnir.";
                        Debug.WriteLine($"[ERROR] {message}");
                        return false;
                    }
                }
            
            

            bool alreadyRegistered = _context.RegistrationForTournament
                .Any(r => r.club_id == clubId && r.tournament_id == tournamentId);

            if (alreadyRegistered)
            {
                message = $"Klub je već prijavljen na turnir.";
                Debug.WriteLine($"[WARNING] {message}");
                return false;
            }

          
            if (!playerIds.Any())
            {
                message = $"Greška: Nema igrača za prijavu u klubu.";
                Debug.WriteLine($"[ERROR] {message}");
                return false;
            }

            try
            {
               
                string sqlInsert = "INSERT INTO RegistrationForTournament (registerByClub, player_id, club_id, tournament_id) VALUES ";

                List<string> valuesList = new List<string>();
                List<object> parameters = new List<object>();

                int paramIndex = 0;
                foreach (int playerId in playerIds)
                {
                    valuesList.Add($"(@p{paramIndex}, @p{paramIndex + 1}, @p{paramIndex + 2}, @p{paramIndex + 3})");
                    parameters.Add("Da");       
                    parameters.Add(playerId);     
                    parameters.Add(clubId);       
                    parameters.Add(tournamentId); 
                    paramIndex += 4;
                }

                sqlInsert += string.Join(", ", valuesList) + ";";

                _context.Database.ExecuteSqlCommand(sqlInsert, parameters.ToArray());

                message = $"Klub i igrači uspješno prijavljeni na turnir.";
                Debug.WriteLine($"[SUCCESS] {message}");
                return true;
            }
            catch (Exception ex)
            {
                message = $"Neuspješna prijava kluba na turnir: {ex.Message}";
                Debug.WriteLine($"[ERROR] {message}");
                return false;
            }
        }
    }
}
