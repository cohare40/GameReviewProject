using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using GameReview.Dto;
using GameReview.Models;

namespace GameReview.Controllers.Api
{
    public class ReviewsController : ApiController
    {
        private ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
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
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateReview(ReviewDto reviewDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var review = Mapper.Map<ReviewDto, Review>(reviewDto);

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + review.Id), reviewDto);
        }

        //PUT /api/reviews/1
        [System.Web.Http.HttpPut]
        [ValidateAntiForgeryToken]
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
