using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Repository;
using WebApi.Services;
using WebApi.Models;
using WebApi.Models.Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get([FromQuery] int? Id, [FromQuery] string? Title, [FromQuery] string? DirectorName)
        {
            if (Id is not null && (Title is not null || DirectorName is not null))
                return BadRequest();

            List<Movie> movies = new List<Movie>();
            if (Id is not null)
            {
                BaseSpecification<Movie> specification = new BaseSpecification<Movie>(e => e.Id == Id);
                movies = _movieService.Get(specification).ToList();
            }
            else if (Title is not null || DirectorName is not null)
            {
                BaseSpecification<Movie> specification = new BaseSpecification<Movie>(e => e.Title == Title || e.Director.Name == DirectorName);
                movies = _movieService.Get(specification).ToList();
            }
            else
            {
                movies = _movieService.Get().ToList();
            }
            return Ok(movies);
        }
        [HttpPost]
        public ActionResult<Movie> Post([FromBody] MovieInput input)
        {
            var movie = _movieService.Add(input);
            return Created($"{movie.Id}", movie);
        }
        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
            _movieService.Delete(id);
        }

        [HttpPatch("{id}")]
        public void Update([FromRoute] int id, [FromBody] MovieInput input)
        {
            _movieService.Update(id, input);
        }
    }
}
