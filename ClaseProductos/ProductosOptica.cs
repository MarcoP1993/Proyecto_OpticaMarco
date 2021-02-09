using System;
using System.Windows.Media.Imaging;

namespace ProyectoDI_OpticaMarco.ClaseProductos
{
    public class ProductosOptica : ICloneable
    {

        public String category { get; set; }
        public String brand { get; set; }
        public String referencia { set; get; }
        public String descripcion { set; get; }
        public String tipo { set; get; }
        public float precio { set; get; }
        public DateTime fecha { set; get; }
        public int stock { set; get; }

        public bool publish { set; get; }
        public BitmapImage imagen { set; get; }

        public ProductosOptica(string referencia, string category, string brand, string descripcion, string tipo, float precio, DateTime fecha, int stock)
        {
            this.referencia = referencia;
            this.category = category;
            this.brand = brand;
            this.descripcion = descripcion;
            this.tipo = tipo;
            this.precio = precio;
            this.fecha = fecha;
            this.stock = stock;
            this.publish = false;
        }

        public ProductosOptica()
        {
            this.fecha = DateTime.Today;
            this.publish = false;
        }

        public String ObtenerTodosLosValores() {

            return brand + " " + descripcion + " " + referencia + " " + tipo;
        }

        public override string ToString()
        {
            return "Categoria: " + category + "Descripcion: " + descripcion;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
