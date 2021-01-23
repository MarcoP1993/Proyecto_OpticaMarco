using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDI_OpticaMarco.ClaseProductos
{
    class ProductosOptica : ICloneable
    {

        public String referencia { set; get; }
        public String descripcion { set; get; }
        public String tipo { set; get; }
        public float precio { set; get; }
        public DateTime fecha { set; get; }
        public int stock { set; get; }

        public ProductosOptica(string referencia, string descripcion, string tipo, float precio, DateTime fecha, int stock)
        {
            this.referencia = referencia;
            this.descripcion = descripcion;
            this.tipo = tipo;
            this.precio = precio;
            this.fecha = fecha;
            this.stock = stock;
        }

        public ProductosOptica()
        {
        }

        public override string ToString()
        {
            return "Tipo: " + tipo + ", Descripcion: " + descripcion;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
