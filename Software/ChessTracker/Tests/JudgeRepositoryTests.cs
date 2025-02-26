using DataAccessLayer.Repositories;
using DataAccessLayer;
using EntitiesLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class JudgeRepositoryTests
    {
        private JudgeRepository _repository;
        private ChessModel _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new ChessModel();
            _repository = new JudgeRepository();
        }

        [TestMethod]
        public void SaveJudge_ValidJudge_ReturnsSuccess()
        {
            
        
            var judge = new Judge
            {
                firstName = "Ivan",
                lastName = "Horvat",
                contact = "0991234567",
                username = "ihorvat123",
                password = "password123",
                federation_id = _context.ChessFederation.Select(f => f.federation_id).FirstOrDefault()
            };

            
            bool result = _repository.SaveJudge(judge, out string message);

           
            Assert.IsTrue(result, "Očekuje se da sudac bude uspješno dodan.");
            Assert.AreEqual("Sudac uspješno dodan!", message);
        }

        [TestMethod]
        public void SaveJudge_UsernameAlreadyExists_ReturnsError()
        {
           
            string existingUsername = _context.Judge.Select(j => j.username).FirstOrDefault();

            if (string.IsNullOrEmpty(existingUsername))
            {
                Assert.Inconclusive("Nema sudaca u bazi s postojećim korisničkim imenom za ovaj test.");
                return;
            }

            var judge = new Judge
            {
                firstName = "Marko",
                lastName = "Kovač",
                contact = "0912345678",
                username = existingUsername, 
                password = "test123",
                federation_id = _context.ChessFederation.Select(f => f.federation_id).FirstOrDefault()
            };

           
            bool result = _repository.SaveJudge(judge, out string message);

           
            Assert.IsFalse(result, "Očekuje se da sudac ne može biti dodan s postojećim korisničkim imenom.");
            Assert.AreEqual("Korisničko ime već postoji.", message);
        }

        [TestMethod]
        public void SaveJudge_InvalidFederation_ReturnsError()
        {
           
            var judge = new Judge
            {
                firstName = "Petar",
                lastName = "Novak",
                contact = "0923456789",
                username = $"pnovak",
                password = "pass123",
                federation_id = 99999 
            };

            
            bool result = _repository.SaveJudge(judge, out string message);

           
            Assert.IsFalse(result, "Očekuje se da sudac ne može biti dodan s nepostojećom federacijom.");
            Assert.IsTrue(message.StartsWith("Došlo je do greške prilikom spremanja suca:"), "Očekuje se greška pri unosu.");
        }
    }
}
