using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using GameReview.Dto;
using GameReview.Models;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace GameReview.Controllers.Api
{

    public class ReviewsController : ApiController
    {
        private ApplicationDbContext _context;

        public ReviewsController()
        {
            _context = new ApplicationDbContext();
        }


        //GET /api/reviews
        public IHttpActionResult GetReviews()
        {
           var reviews = _context.Reviews.ToList();

           return Ok(reviews);
        }

        //GET /api/reviews/1
        public IHttpActionResult GetReview(int id)
        {
            var review = _context.Reviews.SingleOrDefault(r => r.Id == id);

            if (review == null)
                return NotFound();

            return Ok(review);
        }

        //POST /api/reviews
        [HttpPost]
        public IHttpActionResult CreateReview(ReviewDto reviewDto)
        {
            
            var review = Mapper.Map<ReviewDto, Review>(reviewDto);

            _context.Reviews.Add(review);
            _context.SaveChanges();

            var averageRating = _context.Reviews
                .Where(r => r.GameId == review.GameId)
                .Average(r => r.RatingScore);

            var gameInDb = _context.Games.SingleOrDefault(r => r.GameIdentifier == review.GameId);

            if (gameInDb != null) gameInDb.AverageRating = averageRating;
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + review.Id), reviewDto);
        }

        //PUT /api/reviews/1
        [System.Web.Http.HttpPut]
        public IHttpActionResult UpdateReview(int id, ReviewDto reviewDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var reviewInDb = _context.Reviews.SingleOrDefault(r => r.Id == id);
            
            if (reviewInDb == null)
                return NotFound();

            Mapper.Map(reviewDto, reviewInDb);
            _context.SaveChanges();

            return Ok();
        }

        //DELETE /api/reviews/1
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteReview(int id)
        {
            var reviewInDb = _context.Reviews.SingleOrDefault(r => r.Id == id);

            if (reviewInDb == null)
                return NotFound();

            _context.Reviews.Remove(reviewInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
