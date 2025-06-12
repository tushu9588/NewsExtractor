using System;
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
                // Optional: Set default dates (e.g., today and yesterday)
                StartDateTextBox.Text = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
                EndDateTextBox.Text = DateTime.Today.ToString("yyyy-MM-dd");

                LoadNewsFromDatabase(DateTime.Today.AddDays(-1), DateTime.Today);
            }
        }

        protected void FetchNewsButton_Click(object sender, EventArgs e)
        {
            // Parse dates from user input
            if (DateTime.TryParse(StartDateTextBox.Text, out DateTime startDate) &&
                DateTime.TryParse(EndDateTextBox.Text, out DateTime endDate))
            {
                // Ensure endDate includes the full day
                endDate = endDate.AddDays(1).AddSeconds(-1);

                LoadNewsFromDatabase(startDate, endDate);
            }
            else
            {
                // Handle invalid dates
                NewsGridView.DataSource = null;
                NewsGridView.DataBind();
                // Optional: Show an error message (e.g., via Label)
            }
        }

        private void LoadNewsFromDatabase(DateTime startDate, DateTime endDate)
        {
            string connStr = "server=n1nlmysql45plsk.secureserver.net;uid=tushar_TempDB;pwd=tushar_TempDB;database=tushar_TempDB;";
            string query = "SELECT Title, Url, PublicationDate FROM News WHERE PublicationDate BETWEEN @startDate AND @endDate ORDER BY PublicationDate DESC";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

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
