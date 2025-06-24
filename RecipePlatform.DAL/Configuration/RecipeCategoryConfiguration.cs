using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.DAL.Configurations
{
    public class RecipeCategoryConfiguration : IEntityTypeConfiguration<RecipeCategory>
    {
        public void Configure(EntityTypeBuilder<RecipeCategory> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(c => c.Recipes)
                .WithOne(r => r.Category)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
