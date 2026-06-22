using IntelliResumeAI.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelliResumeAI.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options
        ) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Resume> Resumes => Set<Resume>();

        public DbSet<Report> Reports => Set<Report>();

        public DbSet<ResumeAnalysis> ResumeAnalyses => Set<ResumeAnalysis>();
    }
}