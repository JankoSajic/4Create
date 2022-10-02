using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Priprema
{
    public partial class Main : Form
    {

        enum Image
        {
            Empty = 0,
            JPG = 1,
            DICOM = 2
        }

        public delegate NpgsqlConnection Connection();
        Connection conn = Form1.GetConnection;
        public Main()
        {
            InitializeComponent();
            FillComboBox();
        }

        TableView tableView;

        public Main(string name, bool accepting, string image, TableView tv)
        {
            InitializeComponent();
            tableView = tv;
            FillForm(name, accepting, image);
        }

        public bool isLoggedInTableView { get; set; }

        private void Main_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            checkBox1.Checked = true;
            textBox1.Enabled = true;
            StartPosition = FormStartPosition.CenterScreen;
            comboBox1.Enabled = checkBox2.Checked;
        }

        private void FillComboBox()
        {
            foreach (var item in Enum.GetValues(typeof(Image)))
            {
                comboBox1.Items.Add(!(item.Equals(Image.Empty)) ? item : "");
            }
        }

        private void FillForm(string name, bool accepting, string image)
        {
            FillComboBox();
            button1.Visible = false;
            button2.Visible = true;
            textBox1.Text = name;
            checkBox2.Checked = accepting;
            foreach (var item in Enum.GetValues(typeof(Image)))
            {
                if (item.ToString() == image)
                {
                    comboBox1.SelectedItem = item;
                    return;
                }
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            NpgsqlConnection con = conn();
            con.Open();

            NpgsqlCommand npgsqlCommand = con.CreateCommand();

            try
            {
                string sql = "INSERT INTO \"Projects\" VALUES ('" + textBox1.Text.ToString() + "', '" + checkBox2.Checked + "', '" + comboBox1.SelectedItem.ToString() + "')";

                npgsqlCommand.CommandText = sql;
                //npgsqlCommand.Parameters.Add("@textBox3.Text", textBox1.Text.Trim());
                npgsqlCommand.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("there is a error");
                if (ex.Data == null)
                {
                    throw;
                }
            }
            finally
            {
                con.Close();
            }

            isLoggedInTableView = true;

            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = checkBox2.Checked;
            if(checkBox2.Checked)
                comboBox1.SelectedItem = Image.JPG;
            button1.Enabled = checkButton1Enabled();
        }

        private bool checkButton1Enabled()
        {
            if(textBox1.Text.Length > 0 && checkBox2.Checked) { return true; }
            else { return false; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = checkButton1Enabled();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tableView.Visible = true;
            this.Close();
        }
    }
}
