using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTravel.Models
{
    public class UserTravel
    {
        public int UserId { get; set; }

        public int TravelId { get; set; }

        public User User { get; set; }

        public Travel Travel { get; set; }

    }
}
