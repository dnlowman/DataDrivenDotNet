using System.Collections.Generic;
using System.Web.Http;

namespace DataDrivenDotNet.Movie
{
    [System.Web.Mvc.Route("api/[controller]")]
    public sealed class MovieController : ApiController
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        
        public IEnumerable<string> Get()
        {
            return _movieRepository.Get();
        }

        [Route(("{id}"))]
        public string Get(int id)
        {
            return _movieRepository.Get(id);
        }

        [System.Web.Mvc.HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [Route()]
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
