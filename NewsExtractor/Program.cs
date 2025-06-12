using NewsExtractor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EconomicTimesNews
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("🔄 Fetching today’s news...");
                var todayNews = await NewsFetcher.GetNewsItemsAsync("https://m.economictimes.com/sitemap/today");

                Console.WriteLine("🔄 Fetching yesterday’s news...");
                var yesterdayNews = await NewsFetcher.GetNewsItemsAsync("https://m.economictimes.com/sitemap/yesterday");

                // Combine both today's and yesterday's news
                var allNews = new List<NewsItem>();
                allNews.AddRange(todayNews);
                allNews.AddRange(yesterdayNews);

                Console.WriteLine($"\n📋 Total News Fetched: {allNews.Count}\n");

                // Display each news item in the console
                foreach (var news in allNews)
                {
                    Console.WriteLine($"📰 Title        : {news.Title}");
                    Console.WriteLine($"🔗 URL          : {news.Url}");
                    Console.WriteLine($"🗓️ Published On : {news.PublicationDate:dd MMM yyyy HH:mm}");
                    Console.WriteLine(new string('-', 70));
                }

                if (allNews.Count == 0)
                {
                    Console.WriteLine("⚠️ No news to save.");
                    return;
                }

                // Define the MySQL connection string
                string connStr = "server=n1nlmysql45plsk.secureserver.net;uid=tushar_TempDB;pwd=tushar_TempDB;database=tushar_TempDB;";

                // Save news to the database
                var dbSaver = new NewsDatabaseSaver(connStr);
                dbSaver.SaveNewsToDatabase(allNews);

                Console.WriteLine("\n✅ News saved to MySQL database successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n❌ An unexpected error occurred:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
