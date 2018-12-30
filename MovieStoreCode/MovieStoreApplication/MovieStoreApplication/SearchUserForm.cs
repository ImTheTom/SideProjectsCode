using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQL_Lib;

namespace MovieStoreApplication {
    public partial class SearchUserForm : Form {
        public SQLConnection connection;

        /// <summary>
        /// Collects the SQL connection from the previous form to allow for the SQL_Lib library to be used.
        /// </summary>
        public SearchUserForm(SQLConnection lastConnection) {
            InitializeComponent();
            connection = lastConnection;
        }

        /// <summary>
        /// Searches through the customer table and updates the data grid dependent on the results
        /// </summary>
        private void SearchButton_Click(object sender, EventArgs e) {
            ResultsGrid.Rows.Clear();
            string name = "";
            if (NameText.Text.ToString() != null) {
                name = NameText.Text.ToString();
            }
            List<string>[] results = connection.SearchCustomers(name);
            for (int i = 0; i < results[0].Count; i++) {
                ResultsGrid.Rows.Add(results[0][i], results[1][i], results[2][i], results[3][i], results[4][i]);
            }
        }
    }
}
