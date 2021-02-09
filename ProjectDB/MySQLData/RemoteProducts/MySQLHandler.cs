using ProyectoDI_OpticaMarco.ClaseProductos;
using ProyectoDI_OpticaMarco.Imagenes;
using ProyectoDI_OpticaMarco.ProjectDB.MySQLData.RemoteProducts.RemoteProductsDataSet;
using ProyectoDI_OpticaMarco.ProjectDB.MySQLData.RemoteProducts.RemoteProductsDataSet.projectdbDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDI_OpticaMarco.ProjectDB.MySQLData.RemoteProducts
{
    public class MySQLHandler
    {
        private static publishmpTableAdapter imagesAdapter = new publishmpTableAdapter();
        private static projectdbDataSet dataset = new projectdbDataSet();

        public static void AddData_toMySQL(ProductosOptica productos) {
            


            imagesAdapter.Insert(productos.referencia, productos.category, productos.brand, productos.descripcion, productos.tipo, productos.precio, productos.stock, productos.fecha, productos.imagen);
            imagesAdapter.Update(dataset);
        }

        public static void Delete_toMySQL(ProductosOptica productos) {

            imagesAdapter.DeleteMySQL(productos.referencia);
            imagesAdapter.Update(dataset);
        }

      
    }
}
