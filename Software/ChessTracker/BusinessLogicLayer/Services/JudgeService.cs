using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

//Autor: David Brckan

namespace BusinessLogicLayer.Services
{
    public class JudgeService
    {
        private readonly JudgeRepository _judgeRepository;

        public JudgeService()
        {
            _judgeRepository = new JudgeRepository();
        }

        public bool AddJudge(Judge judge, out string message)
        {
            return _judgeRepository.SaveJudge(judge, out message);
        }

        public List<Judge> GetAllJudges()
        {
            return _judgeRepository.GetAll().ToList();
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
