using CsvHelper;
using CsvHelper.Configuration;
using ImporterInterface;
using System.Globalization;

namespace CsvImporter
{
    public class CsvImporter : IBuildingImporter
    {
        public List<BuildingDTO> ImportBuilding()
        {
            string path = @"D:\Ort\DA2\Buildings.csv"; // Ruta del archivo CSV

            List<BuildingDTO> buildings = new List<BuildingDTO>();

            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                }))
                {
                    var records = csv.GetRecords<BuildingCsvRecord>();

                    var groupedRecords = records.GroupBy(r => new { r.Name, r.AddressStreet, r.AddressNumber, r.AddressSecondary, r.Latitude, r.Longitude, r.CommonExpenses, r.ManagerEmail });

                    foreach (var group in groupedRecords)
                    {
                        var buildingDto = new BuildingDTO
                        {
                            Name = group.Key.Name,
                            Address = $"{group.Key.AddressStreet} {group.Key.AddressNumber}, {group.Key.AddressSecondary}",
                            Location = new LocationDTO
                            {
                                Latitude = group.Key.Latitude,
                                Longitude = group.Key.Longitude
                            },
                            CommonExpenses = group.Key.CommonExpenses,
                            Apartments = group.Select(a => new ApartmentDTO
                            {
                                Floor = a.ApartmentFloor,
                                Number = a.ApartmentNumber,
                                HasTerrace = a.ApartmentHasTerrace,
                                NumberOfBathrooms = a.ApartmentBathrooms,
                                NumberOfRooms = a.ApartmentRooms,
                                Owner = new OwnerDTO { Email = a.OwnerEmail }
                            }).ToList(),
                            Manager = string.IsNullOrEmpty(group.Key.ManagerEmail) ? null : new ManagerDTO { Email = group.Key.ManagerEmail }
                        };

                        buildings.Add(buildingDto);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo CSV: {ex.Message}");
            }

            return buildings;
        }
    }
}
