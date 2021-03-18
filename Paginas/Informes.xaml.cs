using ProyectoDI_OpticaMarco.ClaseProductos;
using ProyectoDI_OpticaMarco.ReportsData;
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

namespace ProyectoDI_OpticaMarco.Paginas
{
    /// <summary>
    /// Lógica de interacción para Informes.xaml
    /// </summary>
    public partial class Informes : Page
    {

        ProductHandler productHandler = new ProductHandler();

        public Informes()
        {
            InitializeComponent();
            
            
        }

        private void crearFactura_Click(object sender, RoutedEventArgs e)
        {
            GestionOptica.myFrameNav.NavigationService.Navigate(new CrearFactura(productHandler));
        }

        private void consultaFactura_Click(object sender, RoutedEventArgs e)
        {
            if (panelBotonesConsulta.IsVisible)
            {
                panelBotonesConsulta.Visibility = Visibility.Hidden;
            }
            else { panelBotonesConsulta.Visibility = Visibility.Visible; }
        }

        //------------------------------------------------------------------------------

        private void BotonConsultaCIF_Click(object sender, RoutedEventArgs e)
        {
            if (panelConsultaCIF.IsVisible)
            {
                panelConsultaCIF.Visibility = Visibility.Hidden;
            }
            else { panelConsultaCIF.Visibility = Visibility.Visible; }
        }

        private void BotonConsultaFechas(object sender, RoutedEventArgs e)
        {
            if (panelConsultaFechas.IsVisible)
            {
                panelConsultaFechas.Visibility = Visibility.Hidden;
            }
            else { panelConsultaFechas.Visibility = Visibility.Visible; }
        }

        private void BotonConsultaFactura(object sender, RoutedEventArgs e)
        {
            if (panelConsultaFactura.IsVisible)
            {
                panelConsultaFactura.Visibility = Visibility.Hidden;
            }
            else { panelConsultaFactura.Visibility = Visibility.Visible; }
        }

        //------------------------------------------------------------------------------

        private void botonAceptarCIF(object sender, RoutedEventArgs e)
        {
            if (txt_consultadni.Text != "") {
                string cif_cliente = txt_consultadni.Text;
                ReportPreview reportPreview = new ReportPreview();
                bool okQuery = reportPreview.MostrarInformeCliente(cif_cliente);


                if (okQuery)
                {
                    reportPreview.Show();//si es verdadera mustra esto
                }
                else
                {
                    MessageBox.Show("No existen registros para el cliente indicado");
                }
            }
            else
            {
                MessageBox.Show("Escribe un CIF/DNI de cliente");
            }
        }

        private void BotonAceptarFechas(object sender, RoutedEventArgs e)
        {
            if (fecha1.Text != "" && fecha2.Text != "")
            {
                string fechainicio = fecha1.Text;
                string fechafin = fecha2.Text;
                ReportPreview reportPreview = new ReportPreview();
                bool okConsulta = reportPreview.MostrarInformeFechas(fechainicio, fechafin);

                if (okConsulta)
                {
                    reportPreview.Show();
                }
                else
                {
                    MessageBox.Show("No existen registros para las fechas seleccionadas");
                }
            }
            else
            {
                MessageBox.Show("Selecciona Fecha Inicio y Fecha Fin");

            }
        }

        private void BotonAceptarFacturas(object sender, RoutedEventArgs e)
        {
            if (txt_consultafactura.Text != "")
            {
                string numFactura = txt_consultafactura.Text; 
                ReportPreview reportPreview = new ReportPreview();
                bool okConsulta = reportPreview.MostrarInformeNumFactura(numFactura);

                if (okConsulta)
                {
                    reportPreview.Show();
                }
                else
                {
                    MessageBox.Show("No existen registros para la factura seleccionada");
                }
            }
            else
            {
                MessageBox.Show("Selecciona un numero de factura");

            }
        }
    }
}
