﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.In
{
    public class UpdateBuildingRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ConstructionCompany { get; set; }
        public int CommonExpenses { get; set; }

        public Building ToEntity()
        {
            return new Building
            {
                Name = this.Name,
                Address = this.Address,
                Location = new Location
                {
                    Latitude = this.Latitude,
                    Longitude = this.Longitude
                },
                ConstructionCompany = this.ConstructionCompany,
                CommonExpenses = this.CommonExpenses
            };
        }
    }

}