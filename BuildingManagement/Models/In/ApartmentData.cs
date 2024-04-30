using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.In
{
    public class ApartmentData
    {
        public int Floor { get; set; }
        public int Number { get; set; }
        public OwnerData Owner { get; set; } 
        public int NumberOfBathrooms { get; set; }
        public bool HasTerrace { get; set; }
        public Apartment ToEntity()
        {
            return new Apartment
            {
                Floor = this.Floor,
                Number = this.Number,
                Owner = this.Owner.ToEntity(), 
                NumberOfBathrooms = this.NumberOfBathrooms,
                HasTerrace = this.HasTerrace
            };
        }
    }


}
