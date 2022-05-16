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
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository repository;

        public BookingService(IGenericRepository repository)
        {
            this.repository = repository;
        }

        public string AddBookingModel(Guid Id, Guid CustomerId, Guid OfferId, int NoOfBookedSeats, float TotalPrice)
        {
            try
            {
                var offer = repository.GetAll<OfferEntity>().Where(x => x.Id == OfferId).First();
                var customer = repository.GetAll<CustomerEntity>().Where(x => x.Id == CustomerId).First();

                repository.Add<BookingEntity>(new BookingEntity
                {
                    Id = Guid.NewGuid(),
                    Customer = customer,
                    Offer = repository.GetAll<OfferEntity>().Where(x => x.Id == OfferId).First(),
                    NoOfBookedSeats = NoOfBookedSeats,
                    TotalPrice = offer.Price * NoOfBookedSeats
                });

                
                

                if (offer.NoOfAvailableSpots > 0)
                {
                    offer.NoOfAvailableSpots -= NoOfBookedSeats;

                    repository.Update<OfferEntity>(offer);
                    repository.SaveChanges();
                    return "There still are available spots for this offer";
                }
                else
                {
                    return "There are no more available spots for this offer";
                }

            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return "Not working properly";
            }
        }

        public void DeleteBookingModel(Guid id, Guid OfferId)
        {
            try
            {
                var booking = repository.GetById<BookingEntity>(id);
                var offer = repository.GetAll<OfferEntity>().Where(x => x.Id == OfferId).First();

                repository.Delete<BookingEntity>(booking);

                offer.NoOfAvailableSpots += booking.NoOfBookedSeats;
                repository.Update<OfferEntity>(offer);

                repository.SaveChanges();

            }
            catch (NullReferenceException e)
            {
                _ = e.StackTrace;
            }
        }

        public List<BookingModel> GetAllBookings()
        {
            try
            {
                List<BookingModel> result = new List<BookingModel>();
                foreach (var x in repository.GetAll<BookingEntity>().Include(x => x.Customer).Include(x => x.Offer).ToList())
                {
                    result.Add(new BookingModel
                    {
                        Id = x.Id,
                        CustomerId = x.Customer.Id,
                        OfferId = x.Offer.Id,
                        NoOfBookedSeats = x.NoOfBookedSeats,
                        TotalPrice = x.TotalPrice
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public BookingModel GetById(Guid id)
        {
            try
            {
                BookingModel result = new BookingModel();
                var x = repository.GetAll<BookingEntity>().Include(x => x.Customer).Include(x => x.Offer).FirstOrDefault(x => x.Id == id);
                result = (new BookingModel
                {
                    Id = x.Id,
                    CustomerId = x.Customer.Id,
                    OfferId = x.Offer.Id,
                    NoOfBookedSeats = x.NoOfBookedSeats,
                    TotalPrice = x.TotalPrice
                });

                return result;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
        }

        public void UpdateBookingModel(Guid Id, Guid CustomerId, Guid OfferId, int NoOfBookedSeats, float TotalPrice)
        {
            try
            {
                var customer = repository.GetAll<CustomerEntity>().Where(x => x.Id == CustomerId).First();
                var offer = repository.GetAll<OfferEntity>().Include(x => x.Destination).Where(x => x.Id == OfferId).First();
                Guid idBooking = Id;
                var booking1 = repository.GetAll<BookingEntity>().Include(x => x.Customer).Include(x => x.Offer).FirstOrDefault(x => x.Id == idBooking);
                booking1.Id = idBooking;
                booking1.Customer = customer;
                booking1.Offer = offer;
                booking1.NoOfBookedSeats = NoOfBookedSeats;
                booking1.TotalPrice = offer.Price * booking1.NoOfBookedSeats;

                repository.Update(booking1);
                repository.SaveChanges();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        
    }
}
