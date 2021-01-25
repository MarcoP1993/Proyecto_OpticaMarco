/*using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProyectoDI_OpticaMarco.Imagenes
{
    public class ImageHandler
    {

        public static BitmapImage GetBitmapImage() {

            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Elige imagen |*.jpg;*.png";
            BitmapImage bitmapImage = new BitmapImage();
            bool? dialogOk = opf.ShowDialog();

            if (dialogOk == true) {

                string imageName = opf.FileName;
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(imageName, UriKind.Absolute);
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}*/
