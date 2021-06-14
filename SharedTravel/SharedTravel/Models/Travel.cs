using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTravel.Models
{
    public class Travel
    {
        public int TravelId{ get; set; }

        [DisplayName("Creator")]
        public User Creator { get; set; }
        public int CreatorId { get; set; }

        [DisplayName("From")]
        public string CityFrom { get; set; }
        [DisplayName("From Address")]
        public string AddressFrom { get; set; }
        [DisplayName("Departure Time")]
        public DateTime DepartureTime { get; set; }

        [DisplayName("To")]
        public string CityTo { get; set; }
        [DisplayName("To Address")]
        public string AddressTo { get; set; }
        [DisplayName("Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        [DisplayName("Free seats")]
        public int FreeSeatsCount { get; set; }
        public decimal Price { get; set; }

        public List<UserTravel> UserTravels { get; set; }
        public List<PendingInvite> PendingInvites { get; set; }

    }
}
