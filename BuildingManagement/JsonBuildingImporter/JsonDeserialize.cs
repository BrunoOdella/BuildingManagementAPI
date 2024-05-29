using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JsonImporter
{
    public class Direccion
    {
        public string CallePrincipal { get; set; }
        public int NumeroPuerta { get; set; }
        public string CalleSecundaria { get; set; }
    }

    public class Gps
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }

    public class Departamento
    {
        public int Piso { get; set; }
        public int NumeroPuerta { get; set; }
        public int Habitaciones { get; set; }
        [JsonPropertyName("conTerraza")]
        public bool ConTerraza { get; set; }
        public int Baños { get; set; }
        public string PropietarioEmail { get; set; }
    }

    public class Edificio
    {
        public string Nombre { get; set; }
        public Direccion Direccion { get; set; }
        public string Encargado { get; set; }
        public Gps Gps { get; set; }
        public int GastosComunes { get; set; }
        public List<Departamento> Departamentos { get; set; }
    }

    public class Root
    {
        public List<Edificio> Edificios { get; set; }
    }
}

