﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Harmony.Domain.Entities;
using Harmony.Domain.Enums;

namespace Harmony.Persistence.Configurations
{
    /// <summary>
    /// EF Core entity configuration for Cards
    /// </summary>
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards");

            builder.Property(c => c.BoardListId).IsRequired();

            builder.Property(c => c.Status).IsRequired().HasDefaultValue(CardStatus.Active);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(300);

            builder.Property(c => c.Position).IsRequired();

            builder.HasMany(c => c.Comments)
                .WithOne(c => c.Card)
                .HasForeignKey(c => c.CardId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.CheckLists)
                .WithOne(c => c.Card)
                .HasForeignKey(c => c.CardId);

            builder.HasMany(c => c.Activities)
                .WithOne(c => c.Card)
                .HasForeignKey(c => c.CardId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(c => c.Attachments)
                .WithOne(a => a.Card)
                .HasForeignKey(a => a.CardId);
        }
    }
}
