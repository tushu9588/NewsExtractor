using NewsExtractor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EconomicTimesNews
{
    /// <summary>
    /// Responsible for fetching and parsing news items from an Economic Times sitemap.
    /// </summary>
    public class NewsFetcher
    {
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Asynchronously retrieves news articles from the specified sitemap URL.
        /// </summary>
        /// <param name="sitemapUrl">The URL of the XML sitemap to fetch news from.</param>
        /// <returns>A list of NewsItem objects representing the news articles.</returns>
        public static async Task<List<NewsItem>> GetNewsItemsAsync(string sitemapUrl)
        {
            var newsList = new List<NewsItem>();

            try
            {
                string response = await client.GetStringAsync(sitemapUrl);
                var xmlDoc = XDocument.Parse(response);

                XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                XNamespace newsNs = "http://www.google.com/schemas/sitemap-news/0.9";

                foreach (var item in xmlDoc.Descendants(ns + "url"))
                {
                    string url = item.Element(ns + "loc")?.Value;
                    string title = item.Element(newsNs + "news")?.Element(newsNs + "title")?.Value;
                    string pubDateStr = item.Element(newsNs + "news")?.Element(newsNs + "publication_date")?.Value;

                    if (DateTime.TryParse(pubDateStr, out DateTime pubDate))
                    {
                        // Filter to include only today's and yesterday's news
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error fetching or parsing news data:");
                Console.WriteLine(ex.Message);
            }

            return newsList;
        }
    }
}
