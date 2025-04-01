using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class ERPDbContext :
        IdentityDbContext<IdentityUser>
    {
        public ERPDbContext(DbContextOptions<ERPDbContext> options) :
            base(options)
        {
        }


        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Departamento> tblDepartamento { get; set; }
        public DbSet<Proveedor> tblProveedors { get; set; }
        public DbSet<Producto> tblProductos { get; set; }
    }
}