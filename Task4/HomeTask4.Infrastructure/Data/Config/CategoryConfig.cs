using System;
using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(s => s.Id).HasColumnName("Id");
            builder.Property(s => s.Name).HasColumnName("Name");
            builder.Property(s => s.ParentId).HasColumnName("ParentId");
            builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.Id);
            builder.HasMany(x => x.Children).WithOne(x => x.Parent).HasForeignKey(x => x.ParentId);
        }
    }
}
