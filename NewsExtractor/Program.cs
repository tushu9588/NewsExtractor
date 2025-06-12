using NewsExtractor;
using System;
using System.Collections.Generic;
using System.Configuration;
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

                if (allNews.Count == 0)
                {
                    Console.WriteLine("⚠️ No news to save.");
                    return;
                }

                // Read connection string from App.config
                string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

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
