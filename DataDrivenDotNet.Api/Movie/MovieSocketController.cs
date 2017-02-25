using Microsoft.Web.WebSockets;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DataDrivenDotNet.Api.Movie
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public sealed class MovieSocketController : ApiController
    {
        private readonly IMovieRepository _movieRepository;

        public MovieSocketController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public HttpResponseMessage Get(string username)
        {
            HttpContext.Current.AcceptWebSocketRequest(new MovieWebSocketHandler(_movieRepository, username));
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }

        class MovieWebSocketHandler : WebSocketHandler
        {
            private readonly IMovieRepository _movieRepository;
            private static WebSocketCollection _clients = new WebSocketCollection();
            private string _username;

            public MovieWebSocketHandler(IMovieRepository movieRepository, string username)
            {
                _movieRepository = movieRepository;
                _username = username;

                _movieRepository.Subscribe(() =>
                {
                    _clients.Broadcast("UPDATE_MOVIES");
                });
            }

            public override void OnOpen()
            {
                _clients.Add(this);
            }


            public override void OnMessage(string message)
            {
            }
        }
    }
}
