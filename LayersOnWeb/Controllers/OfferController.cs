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
    public class OfferController : ControllerBase
    {
        private readonly IOfferService offerService;

        public OfferController(IOfferService offerService)
        {
            this.offerService = offerService;
        }

        [HttpGet]
        public IEnumerable<OfferModel> Get()
        {
            var result = new List<OfferModel>();
            foreach (var x in offerService.GetAllOffers())
            {
                result.Add(new OfferModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    NoOfAvailableSpots = x.NoOfAvailableSpots,
                    DestinationId = x.DestinationId
                });
            }
            return result;
        }

        [HttpPost]
        public IActionResult Post(Guid Id, string Name, string Description, float Price, int NoOfAvailableSpots, Guid DestinationId)
        {
            try
            {
                offerService.AddOfferModel(Id, Name, Description, Price, NoOfAvailableSpots, DestinationId);

                return Ok("Added");
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest("Couldn't add");
            }
        }

        [HttpGet("GetById")]
        public IEnumerable<OfferModel> GetById(Guid id)
        {
            var result = new List<OfferModel>();
            var x = offerService.GetById(id);
            result.Add(new OfferModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                NoOfAvailableSpots = x.NoOfAvailableSpots,
                DestinationId = x.DestinationId
            });

            return result;
        }

        [HttpPut("Update")]
        public IActionResult Put(Guid Id, string Name, string Description, float Price, int NoOfAvailableSpots, Guid DestinationId)
        {
            try
            {
                offerService.UpdateOfferModel(Id, Name, Description, Price, NoOfAvailableSpots, DestinationId);

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
                offerService.DeleteOfferModel(id);

                return Ok();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest();
            }
        }

        [HttpGet("GetByDestination")]
        public IEnumerable<OfferModel> GetByDestination(string Name)
        {
            var result = new List<OfferModel>();
            foreach (var x in offerService.GetOffersByDestination(Name))
            {
                result.Add(new OfferModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    NoOfAvailableSpots = x.NoOfAvailableSpots,
                    DestinationId = x.DestinationId
                });
            }
            return result;
        }

        [HttpGet("GetByLowerPrice")]
        public IEnumerable<OfferModel> GetByLowerPrice(float Price)
        {
            var result = new List<OfferModel>();
            foreach (var x in offerService.GetOffersWithLowerPrice(Price))
            {
                result.Add(new OfferModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    NoOfAvailableSpots = x.NoOfAvailableSpots,
                    DestinationId = x.DestinationId
                });
            }
            return result;
        }
    }
}
