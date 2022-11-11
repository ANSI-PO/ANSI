using Microsoft.EntityFrameworkCore;

namespace Database.Models.Database
{
    public partial class ANSIContext : DbContext
    {
        public ANSIContext()
        {
        }

        public ANSIContext(DbContextOptions<ANSIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dish> Dishes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=ANSI;uid=root;pwd=DevUserPassword", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
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
