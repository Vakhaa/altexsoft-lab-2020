using System;
using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class RecipeConfig :IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {

            if (builder == null) throw new ArgumentNullException(nameof(builder));
            builder.ToTable("Recipes");

            builder.HasKey(x => x.Id);

            builder.HasMany(x=>x.Ingredients).WithOne(x=>x.Recipe).HasForeignKey(x => x.RecipeId);
            builder.HasMany(x => x.StepsHowCooking).WithOne(x => x.Recipe).HasForeignKey(x => x.RecipeId);
        }
    }
}
