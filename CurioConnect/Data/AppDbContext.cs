using CurioConnect.Model;
using CurioConnect.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CurioConnect.Data
{
    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<CorrectAnswer> CorrectAnswers { get; set; }
        public DbSet<AnswerOptions> AnswerOptions { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;port=3306;user=root;password=KutLuna;database=native_CurioConnect",
                ServerVersion.Parse("8.0.30")
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Quiz)
                .WithOne(q => q.User)
                .HasForeignKey<User>(u => u.QuizId);

            modelBuilder.Entity<User>().HasData(
           new User { Id = 1, Name = "Max Power", Email = "max.power@example.com", Password = SecureHasher.Hash("SuperSecret1"), Course = "Techniek en Industrie", Gender = "Male",},
           new User { Id = 2, Name = "Luna Light", Email = "luna.light@example.com", Password = SecureHasher.Hash("Moonlight42"), Course = "Techniek en Technologie", Gender = "Female" },
           new User { Id = 3, Name = "Rocky Road", Email = "rocky.road@example.com", Password = SecureHasher.Hash("Mountain77"), Course = "Economie en Onderneming", Gender = "Male" },
           new User { Id = 4, Name = "Stella Star", Email = "stella.star@example.com", Password = SecureHasher.Hash("Galaxy123"), Course = "Logistiek en Mobiliteittechniek", Gender = "Female" },
           new User { Id = 5, Name = "Vince Valor", Email = "vince.valor@example.com", Password = SecureHasher.Hash("Braveheart55"), Course = "Veiligheid en Defensie", Gender = "Male" },
           new User { Id = 6, Name = "Gina Genius", Email = "gina.genius@example.com", Password = SecureHasher.Hash("Brainiac99"), Course = "Techniek en Gebouwde Omgeving", Gender = "Female" },
           new User { Id = 7, Name = "Archie Artist", Email = "archie.artist@example.com", Password = SecureHasher.Hash("Palette123"), Course = "Creative Industrie", Gender = "Male" },
           new User { Id = 8, Name = "Nina Nurse", Email = "nina.nurse@example.com", Password = SecureHasher.Hash("Caregiver88"), Course = "Zorg en Welzijn", Gender = "Female" },
           new User { Id = 9, Name = "Sammy Sprint", Email = "sammy.sprint@example.com", Password = SecureHasher.Hash("FastFeet77"), Course = "Sport en Bewegen", Gender = "Male" },
           new User { Id = 10, Name = "Daisy Dazzle", Email = "daisy.dazzle@example.com", Password = SecureHasher.Hash("ShineBright22"), Course = "Groen en Dier", Gender = "Female" }
           );

            modelBuilder.Entity<Match>().HasData(
               new Match { Id = 1, UserId = 1, MatchedUserId = 2 },
               new Match { Id = 2, UserId = 1, MatchedUserId = 3 },
               new Match { Id = 3, UserId = 1, MatchedUserId = 4 },
               new Match { Id = 4, UserId = 1, MatchedUserId = 5 },
               new Match { Id = 5, UserId = 1, MatchedUserId = 6 }
           );
        }

    }
}
