﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechResourceTrackerDataHandling.Models;

namespace TechResourceTrackerDataHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedsController : ControllerBase
    {
        private readonly TechResourcesContext _context;
        public FeedsController(TechResourcesContext context)
        {
            _context = context;
        }

        // GET: api/Feeds
        [HttpGet]
        public IEnumerable<Feed> GetFeed()
        {
            return _context.Feed;
        }

        // GET: api/Feeds/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeed(int id)
        {
            var feed = await _context.Feed.FindAsync(id);

            if (feed == null)
            {
                return NotFound();
            }

            return Ok(feed);
        }

        // PUT: api/Feeds/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeed(int id, Feed feed)
        {
            if (id != feed.Id)
            {
                return BadRequest();
            }
                
            _context.Entry(feed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Feeds
        [HttpPost]
        public async Task<IActionResult> PostFeed(Feed feed)
        {
            _context.Feed.Add(feed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeed", new { id = feed.Id }, feed);
        }

        // DELETE: api/Feeds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeed([FromRoute] int id)
        {
            var feed = await _context.Feed.FindAsync(id);
            if (feed == null)
            {
                return NotFound();
            }

            _context.Feed.Remove(feed);
            await _context.SaveChangesAsync();

            return Ok(feed);
        }

        private bool FeedExists(int id)
        {
            return _context.Feed.Any(e => e.Id == id);
        }
    }
}