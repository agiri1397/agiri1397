using MauiBlazorCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MauiBlazorCleanArchitecture.Infrastructure.Persistence.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("TodoItems");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title).IsRequired().HasMaxLength(256);
        builder.Property(t => t.Notes).HasMaxLength(2000);
    }
}
