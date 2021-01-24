using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDI_OpticaMarco.ClaseProductos
{
    public class ProductHandler
    {
        public ObservableCollection<ProductosOptica> productoList { get; set; }

        public ProductHandler() {
            this.productoList = new ObservableCollection<ProductosOptica>();
        }

        public void AddProducto(ProductosOptica productos) {

            productoList.Add(productos);
        }

        public void ModificarProducto(ProductosOptica productos, int pos) {

            productoList[pos] = productos;
        }

    }
}
