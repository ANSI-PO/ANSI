using Database.Models.Database;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Database.Infrastructure
{
    internal partial class ANSIContext : DbContext
    {
        private readonly MySqlConnection _connection;

        public ANSIContext(MySqlConnection con)
        {
            _connection = con;
        }

        public ANSIContext(DbContextOptions<ANSIContext> options, MySqlConnection con)
            : base(options)
        {
            _connection = con;
        }

        public virtual DbSet<Dish> Dishes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(_connection, ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("DISH");

                entity.Property(e => e.DishId).HasColumnName("DISH_ID");

                entity.Property(e => e.DishName)
                    .HasMaxLength(150)
                    .HasColumnName("DISH_NAME");

                entity.Property(e => e.DishRecipeUrl)
                    .HasMaxLength(300)
                    .HasColumnName("DISH_RECIPE_URL");

                entity.Property(e => e.IngredientsCategory)
                    .HasMaxLength(50)
                    .HasColumnName("INGREDIENTS_CATEGORY");

                entity.Property(e => e.IngredientsTags)
                    .HasMaxLength(300)
                    .HasColumnName("INGREDIENTS_TAGS");

                entity.Property(e => e.MainCategory)
                    .HasMaxLength(100)
                    .HasColumnName("MAIN_CATEGORY");

                entity.Property(e => e.MakeTimeMin).HasColumnName("MAKE_TIME_MIN");

                entity.Property(e => e.PreparationDifficulty)
                    .HasMaxLength(50)
                    .HasColumnName("PREPARATION_DIFFICULTY");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}