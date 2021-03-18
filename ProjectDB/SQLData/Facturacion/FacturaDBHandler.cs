using ProyectoDI_OpticaMarco.ClaseProductos;
using ProyectoDI_OpticaMarco.ProjectDB.SQLData.Facturacion.FacturasDS;
using ProyectoDI_OpticaMarco.ProjectDB.SQLData.Facturacion.FacturasDS.FacturasDataSetTableAdapters;
using ProyectoDI_OpticaMarco.ReportsData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        private static InformeTableAdapter informeAdapter = new InformeTableAdapter();

        public static bool AñadirCliente(Clientes clientes) {
            try
            {
                clienteAdapter.Insert(clientes.cif, clientes.nombre, clientes.direccion);
                return true;
            }
            catch
            {
               
                return false;
            }
            
        }

        public static bool AñadirFactura(Clientes clientes, ObservableCollection<ProductosOptica> listaProductos, string refFactura) {
            try
            {
                
                facturaAdapter.Insert(refFactura, clientes.cif, DateTime.Today.Date);
                foreach (ProductosOptica p in listaProductos)
                {

                    detalleAdapter.Insert(p.referencia, refFactura, p.cantidad, p.precio, p.descripcion);

                }
                return true;
            }
            catch
            {

                return false;
            }
            
        }

        public static DataTable FacturaPorCif(string cif) {

            return informeAdapter.GetDataCifCliente(cif);
        }

        public static DataTable FacturaPorFechas(string fecha1, string fecha2) {

            return informeAdapter.GetDataFechas(fecha1, fecha2);
        }

        public static DataTable FacturaPorNumFactura(string numFactura) {

            return informeAdapter.GetDataRefFactura(numFactura);
        }
    }   
}
