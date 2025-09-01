using library.CAD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.EN
{
    public class ENProveedor
    {
        private int proveedor_id;
        private string nombre;
        private string cif;
        private string direccion;
        private string telefono;
        private string email;

        public int Proveedor_id
        {
            get { return proveedor_id; }
            set { proveedor_id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string CIF
        {
            get { return cif; }
            set { cif = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        // Operaciones CRUD
        public bool CrearProveedor()
        {
            CADProveedor cad = new CADProveedor();
            return cad.CrearProveedor(this);
        }

        public bool LeerProveedor()
        {
            CADProveedor cad = new CADProveedor();
            return cad.LeerProveedor(this);
        }
        
        public bool ActualizarProveedor()
        {
            CADProveedor cad = new CADProveedor();
            return cad.ActualizarProveedor(this);
        }

        public bool EliminarProveedor()
        {
            try
            {
                CADProveedor cad = new CADProveedor();
                return cad.EliminarProveedor(this.Proveedor_id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ENProveedor.EliminarProveedor: {ex.Message}");
                throw; // Relanzar la excepción para manejo superior
            }
        }


        public DataSet ObtenerProductosPorProveedor()
        {
            CADProveedor cad = new CADProveedor();
            return cad.ObtenerProductosPorProveedor(this.Proveedor_id);
        }

    }
}
