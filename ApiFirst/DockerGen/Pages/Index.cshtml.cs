using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DockerGen.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async void OnGet()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // Call *mywebapi*, and display its response in the page
                var request = new System.Net.Http.HttpRequestMessage();
                // webapi is the container name
                request.RequestUri = new Uri("http://localhost:56973/Counter");
                var response = await client.SendAsync(request);
                string counter = await response.Content.ReadAsStringAsync();
                ViewData["Message"] = $"Counter value from cache :{counter}";
            }
        }
    }
}