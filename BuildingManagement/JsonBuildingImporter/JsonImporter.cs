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
            var rootObject = JsonSerializer.Deserialize<Root>(jsonString);

            foreach (var edificio in rootObject.Edificios)
            {
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
                Console.WriteLine(edificio.Nombre);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error al leer el archivo JSON: {ex.Message}");
        }

        return buildings;
    }
}
