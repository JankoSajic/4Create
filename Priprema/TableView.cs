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
    public partial class TableView : Form
    {

        public delegate DataTable SelectDataTable(string sql);
        SelectDataTable sdt = Form1.SelectData;
        public TableView()
        {
            InitializeComponent();
        }

        private void TableView_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sdt("select * from \"ProjectsView\"");
            SetDataGridView();
            dataGridView1.Columns[0].HeaderText = "Project name";
            dataGridView1.Columns[1].HeaderText = "Accepting new visits";
            dataGridView1.Columns[2].HeaderText = "Supported image type";
        }

        private void SetDataGridView()
        {
            dataGridView1.Height = (int)(this.ClientSize.Height * 0.8);
            dataGridView1.Width = (int)(this.ClientSize.Width * 0.8);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Main main = new Main(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() == "YES" ? true : false, dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), this);
            main.Show();
            this.Visible = false;
        }

        private void TableView_Resize(object sender, EventArgs e)
        {
            SetDataGridView();
        }
    }
}
