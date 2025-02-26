using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Autor: Nika Antolić*/

namespace BusinessLogicLayer.Services
{
    public class StatusService
    {
        private StatusRepository statusRepository = new StatusRepository();
        public List<Status> GetStatuses()
        {
            return statusRepository.GetStatuses().ToList();
        }
    }
}
