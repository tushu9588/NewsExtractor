using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace NewsDisplay
{
    public partial class NewsDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                EndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                LoadBreakingNews();
                LoadRegularNews();
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            LoadBreakingNews();
        }

        private void LoadBreakingNews()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT Title, Link, InsertedAt FROM BreakingNews WHERE 1=1";
                string keyword = SearchText.Text.Trim();
                DateTime startDate, endDate;

                if (DateTime.TryParse(StartDate.Text, out startDate))
                {
                    query += " AND DATE(InsertedAt) >= @startDate";
                }
                if (DateTime.TryParse(EndDate.Text, out endDate))
                {
                    query += " AND DATE(InsertedAt) <= @endDate";
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    query += " AND Title LIKE @keyword";
                }

                query += " ORDER BY InsertedAt DESC";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    if (startDate != DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@startDate", startDate.Date);
                    if (endDate != DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@endDate", endDate.Date);
                    if (!string.IsNullOrEmpty(keyword))
                        cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        BreakingNewsGrid.DataSource = dt;
                        BreakingNewsGrid.DataBind();

                        BreakingNewsRepeater.DataSource = dt;
                        BreakingNewsRepeater.DataBind();

                        BreakingNewsCountLabel.Text = $"Total Breaking News: {dt.Rows.Count}";
                    }
                }
            }
        }

        private void LoadRegularNews()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Title, Url, PublicationDate FROM News ORDER BY PublicationDate DESC";

                using (var cmd = new MySqlCommand(query, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    NewsGrid.DataSource = dt;
                    NewsGrid.DataBind();
                    NewsRepeater.DataSource = dt;
                    NewsRepeater.DataBind();
                }
            }
        }
    }
}
