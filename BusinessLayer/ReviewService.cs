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
    public class ReviewService : IReviewService
    {
        private readonly IGenericRepository repository;

        public ReviewService(IGenericRepository repository)
        {
            this.repository = repository;
        }
        public void AddReviewModel(Guid Id, string Message, Guid CustomerId, Guid OfferId)
        {
            try
            {
                repository.Add<ReviewEntity>(new ReviewEntity
                {
                    Id = Guid.NewGuid(),
                    Message = Message,
                    Customer = repository.GetAll<CustomerEntity>().Where(x => x.Id == CustomerId).First(),
                    Offer = repository.GetAll<OfferEntity>().Where(x => x.Id == OfferId).First()
                });

                repository.SaveChanges();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void DeleteReviewModel(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ReviewModel> GetAllReviews()
        {
            try
            {
                List<ReviewModel> result = new List<ReviewModel>();
                foreach (var x in repository.GetAll<ReviewEntity>().Include(x => x.Customer).Include(x => x.Offer).ToList())
                {
                    result.Add(new ReviewModel
                    {
                        Id = x.Id,
                        Message = x.Message,
                        CustomerId = x.Customer.Id,
                        OfferId = x.Offer.Id
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ReviewModel GetById(Guid id)
        {
            try
            {
                ReviewModel result = new ReviewModel();
                var x = repository.GetAll<ReviewEntity>().Include(x => x.Customer).Include(x => x.Offer).FirstOrDefault(x => x.Id == id);
                result = (new ReviewModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    CustomerId = x.Customer.Id,
                    OfferId= x.Offer.Id
                });

                return result;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
        }

        public void UpdateReviewModel(Guid Id, string Message, Guid CustomerId, Guid OfferId)
        {
            try
            {
                var offer = repository.GetAll<OfferEntity>().Include(x => x.Destination).Where(x => x.Id == OfferId).First();
                var customer = repository.GetAll<CustomerEntity>().Where(x => x.Id == CustomerId).First();
                Guid idReview = Id;
                var review1 = repository.GetAll<ReviewEntity>().Include(x => x.Customer).Include(x => x.Offer).FirstOrDefault(x => x.Id == idReview);
                review1.Id = idReview;
                review1.Message = Message;
                review1.Customer = customer;
                review1.Offer = offer;

                repository.Update(review1);
                repository.SaveChanges();
                
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }
    }
}
