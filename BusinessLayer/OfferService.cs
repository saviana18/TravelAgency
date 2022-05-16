using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class OfferService : IOfferService
    {
        private readonly IGenericRepository repository;

        public OfferService(IGenericRepository repository)
        {
            this.repository = repository;
        }
        public void AddOfferModel(Guid Id, string Name, string Description, float Price, int NoOfAvailableSpots, Guid DestinationId)
        {
            try
            {
                repository.Add<OfferEntity>(new OfferEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Name,
                    Description = Description,
                    Price = Price,
                    NoOfAvailableSpots = NoOfAvailableSpots,
                    Destination = repository.GetAll<DestinationEntity>().Where(x => x.Id == DestinationId).First()
                 });

                repository.SaveChanges();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void DeleteOfferModel(Guid id)
        {
            try
            {
                var offer = repository.GetById<OfferEntity>(id);
                repository.Delete<OfferEntity>(offer);
                repository.SaveChanges();

            }
            catch (NullReferenceException e)
            {
                _ = e.StackTrace;
            }
        }

        public List<OfferModel> GetAllOffers()
        {
            try
            {
                List<OfferModel> result = new List<OfferModel>();
                foreach (var x in repository.GetAll<OfferEntity>().Include(x => x.Destination).ToList())
                {
                    result.Add(new OfferModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        NoOfAvailableSpots = x.NoOfAvailableSpots,
                        DestinationId = x.Destination.Id
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public OfferModel GetById(Guid id)
        {
            try
            {
                OfferModel result = new OfferModel();
                var x = repository.GetAll<OfferEntity>().Include(x => x.Destination).FirstOrDefault(x => x.Id == id);
                result = (new OfferModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    NoOfAvailableSpots = x.NoOfAvailableSpots,
                    DestinationId = x.Destination.Id
                });

                return result;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
        }

        public List<OfferModel> GetOffersByDestination(string Name)
        {
            try
            {
                List<OfferModel> result = new List<OfferModel>();
                foreach (var x in repository.GetAll<OfferEntity>().Include(x => x.Destination).ToList())
                {
                    if (x.Destination.Country.Equals(Name))
                    {
                        result.Add(new OfferModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description,
                            Price = x.Price,
                            NoOfAvailableSpots = x.NoOfAvailableSpots,
                            DestinationId = x.Destination.Id
                        });
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<OfferModel> GetOffersWithLowerPrice(float Price)
        {
            try
            {
                List<OfferModel> result = new List<OfferModel>();
                foreach (var x in repository.GetAll<OfferEntity>().Include(x => x.Destination).ToList())
                {
                    if(x.Price < Price)
                    {
                        result.Add(new OfferModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Description = x.Description,
                            Price = x.Price,
                            NoOfAvailableSpots = x.NoOfAvailableSpots,
                            DestinationId = x.Destination.Id
                        });
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void UpdateOfferModel(Guid Id, string Name, string Description, float Price, int NoOfAvailableSpots, Guid DestinationId)
        {
            try
            {
                var destination = repository.GetAll<DestinationEntity>().Where(x => x.Id == DestinationId).First();
                Guid idOffer = Id;
                var offer1 = repository.GetAll<OfferEntity>().Include(x => x.Destination).FirstOrDefault(x => x.Id == idOffer);
                offer1.Id = idOffer;
                offer1.Name = Name;
                offer1.Description = Description;
                offer1.Price = Price;
                offer1.NoOfAvailableSpots = NoOfAvailableSpots;
                offer1.Destination = destination;

                repository.Update(offer1);
                repository.SaveChanges();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }
    }
}
