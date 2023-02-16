using AppProjetFilRouge.Data.Entities;
using AppProjetFilRouge.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppProjetFilRouge.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Level> Levels { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Technology> Technologies { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<QuestionType> QuestionTypes { get; set; }
		public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
		public DbSet<Quiz> Quizzes { get; set; }
		public DbSet<QuizResult> QuizResults { get; set; }
		public DbSet<UserAnswer> UserAnswers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
		}
	}



	public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(u => u.FirstName).HasMaxLength(75);
			builder.Property(u => u.LastName).HasMaxLength(75);
			builder.Property(u => u.BirthDate);
			builder.Property(u => u.HandleBy).HasMaxLength(450);
		}
	}

}