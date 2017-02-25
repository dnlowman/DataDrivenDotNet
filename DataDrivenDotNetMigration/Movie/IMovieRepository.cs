using System.Collections.Generic;

namespace DataDrivenDotNet.Movie
{
    public interface IMovieRepository
    {
        IEnumerable<string> Get();
        string Get(int id);
    }
}
