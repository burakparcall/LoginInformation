using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginInformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection url = new SqlConnection("Data Source=Burak\\SQLEXPRESS;Initial Catalog=LoginInformation;Integrated Security=True");

        public void refresh()
        {
            this.loginInformationTableAdapter.Fill(this.loginInformationDataSet.LoginInformation);
        }

        public void clear()
        {
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtName.Text = "";
            txtID.Text = "0";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            url.Open();
            SqlCommand save = new SqlCommand("Insert Into LoginInformation (Email,Username,Password,Name) values (@email, @username, @password,@name)", url);

            save.Parameters.AddWithValue("@email", txtEmail.Text);
            save.Parameters.AddWithValue("@username", txtUsername.Text);
            save.Parameters.AddWithValue("@password", txtPassword.Text);
            save.Parameters.AddWithValue("@name", txtName.Text);
            save.ExecuteNonQuery();
            refresh();
            clear();
            url.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            url.Open();
            SqlCommand deletepersonel = new SqlCommand("Delete from LoginInformation  Where ID=@id", url);
            deletepersonel.Parameters.AddWithValue("id", txtID.Text);
            deletepersonel.ExecuteNonQuery();
            refresh();
            clear();
            url.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            url.Open();
            SqlCommand edit = new SqlCommand("Update LoginInformation Set Email=@email, Username=@username, Password=@password, Name=@name Where ID=@id", url);
            edit.Parameters.AddWithValue("@email", txtEmail.Text);
            edit.Parameters.AddWithValue("@username", txtUsername.Text);
            edit.Parameters.AddWithValue("@password", txtPassword.Text);
            edit.Parameters.AddWithValue("@name", txtName.Text);
            edit.Parameters.AddWithValue("@id", txtID.Text);
            edit.ExecuteNonQuery();
            refresh();
            url.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;

            txtID.Text = dataGridView1.Rows[selected].Cells[0].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[selected].Cells[1].Value.ToString();
            txtUsername.Text = dataGridView1.Rows[selected].Cells[2].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[selected].Cells[3].Value.ToString();
            txtName.Text = dataGridView1.Rows[selected].Cells[4].Value.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
