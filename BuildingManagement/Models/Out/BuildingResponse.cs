using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Out
{
    public class BuildingResponse
    {
        public Guid BuildingId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Location Location { get; set; }
        public string ConstructionCompanyName { get; set; }
        public int CommonExpenses { get; set; }
    }

}
