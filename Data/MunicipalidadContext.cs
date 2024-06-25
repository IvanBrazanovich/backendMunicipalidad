using Microsoft.EntityFrameworkCore;
using WebMunicipalidadTP.Models.MunicipalidadTPApi.Models;
using WebMunicipalidadTP.Models;

namespace MunicipalidadTPApi.Data
{
    public class MunicipalidadContext : DbContext
    {
        public MunicipalidadContext(DbContextOptions<MunicipalidadContext> options) : base(options) { }

        public DbSet<Bien> Bienes { get; set; }
        public DbSet<Propietario> Propietarios { get; set; }
        public DbSet<Cuota> Cuotas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<BienPropietario> BienesPropietarios { get; set; }
        public DbSet<CDCuotaestado> CuotaEstados { get; set; }
        public DbSet<CDest_civ> EstadosCiviles { get; set; }
        public DbSet<CDtipoBien> TiposBienes { get; set; }
        public DbSet<PagoPropietario> PagosPropietarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BienPropietario>()
                .HasKey(bp => new { bp.Id_Bien, bp.Id_Propietario });

            modelBuilder.Entity<PagoPropietario>()
                .HasKey(pp => new { pp.Id_Pago, pp.Id_Propietario, pp.Id_Bien });

            modelBuilder.Entity<Bien>()
                .HasOne(b => b.TipoBien)
                .WithMany(tb => tb.Bienes)
                .HasForeignKey(b => b.Tipo);

            modelBuilder.Entity<BienPropietario>()
                .HasOne(bp => bp.Bien)
                .WithMany(b => b.BienesPropietarios)
                .HasForeignKey(bp => bp.Id_Bien);

            modelBuilder.Entity<BienPropietario>()
                .HasOne(bp => bp.Propietario)
                .WithMany(p => p.BienesPropietarios)
                .HasForeignKey(bp => bp.Id_Propietario);

            modelBuilder.Entity<Cuota>()
                .HasOne(c => c.Bien)
                .WithMany(b => b.Cuotas)
                .HasForeignKey(c => c.Id_bien);

            modelBuilder.Entity<Cuota>()
                .HasOne(c => c.Estado)
                .WithMany(e => e.Cuotas)
                .HasForeignKey(c => c.CodEstado);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Cuota)
                .WithMany(c => c.Pagos)
                .HasForeignKey(p => p.Id_cuota);

            modelBuilder.Entity<PagoPropietario>()
                .HasOne(pp => pp.Bien)
                .WithMany(b => b.PagosPropietarios)
                .HasForeignKey(pp => pp.Id_Bien);

            modelBuilder.Entity<PagoPropietario>()
                .HasOne(pp => pp.Propietario)
                .WithMany(p => p.PagosPropietarios)
                .HasForeignKey(pp => pp.Id_Propietario);

            modelBuilder.Entity<PagoPropietario>()
                .HasOne(pp => pp.Pago)
                .WithMany(p => p.PagosPropietarios)
                .HasForeignKey(pp => pp.Id_Pago);

            modelBuilder.Entity<Propietario>()
                .HasOne(p => p.EstadoCivil)
                .WithMany(ec => ec.Propietarios)
                .HasForeignKey(p => p.Cod_estado_Civil);
        }
    }
}
