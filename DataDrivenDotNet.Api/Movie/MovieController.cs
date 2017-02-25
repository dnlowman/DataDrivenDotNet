using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DataDrivenDotNet.Api.Movie
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public sealed class MovieController : ApiController
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        
        public IEnumerable<Movie> Get()
        {
            return _movieRepository.Get();
        }

        public Movie Get(int id)
        {
            return _movieRepository.Get(id);
        }

        public HttpResponseMessage Post([FromBody]Movie movie)
        {
            try
            {
                _movieRepository.Create(movie);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Created the movie!");
            }
            catch(Exception)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, "Oops, something went wrong!");
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]Movie movie)
        {
            _movieRepository.Update(movie);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Updated!");
        }

        public HttpResponseMessage Delete(int id)
        {
            _movieRepository.Delete(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Deleted!");
        }

        class MovieWebSocketHandler : WebSocketHandler
        {
            private static WebSocketCollection _clients = new WebSocketCollection();
            private string _username;

            public MovieWebSocketHandler(string username)
            {
                _username = username;
            }

            public override void OnOpen()
            {
                _clients.Add(this);
            }


            public override void OnMessage(string message)
            {
                _clients.Broadcast(_username + ": " + message);
            }
        }
    }
}
