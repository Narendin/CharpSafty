using CardStorageService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardStorageService.Data.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards");
            builder.HasKey(card => card.Id);
            builder.Property(card => card.Id).ValueGeneratedOnAdd();

            builder.HasOne(card => card.Client)
                .WithMany(client => client.Cards)
                .HasForeignKey(card => card.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}