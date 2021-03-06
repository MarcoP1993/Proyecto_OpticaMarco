using Microsoft.Win32;
using ProyectoDI_OpticaMarco.ProjectDB.SQLData.LocalImages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProyectoDI_OpticaMarco.Imagenes
{
    public class ImageHandler
    {

        public static BitmapImage GetBitmapImageFromFile()
        {

            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Elige una imagen |*.jpg; *.png"; //frase que quiero mostrar | y que la extension que obligo para la imagen
            BitmapImage bitmapImage = new BitmapImage();

            bool? dialogOK = opf.ShowDialog();

            if (dialogOK == true)
            {
                String imageName = opf.FileName;
                bitmapImage.BeginInit(); //esto inicia la imagen
                bitmapImage.UriSource = new Uri(imageName, UriKind.Absolute);
                bitmapImage.EndInit();
                return bitmapImage;
            }

            return null;

        }

        public static void AddImage(string referencia, BitmapImage bitmapImage)
        { // este metodo se encarga de enlazar desde la interfaz y realizar operaciones desde la base de datos

            //llamamos al manejador de imagenes
            LocalImageDBHandler.AddData_toDB(referencia, EncodeImage(bitmapImage));
        }


        public static BitmapImage CargarImagenPorDefecto()
        {

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("/Imagenes/no-disponible.png", UriKind.Relative);
            bitmapImage.EndInit();
            return bitmapImage;
        }

        public static BitmapImage LoadImage(string referencia)
        {

            byte[] imageData = LocalImageDBHandler.GetDataFromDB(referencia);
            BitmapImage bitmapImage = CargarImagenPorDefecto();

            if (imageData != null)
            {

                bitmapImage = DecodeImage(imageData);
            }

            return bitmapImage;
        }

        //este metodo coge un objeto bitmapimage y lo transforma a byte 
        public static byte[] EncodeImage(BitmapImage bitmapImage)
        {

            byte[] imageByte;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                imageByte = ms.ToArray();
            }
            return imageByte;
        }

        //este metodo coge un objeto byte y lo transforma a imagen
        public static BitmapImage DecodeImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        public static void ModificarImagen(string referencia, BitmapImage bitmapImage)
        {
            LocalImageDBHandler.ActualizarDataFromDB(referencia, EncodeImage(bitmapImage));
        }
    }
}

