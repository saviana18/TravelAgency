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
    public class DestinationController : ControllerBase
    {
        private readonly IDestinationService destinationService;

        public DestinationController(IDestinationService destinationService)
        {
            this.destinationService = destinationService;
        }

        [HttpGet]
        public IEnumerable<DestinationModel> Get()
        {
            var result = new List<DestinationModel>();
            foreach (var x in destinationService.GetAllDestinations())
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

        [HttpPost]
        public IActionResult Post(Guid Id, string Country, string Description)
        {
            try
            {
                destinationService.AddDestinationModel(Id, Country, Description);

                return Ok("Added");
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest("Couldn't add");
            }
        }

        [HttpGet("GetById")]
        public IEnumerable<DestinationModel> GetById(Guid id)
        {
            var result = new List<DestinationModel>();
            var x = destinationService.GetById(id);
            result.Add(new DestinationModel
            {
                Id = x.Id,
                Country = x.Country,
                Description = x.Description
            });

            return result;
        }

        [HttpPut("Update")]
        public IActionResult Put(Guid Id, string Country, string Description)
        {
            try
            {
                destinationService.UpdateDestinationModel(Id, Country, Description);

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
                destinationService.DeleteDestinationModel(id);

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
