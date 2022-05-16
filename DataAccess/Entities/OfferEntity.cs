using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class OfferEntity
    {
        public Guid Id { get; set; }   
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int NoOfAvailableSpots { get; set; }
        public DestinationEntity Destination { get; set; }
    }
}
