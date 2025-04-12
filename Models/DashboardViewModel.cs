using System;
using System.Collections.Generic;

namespace laboratorio1ElvisOrtiz160625.Models
{
    public class DashboardViewModel
    {
        public int TotalProductos { get; set; }
        public int TotalProveedores { get; set; }
        public int StockMaximo { get; set; }

        public List<Producto> ProductosConMasExistencia { get; set; }
        public List<Producto> ProductosBajoStock { get; set; }
        public List<Producto> ProductosAltoStock { get; set; }         // NUEVO
        public List<Producto> ProductosExpirados { get; set; }
        public List<Producto> ProductosPorExpirar { get; set; }        // NUEVO
        public List<Producto> ProductosRecientes { get; set; }         // NUEVO
    }
}
