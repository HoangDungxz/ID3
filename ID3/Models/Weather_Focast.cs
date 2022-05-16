using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ID3.Models
{
    public partial class Weather_Focast : DbContext
    {
        public Weather_Focast()
            : base("name=Weather_Focast")
        {
        }

        public virtual DbSet<Humididity> Humididities { get; set; }
        public virtual DbSet<Precipitation> Precipitations { get; set; }
        public virtual DbSet<Prediction> Predictions { get; set; }
        public virtual DbSet<Temperature> Temperatures { get; set; }
        public virtual DbSet<Weather> Weathers { get; set; }
        public virtual DbSet<Wind> Winds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Humididity>()
                .Property(e => e.Value)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Humididity>()
                .HasMany(e => e.Predictions)
                .WithRequired(e => e.Humididity)
                .HasForeignKey(e => e.HumidityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Precipitation>()
                .Property(e => e.Value)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Precipitation>()
                .HasMany(e => e.Predictions)
                .WithRequired(e => e.Precipitation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Temperature>()
                .Property(e => e.Value)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Temperature>()
                .HasMany(e => e.Predictions)
                .WithRequired(e => e.Temperature)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Weather>()
                .HasMany(e => e.Predictions)
                .WithRequired(e => e.Weather)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Wind>()
                .Property(e => e.Value)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Wind>()
                .HasMany(e => e.Predictions)
                .WithRequired(e => e.Wind)
                .WillCascadeOnDelete(false);
        }
    }
}
