using System.Collections.Generic;

namespace DataDrivenDotNet.Movie
{
    public sealed class MovieRepository : IMovieRepository
    {
        public IEnumerable<string> Get()
        {
            return new[] { "Movie 1", "Movie 2" };
        }

        public string Get(int id)
        {
            return "Movie 1";
        }
    }
}
