using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JsonImporter
{
    public class Direccion
    {
        [JsonPropertyName("calle_principal")]
        public string CallePrincipal { get; set; }
        [JsonPropertyName("numero_puerta")]
        public int NumeroPuerta { get; set; }
        [JsonPropertyName("calle_secundaria")]
        public string CalleSecundaria { get; set; }
    }

    public class Gps
    {
        [JsonPropertyName("latitud")]
        public double Latitud { get; set; }
        [JsonPropertyName("longitud")]
        public double Longitud { get; set; }
    }

    public class Departamento
    {
        public int Piso { get; set; }
        [JsonPropertyName("numero_puerta")]
        public int NumeroPuerta { get; set; }
        public int Habitaciones { get; set; }
        [JsonPropertyName("conTerraza")]
        public bool ConTerraza { get; set; }
        [JsonPropertyName("baños")]
        public int Baños { get; set; }
        [JsonPropertyName("propietarioEmail")]
        public string PropietarioEmail { get; set; }
    }

    public class Edificio
    {
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("direccion")]
        public Direccion Direccion { get; set; }
        [JsonPropertyName("encargado")]
        public string Encargado { get; set; }
        [JsonPropertyName("gps")]
        public Gps Gps { get; set; }
        [JsonPropertyName("gastos_comunes")]
        public int GastosComunes { get; set; }
        [JsonPropertyName("departamentos")]
        public List<Departamento> Departamentos { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("edificios")]
        public List<Edificio> Edificios { get; set; }
    }
}