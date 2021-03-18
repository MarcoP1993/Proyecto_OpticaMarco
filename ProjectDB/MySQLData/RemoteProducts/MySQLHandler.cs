using ProyectoDI_OpticaMarco.ClaseProductos;
using ProyectoDI_OpticaMarco.Imagenes;
using ProyectoDI_OpticaMarco.ProjectDB.MySQLData.RemoteProducts.RemoteProductsDataSet;
using ProyectoDI_OpticaMarco.ProjectDB.MySQLData.RemoteProducts.RemoteProductsDataSet.projectdbDataSetTableAdapters;
using ProyectoDI_OpticaMarco.ProjectDB.SQLData.LocalImages.LocalImageDataSet.DataSet_Local_ImagesTableAdapters;
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

            
            imagesAdapter.Insert(productos.referencia, productos.category, productos.brand, productos.descripcion, productos.tipo, productos.precio, productos.stock, productos.fecha, ImageHandler.EncodeImage(productos.imagen));
            imagesAdapter.Update(dataset);
        }

        public static void Delete_toMySQL(ProductosOptica productos) {


            imagesAdapter.DeleteMySQL(productos.referencia);
            imagesAdapter.Update(dataset);
        }

        public static bool updatetoMySQL(ProductosOptica productos)
        {
            bool borrar = false;
            if (productos != null)
            {
                byte[] imagen = ImageHandler.EncodeImage(productos.imagen);
                float precio = productos.precio;
                decimal Precio = Convert.ToDecimal(precio);
                imagesAdapter.UpdateMySQL(productos.referencia, productos.category, productos.brand, productos.descripcion, productos.tipo, Precio, productos.stock, productos.fecha, ImageHandler.EncodeImage(productos.imagen));
            }
            return borrar;
        }

    }
}
