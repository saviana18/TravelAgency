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
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository repository;

        public CustomerService(IGenericRepository repository)
        {
            this.repository = repository;
        }

        public void AddCustomerModel(Guid Id, string Name, string Email, string Address, string City, string PhoneNumber)
        {
            try
            {
                repository.Add<CustomerEntity>(new CustomerEntity
                {
                     Id = Guid.NewGuid(),
                     Name = Name,
                     Email = Email,
                     Address =  Address,
                     City = City,
                     PhoneNumber = PhoneNumber
                });

                repository.SaveChanges();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void DeleteCustomerModel(Guid id)
        {
            try
            {
                var customer = repository.GetById<CustomerEntity>(id);
                repository.Delete<CustomerEntity>(customer);
                repository.SaveChanges();

            }
            catch (NullReferenceException e)
            {
                _ = e.StackTrace;
            }
        }

        public List<CustomerModel> GetAllCustomers()
        {
            try
            {
                List<CustomerModel> result = new List<CustomerModel>();
                foreach (var x in repository.GetAll<CustomerEntity>())
                {
                    result.Add(new CustomerModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        Address = x.Address,
                        City = x.City,
                        PhoneNumber = x.PhoneNumber
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public CustomerModel GetById(Guid id)
        {
            try
            {
                CustomerModel result = new CustomerModel();
                var x = repository.GetById<CustomerEntity>(id);
                result = (new CustomerModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Address = x.Address,
                    City = x.City,
                    PhoneNumber = x.PhoneNumber
                });

                return result;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
        }

        public void UpdateCustomerModel(Guid Id, string Name, string Email, string Address, string City, string PhoneNumber)
        {
            try
            {
                var customer1 = repository.GetById<CustomerEntity>(Id);
                customer1.Id = Id;
                customer1.Name = Name;
                customer1.Email = Email;
                customer1.Address = Address;
                customer1.City = City;
                customer1.PhoneNumber = PhoneNumber;

                repository.Update(customer1);
                repository.SaveChanges();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }

        public void UpdateObserver(BillingModel billing)
        {
            Console.WriteLine("The billing '{0}' for the booking '{1} was exported. An email was sent to the customer.",
                billing.Id.ToString(), billing.BookingId.ToString());
        }
    }
}
