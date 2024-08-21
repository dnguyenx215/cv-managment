using CVManagementAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CVManagementAPI.Repository
{
    public class CVManagementDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<CV> CV { get; set; }
        public DbSet<Source> Source { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<ColumnLayout> ColumnLayout { get; set; }
        public DbSet<ColumnRelationship> ColumnRelationship { get; set; }
        public DbSet<EmailMessage> EmailMessage { get; set; }

        public CVManagementDbContext(DbContextOptions<CVManagementDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationships between entities
            modelBuilder.Entity<CV>()
                .HasOne(c => c.Position)
                .WithMany(p => p.CVs)
                .HasForeignKey(c => c.PositionId);


            modelBuilder.Entity<CV>()
                .HasOne(c => c.Source)
                .WithMany(s => s.CVs)
                .HasForeignKey(c => c.SourceId);

            modelBuilder.Entity<CV>()
                .HasOne(c => c.AppUser)
                .WithMany(a => a.CVs)
            .HasForeignKey(c => c.AppUserId);

            modelBuilder.Entity<CV>().HasOne(c => c.ColumnLayout).
                WithMany(a => a.CVs).
                HasForeignKey(c => c.ColumnId);

            modelBuilder.Entity<ColumnRelationship>()
                      .HasOne(r => r.PutColumn)
                      .WithMany(p => p.PutColumns)
                      .HasForeignKey(r => r.PutColumnId);

            modelBuilder.Entity<ColumnRelationship>()
                     .HasOne(r => r.PullColumn)
                     .WithMany(p => p.PullColumns)
                     .HasForeignKey(r => r.PullColumnId);

            modelBuilder.Entity<ColumnLayout>()
           .HasOne(pd => pd.EmailMessage)
           .WithOne(p => p.ColumnLayout)
           .HasForeignKey<ColumnLayout>(pd => pd.EmailMessageId)
           .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<ColumnRelationship>().HasOne(c => c.ColumnOwnRelationship).
                WithMany(a => a.Relationships).
                HasForeignKey(c => c.ColumnLayoutId);



        }
    }
}