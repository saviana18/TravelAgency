using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using BusinessLayer.Observer;
using BusinessLayer.Observer.Interfaces;
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
    public class BillingService : IBillingService
    {
        private readonly IGenericRepository repository;
        public List<IBillingObserver> Observers = new List<IBillingObserver>();

        public BillingService(IGenericRepository repository)
        {
            this.repository = repository;
        }

        public void AddBillingModel(Guid Id, Guid BookingId, string AdditionalComments, DateTime Date)
        {
            try
            {
                repository.Add<BillingEntity>(new BillingEntity
                {
                    Id = Guid.NewGuid(),
                    Booking = repository.GetAll<BookingEntity>().Where(x => x.Id == BookingId).First(),
                    AdditionalComments = AdditionalComments,
                    Date = DateTime.Now
                 });

                repository.SaveChanges();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void DeleteBillingModel(Guid id)
        {
            try
            {
                var billing = repository.GetById<BillingEntity>(id);
                repository.Delete<BillingEntity>(billing);
                repository.SaveChanges();

            }
            catch (NullReferenceException e)
            {
                _ = e.StackTrace;
            }
        }

        public List<BillingModel> GetAllBillings()
        {
            try
            {
                List<BillingModel> result = new List<BillingModel>();
                foreach (var x in repository.GetAll<BillingEntity>().Include(x => x.Booking).ToList())
                {
                    result.Add(new BillingModel
                    {
                        Id = x.Id,
                        BookingId = x.Booking.Id,
                        AdditionalComments = x.AdditionalComments,
                        Date = x.Date
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public BillingModel GetById(Guid id)
        {
            try
            {
                BillingModel result = new BillingModel();
                var x = repository.GetAll<BillingEntity>().Include(x => x.Booking).FirstOrDefault(x => x.Id == id);
                result = (new BillingModel
                {
                    Id = x.Id,
                    BookingId = x.Booking.Id,
                    AdditionalComments = x.AdditionalComments,
                    Date = x.Date
                });

                var smsObserver = new SmsObserver();
                var emailObserver = new EmailObserver();

                Attach(smsObserver);
                Attach(emailObserver);
                Notify(result);

                return result;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
        }

        public void UpdateBillingModel(Guid Id, Guid BookingId, string AdditionalComments, DateTime Date)
        {
            try
            {
                var booking = repository.GetAll<BookingEntity>().Where(x => x.Id == BookingId).First();

                var billing1 = repository.GetById<BillingEntity>(Id);
                billing1.Id = Id;
                billing1.AdditionalComments = AdditionalComments;
                billing1.Date = Date;
                billing1.Booking = booking;

                repository.Update(billing1);
                repository.SaveChanges();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void Attach(IBillingObserver observer)
        {
            Observers.Add(observer);
        }

        public void Detach(IBillingObserver observer)
        {
            Observers.Remove(observer);
        }

        public void Notify(BillingModel billing)
        {
            foreach (var observer in Observers)
            {
                observer.Update(billing);
            }
        }
    }
}
