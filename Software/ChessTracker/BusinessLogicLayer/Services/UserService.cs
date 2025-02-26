using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Autor: David Brckan

namespace BusinessLogicLayer.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public bool Login(string username, string password, out string message)
        {
            var result = _userRepository.AuthenticateUser(username, password);

            if (result.Role != null)
            {
                CurrentUser.Role = result.Role;
                CurrentUser.UserId = result.UserId;
                CurrentUser.Username = username;

                message = null;
                return true;
            }

            message = result.Message ?? "Pogrešno korisničko ime ili lozinka!";
            return false;
        }


    }
}
