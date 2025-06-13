using EconomicTimesNews;
using NewsExtractor;
using System.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            // 🔴 1. Fetch Breaking News
            Console.WriteLine("🔴 Fetching Breaking News...");
            var breakingNews = await BreakingNewsFetcher.GetBreakingNewsAsync();

            foreach (var news in breakingNews)
            {
                Console.WriteLine($"📰 {news.title}");
                Console.WriteLine($"🔗 {news.link}\n");
            }

            if (breakingNews.Count > 0)
            {
                string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
                var breakingNewsSaver = new BreakingNewsDatabaseSaver(connStr);
                breakingNewsSaver.SaveBreakingNewsToDatabase(breakingNews);
                Console.WriteLine("✅ Breaking news saved to database.");
            }

            // 📰 2. Fetch Sitemap News
            Console.WriteLine("🔄 Fetching today’s news...");
            var todayNews = await NewsFetcher.GetNewsItemsAsync("https://m.economictimes.com/sitemap/today");

            Console.WriteLine("🔄 Fetching yesterday’s news...");
            var yesterdayNews = await NewsFetcher.GetNewsItemsAsync("https://m.economictimes.com/sitemap/yesterday");

            var allNews = new List<NewsItem>();
            allNews.AddRange(todayNews);
            allNews.AddRange(yesterdayNews);

            Console.WriteLine($"\n📋 Total News Fetched: {allNews.Count}\n");

            if (allNews.Count > 0)
            {
                string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
                var dbSaver = new NewsDatabaseSaver(connStr);
                dbSaver.SaveNewsToDatabase(allNews);

                Console.WriteLine("\n✅ News saved to MySQL database successfully.");
            }
            else
            {
                Console.WriteLine("⚠️ No sitemap news to save.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("\n❌ An unexpected error occurred:");
            Console.WriteLine(ex.Message);
        }
    }
}
