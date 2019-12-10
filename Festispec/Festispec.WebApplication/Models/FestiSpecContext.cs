namespace Festispec.WebApplication.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FestiSpecContext : DbContext
    {
        public FestiSpecContext()
            : base("name=FestiSpecContext")
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Antwoorden> Antwoorden { get; set; }
        public virtual DbSet<Beschikbaarheid_inspecteurs> Beschikbaarheid_inspecteurs { get; set; }
        public virtual DbSet<Contactpersoon> Contactpersoon { get; set; }
        public virtual DbSet<Inspectieformulier> Inspectieformulier { get; set; }
        public virtual DbSet<Klant> Klant { get; set; }
        public virtual DbSet<Offerte> Offerte { get; set; }
        public virtual DbSet<Opdracht> Opdracht { get; set; }
        public virtual DbSet<Rapport_template> Rapport_template { get; set; }
        public virtual DbSet<Rol_lookup> Rol_lookup { get; set; }
        public virtual DbSet<Status_lookup> Status_lookup { get; set; }
        public virtual DbSet<Vraag> Vraag { get; set; }
        public virtual DbSet<Vraag_mogelijk_antwoord> Vraag_mogelijk_antwoord { get; set; }
        public virtual DbSet<Vraagtype_lookup> Vraagtype_lookup { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Antwoorden)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.InspecteurID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Beschikbaarheid_inspecteurs)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.MedewerkerID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Inspectieformulier)
                .WithMany(e => e.Account)
                .Map(m => m.ToTable("Ingeplande_inspecteurs").MapLeftKey("InspecteurID").MapRightKey("OpdrachtID"));

            modelBuilder.Entity<Antwoorden>()
                .Property(e => e.Antwoord_text)
                .IsUnicode(false);

            modelBuilder.Entity<Contactpersoon>()
                .Property(e => e.Notities)
                .IsUnicode(false);

            modelBuilder.Entity<Inspectieformulier>()
                .Property(e => e.Beschrijving)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .HasMany(e => e.Contactpersoon)
                .WithRequired(e => e.Klant)
                .HasForeignKey(e => e.KlantID);

            modelBuilder.Entity<Klant>()
                .HasMany(e => e.Opdracht)
                .WithRequired(e => e.Klant)
                .HasForeignKey(e => e.KlantID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Offerte>()
                .Property(e => e.Totaalbedrag)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Offerte>()
                .Property(e => e.Beschrijving)
                .IsUnicode(false);

            modelBuilder.Entity<Offerte>()
                .Property(e => e.Klantbeslissing_reden)
                .IsUnicode(false);

            modelBuilder.Entity<Opdracht>()
                .Property(e => e.Klantwensen)
                .IsUnicode(false);

            modelBuilder.Entity<Opdracht>()
                .Property(e => e.Gebruikte_rechtsgebieden)
                .IsUnicode(false);

            modelBuilder.Entity<Opdracht>()
                .Property(e => e.Rapportage)
                .IsUnicode(false);

            modelBuilder.Entity<Rapport_template>()
                .Property(e => e.TemplateText)
                .IsUnicode(false);

            modelBuilder.Entity<Rapport_template>()
                .HasMany(e => e.Opdracht)
                .WithOptional(e => e.Rapport_template)
                .HasForeignKey(e => e.Rapportage_uses_template);

            modelBuilder.Entity<Rol_lookup>()
                .HasMany(e => e.Account)
                .WithRequired(e => e.Rol_lookup)
                .HasForeignKey(e => e.Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status_lookup>()
                .HasMany(e => e.Opdracht)
                .WithRequired(e => e.Status_lookup)
                .HasForeignKey(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vraag>()
                .Property(e => e.Vraagstelling)
                .IsUnicode(false);

            modelBuilder.Entity<Vraag>()
                .HasMany(e => e.Antwoorden)
                .WithRequired(e => e.Vraag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vraag>()
                .HasMany(e => e.Vraag_mogelijk_antwoord)
                .WithRequired(e => e.Vraag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vraag_mogelijk_antwoord>()
                .Property(e => e.Antwoord_text)
                .IsUnicode(false);

            modelBuilder.Entity<Vraagtype_lookup>()
                .HasMany(e => e.Vraag)
                .WithRequired(e => e.Vraagtype_lookup)
                .HasForeignKey(e => e.Vraagtype)
                .WillCascadeOnDelete(false);
        }
    }
}
