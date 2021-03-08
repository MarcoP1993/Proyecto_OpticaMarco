using ProyectoDI_OpticaMarco.ClaseProductos;
using ProyectoDI_OpticaMarco.ProjectDB.SQLData.Facturacion;
using ProyectoDI_OpticaMarco.ReportsData;
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

namespace ProyectoDI_OpticaMarco.Paginas
{
    /// <summary>
    /// Lógica de interacción para CrearFactura.xaml
    /// </summary>
    public partial class CrearFactura : Page
    {
        ProductHandler productHandler;
        ObservableCollection<ProductosOptica> listaProductos;
        ProductosOptica producto;
        Clientes cliente;

        public CrearFactura(ProductHandler productHandler)
        {
            InitializeComponent();
            cliente = new Clientes();
            this.datosCliente.DataContext = cliente;
            this.productHandler = productHandler;
            this.comboProductos.ItemsSource = productHandler.productoList;
            listaProductos = new ObservableCollection<ProductosOptica>();
            tablaproductos.ItemsSource = listaProductos;
        }

        private void btn_aceptar(object sender, RoutedEventArgs e)
        {
            Boolean productoRepetido = false;
            if (producto != null) {
                foreach (ProductosOptica p in listaProductos) {

                    if (p.referencia == producto.referencia) {

                        productoRepetido = true;
                        p.cantidad = p.cantidad + int.Parse(txt_cantidad.Text);
                    }
                }
                if (!productoRepetido) {

                    listaProductos.Add(producto);
                }

               comboProductos.SelectedIndex = -1;
               txt_cantidad.Text = "";
               tablaproductos.Items.Refresh();
            } 
           
        }

        private void comboProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            producto = (ProductosOptica)comboProductos.SelectedItem;
            txt_cantidad.DataContext = producto;
        }

        private void btn_crearFactura(object sender, RoutedEventArgs e)
        {
            if (listaProductos.Count > 0 && txt_factura.Text !="" && cliente !=null) {

                FacturaDBHandler.AñadirCliente(cliente);
                FacturaDBHandler.AñadirFactura(cliente, listaProductos, txt_factura.Text);
                MainWindow.myFrameNav.NavigationService.Navigate(new PaginaPrincipal());
                ReportPreview reportPreview = new ReportPreview();
                reportPreview.MostrarInformeNumFactura(txt_factura.Text);
                reportPreview.Show();
            }
        }
    }
}
