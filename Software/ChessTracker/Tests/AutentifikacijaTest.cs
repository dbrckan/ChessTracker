using DataAccessLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class AutentifikacijaTest
    {

        private UserRepository _userRepository;

        [TestInitialize]
        public void SetUp()
        {
            _userRepository = new UserRepository();
        }

        [TestMethod]
        public void AuthenticateUser_CorrectFederationCredentials_ReturnsFederation()
        {
            var result = _userRepository.AuthenticateUser("admin", "lozinka123");

            Assert.AreEqual("Savez", result.Role);
            Assert.IsNotNull(result.UserId);
            Assert.IsNull(result.Message);
        }

        [TestMethod]
        public void AuthenticateUser_CorrectClubCredentials_ReturnsClub()
        {
            var result = _userRepository.AuthenticateUser("noviGrad", "lozinka123");

            Assert.AreEqual("Klub", result.Role);
            Assert.IsNotNull(result.UserId);
            Assert.IsNull(result.Message);
        }

        [TestMethod]
        public void AuthenticateUser_CorrectPlayerCredentials_ReturnsPlayer()
        {
            var result = _userRepository.AuthenticateUser("kkajic", "kkajic");

            Assert.AreEqual("Igrač", result.Role);
            Assert.IsNotNull(result.UserId);
            Assert.IsNull(result.Message);
        }

        [TestMethod]
        public void AuthenticateUser_IncorrectCredentials_ReturnsErrorMessage()
        {
            var result = _userRepository.AuthenticateUser("nepostoji", "pogresnalozinka");

            Assert.IsNull(result.Role);
            Assert.IsNull(result.UserId);
            Assert.AreEqual("Pogrešno korisničko ime ili lozinka!", result.Message);
        }

        [TestMethod]
        public void AuthenticateUser_DeactivatedPlayer_ReturnsErrorMessage()
        {
            var result = _userRepository.AuthenticateUser("aanic", "aanic");

            Assert.IsNull(result.Role);
            Assert.IsNull(result.UserId);
            Assert.AreEqual("Vaš račun je deaktiviran!", result.Message);
        }
    }

}

