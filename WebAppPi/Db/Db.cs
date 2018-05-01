using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppPi.Db
{
    public class Db : DbContext
    {
        public DbSet<QuestionNode> Questions { get; set; }
        public DbSet<AnswerNode> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionNode>()
                .HasIndex(p=>p.Question);
            //modelBuilder.Entity<AnswerNode>();
        }


        public Db(DbContextOptions<Db> contextOptions) : base(contextOptions)
        {
        }

        //protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder, AppSettings settings)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlite(settings.SqliteConnectionString);
        //    }
        //}
    }

    public class DfsRoot
    {
        public List<QuestionNode> Questions { get; set; }
    }

    public class QuestionNode
    {
        public int Id { get; set; }
        public int LastUsed { get; set; }
        public byte[] Image { get; set; }
        public string Header { get; set; }
        public string Question { get; set; }
        public List<AnswerNode> Answers { get; set; }
    }

    public class AnswerNode
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public decimal Score { get; set; }
        public string Conclusion { get; set; }
    }
}
