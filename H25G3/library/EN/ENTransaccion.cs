using library.CAD;
using Library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.EN
{
    public class ENTransaccion
    {
        private int transaccion_id;
        private int pedido_id;
        private int articulo_id;
        private string vendedor_UName;
        private float importe_total;
        private float comision_vendedor;
        private float comision_empresa;
        private DateTime fecha_transaccion;
        private string nombreArticulo;
        public string NombreArticulo
        {
            get { return nombreArticulo; }
            set { nombreArticulo = value; }
        }

        public int Transaccion_id
        {
            get { return transaccion_id; }
            set { transaccion_id = value; }
        }
        public int Pedido_id
        {
            get { return pedido_id; }
            set { pedido_id = value; }
        }
        public int Articulo_id
        {
            get { return articulo_id; }
            set { articulo_id = value; }
        }
        public string Vendedor_UName
        {
            get { return vendedor_UName; }
            set { vendedor_UName = value; }
        }
        public float Importe_total
        {
            get { return importe_total; }
            set { importe_total = value; }
        }
        public float Comision_vendedor
        {
            get { return comision_vendedor; }
            set { comision_vendedor = value; }
        }
        public float Comision_empresa
        {
            get { return comision_empresa; }
            set { comision_empresa = value; }
        }
        public DateTime Fecha_transaccion
        {
            get { return fecha_transaccion; }
            set { fecha_transaccion = value; }
        }
        public bool Crear()
        {
            CADTransaccion t = new CADTransaccion();
            return t.Crear(this);
        }

        public bool Editar()
        {
            CADTransaccion t = new CADTransaccion();
            return t.Editar(this);
        }

        public bool Eliminar()
        {
            CADTransaccion t = new CADTransaccion();
            return t.Eliminar(this.transaccion_id);
        }

        public ENCategoria Leer()
        {
            CADTransaccion t = new CADTransaccion();
            return t.Leer(this.transaccion_id);
        }

        public List<ENTransaccion> ObtenerTransaccionesPorVendedor(string vendedor)
        {
            CADTransaccion cad = new CADTransaccion();
            return cad.ObtenerTransaccionesPorVendedor(vendedor);
        }

    }
}
