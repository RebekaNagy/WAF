using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarService.Data
{
    public class ApplicationDbContext : IdentityDbContext<CarServiceUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            builder.Entity<Reservation>()
                .HasOne(r => r.Worksheet)
                .WithOne(w => w.Reservation)
                .HasForeignKey<Worksheet>(w => w.ReservationId);

            builder.Entity<Worksheet>()
                .HasOne(w => w.Reservation)
                .WithOne(r => r.Worksheet)
                .HasForeignKey<Reservation>(r => r.WorksheetId);

            builder.Entity<Reservation>()
                .HasOne<CarServiceUser>(r => r.Client)
                .WithMany(c => c.ReservationsOfClient)
                .HasForeignKey(r => r.ClientId);

            builder.Entity<Reservation>()
                .HasOne<CarServiceUser>(r => r.Mechanic)
                .WithMany(c => c.ReservationsOfMechanic)
                .HasForeignKey(r => r.MechanicId);

            builder.Entity<Worksheet>()
                .HasOne<CarServiceUser>(r => r.Client)
                .WithMany(c => c.WorksheetsOfClient)
                .HasForeignKey(r => r.ClientId);

            builder.Entity<Worksheet>()
                .HasOne<CarServiceUser>(r => r.Mechanic)
                .WithMany(c => c.WorksheetsOfMechanic)
                .HasForeignKey(r => r.MechanicId);

            builder.Entity<CostToWorksheet>().HasKey(cw => new { cw.CostId, cw.WorksheetId });

            builder.Entity<CostToWorksheet>()
                .HasOne<Cost>(cw => cw.Cost)
                .WithMany(c => c.CostToWorksheet)
                .HasForeignKey(cw => cw.CostId);

            builder.Entity<CostToWorksheet>()
                .HasOne<Worksheet>(cw => cw.Worksheet)
                .WithMany(w => w.CostToWorksheet)
                .HasForeignKey(cw => cw.WorksheetId);
        }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Worksheet> Worksheets { get; set; }

        public DbSet<Cost> Costs { get; set; }

        public DbSet<CostToWorksheet> CostsToWorksheets { get; set; }

    }
}
