using WebApi.Repository;
using WebApi.Models;
using WebApi.Models.Dto;
using WebApi.Models.Mapper;

namespace WebApi.Services
{
    public class MovieService
    {
        private readonly GenericRepository<Movie> _movies;

        public MovieService(GenericRepository<Movie> movies)
        {
            _movies = movies;
        }

        public Movie Add(MovieInput input)
        {
            var mapped = Mapper.Map(e =>
            new Movie()
            {
                Director = new Director() { Name = e.Name, SureName = e.SureName },
                Date = e.Date is null ? null : e.Date,
                Title = e.Title
            }, input);
            var output = _movies.Add(mapped);
            return output;
        }
        public IEnumerable<Movie> Get(ISpecification<Movie> specification = null)
        {
            var movies = _movies.Find(specification);
            return movies;
        }
        public void Delete(int id)
        {
            _movies.RemoveById(id);
        }
        public void Update(int id, MovieInput input)
        {
            var mapped = Mapper.Map(e =>
            new Movie()
            {
                Director = new Director() { Name = e.Name, SureName = e.SureName },
                Date = e.Date is null ? null : e.Date,
                Title = e.Title
            }, input);
            mapped.Id = id;
            _movies.Update(id, mapped);
        }

    }
}
