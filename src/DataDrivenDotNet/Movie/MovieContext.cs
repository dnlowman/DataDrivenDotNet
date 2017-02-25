using System.Data;

namespace DataDrivenDotNet.Movie
{
    public class MovieContext
    {
        private readonly IDbConnection _connection;

        public MovieContext()
        {
            _connection = new OracleConnection();
        }
    }
}
