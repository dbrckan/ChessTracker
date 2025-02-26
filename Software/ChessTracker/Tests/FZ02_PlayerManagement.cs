using BusinessLogicLayer.Services;
using EntitiesLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

/*Autor: Nika Antolić*/

namespace Tests
{
    [TestClass]
    public class FZ02_PlayerManagement
    {
        [TestMethod]
        public void AddPlayer_DuplicateUsername_ShouldReturnFalse()
        {
            PlayerService service = new PlayerService();
            var player = new Player
            {
                firstName = "test",
                lastName = "test",
                dateOfBirth = new DateTime(2000, 1, 1),
                contact = "test@test@gmail.com",
                rating = 1234,
                gender = "Muško",
                username = "test",
                password = "test",
                club_id = 1,
                status_id = 1
            };


            bool success = service.AddPlayer(player, out string message);
            Assert.IsFalse(success);
            Assert.AreEqual("Korisničko ime već postoji", message);
        }
        [TestMethod]
        public void AddPlayer_PlayerAddInBase()
        {
            PlayerService service = new PlayerService();
            var player = new Player
            {
                firstName = "Marko",
                lastName = "Markić",
                dateOfBirth = new DateTime(1990, 12, 12),
                contact = "test@test@gmail.com",
                rating = 1234,
                gender = "Muško",
                username = "mmarkic123",
                password = "test",
                club_id = 2,
                status_id = 1
            };


            bool success = service.AddPlayer(player, out string message);
            Assert.IsTrue(success == true);
        }

        [TestMethod]
        public void UpdatePlayer_InvalidData_ShouldReturnFalse()
        {
            PlayerService service = new PlayerService();

            var player = new Player
            {
                player_id = 1,
                firstName = "",
                lastName = "",
                dateOfBirth = new DateTime(1990, 1, 1),
                contact = "", 
                rating = 1234,
                gender = "Muško",
                username = "test123",
                club_id = 1,
                status_id = 1
            };

            bool success = service.UpdatePlayer(player, out string message);

            Assert.IsFalse(success);
        }


    }
}
