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

namespace DataGridView
{
    public partial class StudentForm : Form
    {
        private static readonly SqlConnection Con = new SqlConnection(Properties.Settings.Default.SqlConnetionString);

        readonly DataGridHelper dataGridHelper = new DataGridHelper();
        public StudentForm()
        {
            InitializeComponent();            
        }
        public void Populate()
        {
            string query = "Select * from Student";
            DataGridViewStudent.DataSource = dataGridHelper.GetData(query).Tables[0];
        }
        private void StudentForm_Load(object sender, EventArgs e)
        {
            Populate();
        }
        private void Button_Add_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "insert into Student values(" + textBox_ID.Text + ",'" + textBox_FirstName.Text + "','" + textBoxLastName.Text + "','" + textBoxPhone.Text + "','" + textBox_Email.Text + "','"+textBox_Gender.Text+"')";
                dataGridHelper.InsertOrEdit(query);
                MessageBox.Show("ADD STUDENT SUCCESSFULLY");
                Populate();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }       
        private void Button_Edit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox_ID.Text) || string.IsNullOrEmpty(textBox_FirstName.Text) || string.IsNullOrEmpty(textBoxLastName.Text) || string.IsNullOrEmpty(textBoxPhone.Text) || string.IsNullOrEmpty(textBox_Email.Text))
            {
                MessageBox.Show("Missing information");
            }
            {
                try
                {
                    string query = "update Student set Id=" + textBox_ID.Text + ",FirstName='" + textBox_FirstName.Text + "',LastName='" + textBoxLastName.Text + "',Phone='" + textBoxPhone.Text + "',Email='" + textBox_Email.Text + "',Gender='"+textBox_Gender+"' where Id="+textBox_ID.Text+";";
                    dataGridHelper.InsertOrEdit(query);
                    MessageBox.Show("EDIT STUDENT SUCCESSFULLY");
                    Populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void Button_Delete_Click(object sender, EventArgs e)
        {
            string query = "delete from Student where Id=" + textBox_ID.Text + "";
            dataGridHelper.Delete(query);
            Populate();
        }
        private void DataGridViewStudent_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridViewStudent.SelectedRows.Count > 0)
            {
                var selectedRow = DataGridViewStudent.SelectedRows[0];
                textBox_ID.Text = selectedRow.Cells[0].Value.ToString();
                textBox_FirstName.Text = selectedRow.Cells[1].Value.ToString();
                textBoxLastName.Text = selectedRow.Cells[2].Value.ToString();
                textBoxPhone.Text = selectedRow.Cells[3].Value.ToString();
                textBox_Email.Text = selectedRow.Cells[4].Value.ToString();
                textBox_Gender.Text = selectedRow.Cells[5].Value.ToString();

            }
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = comboBox1.SelectedItem;
            if (selectedValue != null)
            {
                Con.Open();
                string query = "select * from Student where Gender='" + selectedValue.ToString() + "'";
                DataGridViewStudent.DataSource = dataGridHelper.GetData(query).Tables[0];
                Con.Close();
            }
        }        
        public void SearchData(string search)
        {         
            string query = "select * from Student where FirstName like '%" + search + "%'";
            DataGridViewStudent.DataSource = dataGridHelper.GetData(query).Tables[0];          
        }
        private void TextBox_SearchName_TextChanged(object sender, EventArgs e)
        {
            SearchData(textBox_SearchName.Text);
        }
    }
}
