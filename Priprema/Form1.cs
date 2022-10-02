using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Priprema
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool isLoggedInMain { get; set; }

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=Atlas1997#;Database=JankoDB;");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TxtPassword(object sender, EventArgs e)
        {
            passwordtxt.PasswordChar = '*';
        }

        public static DataTable SelectData(string query)
        {
            NpgsqlConnection connection = GetConnection();
            connection.Open();
            using (var cmd = new NpgsqlCommand(query, connection))
            {
                cmd.Prepare();

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);

                DataSet _ds = new DataSet();
                DataTable _dt = new DataTable();

                da.Fill(_ds);

                try
                {
                    _dt = _ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: ---> " + ex.Message);
                }

                connection.Close();
                return _dt;
            }

        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            if(usernametxt.Text.Length == 0)
            {
                MessageBox.Show("Username is a required field!");
                return;
            }
            if(passwordtxt.Text.Length == 0)
            {
                MessageBox.Show("Password is a required field");
                return;
            }
            if (SelectData("select * from \"Login\" where " + "\"Username\" = " + "'" + usernametxt.Text + "'" + " and " + "\"Password\" = " + "'" + passwordtxt.Text + "'").Rows.Count == 1)
            {
                isLoggedInMain = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("The username or password was not entered correctly!");
                return;
            }
        }

        private void cancleBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
