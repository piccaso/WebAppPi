using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppPi.Db;

namespace WebAppPi.Controllers
{
    [Produces("application/json")]
    [Route("api/db")]
    public class DbController
    {
        private readonly Db.Db _db;

        public DbController(Db.Db db)
        {
            _db = db;
        }
        [HttpGet("export")]
        public DfsRoot Export()
        {
            var all = _db.Questions.ToList();
            return new DfsRoot
            {
               Questions = all,
            };
        }

        [HttpPost("import")]
        public void Import([FromBody] DfsRoot root)
        {
            if (root?.Questions == null || !root.Questions.Any())
            {
                throw new InvalidOperationException("no questions");
            }

            _db.Database.ExecuteSqlCommand("DELETE FROM Questions");
            _db.Database.ExecuteSqlCommand("DELETE FROM Answers");
            foreach (var question in root.Questions)
            {
                question.Id = 0;
                if (question.Answers != null && question.Answers.Any())
                {
                    question.Answers.ForEach(a=>a.Id = 0);
                    _db.Questions.Add(question);
                }
            }

            _db.SaveChanges();
        }
    }
}
