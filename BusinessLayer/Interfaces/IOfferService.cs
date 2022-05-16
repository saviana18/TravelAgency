using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IOfferService
    {
        public List<OfferModel> GetAllOffers();
        public OfferModel GetById(Guid id);
        public void AddOfferModel(Guid Id, string Name, string Description, float Price, int NoOfAvailableSpots, Guid DestinationId);
        public void UpdateOfferModel(Guid Id, string Name, string Description, float Price, int NoOfAvailableSpots, Guid DestinationId);
        public void DeleteOfferModel(Guid id);
        public List<OfferModel> GetOffersByDestination(string Name);
        public List<OfferModel> GetOffersWithLowerPrice(float Price);
    }
}
