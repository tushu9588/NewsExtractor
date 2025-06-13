using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace NewsDisplay
{
    public class NewsApiController : ApiController
    {
        // GET: api/news/searchbreakingnews?startDate=yyyy-MM-dd&endDate=yyyy-MM-dd&title=keyword
        [HttpGet]
        [Route("api/news/searchbreakingnews")]
        public IHttpActionResult SearchBreakingNews(string startDate, string endDate, string title = "")
        {
            List<BreakingNewsItem> filteredNews = new List<BreakingNewsItem>();

            string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = @"SELECT Title, Link, InsertedAt FROM breakingnews 
                                 WHERE DATE(InsertedAt) BETWEEN @startDate AND @endDate 
                                 AND Title LIKE @title
                                 ORDER BY InsertedAt DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@title", "%" + title + "%"); // Correct usage

                    try
                    {
                        conn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                filteredNews.Add(new BreakingNewsItem
                                {
                                    Title = reader["Title"].ToString(),
                                    Link = reader["Link"].ToString(),
                                    InsertedAt = Convert.ToDateTime(reader["InsertedAt"])
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return InternalServerError(ex);
                    }
                }
            }

            if (filteredNews.Count == 0)
            {
                return NotFound(); // 404 if no results
            }

            return Ok(filteredNews); // 200 OK with JSON result
        }
    }

    // Model class for breaking news
    public class BreakingNewsItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public DateTime InsertedAt { get; set; }
    }
}
