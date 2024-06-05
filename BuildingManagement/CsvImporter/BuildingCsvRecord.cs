using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvImporter
{
    public class BuildingCsvRecord
    {
        public string Name { get; set; }
        public string AddressStreet { get; set; }
        public int AddressNumber { get; set; }
        public string AddressSecondary { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CommonExpenses { get; set; }
        public int ApartmentFloor { get; set; }
        public int ApartmentNumber { get; set; }
        public bool ApartmentHasTerrace { get; set; }
        public int ApartmentBathrooms { get; set; }
        public int ApartmentRooms { get; set; }
        public string OwnerEmail { get; set; }
        public string ManagerEmail { get; set; }
    }
}
