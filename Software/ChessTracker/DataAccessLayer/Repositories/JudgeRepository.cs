using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Autor: David Brckan

namespace DataAccessLayer.Repositories
{
    public class JudgeRepository : Repository<Judge>
    {
        public JudgeRepository() : base(new ChessModel()) { }

        public bool SaveJudge(Judge judge, out string message)
        {
            if (Entities.Any(j => j.username == judge.username))
            {
                message = "Korisničko ime već postoji.";
                return false;
            }

            try
            {
                Entities.Add(judge);
                Context.SaveChanges();
                message = "Sudac uspješno dodan!";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Došlo je do greške prilikom spremanja suca: {ex.Message}";
                return false;
            }
        }

        public override int Update(Judge entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
