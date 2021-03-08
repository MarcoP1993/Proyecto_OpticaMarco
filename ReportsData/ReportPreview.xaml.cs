using Microsoft.Reporting.WinForms;
using ProyectoDI_OpticaMarco.ProjectDB.SQLData.Facturacion;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace ProyectoDI_OpticaMarco.ReportsData
{
    /// <summary>
    /// Lógica de interacción para ReportPreview.xaml
    /// </summary>
    public partial class ReportPreview : Window
    {
        public ReportPreview()
        {
            InitializeComponent();
        }

        public bool MostrarInformeCliente(string cif_cliente) {

            bool okConsulta = false; //creamos el bool en false
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DatosCIF";
            DataTable clientesCif = FacturaDBHandler.FacturaPorCif(cif_cliente);
            rds.Value = clientesCif;

            myReportView.LocalReport.ReportPath = "../../Informes/FacturaCif.rdlc";
            myReportView.LocalReport.DataSources.Add(rds);
            myReportView.RefreshReport();

            if (clientesCif.Rows.Count > 0) {

                okConsulta = true;
            }

            return okConsulta;
        }

        public bool MostrarInformeFechas(string fecha1, string fecha2) {
            bool okConsulta = false; //creamos el bool en false
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DatosFecha";
            DataTable clientesCif = FacturaDBHandler.FacturaPorFechas(fecha1,  fecha2);
            rds.Value = clientesCif;

            myReportView.LocalReport.ReportPath = "../../Informes/FacturaFechas.rdlc";
            myReportView.LocalReport.DataSources.Add(rds);
            myReportView.RefreshReport();

            if (clientesCif.Rows.Count > 0)
            {

                okConsulta = true;
            }

            return okConsulta;
        }

        public bool MostrarInformeNumFactura(string numFactura) {
            bool okConsulta = false; //creamos el bool en false
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DatosFactura";
            DataTable clientesCif = FacturaDBHandler.FacturaPorNumFactura(numFactura);
            rds.Value = clientesCif;

            myReportView.LocalReport.ReportPath = "../../Informes/FacturaNumero.rdlc";
            myReportView.LocalReport.DataSources.Add(rds);
            myReportView.RefreshReport();

            if (clientesCif.Rows.Count > 0)
            {

                okConsulta = true;
            }

            return okConsulta;
        }
    }
}
