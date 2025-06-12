using System;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace NewsDisplay
{
    public partial class NewsDisplay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set default to today's date
                string today = DateTime.Today.ToString("yyyy-MM-dd");
                StartDateTextBox.Text = today;
                EndDateTextBox.Text = today;

                LoadNewsFromDatabase(DateTime.Today, DateTime.Today, string.Empty);
            }
        }

        protected void FetchNewsButton_Click(object sender, EventArgs e)
        {
            if (DateTime.TryParse(StartDateTextBox.Text, out DateTime startDate) &&
                DateTime.TryParse(EndDateTextBox.Text, out DateTime endDate))
            {
                string keyword = SearchTextBox.Text.Trim();
                // Include full end date range
                endDate = endDate.AddDays(1).AddSeconds(-1);

                LoadNewsFromDatabase(startDate, endDate, keyword);
            }
        }

        private void LoadNewsFromDatabase(DateTime startDate, DateTime endDate, string keyword)
        {
            string connStr = "server=n1nlmysql45plsk.secureserver.net;uid=tushar_TempDB;pwd=tushar_TempDB;database=tushar_TempDB;";
            string query = @"SELECT DISTINCT Title, Url, PublicationDate
                             FROM News 
                             WHERE PublicationDate BETWEEN @startDate AND @endDate
                             AND (@keyword = '' OR Title LIKE CONCAT('%', @keyword, '%') OR Url LIKE CONCAT('%', @keyword, '%'))
                             ORDER BY PublicationDate DESC";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@keyword", keyword);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        NewsGridView.DataSource = dt;
                        NewsGridView.DataBind();
                    }
                }
            }
        }
    }
}
