using ProyectoDI_OpticaMarco.ClaseProductos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProyectoDI_OpticaMarco.XML
{
    class XMLHandler
    {
        private static XDocument xml;
        private static ProductosOptica producto;
        private static XElement xmlProductos;
        private static XElement xmlBrand;
        private static void CargarXML() { //Creo el moetodo para cargar el XML
            xml = XDocument.Load("../../XML/XMLOptica.xml"); //Cargo el documento XMLOptica de la carpeta XML
        }

        private static void GuardarXML() {
            xml.Save("../../XML/XMLOptica.xml");
        }

        public static void AñadirProductoXML(ProductosOptica p) {

            producto = p;
            CargarXML();
            AddCategoria();
            AddMarca();
            AddArticulo();
            GuardarXML();
        }

        private static void AddArticulo() {

            XElement xmlArticulo = new XElement("Articulo", new XAttribute("Referencia", producto.referencia),
                                    new XAttribute("Descripcion", producto.descripcion), new XAttribute("Tipo", producto.tipo),
                                    new XAttribute("Precio", producto.precio), new XAttribute("FechaEntrada", producto.fecha),
                                    new XAttribute("Stock", producto.stock));
           
            xmlBrand.Add(xmlArticulo);
        }

        private static void AddCategoria() {

            var listaProductos = xml.Root.Elements("Productos").Attributes("IdProducto");
            bool isNewCategory = true;

            foreach (XAttribute categoria in listaProductos) {
                if (categoria.Value.Equals(producto.category)) {
                    xmlProductos = categoria.Parent;
                    isNewCategory = false;
                    break;
                } else {
                    xmlProductos = new XElement("Productos", new XAttribute("IdProducto", producto.category));
                    xmlBrand = new XElement("Marca", new XAttribute("Nombre", producto.brand));
                    isNewCategory = true;
                }
            }
            if (isNewCategory) {

                xmlProductos.Add(xmlBrand);
                xml.Root.Add(xmlProductos);
            }
        }

        private static void AddMarca() {

            bool isNewBrand = true;
            foreach (XAttribute brand in xmlProductos.Elements().Attributes("Nombre")){

                if (brand.Value.Equals(producto.brand))
                {
                    xmlBrand = brand.Parent;
                    isNewBrand = false;
                    break;

                }
                else {
                    xmlBrand = new XElement("Marca", new XAttribute("Nombre", producto.brand));
                    isNewBrand = true;
                }
                if (isNewBrand) {

                    xmlProductos.Add(xmlBrand);
                }
            }
        }
    }
}
