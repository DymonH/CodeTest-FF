using Finflow.Hlopov.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finflow.Hlopov.Infrastructure.Data
{
    public class FinflowContext : DbContext
    {
        public FinflowContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Remittance> Remittances { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Client>(ConfigureClient);
            builder.Entity<Currency>(ConfigureCurrency);
            builder.Entity<Remittance>(ConfigureRemittance);
            builder.Entity<RemittanceStatus>(ConfigureRemittanceStatus);
            builder.Entity<RemittanceStatuses>(ConfigureRemittanceStatuses);
            builder.Entity<Status>(ConfigureStatus);
        }

        private void ConfigureClient(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");
            builder.HasKey(ci => ci.Id);
            builder.HasIndex(ci => ci.Name);
            builder.HasIndex(ci => ci.Surname);

            builder.Property(ci => ci.Id)
                .ValueGeneratedOnAdd()
                .UseHiLo("finflow_client_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ci => ci.Surname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ci => ci.DateOfBirth)
                .IsRequired();
        }

        private void ConfigureCurrency(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currency");
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .ValueGeneratedNever();

            builder.Property(ci => ci.Value)
                .IsRequired();
        }

        private void ConfigureRemittance(EntityTypeBuilder<Remittance> builder)
        {
            builder.ToTable("Remittance");
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Code)
                .IsRequired();

            builder.Property(ci => ci.Code)
                .IsRequired();

            builder.Property(ci => ci.ReceiveCurrencyId)
                .IsRequired();

            builder.Property(ci => ci.ReceiverId)
                .IsRequired();

            builder.Property(ci => ci.SendCurrencyId)
                .IsRequired();

            builder.Property(ci => ci.SenderId)
                .IsRequired();

            builder.Property(ci => ci.Rate).HasColumnType("decimal(18, 2)");
            builder.Property(ci => ci.ReceiveAmount).HasColumnType("decimal(18, 2)");
            builder.Property(ci => ci.SendAmount).HasColumnType("decimal(18, 2)");
        }
        
        private void ConfigureRemittanceStatus(EntityTypeBuilder<RemittanceStatus> builder)
        {
            builder.ToTable("RemittanceStatus");
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.StatusId)
                .IsRequired();
        }

        private void ConfigureRemittanceStatuses(EntityTypeBuilder<RemittanceStatuses> builder)
        {
            builder.ToTable("RemittanceStatuses");
            builder.HasKey(ci => new { ci.RemittanceId, ci.RemittanceStatusId });
        }

        private void ConfigureStatus(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status");
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .ValueGeneratedNever();

            builder.Property(ci => ci.Value)
                .IsRequired();
        }
    }
}