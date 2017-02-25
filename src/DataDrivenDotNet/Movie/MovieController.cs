using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DataDrivenDotNet.Movie;

namespace DataDrivenDotNet.Movie
{
    [Route("api/[controller]")]
    public sealed class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _movieRepository.Get();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return _movieRepository.Get(id);
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
