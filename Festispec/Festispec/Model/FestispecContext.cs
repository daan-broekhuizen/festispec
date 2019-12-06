namespace Festispec.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FestispecContext : DbContext
    {
        public FestispecContext()
            : base("name=FestiSpecEntities")
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Antwoorden> Antwoorden { get; set; }
        public virtual DbSet<BeschikbaarheidInspecteurs> BeschikbaarheidInspecteurs { get; set; }
        public virtual DbSet<Contactpersoon> Contactpersoon { get; set; }
        public virtual DbSet<Inspectieformulier> Inspectieformulier { get; set; }
        public virtual DbSet<Klant> Klant { get; set; }
        public virtual DbSet<Offerte> Offerte { get; set; }
        public virtual DbSet<Opdracht> Opdracht { get; set; }
        public virtual DbSet<RapportTemplate> RapportTemplate { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Vraag> Vraag { get; set; }
        public virtual DbSet<VraagMogelijkAntwoord> VraagMogelijkAntwoord { get; set; }
        public virtual DbSet<VraagType> VraagType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Antwoorden)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.InspecteurID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.BeschikbaarheidInspecteurs)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.MedewerkerID);

            //modelBuilder.Entity<Account>()
            //    .HasMany(e => e.Opdracht)
            //    .WithRequired(e => e.Account)
            //    .HasForeignKey(e => e.MedewerkerID)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Ingepland)
                .WithMany(e => e.Ingepland)
                .Map(m => m.ToTable("Ingeplande_inspecteurs").MapLeftKey("InspecteurID").MapRightKey("OpdrachtID"));

            modelBuilder.Entity<Antwoorden>()
                .Property(e => e.AntwoordText)
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
                .Property(e => e.KlantbeslissingReden)
                .IsUnicode(false);

            modelBuilder.Entity<Opdracht>()
                .Property(e => e.Klantwensen)
                .IsUnicode(false);

            modelBuilder.Entity<Opdracht>()
                .Property(e => e.GebruikteRechtsgebieden)
                .IsUnicode(false);

            modelBuilder.Entity<Opdracht>()
                .Property(e => e.Rapportage)
                .IsUnicode(false);

            modelBuilder.Entity<RapportTemplate>()
                .Property(e => e.TemplateText)
                .IsUnicode(false);

            modelBuilder.Entity<RapportTemplate>()
                .HasMany(e => e.Opdracht)
                .WithOptional(e => e.RapportTemplate)
                .HasForeignKey(e => e.RapportageUsesTemplate);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.Account)
                .WithRequired(e => e.RolType)
                .HasForeignKey(e => e.Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Opdracht)
                .WithRequired(e => e.StatusLookup)
                .HasForeignKey(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vraag>()
                .Property(e => e.Vraagstelling)
                .IsUnicode(false);

            modelBuilder.Entity<Vraag>()
                .HasMany(e => e.VraagMogelijkAntwoord)
                .WithRequired(e => e.Vraag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VraagMogelijkAntwoord>()
                .Property(e => e.AntwoordText)
                .IsUnicode(false);

            modelBuilder.Entity<VraagType>()
                .HasMany(e => e.Vraag)
                .WithRequired(e => e.VraagtypeLookup)
                .HasForeignKey(e => e.Vraagtype)
                .WillCascadeOnDelete(false);
        }
    }
}
