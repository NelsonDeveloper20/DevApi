using Api_Dc.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ApiPortal_DataLake.Domain.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { 
        }  
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolUsuario> RolesUsuarios { get; set; }
        public DbSet<TokenSecurity> TokenSecurity { get; set; }
         
         
        public DbSet<TipoSolicitud> TipoSolicitud { get; set; }
       

        //BD
        public DbSet<Tbl_Ambiente> Tbl_Ambiente{ get; set; }
        public DbSet<Tbl_Destino> Tbl_Destino { get; set; }
        public DbSet<Tbl_DetalleOpGrupo> Tbl_DetalleOp { get; set; }
        public DbSet<TBL_DetalleOrdenProduccion> TBL_DetalleOrdenProduccion { get; set; }
        public DbSet<Tbl_Escuadra> Tbl_Escuadra { get; set; }
        public DbSet<Tbl_Estado> Tbl_Estado { get; set; }
        public DbSet<Tbl_ModuloRol> Tbl_ModuloRol { get; set; }
        public DbSet<Tbl_Modulos> Tbl_Modulos { get; set; }
        public DbSet<Tbl_OrdenProduccion> Tbl_OrdenProduccion { get; set; }
        public DbSet<Tbl_Proyecto> Tbl_Proyecto { get; set; }
        public DbSet<Tbl_Rol> Tbl_Rol { get; set; }
        public DbSet<Tbl_TipoCliente> Tbl_TipoCliente { get; set; }
        public DbSet<Tbl_TipoOperacion> Tbl_TipoOperacion { get; set; }
        public DbSet<Tbl_TipoUsuario> Tbl_TipoUsuario { get; set; }
        public DbSet<Tbl_Usuario> Tbl_Usuario { get; set; }
        public DbSet<Tbl_UsuarioRol> Tbl_UsuarioRol { get; set; }
        public DbSet<Tbl_DetalleOpGrupo> Tbl_DetalleOpGrupo { get; set; }
        public DbSet<Tbl_Estacion> Tbl_Estacion { get; set; }
        public DbSet<Tbl_Componentes> Tbl_Componentes { get; set; }
        public DbSet<Tbl_ProduccionEstacion> Tbl_ProduccionEstacion { get; set; }
        public DbSet<Tbl_SupervisorAprobacion> Tbl_SupervisorAprobacion { get; set; }
        public DbSet<Tbl_Explocion> Tbl_Explocion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            /*
        

            //SEGURIDAD
            modelBuilder.Entity<Sede>().ToTable("TblSede");
            modelBuilder.Entity<UnidadNegocio>().ToTable("TblUnidadNegocio");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<RolUsuario>().ToTable("RolUsuario");
            modelBuilder.Entity<TokenSecurity>().ToTable("TokenSecurity");
            modelBuilder.Entity<EnvioEmail>().ToTable("TblEnvioEmail");*/

            //BD 
            modelBuilder.Entity<Tbl_Ambiente>().ToTable("Tbl_Ambiente");
            modelBuilder.Entity<Tbl_Destino>().ToTable("Tbl_Destino");
            modelBuilder.Entity<Tbl_DetalleOpGrupo>().ToTable("Tbl_DetalleOp");
            modelBuilder.Entity<TBL_DetalleOrdenProduccion>().ToTable("TBL_DetalleOrdenProduccion");
            modelBuilder.Entity<Tbl_Escuadra>().ToTable("Tbl_Escuadra");
            modelBuilder.Entity<Tbl_Estado>().ToTable("Tbl_Estado");
            modelBuilder.Entity<Tbl_ModuloRol>().ToTable("Tbl_ModuloRol");
            modelBuilder.Entity<Tbl_Modulos>().ToTable("Tbl_Modulos");
            modelBuilder.Entity<Tbl_OrdenProduccion>().ToTable("Tbl_OrdenProduccion");
            modelBuilder.Entity<Tbl_Proyecto>().ToTable("Tbl_Proyecto");
            modelBuilder.Entity<Tbl_Rol>().ToTable("Tbl_Rol");
            modelBuilder.Entity<Tbl_TipoCliente>().ToTable("Tbl_TipoCliente");
            modelBuilder.Entity<Tbl_TipoOperacion>().ToTable("Tbl_TipoOperacion");
            modelBuilder.Entity<Tbl_TipoUsuario>().ToTable("Tbl_TipoUsuario");
            modelBuilder.Entity<Tbl_Usuario>().ToTable("Tbl_Usuario");

            modelBuilder.Entity<Tbl_UsuarioRol>().ToTable("Tbl_UsuarioRol");
            modelBuilder.Entity<Tbl_DetalleOpGrupo>().ToTable("Tbl_DetalleOpGrupo");
            modelBuilder.Entity<Tbl_Estacion>().ToTable("Tbl_Estacion");
            modelBuilder.Entity<Tbl_Componentes>().ToTable("Tbl_Componentes");
            modelBuilder.Entity<Tbl_ProduccionEstacion>().ToTable("Tbl_ProduccionEstacion");
            modelBuilder.Entity<Tbl_SupervisorAprobacion>().ToTable("Tbl_SupervisorAprobacion");
            modelBuilder.Entity<Tbl_Explocion>().ToTable("Tbl_Explocion");
            




        }
        /*
        internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }*/
    }
}
