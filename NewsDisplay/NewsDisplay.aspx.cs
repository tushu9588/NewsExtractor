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
                LoadBreakingNews();
                LoadRegularNews();
            }
        }

        private void LoadBreakingNews()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Title, Link, InsertedAt FROM BreakingNews ORDER BY InsertedAt DESC";

                using (var cmd = new MySqlCommand(query, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    BreakingNewsGrid.DataSource = dt;
                    BreakingNewsGrid.DataBind();
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
                }
            }
        }
    }
}
