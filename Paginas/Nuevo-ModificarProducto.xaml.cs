using ProyectoDI_OpticaMarco.ClaseProductos;
using ProyectoDI_OpticaMarco.Imagenes;
using ProyectoDI_OpticaMarco.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ProyectoDI_OpticaMarco.Paginas
{
    /// <summary>
    /// Lógica de interacción para Nuevo_ModificarProducto.xaml
    /// </summary>
    public partial class Nuevo_ModificarProducto : Page

    {
        private XDocument xml = XDocument.Load("../../XML/XMLOptica.xml");

        public ProductHandler productHandler;
        public ProductosOptica productos;
        public bool modify;
       

        public Nuevo_ModificarProducto(String title, ProductHandler productHandler, ProductosOptica productos) {

            InitializeComponent();
            txt_referencia.Text = title;
            this.productHandler = productHandler;
            this.productos = productos;
            productoGrid.DataContext = productos;
            initCategoryCombo();
            ComboCategory.Text = productos.category;
            ComboMarca.Text = productos.brand;
            modify = true;
        }

        private void initCategoryCombo()
        {
            var listaCategorias = xml.Root.Elements("Productos").Attributes("IdProducto");

            for (int i = 0; i < listaCategorias.Count(); i++) {

                ComboCategory.Items.Add(listaCategorias.ElementAt(i).Value);
            }
        }

        public Nuevo_ModificarProducto(String title, ProductHandler productHandler)
        {
            InitializeComponent();
            this.mytitle.Text = title;
            this.productHandler = productHandler;
            this.productos = new ProductosOptica();
            productoGrid.DataContext = productos;
            initCategoryCombo();
            modify = false;


        }

        private void Button_Aceptar(object sender, RoutedEventArgs e)
        {
            if (txt_referencia.Text.Length > 0) {

                if (modify)
                {
                    //productHandler.ModificarProducto(productos, pos);
                    XMLHandler.ModificarProducto(productos);

                }
                else {

                    //productHandler.AddProducto(productos);
                    XMLHandler.AñadirProductoXML(productos);
                    ImageHandler.AddImage(productos.referencia, (BitmapImage)myImage.Source);
                    
                }

                MainWindow.myFrameNav.NavigationService.Navigate(new PaginaPrincipal());
            }
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            MainWindow.myFrameNav.NavigationService.Navigate(new PaginaPrincipal());
        }

        private void ComboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboMarca.Items.Clear();

            var brandList = xml.Root.Elements("Productos").ElementAt(ComboCategory.SelectedIndex).Elements("Marca").Attributes("Nombre");

            for (int i = 0; i < brandList.Count(); i++) {

                ComboMarca.Items.Add(brandList.ElementAt(i).Value);
            }
        
        }

        private void ComboMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboMarca.Items.Clear();

            var brandList = xml.Root.Elements("Productos").ElementAt(ComboCategory.SelectedIndex).Elements("Marca").Attributes("Nombre");

            for (int i = 0; i < brandList.Count(); i++)
            {

                ComboMarca.Items.Add(brandList.ElementAt(i).Value);
            }
        }

        private void txt_referencia_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void categoryCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (txtCategory.IsVisible)
            {

                txtCategory.Visibility = Visibility.Hidden;
                txtMarca.Visibility = Visibility.Hidden;
                brandCheck.IsEnabled = true;
            }
            else {
                txtCategory.Visibility = Visibility.Visible;
                txtMarca.Visibility = Visibility.Visible;
                brandCheck.IsEnabled = false;
            }
        }

        private void brandCheck_Checked(object sender, RoutedEventArgs e)
        {
           if (txtMarca.IsVisible)
            {

                txtMarca.Visibility = Visibility.Hidden;
                txtMarca.Visibility = Visibility.Hidden;
                brandCheck.IsEnabled = true;
            }
            else
            {
                txtMarca.Visibility = Visibility.Visible;
                txtMarca.Visibility = Visibility.Visible;
                
            }
        }

        private void btnAñadirImagen_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapImage = ImageHandler.GetBitmapImageFromFile();
            if (bitmapImage != null) {

                myImage.Source = bitmapImage;
            }
        }
    }
}
