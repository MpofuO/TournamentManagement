using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<TournamentTeam> TournamentTeams { get; set; }
    public DbSet<MatchTeam> MatchTeams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tournament>()
            .HasMany(t => t.TournamentTeams)
            .WithOne(tt => tt.Tournament)
            .HasForeignKey(tt => tt.TournamentId);

        modelBuilder.Entity<Tournament>()
            .HasMany(t => t.Matches)
            .WithOne(m => m.Tournament)
            .HasForeignKey(m => m.TournamentId);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.TournamentTeams)
            .WithOne(tt => tt.Team)
            .HasForeignKey(tt => tt.TeamID);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.MatchTeams)
            .WithOne(mt => mt.Team)
            .HasForeignKey(mt => mt.TeamID);

        modelBuilder.Entity<Match>()
            .HasMany(m => m.MatchTeams)
            .WithOne(mt => mt.Match)
            .HasForeignKey(mt => mt.MatchId);
    }

}
