using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Autor: Nika Antolić*/

namespace DataAccessLayer.Repositories
{
    public class StatusRepository : Repository<Status>
    {
        public StatusRepository() : base(new ChessModel())
        {
            
        }
        public List<Status> GetStatuses()
        {
            return this.Context.Status.ToList();
        }
        public override int Update(Status entity, bool saveChanges = true)
        {
            throw new NotImplementedException();
        }
    }
}
