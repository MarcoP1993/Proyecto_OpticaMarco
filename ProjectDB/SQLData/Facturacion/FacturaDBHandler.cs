using ProyectoDI_OpticaMarco.ClaseProductos;
using ProyectoDI_OpticaMarco.ProjectDB.SQLData.Facturacion.FacturasDS;
using ProyectoDI_OpticaMarco.ProjectDB.SQLData.Facturacion.FacturasDS.FacturasDataSetTableAdapters;
using ProyectoDI_OpticaMarco.ReportsData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDI_OpticaMarco.ProjectDB.SQLData.Facturacion
{
    class FacturaDBHandler
    {
        private static clienteTableAdapter clienteAdapter = new clienteTableAdapter();
        private static facturaTableAdapter facturaAdapter = new facturaTableAdapter();
        private static producto_facturaTableAdapter detalleAdapter = new producto_facturaTableAdapter();
        private static FacturasDataSet dataset = new FacturasDataSet();

        public static void AñadirCliente(Clientes clientes) {

            clienteAdapter.Insert(clientes.cif, clientes.nombre, clientes.direccion);
        }

        public static void AñadirFactura(Clientes clientes, ObservableCollection<ProductosOptica> listaProductos, string refFactura) {

            facturaAdapter.Insert(refFactura, clientes.cif, DateTime.Today.Date);
            foreach (ProductosOptica p in listaProductos) {

                detalleAdapter.Insert(p.referencia, refFactura, p.cantidad, p.precio, p.descripcion);

            }
        }
    }
}
