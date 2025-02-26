using EntitiesLayer;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataAccessLayer
{
    public partial class ChessModel : DbContext
    {
        public ChessModel()
            : base("name=ChessModel")
        {
        }

        public virtual DbSet<ChessFederation> ChessFederation { get; set; }
        public virtual DbSet<Club> Club { get; set; }
        public virtual DbSet<ClubResult> ClubResult { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameRecord> GameRecord { get; set; }
        public virtual DbSet<Judge> Judge { get; set; }
        public virtual DbSet<Pairs> Pairs { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<PlayerResult> PlayerResult { get; set; }
        public virtual DbSet<RegistrationForTournament> RegistrationForTournament { get; set; }
        public virtual DbSet<Round> Round { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tournament> Tournament { get; set; }
        public virtual DbSet<ChangeOfRating> ChangeOfRating { get; set; }
        public virtual DbSet<GameHistory> GameHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChessFederation>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<ChessFederation>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<ChessFederation>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<ChessFederation>()
                .HasMany(e => e.Club)
                .WithRequired(e => e.ChessFederation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChessFederation>()
                .HasMany(e => e.Judge)
                .WithRequired(e => e.ChessFederation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChessFederation>()
                .HasMany(e => e.Tournament)
                .WithRequired(e => e.ChessFederation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.contact)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .HasMany(e => e.ClubResult)
                .WithRequired(e => e.Club)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Club>()
                .HasMany(e => e.Player)
                .WithRequired(e => e.Club)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Club>()
                .HasMany(e => e.RegistrationForTournament)
                .WithRequired(e => e.Club)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClubResult>()
                .Property(e => e.score)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Game>()
                .HasMany(e => e.Pairs)
                .WithRequired(e => e.Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GameRecord>()
                .Property(e => e.result)
                .IsUnicode(false);

            modelBuilder.Entity<GameRecord>()
                .HasMany(e => e.ChangeOfRating)
                .WithRequired(e => e.GameRecord)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GameRecord>()
                .HasMany(e => e.GameHistory)
                .WithRequired(e => e.GameRecord)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Judge>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<Judge>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<Judge>()
                .Property(e => e.contact)
                .IsUnicode(false);

            modelBuilder.Entity<Judge>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Judge>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Judge>()
                .HasMany(e => e.Tournament)
                .WithRequired(e => e.Judge)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.contact)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.rating)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Player>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Pairs)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.player1_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.Pairs1)
                .WithRequired(e => e.Player1)
                .HasForeignKey(e => e.player2_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.ChangeOfRating)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.GameHistory)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.PlayerResult)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.RegistrationForTournament)
                .WithRequired(e => e.Player)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlayerResult>()
                .Property(e => e.score)
                .HasPrecision(18, 0);

            modelBuilder.Entity<RegistrationForTournament>()
                .Property(e => e.registerByClub)
                .IsUnicode(false);

            modelBuilder.Entity<Round>()
                .HasMany(e => e.Game)
                .WithRequired(e => e.Round)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Player)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tournament>()
                .Property(e => e.place)
                .IsUnicode(false);

            modelBuilder.Entity<Tournament>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<Tournament>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Tournament>()
                .HasMany(e => e.ClubResult)
                .WithRequired(e => e.Tournament)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tournament>()
                .HasMany(e => e.PlayerResult)
                .WithRequired(e => e.Tournament)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tournament>()
                .HasMany(e => e.RegistrationForTournament)
                .WithRequired(e => e.Tournament)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tournament>()
                .HasMany(e => e.Round)
                .WithRequired(e => e.Tournament)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChangeOfRating>()
                .Property(e => e.change)
                .HasPrecision(10, 2);
        }
    }
}
