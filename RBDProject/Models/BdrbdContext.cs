using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RBDProject.Models;

public partial class BdrbdContext : DbContext
{
    public BdrbdContext()
    {
    }

    public BdrbdContext(DbContextOptions<BdrbdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RbdArticulo> RbdArticulos { get; set; }

    public virtual DbSet<RbdCalle> RbdCalles { get; set; }

    public virtual DbSet<RbdCargo> RbdCargos { get; set; }

    public virtual DbSet<RbdCiudade> RbdCiudades { get; set; }

    public virtual DbSet<RbdCliente> RbdClientes { get; set; }

    public virtual DbSet<RbdComprobanteFiscal> RbdComprobanteFiscals { get; set; }

    public virtual DbSet<RbdCuentasPorCobrar> RbdCuentasPorCobrars { get; set; }

    public virtual DbSet<RbdDetalleCuentaPorCobrar> RbdDetalleCuentaPorCobrars { get; set; }

    public virtual DbSet<RbdDetalleFactura> RbdDetalleFacturas { get; set; }

    public virtual DbSet<RbdEmpleado> RbdEmpleados { get; set; }

    public virtual DbSet<RbdEstado> RbdEstados { get; set; }

    public virtual DbSet<RbdFactura> RbdFacturas { get; set; }

    public virtual DbSet<RbdGenero> RbdGeneros { get; set; }

    public virtual DbSet<RbdGrupo> RbdGrupos { get; set; }

    public virtual DbSet<RbdListaDePrecio> RbdListaDePrecios { get; set; }

    public virtual DbSet<RbdTelefonoCliente> RbdTelefonoClientes { get; set; }

    public virtual DbSet<RbdTelefonoEmpleado> RbdTelefonoEmpleados { get; set; }

    public virtual DbSet<RbdTipoComprobante> RbdTipoComprobantes { get; set; }

    public virtual DbSet<RbdTipoPago> RbdTipoPagos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Edwin; Database=BDRBD; Trusted_Connection=true; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RbdArticulo>(entity =>
        {
            entity.HasKey(e => e.CodArt).HasName("PK__RBD_Arti__0025E2D68835E800");

            entity.ToTable("RBD_Articulo");

            entity.HasIndex(e => e.IdArt, "UQ__RBD_Arti__29146C1B2F4C2956").IsUnique();

            entity.Property(e => e.CodArt).HasColumnName("Cod_art");
            entity.Property(e => e.CodEst).HasColumnName("Cod_est");
            entity.Property(e => e.CodGrup).HasColumnName("Cod_grup");
            entity.Property(e => e.DesArt)
                .HasColumnType("text")
                .HasColumnName("Des_art");
            entity.Property(e => e.ExistArt).HasColumnName("Exist_art");
            entity.Property(e => e.FecArt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fec_art");
            entity.Property(e => e.IdArt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Id_art");
            entity.Property(e => e.NomArt)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nom_art");

            entity.HasOne(d => d.CodGrupNavigation).WithMany(p => p.RbdArticulos)
                .HasForeignKey(d => d.CodGrup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Articulo_RBD_Grupo");
        });

        modelBuilder.Entity<RbdCalle>(entity =>
        {
            entity.HasKey(e => e.IdCalle).HasName("PK__RBD_Call__13C5F0BED285E025");

            entity.ToTable("RBD_Calle");

            entity.Property(e => e.IdCalle).HasColumnName("Id_calle");
            entity.Property(e => e.NomCalle)
                .HasColumnType("text")
                .HasColumnName("Nom_calle");
        });

        modelBuilder.Entity<RbdCargo>(entity =>
        {
            entity.HasKey(e => e.CodCar).HasName("PK__RBD_Carg__3E81B77CF6618E22");

            entity.ToTable("RBD_Cargo");

            entity.Property(e => e.CodCar).HasColumnName("Cod_car");
            entity.Property(e => e.CodEst).HasColumnName("Cod_est");
            entity.Property(e => e.DesCar)
                .HasColumnType("text")
                .HasColumnName("Des_car");
            entity.Property(e => e.FecCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fec_creacion");
            entity.Property(e => e.NomCar)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nom_car");

            entity.HasOne(d => d.CodEstNavigation).WithMany(p => p.RbdCargos)
                .HasForeignKey(d => d.CodEst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Cargo_RBD_Estado");
        });

        modelBuilder.Entity<RbdCiudade>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("PK__RBD_Ciud__E2EF25D6D12A7A3F");

            entity.ToTable("RBD_Ciudades");

            entity.Property(e => e.IdCiudad).HasColumnName("Id_ciudad");
            entity.Property(e => e.CodPostal)
                .HasColumnType("text")
                .HasColumnName("Cod_postal");
            entity.Property(e => e.NomCiudad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nom_ciudad");
        });

        modelBuilder.Entity<RbdCliente>(entity =>
        {
            entity.HasKey(e => e.CodCli).HasName("PK__RBD_Clie__3E81CFCF7C6B7450");

            entity.ToTable("RBD_Clientes");

            entity.HasIndex(e => e.IdCli, "UQ__RBD_Clie__56642C0697B308FD").IsUnique();

            entity.Property(e => e.CodCli).HasColumnName("Cod_cli");
            entity.Property(e => e.CodGen).HasColumnName("Cod_gen");
            entity.Property(e => e.CorrCli)
                .HasColumnType("text")
                .HasColumnName("Corr_cli");
            entity.Property(e => e.DetallDirec)
                .HasColumnType("text")
                .HasColumnName("Detall_direc");
            entity.Property(e => e.DniCli)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Dni_cli");
            entity.Property(e => e.FecEnt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdCalle).HasColumnName("Id_calle");
            entity.Property(e => e.IdCiudad).HasColumnName("Id_ciudad");
            entity.Property(e => e.IdCli)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Id_cli");
            entity.Property(e => e.NomCli)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nom_cli");

            entity.HasOne(d => d.CodGenNavigation).WithMany(p => p.RbdClientes)
                .HasForeignKey(d => d.CodGen)
                .HasConstraintName("FK_RBD_Clientes_RBD_Genero");

            entity.HasOne(d => d.IdCalleNavigation).WithMany(p => p.RbdClientes)
                .HasForeignKey(d => d.IdCalle)
                .HasConstraintName("FK_RBD_Clientes_RBD_Calle");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.RbdClientes)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("FK_RBD_Clientes_RBD_Ciudades");
        });

        modelBuilder.Entity<RbdComprobanteFiscal>(entity =>
        {
            entity.HasKey(e => e.CodNcf).HasName("PK__RBD_Comp__3CC4FB6B7B7C9BCE");

            entity.ToTable("RBD_Comprobante_Fiscal");

            entity.Property(e => e.CodNcf).HasColumnName("Cod_ncf");
            entity.Property(e => e.CodTipocom).HasColumnName("Cod_tipocom");
            entity.Property(e => e.DesCom)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Des_com");
            entity.Property(e => e.DocFec)
                .HasColumnType("datetime")
                .HasColumnName("Doc_fec");
            entity.Property(e => e.FechaVec)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_vec");
            entity.Property(e => e.ImpCom).HasColumnName("Imp_com");
            entity.Property(e => e.SecCom)
                .HasColumnType("text")
                .HasColumnName("Sec_com");

            entity.HasOne(d => d.CodTipocomNavigation).WithMany(p => p.RbdComprobanteFiscals)
                .HasForeignKey(d => d.CodTipocom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Comprobante_Fiscal_RBD_Tipo_Comprobante");
        });

        modelBuilder.Entity<RbdCuentasPorCobrar>(entity =>
        {
            entity.HasKey(e => e.CodCcobro).HasName("PK__RBD_Cuen__29036C5B5DD294E8");

            entity.ToTable("RBD_Cuentas_Por_Cobrar");

            entity.HasIndex(e => e.NumFact, "UQ__RBD_Cuen__EB7B4E033550FEE7").IsUnique();

            entity.Property(e => e.CodCcobro).HasColumnName("Cod_ccobro");
            entity.Property(e => e.CodEm).HasColumnName("Cod_em");
            entity.Property(e => e.FechaEmi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_emi");
            entity.Property(e => e.NumFact).HasColumnName("Num_fact");
            entity.Property(e => e.TotalFact).HasColumnName("Total_fact");
            entity.Property(e => e.VencPago)
                .HasColumnType("datetime")
                .HasColumnName("Venc_pago");

            entity.HasOne(d => d.NumFactNavigation).WithOne(p => p.RbdCuentasPorCobrar)
                .HasForeignKey<RbdCuentasPorCobrar>(d => d.NumFact)
                .HasConstraintName("FK_RBD_Cuentas_Por_Cobrar_RBD_Factura");
        });

        modelBuilder.Entity<RbdDetalleCuentaPorCobrar>(entity =>
        {
            entity.HasKey(e => new { e.CodCcobro, e.FechaPago }).HasName("PK__RBD_Deta__13EA942F44E86F5D");

            entity.ToTable("RBD_Detalle_Cuenta_Por_cobrar");

            entity.Property(e => e.CodCcobro).HasColumnName("Cod_ccobro");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_pago");
            entity.Property(e => e.CodEm).HasColumnName("Cod_em");
            entity.Property(e => e.CodTippago).HasColumnName("Cod_tippago");

            entity.HasOne(d => d.CodCcobroNavigation).WithMany(p => p.RbdDetalleCuentaPorCobrars)
                .HasForeignKey(d => d.CodCcobro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Detalle_Cuenta_Por_cobrar_RBD_Cuentas_Por_Cobrar");

            entity.HasOne(d => d.CodTippagoNavigation).WithMany(p => p.RbdDetalleCuentaPorCobrars)
                .HasForeignKey(d => d.CodTippago)
                .HasConstraintName("FK_RBD_Detalle_Cuenta_Por_cobrar_RBD_Tipo_Pago");
        });

        modelBuilder.Entity<RbdDetalleFactura>(entity =>
        {
            entity.HasKey(e => new { e.NumFac, e.CodArt }).HasName("PK__RBD_Deta__C62F1A6A37DDD5CA");

            entity.ToTable("RBD_Detalle_Factura");

            entity.Property(e => e.NumFac).HasColumnName("Num_fac");
            entity.Property(e => e.CodArt).HasColumnName("Cod_art");
            entity.Property(e => e.CantArt).HasColumnName("Cant_art");
            entity.Property(e => e.DescuentoArt).HasColumnName("Descuento_art");

            entity.HasOne(d => d.CodArtNavigation).WithMany(p => p.RbdDetalleFacturas)
                .HasForeignKey(d => d.CodArt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Detalle_Factura_RBD_Articulo");

            entity.HasOne(d => d.NumFacNavigation).WithMany(p => p.RbdDetalleFacturas)
                .HasForeignKey(d => d.NumFac)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Detalle_Factura_RBD_Factura");
        });

        modelBuilder.Entity<RbdEmpleado>(entity =>
        {
            entity.HasKey(e => e.CodEm).HasName("PK__RBD_Empl__DF7775DD1C1083B9");

            entity.ToTable("RBD_Empleado");

            entity.HasIndex(e => e.IdEm, "UQ__RBD_Empl__16EBBBA7B6ADF89D").IsUnique();

            entity.Property(e => e.CodEm).HasColumnName("Cod_em");
            entity.Property(e => e.CodCar).HasColumnName("Cod_Car");
            entity.Property(e => e.CodEst).HasColumnName("Cod_est");
            entity.Property(e => e.CodGen).HasColumnName("Cod_gen");
            entity.Property(e => e.DetallDirec)
                .HasColumnType("text")
                .HasColumnName("Detall_direc");
            entity.Property(e => e.DniEm)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Dni_em");
            entity.Property(e => e.IdCalle).HasColumnName("Id_calle");
            entity.Property(e => e.IdCiudad).HasColumnName("Id_ciudad");
            entity.Property(e => e.IdEm)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Id_em");
            entity.Property(e => e.NomClav)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nom_clav");
            entity.Property(e => e.NomEm)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nom_em");
            entity.Property(e => e.NomUs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nom_us");
            entity.Property(e => e.NumPer)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Num_per");

            entity.HasOne(d => d.CodCarNavigation).WithMany(p => p.RbdEmpleados)
                .HasForeignKey(d => d.CodCar)
                .HasConstraintName("FK_RBD_Empleado_RBD_Cargo");

            entity.HasOne(d => d.CodEstNavigation).WithMany(p => p.RbdEmpleados)
                .HasForeignKey(d => d.CodEst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Empleado_RBD_Estado");

            entity.HasOne(d => d.CodGenNavigation).WithMany(p => p.RbdEmpleados)
                .HasForeignKey(d => d.CodGen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Empleado_RBD_Genero");

            entity.HasOne(d => d.IdCalleNavigation).WithMany(p => p.RbdEmpleados)
                .HasForeignKey(d => d.IdCalle)
                .HasConstraintName("FK_RBD_Empleado_RBD_Calle");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.RbdEmpleados)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("FK_RBD_Empleado_RBD_Ciudades");
        });

        modelBuilder.Entity<RbdEstado>(entity =>
        {
            entity.HasKey(e => e.CodEst).HasName("PK__RBD_Esta__3D0C1CF11ADDC862");

            entity.ToTable("RBD_Estado");

            entity.Property(e => e.CodEst).HasColumnName("Cod_est");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.FecCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fec_creacion");
            entity.Property(e => e.NomEst)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nom_est");
        });

        modelBuilder.Entity<RbdFactura>(entity =>
        {
            entity.HasKey(e => e.NumFac).HasName("PK__RBD_Fact__B62D4447857032B6");

            entity.ToTable("RBD_Factura");

            entity.Property(e => e.NumFac).HasColumnName("Num_fac");
            entity.Property(e => e.CodCli).HasColumnName("Cod_cli");
            entity.Property(e => e.CodEm).HasColumnName("Cod_em");
            entity.Property(e => e.CodEst).HasColumnName("Cod_Est");
            entity.Property(e => e.CodNCf).HasColumnName("Cod_nCF");
            entity.Property(e => e.CodTipago).HasColumnName("Cod_tipago");
            entity.Property(e => e.FechaReg)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_reg");
            entity.Property(e => e.TotalBalance).HasColumnName("Total_balance");
            entity.Property(e => e.TotalNeto).HasColumnName("Total_neto");

            entity.HasOne(d => d.CodCliNavigation).WithMany(p => p.RbdFacturas)
                .HasForeignKey(d => d.CodCli)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Factura_RBD_Clientes");

            entity.HasOne(d => d.CodEmNavigation).WithMany(p => p.RbdFacturas)
                .HasForeignKey(d => d.CodEm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Factura_RBD_Empleado");

            entity.HasOne(d => d.CodEstNavigation).WithMany(p => p.RbdFacturas)
                .HasForeignKey(d => d.CodEst)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Factura_RBD_Estado");

            entity.HasOne(d => d.CodNCfNavigation).WithMany(p => p.RbdFacturas)
                .HasForeignKey(d => d.CodNCf)
                .HasConstraintName("FK_RBD_Factura_RBD_Comprobante_Fiscal");

            entity.HasOne(d => d.CodTipagoNavigation).WithMany(p => p.RbdFacturas)
                .HasForeignKey(d => d.CodTipago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Factura_RBD_Tipo_Pago");
        });

        modelBuilder.Entity<RbdGenero>(entity =>
        {
            entity.HasKey(e => e.CodGen).HasName("PK__RBD_Gene__3F99E7689B72AF06");

            entity.ToTable("RBD_Genero");

            entity.Property(e => e.CodGen).HasColumnName("Cod_gen");
            entity.Property(e => e.NomGen)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nom_gen");
        });

        modelBuilder.Entity<RbdGrupo>(entity =>
        {
            entity.HasKey(e => e.CodGrup).HasName("PK__RBD_Grup__FCBF0A71DE088454");

            entity.ToTable("RBD_Grupo");

            entity.Property(e => e.CodGrup).HasColumnName("Cod_grup");
            entity.Property(e => e.CodEst).HasColumnName("Cod_est");
            entity.Property(e => e.DesGrup)
                .HasColumnType("text")
                .HasColumnName("Des_grup");
            entity.Property(e => e.FecCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fec_creacion");
            entity.Property(e => e.NomGrup)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nom_grup");

            entity.HasOne(d => d.CodEstNavigation).WithMany(p => p.RbdGrupos)
                .HasForeignKey(d => d.CodEst)
                .HasConstraintName("FK_RBD_Grupo_RBD_Estado");
        });

        modelBuilder.Entity<RbdListaDePrecio>(entity =>
        {
            entity.HasKey(e => new { e.CodArt, e.Precio }).HasName("PK__RBD_List__C021E059DE1B8EFB");

            entity.ToTable("RBD_Lista_De_Precios");

            entity.Property(e => e.CodArt).HasColumnName("Cod_art");
            entity.Property(e => e.CodEst).HasColumnName("Cod_est");
            entity.Property(e => e.FecCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fec_creacion");

            entity.HasOne(d => d.CodArtNavigation).WithMany(p => p.RbdListaDePrecios)
                .HasForeignKey(d => d.CodArt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Lista_De_Precios_RBD_Articulo");

            entity.HasOne(d => d.CodEstNavigation).WithMany(p => p.RbdListaDePrecios)
                .HasForeignKey(d => d.CodEst)
                .HasConstraintName("FK_RBD_Lista_De_Precios_RBD_Estado");
        });

        modelBuilder.Entity<RbdTelefonoCliente>(entity =>
        {
            entity.HasKey(e => new { e.CodCli, e.TelCli }).HasName("PK__RBD_Tele__448E5C60C1C6149D");

            entity.ToTable("RBD_Telefono_Cliente");

            entity.Property(e => e.CodCli).HasColumnName("Cod_cli");
            entity.Property(e => e.TelCli)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tel_cli");

            entity.HasOne(d => d.CodCliNavigation).WithMany(p => p.RbdTelefonoClientes)
                .HasForeignKey(d => d.CodCli)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Telefono_Cliente_RBD_Clientes");
        });

        modelBuilder.Entity<RbdTelefonoEmpleado>(entity =>
        {
            entity.HasKey(e => new { e.CodEm, e.TelEm }).HasName("PK__RBD_Tele__EEC1CA2AE5484BAD");

            entity.ToTable("RBD_Telefono_Empleado");

            entity.Property(e => e.CodEm).HasColumnName("Cod_em");
            entity.Property(e => e.TelEm)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tel_em");

            entity.HasOne(d => d.CodEmNavigation).WithMany(p => p.RbdTelefonoEmpleados)
                .HasForeignKey(d => d.CodEm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RBD_Telefono_Empleado_RBD_Empleado");
        });

        modelBuilder.Entity<RbdTipoComprobante>(entity =>
        {
            entity.HasKey(e => e.CodTipocom).HasName("PK__RBD_Tipo__E3B1B5B20DDA428A");

            entity.ToTable("RBD_Tipo_Comprobante");

            entity.Property(e => e.CodTipocom).HasColumnName("Cod_tipocom");
            entity.Property(e => e.DesTipocom)
                .HasColumnType("text")
                .HasColumnName("Des_tipocom");
            entity.Property(e => e.NomTipocom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nom_tipocom");
        });

        modelBuilder.Entity<RbdTipoPago>(entity =>
        {
            entity.HasKey(e => e.CodTipago).HasName("PK__RBD_Tipo__CC55D3E653EEF053");

            entity.ToTable("RBD_Tipo_Pago");

            entity.Property(e => e.CodTipago).HasColumnName("Cod_tipago");
            entity.Property(e => e.CodEst).HasColumnName("Cod_est");
            entity.Property(e => e.FecCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fec_creacion");
            entity.Property(e => e.NomPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nom_pago");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
