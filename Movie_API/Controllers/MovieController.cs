using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MoviesDBContext _context = new MoviesDBContext();
        public MovieController(MoviesDBContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie newMovie)
        {
            _context.Movies.Add(newMovie);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { id = newMovie.Id }, newMovie);
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return movie;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutMovie(int id, Movie _movie)
        {
            if (id != _movie.Id || !ModelState.IsValid)
            {
                return BadRequest();

            }
            else
            {
                Movie dbMovie = _context.Movies.Find(id);
                dbMovie.Title = _movie.Title;
                dbMovie.Genre = _movie.Genre;
                dbMovie.Runtime = _movie.Runtime;

                _context.Entry(dbMovie).State = EntityState.Modified;
                _context.Update(dbMovie);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if(movie == null)
            {
                return NotFound();
            }
            else
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }


    }
}
