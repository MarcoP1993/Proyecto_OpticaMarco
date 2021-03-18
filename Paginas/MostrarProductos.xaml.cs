using ProyectoDI_OpticaMarco.ClaseProductos;
using ProyectoDI_OpticaMarco.ProjectDB.MySQLData.RemoteProducts;
using ProyectoDI_OpticaMarco.ProjectDB.SQLData.LocalImages;
using ProyectoDI_OpticaMarco.XML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para MostrarProductos.xaml
    /// </summary>
    public partial class MostrarProductos : Page
    {
        ProductHandler productHandler;
        ObservableCollection<ProductosOptica> filtrarProductos;
        private XDocument xml = XMLHandler.ReturnXDocument();

        public MostrarProductos(ProductHandler productHandler)
        {
            InitializeComponent();
            this.productHandler = productHandler; 
            ActualizarProductList();
            InitCategoryCombo();
        }

        private void ActualizarProductList()
        {
            busquedaTextBox.Text = "";
            categoryCombo.SelectedIndex = 0;
            productHandler.ActualizarProductList();
            filtrarProductos = new ObservableCollection<ProductosOptica>(productHandler.productoList);
            myDataGrid.DataContext = productHandler.productoList;
            myDataGrid.ItemsSource = productHandler.productoList;
            myDataGrid.Items.Refresh();
        }

        private void Button_Editar(object sender, RoutedEventArgs e)
        {
           
            ProductosOptica productos = (ProductosOptica)myDataGrid.SelectedItem;
            GestionOptica.myFrameNav.NavigationService.Navigate(new Nuevo_ModificarProducto("MODIFICAR PRODUCTOS", productHandler, (ProductosOptica)productos.Clone()));

        }

        private void Boton_Borrar(object sender, RoutedEventArgs e)
        {
            
            
            MessageBoxResult mensaje = MessageBox.Show("¿Deseas borrar el producto?", "Borrar Producto",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);

            switch (mensaje) {
                case MessageBoxResult.Yes:
                    ProductosOptica productos = (ProductosOptica)myDataGrid.SelectedItem;
                    XMLHandler.EliminarProducto(productos.referencia);
                    LocalImageDBHandler.BorrarDataFromDB(productos.referencia);
                    MySQLHandler.Delete_toMySQL(productos);
                    ActualizarProductList();
                    MessageBox.Show("Producto Borrado"); 
                    break;

                case MessageBoxResult.No:
                    break;
                

            }
            

        }

        private void InitCategoryCombo() {

            categoryCombo.Items.Add("Todas...");
            var listaCategorias = xml.Root.Elements("Productos").Attributes("IdProducto");
            foreach (XAttribute categoria in listaCategorias) {

                categoryCombo.Items.Add(categoria.Value);
            }
            categoryCombo.SelectedIndex = 0;
        }

        private void categoryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryCombo.SelectedIndex == 0)
            {

                ActualizarProductList();
            }
            else {

                filtrarProductos.Clear();

                foreach (ProductosOptica productos in productHandler.productoList) {

                    if (productos.category.Equals((String)categoryCombo.SelectedItem)) {

                        filtrarProductos.Add(productos);
                    }
                }
                myDataGrid.DataContext = filtrarProductos;
                myDataGrid.ItemsSource = filtrarProductos;
                myDataGrid.Items.Refresh();
            }
        }

        private void busquedaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<ProductosOptica> filtrarLista = new ObservableCollection<ProductosOptica>();

            foreach (ProductosOptica productos in filtrarProductos) {

                if (productos.ObtenerTodosLosValores().Contains(busquedaTextBox.Text)) {

                    filtrarLista.Add(productos);
                }
                myDataGrid.DataContext = filtrarLista;
                myDataGrid.ItemsSource = filtrarLista;
             
            }
        }

        private void Button_Actualizar(object sender, RoutedEventArgs e)
        {
            ActualizarProductList();
        }

        private void Button_Publicar(object sender, RoutedEventArgs e)
        {
            ProductosOptica productos = (ProductosOptica)myDataGrid.SelectedItem;
            if (productos.publish == false)
            {
                
                MySQLHandler.AddData_toMySQL(productos);
                productos.publish = true;
                XMLHandler.ModificarProducto(productos);
                ActualizarProductList();
                MessageBox.Show("Producto publicado correctamente");
            }
            else {
                MySQLHandler.Delete_toMySQL(productos);
                productos.publish = false;
                XMLHandler.ModificarProducto(productos);
                ActualizarProductList();
                MessageBox.Show("Producto despublicado");
            }
           
        }

       
    }
}
