using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Templaty.Postgres;

public sealed class ContentConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.ToTable("contents");
        
        builder.HasKey(x => x.Path);

        builder.Property(x => x.Path)
            .IsRequired()
            .HasMaxLength(Content.PathMaxLength);

        builder.Property(x => x.Body)
            .HasColumnType("text");

        builder.HasIndex(x => x.Path)
            .IsUnique();
    }
}