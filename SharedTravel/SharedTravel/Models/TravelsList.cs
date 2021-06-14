using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTravel.Models
{
    public class TravelsList
    {
        public IEnumerable<Travel> Travels { get; set; }

        public string SearchedName { get; set; }

    }
}
