using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LayersOnWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public IEnumerable<CustomerModel> Get()
        {
            var result = new List<CustomerModel>();
            foreach (var x in customerService.GetAllCustomers())
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

        [HttpPost]
        public IActionResult Post(Guid Id, string Name, string Email, string Address, string City, string PhoneNumber)
        {
            try
            {
                customerService.AddCustomerModel(Id, Name, Email, Address, City, PhoneNumber);

                return Ok("Added");
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest("Couldn't add");
            }
        }

        [HttpGet("GetById")]
        public IEnumerable<CustomerModel> GetById(Guid id)
        {
            var result = new List<CustomerModel>();
            var x = customerService.GetById(id);
            result.Add(new CustomerModel
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

        [HttpPut("Update")]
        public IActionResult Put(Guid Id, string Name, string Email, string Address, string City, string PhoneNumber)
        {
            try
            {
                customerService.UpdateCustomerModel(Id, Name, Email, Address, City, PhoneNumber);

                return Ok();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest();
            }

        }

        [HttpDelete("Delete")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                customerService.DeleteCustomerModel(id);

                return Ok();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest();
            }
        }
    }
}
