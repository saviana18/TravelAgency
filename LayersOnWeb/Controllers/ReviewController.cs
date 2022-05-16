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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        public IEnumerable<ReviewModel> Get()
        {
            var result = new List<ReviewModel>();
            foreach (var x in reviewService.GetAllReviews())
            {
                result.Add(new ReviewModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    CustomerId = x.CustomerId,
                    OfferId = x.OfferId
                });
            }
            return result;
        }

        [HttpPost]
        public IActionResult Post(Guid Id, string Message, Guid CustomerId, Guid OfferId)
        {
            try
            {
                reviewService.AddReviewModel(Id, Message, CustomerId, OfferId);

                return Ok("Added");
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest("Couldn't add");
            }
        }

        [HttpGet("GetById")]
        public IEnumerable<ReviewModel> GetById(Guid id)
        {
            var result = new List<ReviewModel>();
            var x = reviewService.GetById(id);
            result.Add(new ReviewModel
            {
                Id = x.Id,
                Message = x.Message,
                CustomerId = x.CustomerId,
                OfferId = x.OfferId
            });

            return result;
        }

        [HttpPut("Update")]
        public IActionResult Put(Guid Id, string Message, Guid CustomerId, Guid OfferId)
        {
            try
            {
                reviewService.UpdateReviewModel(Id, Message, CustomerId, OfferId);

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
                reviewService.DeleteReviewModel(id);

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
