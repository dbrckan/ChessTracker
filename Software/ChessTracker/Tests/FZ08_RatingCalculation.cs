using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using EntitiesLayer.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

/*Autor: Nika Antolić*/

namespace Tests
{
    [TestClass]
    public class FZ08_RatingCalculation
    {
        private GameRecordService _gameRecordService = new GameRecordService();
        private PlayerRepository _playerRepository = new PlayerRepository();
        private ChangeOfRatingRepository _changeOfRatingRepository = new ChangeOfRatingRepository();

        [TestMethod]

        public void CheckIfChangeOfRatingExistsBeforeUpdate()
        {
            int playerId = 1;
            var changes = _changeOfRatingRepository.GetRatingChangesForPlayerInTournament(playerId);

            Assert.IsTrue(changes.Count > 0, "Ima promjena rejtinga za ovog igrača.");
        }
    }

}
