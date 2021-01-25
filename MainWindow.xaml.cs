using ProyectoDI_OpticaMarco.ClaseProductos;
using ProyectoDI_OpticaMarco.Paginas;
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

namespace ProyectoDI_OpticaMarco
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame myFrameNav;
        public ProductHandler productHandler = new ProductHandler();

        public MainWindow()
        {
            InitializeComponent();
            myFrameNav = myFrameNavigation;
            myFrameNavigation.NavigationService.Navigate(new PaginaPrincipal());
        }


        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            myFrameNavigation.NavigationService.Navigate(new Nuevo_ModificarProducto("Modificar Producto", productHandler));
        }

        private void btnMostrar_Click(object sender, RoutedEventArgs e)
        {
            productHandler.ActualizarProductList();
            myFrameNavigation.NavigationService.Navigate(new MostrarProductos(productHandler));
        }

        private void btnPricipal_Click(object sender, RoutedEventArgs e)
        {
            myFrameNav.NavigationService.Navigate(new PaginaPrincipal());
        }
    }
}
