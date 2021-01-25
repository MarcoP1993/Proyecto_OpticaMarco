using ProyectoDI_OpticaMarco.XML;
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
        public ObservableCollection<ProductosOptica> productoList { set; get; }

        public ProductHandler() {
            this.productoList = new ObservableCollection<ProductosOptica>();
            ActualizarProductList();
        }

        public void ActualizarProductList()
        {
            this.productoList = XMLHandler.CargarProductos();
        }

        public void AddProducto(ProductosOptica productos) {

            productoList.Add(productos);
        }

        public void ModificarProducto(ProductosOptica productos, int pos) {

            productoList[pos] = productos;
        }

        public void BorrarProducto(int pos)
        {
            productoList.RemoveAt(pos);
        }
    }
}
