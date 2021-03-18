using ProyectoDI_OpticaMarco.ClaseProductos;
using ProyectoDI_OpticaMarco.Imagenes;
using ProyectoDI_OpticaMarco.ProjectDB.MySQLData.RemoteProducts;
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
        private XDocument xml = XMLHandler.ReturnXDocument();

        public ProductHandler productHandler;
        public ProductosOptica productos;
        public bool modify;
        public bool nuevaImagen = false;
        

        //Constructor modificar
        public Nuevo_ModificarProducto(String title, ProductHandler productHandler, ProductosOptica productos) {

            InitializeComponent();
            txt_referencia.Text = title;
            this.productHandler = productHandler;
            this.productos = productos;
            productoGrid.DataContext = productos;
            initCategoryCombo();
            ComboCategory.Text = productos.category;
            ComboMarca.Text = productos.brand;
            myImage.Source = ImageHandler.LoadImage(productos.referencia);
            modify = true;
        }

        private void initCategoryCombo()
        {
            var listaCategorias = xml.Root.Elements("Productos").Attributes("IdProducto");

            for (int i = 0; i < listaCategorias.Count(); i++) {

                ComboCategory.Items.Add(listaCategorias.ElementAt(i).Value);
            }
        }

        //Constructor nuevo producto
        public Nuevo_ModificarProducto(String title, ProductHandler productHandler)
        {
            InitializeComponent();
            this.mytitle.Text = title;
            this.productHandler = productHandler;
            this.productos = new ProductosOptica();
            productoGrid.DataContext = productos;
            initCategoryCombo();
            myImage.Source = ImageHandler.CargarImagenPorDefecto();
            modify = false;


        }

        private void Button_Aceptar(object sender, RoutedEventArgs e)
        {

            if (txt_referencia.Text.Length > 0 && ComboCategory.SelectedItem != null && ComboMarca.SelectedItem != null
                && ComboTipo.SelectedItem != null && txtDescripcion.Text != "" && txtStock.Text != "" && txtPrecio.Text != ""
                && txtFecha.SelectedDate != null) {

                warningLabel.Visibility = Visibility.Hidden;
                if (modify)
                {
                    
                    //productHandler.ModificarProducto(productos, pos);
                    XMLHandler.ModificarProducto(productos);
                    productos.imagen = (BitmapImage)myImage.Source;
                    MySQLHandler.updatetoMySQL(productos);
                    
                    if (nuevaImagen) {

                        ImageHandler.ModificarImagen(productos.referencia,(BitmapImage) myImage.Source);

                    }
                    MessageBox.Show("Producto Modificado", "modificar producto", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else {

                    //productHandler.AddProducto(productos);
                    XMLHandler.AñadirProductoXML(productos);
                    if (nuevaImagen) {

                        ImageHandler.AddImage(productos.referencia, (BitmapImage)myImage.Source);
                    }
                    MessageBox.Show("Producto añadido", "Nuevo producto", MessageBoxButton.OK, MessageBoxImage.Information);

                }

                GestionOptica.myFrameNav.NavigationService.Navigate(new PaginaPrincipal());
            }
            warningLabel.Visibility = Visibility.Visible;
        }

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            GestionOptica.myFrameNav.NavigationService.Navigate(new PaginaPrincipal());
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
                nuevaImagen = true;
            }
        }

        private void txt_referencia_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
