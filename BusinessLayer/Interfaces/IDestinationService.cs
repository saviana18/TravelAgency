using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IDestinationService
    {
        public List<DestinationModel> GetAllDestinations();
        public DestinationModel GetById(Guid id);
        public void AddDestinationModel(Guid Id, string Country, string Description);
        public void UpdateDestinationModel(Guid Id, string Country, string Description);
        public void DeleteDestinationModel(Guid id);
    }
}
