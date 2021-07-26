using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class MoviesDAL
    {
        public HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44384");
            return client;

        }

        public async Task<List<Movie>> GetMovies()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("/api/movie");
            var movies = await response.Content.ReadAsAsync<List<Movie>>();
            return movies;

        }

        public async Task<Movie> GetMovie(int id)
        {
            var client = GetHttpClient();
            var response = await client.GetAsync($"/api/movie/{id}");
            var movie = await response.Content.ReadAsAsync<Movie>();
            return movie;

        }

        public async Task AddMovie(Movie movie)
        {
            var client = GetHttpClient();
            var response = await client.PostAsJsonAsync("/api/movie", movie);

        }

        public async Task DeleteMovie(int id)
        {
            var client = GetHttpClient();
            var response = await client.DeleteAsync($"/api/movie/{id}");
        }

        public async Task EditMovie(Movie newMovie, int id)
        {
            var client = GetHttpClient();
            var response = await client.PutAsJsonAsync($"/api/movie/{id}", newMovie);
        }

    }
}
