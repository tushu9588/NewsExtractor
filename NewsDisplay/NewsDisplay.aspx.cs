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
                LoadNewsFromDatabase();
            }
        }

        private void LoadNewsFromDatabase()
        {
            string connStr = "server=n1nlmysql45plsk.secureserver.net;uid=tushar_TempDB;pwd=tushar_TempDB;database=tushar_TempDB;";
            string query = "SELECT Title, Url, PublicationDate FROM News ORDER BY PublicationDate DESC";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
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
