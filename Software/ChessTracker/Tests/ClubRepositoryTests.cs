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
    public class ClubRepositoryTests
    {
        private ClubRepository _repository;
        private ChessModel _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new ChessModel();
            _repository = new ClubRepository();
        }

        [TestMethod]
        public void SaveClub_ValidClub_ReturnsSuccess()
        {
            
            
            var club = new Club
            {
                name = "Test Klub",
                address = "Test Adresa 123",
                contact = "0991234567",
                dateOfEstablishment = DateTime.Now,
                username = "tklub",
                password = "password123",
                federation_id = _context.ChessFederation.Select(f => f.federation_id).FirstOrDefault()
            };

           
            bool result = _repository.SaveClub(club, out string message);

           
            Assert.IsTrue(result, "Očekuje se da klub bude uspješno dodan.");
            Assert.AreEqual("Klub je uspješno dodan!", message);
        }

        [TestMethod]
        public void SaveClub_UsernameAlreadyExists_ReturnsError()
        {
           
            string existingUsername = _context.Club.Select(c => c.username).FirstOrDefault();

            if (string.IsNullOrEmpty(existingUsername))
            {
                Assert.Inconclusive("Nema klubova u bazi s postojećim korisničkim imenom za ovaj test.");
                return;
            }

            var club = new Club
            {
                name = "Test Klub 2",
                address = "Druga Adresa 456",
                contact = "0912345678",
                dateOfEstablishment = DateTime.Now,
                username = existingUsername, 
                password = "test123",
                federation_id = _context.ChessFederation.Select(f => f.federation_id).FirstOrDefault()
            };

        
            bool result = _repository.SaveClub(club, out string message);

           
            Assert.IsFalse(result, "Očekuje se da klub ne može biti dodan s postojećim korisničkim imenom.");
            Assert.AreEqual("Korisničko ime već postoji.", message);
        }

        [TestMethod]
        public void SaveClub_InvalidFederation_ReturnsError()
        {
            
            var club = new Club
            {
                name = "Test Klub 3",
                address = "Treća Adresa 789",
                contact = "0923456789",
                dateOfEstablishment = DateTime.Now,
                username = $"invalid_fed_{Guid.NewGuid()}",
                password = "pass123",
                federation_id = 99999 
            };

          
            bool result = _repository.SaveClub(club, out string message);

         
            Assert.IsFalse(result, "Očekuje se da klub ne može biti dodan s nepostojećom federacijom.");
            Assert.AreEqual("Došlo je do greške prilikom spremanja kluba.", message);
        }
    }
}
