using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

//Autor: David Brckan

namespace DataAccessLayer.Repositories
{
    public class UserRepository
    {
        private readonly ChessModel _context;

        public UserRepository()
        {
            _context = new ChessModel();
        }

        public (string Role, int? UserId, string Message) AuthenticateUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            var federation = _context.ChessFederation.FirstOrDefault(f => f.username == username && f.password == password);
            if (federation != null) return ("Savez", federation.federation_id, null);

            var club = _context.Club.FirstOrDefault(c => c.username == username && c.password == hashedPassword);
            if (club != null) return ("Klub", club.club_id, null);

            var player = _context.Player.FirstOrDefault(p => p.username == username && p.password == hashedPassword);
            if (player != null)
            {
                if (player.status_id == 2) return (null, null, "Vaš račun je deaktiviran!");
                return ("Igrač", player.player_id, null);
            }

            var judge = _context.Judge.FirstOrDefault(j => j.username == username && j.password == hashedPassword);
            if (judge != null) return ("Sudac", judge.judge_id, null);

            return (null, null, "Pogrešno korisničko ime ili lozinka!");
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
