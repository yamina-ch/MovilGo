using Library.CAD;
using System;
using System.Collections.Generic;

namespace Library.EN
{
    public class ENVenta
    {
        public int VentaId { get; set; }
        public int ArticuloId { get; set; }
        public DateTime FechaAnuncio { get; set; }
        public string VendedorUName { get; set; }
        public decimal? PrecioOriginal { get; set; }
        public decimal PrecioPublicado { get; set; }
        public string MotivoVenta { get; set; }
        //DisponibleHasta me refiero ala fecha que esta disponible apartir de tal dia 
        public DateTime? DisponibleHasta { get; set; }

        public ENVenta() { }

        public ENVenta(int articuloId, DateTime fechaAnuncio, string vendedorUName,
                       decimal? precioOriginal, decimal precioPublicado, string motivoVenta, DateTime? disponibleHasta)
        {
            ArticuloId = articuloId;
            FechaAnuncio = fechaAnuncio;
            VendedorUName = vendedorUName;
            PrecioOriginal = precioOriginal;
            PrecioPublicado = precioPublicado;
            MotivoVenta = motivoVenta;
            DisponibleHasta = disponibleHasta;
        }
        public bool CrearVenta()
        {
            CADVenta cad = new CADVenta();
            return cad.CrearVenta(this);
        }

        public static List<ENVenta> ObtenerVentasPorVendedor(string vendedorUName)
        {
            CADVenta cad = new CADVenta();
            return cad.ObtenerVentasPorVendedor(vendedorUName);
        }

        public bool EliminarVenta()
        {
            CADVenta cad = new CADVenta();
            return cad.EliminarVenta(this.VentaId);
        }

        public bool ActualizarVenta()
        {
            CADVenta cad = new CADVenta();
            return cad.ActualizarVenta(this); 
        }
    }
}
