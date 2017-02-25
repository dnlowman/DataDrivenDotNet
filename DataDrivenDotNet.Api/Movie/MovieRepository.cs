using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace DataDrivenDotNet.Api.Movie
{
    public sealed class MovieRepository : IMovieRepository
    {
        private readonly OracleConnection _connection;
        private const string CONNECTION_STRING = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = xe)));User Id = awesome; Password=123;";

        public MovieRepository()
        {
            _connection = new OracleConnection(CONNECTION_STRING);
        }

        public IEnumerable<Movie> Get()
        {
            return _connection.Query<Movie>("SELECT * FROM AWESOME.MOVIE");
        }

        public Movie Get(int id)
        {
            return _connection.QueryFirstOrDefault<Movie>($"SELECT * FROM AWESOME.MOVIE WHERE ID = {id}");
        }

        public void Create(Movie movie)
        {
            _connection.Query($"INSERT INTO AWESOME.MOVIE (NAME) VALUES ('{movie.Name}')");
        }

        public void Update(Movie movie)
        {
            _connection.Query($"UPDATE AWESOME.MOVIE SET NAME = '{movie.Name}' WHERE ID = {movie.Id}");
        }

        public void Delete(int id)
        {
            _connection.Query($"DELETE FROM AWESOME.MOVIE WHERE ID = {id}");
        }

        public void Subscribe(Action onUpdate)
        {
            var command = new OracleCommand("SELECT ID, NAME FROM AWESOME.MOVIE", _connection);
            _connection.Open();
            command.AddRowid = true;

            var dependency = new OracleDependency(command);

            command.Notification.IsNotifiedOnce = false;

            

            dependency.OnChange += new OnChangeEventHandler((object o, OracleNotificationEventArgs e) =>
            {
                onUpdate();
            });

            command.ExecuteNonQuery();

            _connection.Close();
            command.Dispose();
        }
    }
}
