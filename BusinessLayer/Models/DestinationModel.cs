using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class DestinationModel
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}
