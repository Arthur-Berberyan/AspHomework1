using AspHomework1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using AspHomework1.Models;

namespace AspHomework1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly HttpClient _jsonPlaceholderClient;
        private readonly HttpClient _reqResClient;

        public ApiController(IHttpClientFactory httpClientFactory)
        {
            _jsonPlaceholderClient = httpClientFactory.CreateClient();
            _jsonPlaceholderClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

            _reqResClient = httpClientFactory.CreateClient();
            _reqResClient.BaseAddress = new Uri("https://reqres.in/");
        }

        // GET /posts?userId=1&title=qui%20est%20esse
        [HttpGet("posts")]
        public IActionResult GetPosts(int? userId, string title)
        {
            var query = $"/posts?userId={userId}&title={title}";
            var response = _jsonPlaceholderClient.GetAsync(query).Result;
            return Content(response.Content.ReadAsStringAsync().Result, "application/json");
        }

        // GET /posts/3
        [HttpGet("posts/{id}")]
        public IActionResult GetPost(int id)
        {
            var response = _jsonPlaceholderClient.GetAsync($"/posts/{id}").Result;
            return Content(response.Content.ReadAsStringAsync().Result, "application/json");
        }

        // POST /users
        [HttpPost("users")]
        public IActionResult CreateUser([FromBody] UserRequest user)
        {
            var content = new StringContent(
                $"{{ \"name\": \"{user.Name}\", \"job\": \"{user.Job}\" }}",
                Encoding.UTF8,
                "application/json"
            );

            var response = _reqResClient.PostAsync("/api/users", content).Result;
            return Content(response.Content.ReadAsStringAsync().Result, "application/json");
        }

        // PUT /users/5
        [HttpPut("users/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserRequest user)
        {
            var content = new StringContent(
                $"{{ \"name\": \"{user.Name}\", \"job\": \"{user.Job}\" }}",
                Encoding.UTF8,
                "application/json"
            );

            var response = _reqResClient.PutAsync($"/api/users/{id}", content).Result;
            return Content(response.Content.ReadAsStringAsync().Result, "application/json");
        }

        // DELETE /posts/5
        [HttpDelete("posts/{id}")]
        public IActionResult DeletePost(int id)
        {
            var response = _jsonPlaceholderClient.DeleteAsync($"/posts/{id}").Result;
            return StatusCode((int)response.StatusCode);
        }

    }
}