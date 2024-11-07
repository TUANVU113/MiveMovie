using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace MineMovie_Nhom7_CNPM.Services
{
    public class MovieService
    {
        private readonly string apiKey = "696c897d"; //API key, cái này có thể lên omdb và gửi yêu cầu tạo API key khác, mỗi key được tối đa 1000 request thông tin
        private readonly string baseUrl = "http://www.omdbapi.com/";

        public async Task<MovieDetails> GetMovieDetailsAsync(string imdbid) //Hàm dùng để lấy thông tin phim từ mã của phim trên imdb
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"{baseUrl}?i={imdbid}&apikey={apiKey}");
                var movieDetails = JsonConvert.DeserializeObject<MovieDetails>(response);
                return movieDetails;
            }
        }
    }

    public class MovieDetails
    {
        //public string Title { get; set; }
        //public string Year { get; set; }
        //public string Rated { get; set; }
        //public string Released { get; set; }
        //public string Runtime { get; set; }
        //public string Genre { get; set; }
        //public string Director { get; set; }
        //public string Writer { get; set; }
        //public string Actors { get; set; }
        //public string Plot { get; set; }
        //public string Language { get; set; }
        //public string Country { get; set; }
        //public string Awards { get; set; }
        //public string Poster { get; set; }
        public Ratings[] Ratings { get; set; } //có nhiều thuộc tính nhưng mà hiện tại chỉ lấy rating thôi
    }

    public class Ratings
    {
        public string Source { get; set; }
        public string Value { get; set; }
    }
}
