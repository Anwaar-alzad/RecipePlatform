using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.DAL.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(r => r.Description)
                .HasMaxLength(1000);

            builder.Property(r => r.Ingredients)
                .IsRequired();

            builder.Property(r => r.Instructions)
                .IsRequired();

            builder.Property(r => r.PrepTimeMinutes).IsRequired();
            builder.Property(r => r.CookTimeMinutes).IsRequired();
            builder.Property(r => r.Servings).IsRequired();

            builder.Property(r => r.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(r => r.User)
                .WithMany(u => u.Recipe)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Category)
                .WithMany(c => c.Recipes)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
