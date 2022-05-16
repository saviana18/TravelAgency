using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICustomerService
    {
        public List<CustomerModel> GetAllCustomers();
        public CustomerModel GetById(Guid id);
        public void AddCustomerModel(Guid Id, string Name, string Email, string Address, string City, string PhoneNumber);
        public void UpdateCustomerModel(Guid Id, string Name, string Email, string Address, string City, string PhoneNumber);
        public void DeleteCustomerModel(Guid id);
    }
}
