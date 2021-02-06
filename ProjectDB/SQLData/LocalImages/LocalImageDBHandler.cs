using ProyectoDI_OpticaMarco.ProjectDB.SQLData.LocalImages.LocalImageDataSet;
using ProyectoDI_OpticaMarco.ProjectDB.SQLData.LocalImages.LocalImageDataSet.DataSet_Local_ImagesTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDI_OpticaMarco.ProjectDB.SQLData.LocalImages
{
    public class LocalImageDBHandler
    {
        private static ImagesTableAdapter imagesAdapter = new ImagesTableAdapter();
        private static DataSet_Local_Images dataset = new DataSet_Local_Images();

        public static void AddData_toDB(string idImage, byte[] productImage) {

            imagesAdapter.Insert(idImage, productImage); //este metodo es para insertar/añadir imagenes a la base de datos
            imagesAdapter.Update(dataset);
        }
    }
}
