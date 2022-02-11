using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace DataGridView
{
    public class DataGridHelper
    {
        private static readonly SqlConnection Con = new SqlConnection(Properties.Settings.Default.SqlConnetionString);
        public DataSet GetData(string query)
        {
            Con.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, Con);
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            Con.Close();
            return dataSet;
        }
        public void Delete(string query)
        {
            Con.Open();
            SqlCommand command = new SqlCommand(query, Con);
            command.ExecuteNonQuery();
            Con.Close();
        }
        public void InsertOrEdit(string query)
        {
            Con.Open();
            SqlCommand command = new SqlCommand(query, Con);
            command.ExecuteNonQuery();
            Con.Close();
        }

        
    }
}
