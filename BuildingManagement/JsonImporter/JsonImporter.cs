using Domain;
using System.Text.Json;
using ImporterInterface;

namespace JsonImporter;

public class JsonImporter : IBuildingImporter
{
    public List<Building> ImportBuilding()
    {
        string path = @"D:\Ort\DA2\Buildings.json"; // Ruta del archivo //"D:\Ort\DA2\Buildings.json"

        List<Building> buildings = new List<Building>();

        try
        {
            string jsonString = File.ReadAllText(path);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            };
            Root rootObject = JsonSerializer.Deserialize<Root>(jsonString, options);

            if (rootObject != null && rootObject.Edificios != null)
            {
                Console.WriteLine("Deserialización exitosa!");

                foreach (var edificio in rootObject.Edificios)
                {
                    Console.WriteLine($"Nombre del Edificio: {edificio.Nombre}");

                    Building building = new Building
                    {
                        Name = edificio.Nombre,
                        Address = $"{edificio.Direccion.CallePrincipal} {edificio.Direccion.NumeroPuerta}, {edificio.Direccion.CalleSecundaria}",
                        Location = new Location
                        {
                            Latitude = edificio.Gps.Latitud,
                            Longitude = edificio.Gps.Longitud
                        },
                        CommonExpenses = edificio.GastosComunes,
                        Apartments = edificio.Departamentos.Select(d => new Apartment
                        {
                            Floor = d.Piso,
                            Number = d.NumeroPuerta,
                            HasTerrace = d.ConTerraza,
                            NumberOfBathrooms = d.Baños,
                            Owner = new Owner() { Email = d.PropietarioEmail },

                        }).ToList(),
                        Manager = new Manager() { Email = edificio.Encargado },
                    };

                    buildings.Add(building);
                    Console.WriteLine(edificio.Nombre);
                }
            }
            else
            {
                Console.WriteLine("Deserialización fallida o lista de edificios es null.");
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error en la deserialización: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
        }

        return buildings;
    }
}
