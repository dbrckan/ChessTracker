using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ChessFederationRepository
    {
        private readonly ChessModel _context;

        public ChessFederationRepository()
        {
            _context = new ChessModel();
        }

        public List<ChessFederation> GetAllFederations()
        {
            return _context.ChessFederation.ToList();
        }
    }
}
