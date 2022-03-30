using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConsoleTables;

namespace ConsoleAppAssignment
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
            Console.WriteLine("sjshheihief".Substring(0,4));
        }
        private static async Task ProcessRepositories()
        {
            var streamTask = client.GetStreamAsync("https://jsonmock.hackerrank.com/api/article_users/search");
            var repositories = await JsonSerializer.DeserializeAsync<Repository>(await streamTask);

            var dat = repositories.Data;


            var table1 = new ConsoleTable("page", "per_page", "total", "total_pages");
            table1.AddRow(repositories.Page, repositories.PerPage, repositories.Total, repositories.Total_pages);
            table1.Write();

            var table2 = new ConsoleTable("id", "username", "about", "submitted", "created_at");
            foreach (var data in dat)
                table2.AddRow(data.Id, data.Username, data.About.Length > 20 ? $"{data.About.Substring(1, 10)}..." : $"{data.About}..." , data.Submitted,data.CreatedAt);
            table2.Write();
        }
    }
    public class Repository
    {
        [JsonPropertyName("page")]
        public int  Page { get; set; }
        [JsonPropertyName("data")]
        public List<data> Data{ get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("total")]
        public  int Total { get; set; }

        [JsonPropertyName("total_pages")]
        public int Total_pages { get; set; }
        
    }
    public class data
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("about")]
        public string About { get; set; }

        [JsonPropertyName("submitted")]
        public int Submitted { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }
    }
}
