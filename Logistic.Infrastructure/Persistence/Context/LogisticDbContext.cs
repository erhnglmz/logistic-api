using System;
using System.Collections.Generic;
using Logistic.Infrastructure.Persistence.Scaffolded.Models;
using Microsoft.EntityFrameworkCore;

namespace Logistic.Infrastructure.Persistence.Context;

public partial class LogisticDbContext : DbContext
{
    public LogisticDbContext(DbContextOptions<LogisticDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyBankAccount> CompanyBankAccounts { get; set; }

    public virtual DbSet<CompanyContact> CompanyContacts { get; set; }

    public virtual DbSet<CompanyNote> CompanyNotes { get; set; }

    public virtual DbSet<CompanyRole> CompanyRoles { get; set; }

    public virtual DbSet<SchemaVersion> SchemaVersions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CompanyPkey");

            entity.ToTable("Company");

            entity.HasIndex(e => e.IsActive, "IxCompanyIsActive");

            entity.HasIndex(e => e.Name, "IxCompanyName");

            entity.HasIndex(e => e.NationalId, "UqCompanyNationalId").IsUnique();

            entity.HasIndex(e => e.TaxNumber, "UqCompanyTaxNumber").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("Id");
            entity.Property(e => e.AccountingContactInfo).HasColumnName("AccountingContactInfo");
            entity.Property(e => e.Address).HasColumnName("Address");
            entity.Property(e => e.CompanyType)
                .HasMaxLength(50)
                .HasColumnName("CompanyType");
            entity.Property(e => e.CreatedById).HasColumnName("CreatedById");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("CreatedDate");
            entity.Property(e => e.DefaultPaymentTermDays).HasColumnName("DefaultPaymentTermDays");
            entity.Property(e => e.DefaultVatRate)
                .HasPrecision(5, 2)
                .HasColumnName("DefaultVatRate");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .HasColumnName("Email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("IsActive");
            entity.Property(e => e.ModifiedById).HasColumnName("ModifiedById");
            entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("Name");
            entity.Property(e => e.NationalId)
                .HasMaxLength(11)
                .HasColumnName("NationalId");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("Phone");
            entity.Property(e => e.TaxNumber)
                .HasMaxLength(20)
                .HasColumnName("TaxNumber");
            entity.Property(e => e.TaxOffice)
                .HasMaxLength(100)
                .HasColumnName("TaxOffice");
        });

        modelBuilder.Entity<CompanyBankAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CompanyBankAccountPkey");

            entity.ToTable("CompanyBankAccount");

            entity.HasIndex(e => e.CompanyId, "IxCompanyBankAccountCompanyId");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("Id");
            entity.Property(e => e.BankName)
                .HasMaxLength(100)
                .HasColumnName("BankName");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyId");
            entity.Property(e => e.CreatedById).HasColumnName("CreatedById");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("CreatedDate");
            entity.Property(e => e.Iban)
                .HasMaxLength(34)
                .HasColumnName("Iban");
            entity.Property(e => e.IsDefault).HasColumnName("IsDefault");
            entity.Property(e => e.ModifiedById).HasColumnName("ModifiedById");
            entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyBankAccounts)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("CompanyBankAccountCompanyIdFkey");
        });

        modelBuilder.Entity<CompanyContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CompanyContactPkey");

            entity.ToTable("CompanyContact");

            entity.HasIndex(e => e.CompanyId, "IxCompanyContactCompanyId");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("Id");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyId");
            entity.Property(e => e.CreatedById).HasColumnName("CreatedById");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("CreatedDate");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .HasColumnName("Email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("FirstName");
            entity.Property(e => e.IsPrimary).HasColumnName("IsPrimary");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("LastName");
            entity.Property(e => e.ModifiedById).HasColumnName("ModifiedById");
            entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("Phone");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("Title");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyContacts)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("CompanyContactCompanyIdFkey");
        });

        modelBuilder.Entity<CompanyNote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CompanyNotePkey");

            entity.ToTable("CompanyNote");

            entity.HasIndex(e => e.CompanyId, "IxCompanyNoteCompanyId");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("Id");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyId");
            entity.Property(e => e.ContactDate).HasColumnName("ContactDate");
            entity.Property(e => e.CreatedById).HasColumnName("CreatedById");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("CreatedDate");
            entity.Property(e => e.ModifiedById).HasColumnName("ModifiedById");
            entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate");
            entity.Property(e => e.NoteText).HasColumnName("NoteText");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyNotes)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("CompanyNoteCompanyIdFkey");
        });

        modelBuilder.Entity<CompanyRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CompanyRolePkey");

            entity.ToTable("CompanyRole");

            entity.HasIndex(e => e.Role, "IxCompanyRoleRole");

            entity.HasIndex(e => new { e.CompanyId, e.Role }, "UqCompanyRoleCompanyRole").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("Id");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyId");
            entity.Property(e => e.CreatedById).HasColumnName("CreatedById");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("CreatedDate");
            entity.Property(e => e.ModifiedById).HasColumnName("ModifiedById");
            entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("Role");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyRoles)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("CompanyRoleCompanyIdFkey");
        });

        modelBuilder.Entity<SchemaVersion>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("SchemaVersionPkey");

            entity.ToTable("SchemaVersion");

            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasColumnName("Version");
            entity.Property(e => e.AppliedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("AppliedAt");
            entity.Property(e => e.Description).HasColumnName("Description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
