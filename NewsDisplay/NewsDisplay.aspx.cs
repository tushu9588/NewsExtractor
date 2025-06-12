using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NewsDisplay
{
    public partial class NewsDisplay : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                // Fire and forget async call for breaking news
                _ = LoadBreakingNews();
            }
        }

        private async Task LoadBreakingNews()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Fetch JS-wrapped JSON string
                    string jsText = await httpClient.GetStringAsync("https://economictimes.indiatimes.com/etstatic/breakingnews/etjson_bnews.html");

                    // Extract JSON array from JS code
                    int startIndex = jsText.IndexOf("[");
                    int endIndex = jsText.LastIndexOf("]") + 1;

                    if (startIndex >= 0 && endIndex > startIndex)
                    {
                        string jsonArray = jsText.Substring(startIndex, endIndex - startIndex);

                        // Deserialize to C# objects
                        var serializer = new JavaScriptSerializer();
                        var breakingNews = serializer.Deserialize<List<BreakingNewsItem>>(jsonArray);

                        // Bind to Repeater
                        BreakingNewsRepeater.DataSource = breakingNews;
                        BreakingNewsRepeater.DataBind();

                        BreakingNewsCount.Text = $"🛑 Breaking News Count: {breakingNews.Count}";
                    }
                    else
                    {
                        BreakingNewsCount.Text = "⚠️ Invalid breaking news format.";
                    }
                }
            }
            catch (Exception ex)
            {
                BreakingNewsCount.Text = "❌ Failed to load breaking news.";
                // Optionally log: System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        // Model class representing each breaking news item
        public class BreakingNewsItem
        {
            public string title { get; set; }
            public string type { get; set; }
            public string link { get; set; }
        }

        // You can add more server-side methods like FetchNewsButton_Click here
    }
}
