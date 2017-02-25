using System;
using System.Collections.Generic;

namespace DataDrivenDotNet.Api.Movie
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> Get();
        Movie Get(int id);
        void Create(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
        void Subscribe(Action onUpdate);
    }
}
