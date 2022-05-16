using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DestinationService : IDestinationService
    {
        private readonly IGenericRepository repository;

        public DestinationService(IGenericRepository repository)
        {
            this.repository = repository;
        }
        public void AddDestinationModel(Guid Id, string Country, string Description)
        {
            try
            {
                repository.Add<DestinationEntity>(new DestinationEntity
                {
                    Id = Guid.NewGuid(),
                    Country = Country,
                    Description = Description
                });

                repository.SaveChanges();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void DeleteDestinationModel(Guid id)
        {
            try
            {
                var destination = repository.GetById<DestinationEntity>(id);
                repository.Delete<DestinationEntity>(destination);
                repository.SaveChanges();

            }
            catch (NullReferenceException e)
            {
                _ = e.StackTrace;
            }
        }

        public List<DestinationModel> GetAllDestinations()
        {
            try
            {
                List<DestinationModel> result = new List<DestinationModel>();
                foreach (var x in repository.GetAll<DestinationEntity>())
                {
                    result.Add(new DestinationModel
                    {
                        Id = x.Id,
                        Country = x.Country,
                        Description = x.Description
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DestinationModel GetById(Guid id)
        {
            try
            {
                DestinationModel result = new DestinationModel();
                var x = repository.GetById<DestinationEntity>(id);
                result = (new DestinationModel
                {
                    Id = x.Id,
                    Country = x.Country,
                    Description = x.Description
                });

                return result;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
        }

        public void UpdateDestinationModel(Guid Id, string Country, string Description)
        {
            try
            {
                var destination1 = repository.GetById<DestinationEntity>(Id);
                destination1.Id = Id;
                destination1.Country = Country;
                destination1.Description = Description;

                repository.Update(destination1);
                repository.SaveChanges();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }
    }
}
