using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipePlatform.Models;
using RecipePlatform.Models.RecipeModule;

namespace RecipePlatform.DAL.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rate)
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Recipe)
                .WithMany(rcp => rcp.Ratings)
                .HasForeignKey(r => r.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
