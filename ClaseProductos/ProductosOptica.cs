﻿using System;

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

        public ProductosOptica(string category, string brand, string referencia, string descripcion, string tipo, float precio, DateTime fecha, int stock)
        {
            this.category = category;
            this.brand = brand;
            this.referencia = referencia;
            this.descripcion = descripcion;
            this.tipo = tipo;
            this.precio = precio;
            this.fecha = fecha;
            this.stock = stock;
        }

        public ProductosOptica()
        {
            this.fecha = DateTime.Today;
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
            throw new NotImplementedException();
        }
    }
}
