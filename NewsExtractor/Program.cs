using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace EconomicTimesNews
{
    public class NewsItem
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            string sitemapUrl = "https://m.economictimes.com/sitemap/today";
            var newsList = await GetNewsItemsAsync(sitemapUrl);

            foreach (var news in newsList)
            {
                Console.WriteLine($"📌 {news.Title}");
                Console.WriteLine($"🔗 {news.Url}");
                Console.WriteLine($"🗓️ {news.PublicationDate}");
                Console.WriteLine(new string('-', 50));
            }

            SaveNewsToDatabase(newsList);
            Console.WriteLine("✅ News saved to database.");
        }

        public static async Task<List<NewsItem>> GetNewsItemsAsync(string sitemapUrl)
        {
            List<NewsItem> newsList = new List<NewsItem>();
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(sitemapUrl);
            XDocument xmlDoc = XDocument.Parse(response);

            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace newsNs = "http://www.google.com/schemas/sitemap-news/0.9";

            foreach (var item in xmlDoc.Descendants(ns + "url"))
            {
                string url = item.Element(ns + "loc")?.Value;
                string title = item.Element(newsNs + "news")?.Element(newsNs + "title")?.Value;
                string pubDateStr = item.Element(newsNs + "news")?.Element(newsNs + "publication_date")?.Value;

                if (DateTime.TryParse(pubDateStr, out DateTime pubDate))
                {
                    if (pubDate.Date == DateTime.Today || pubDate.Date == DateTime.Today.AddDays(-1))
                    {
                        newsList.Add(new NewsItem
                        {
                            Url = url,
                            Title = title,
                            PublicationDate = pubDate
                        });
                    }
                }
            }

            return newsList;
        }

        static void SaveNewsToDatabase(List<NewsItem> newsList)
        {
            string connStr = "server=n1nlmysql45plsk.secureserver.net;uid=tushar_TempDB;pwd=tushar_TempDB;database=tushar_TempDB;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                foreach (var news in newsList)
                {
                    string query = "INSERT INTO News (Url, Title, PublicationDate) VALUES (@Url, @Title, @PublicationDate)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Url", news.Url);
                        cmd.Parameters.AddWithValue("@Title", news.Title);
                        cmd.Parameters.AddWithValue("@PublicationDate", news.PublicationDate);
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }
    }
}
