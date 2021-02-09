using ProyectoDI_OpticaMarco.ClaseProductos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static ObservableCollection<ProductosOptica> CargarProductos()
        {
            ObservableCollection<ProductosOptica> productList = new ObservableCollection<ProductosOptica>();

            CargarXML();
            var listaProductosXML = xml.Root.Elements("Productos").Elements("Marca").Elements("Articulo");

            foreach (XElement productXML in listaProductosXML) {

                producto = new ProductosOptica();
                producto.referencia = productXML.Attribute("Referencia").Value;
                producto.descripcion = productXML.Attribute("Descripcion").Value;
                producto.tipo = productXML.Attribute("Tipo").Value;
                producto.precio = float.Parse(productXML.Attribute("Precio").Value);
                producto.fecha = DateTime.Parse (productXML.Attribute("FechaEntrada").Value);
                producto.stock = int.Parse(productXML.Attribute("Stock").Value);
                producto.brand = productXML.Parent.Attribute("Nombre").Value;
                producto.category = productXML.Parent.Parent.Attribute("IdProducto").Value;
                producto.publish = bool.Parse(productXML.Attribute("Publish").Value);
                productList.Add(producto);
            }

            return productList;
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

        public static void ModificarProducto(ProductosOptica p)
        {

            CargarXML();
            var listaReferenciaXML = xml.Root.Elements("Productos").Elements("Marca").Elements("Articulo").Attributes("Referencia");
            foreach (XAttribute productoRef in listaReferenciaXML)
            {

                if (p.referencia == productoRef.Value)
                {

                    productoRef.Parent.Remove();
                    break;
                }
            }
            GuardarXML();
            AñadirProductoXML(p);
        }

        public static void EliminarProducto(String referencia) {

            CargarXML();
            var listaReferenciaXML = xml.Root.Elements("Productos").Elements("Marca").Elements("Articulo").Attributes("Referencia");
            foreach (XAttribute productoRef in listaReferenciaXML) {

                if (referencia == productoRef.Value) {

                    productoRef.Parent.Remove();
                    break;
                }
            }
            GuardarXML();
        }

        private static void AddArticulo() {

            XElement xmlArticulo = new XElement("Articulo", new XAttribute("Referencia", producto.referencia),
                                    new XAttribute("Descripcion", producto.descripcion), new XAttribute("Tipo", producto.tipo),
                                    new XAttribute("Precio", producto.precio), new XAttribute("FechaEntrada", producto.fecha),
                                    new XAttribute("Stock", producto.stock), new XAttribute("Publish", producto.publish));
           
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
           
            }
            if (isNewBrand)
            {

                xmlProductos.Add(xmlBrand);
            }
        }
    }
}
