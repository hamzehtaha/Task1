using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Survey;

namespace Task1
{
   public abstract class Qustions
    {
        
        public string NewText { get; set;}
        public int Order { get; set; }
        public int Id { get; set; }
        public string TypeOfQuestion { get; set;}
        public void ShowQuestion() { }
        public abstract void AddQuestion(string [] Att);
        public abstract void EditQuestion(string[] Att);
        public static void GetData(DataGridView dataGridView1)
        {
            // Get Data Function to Get data from database 
            SqlConnection con = Attributes.GetConnection();
            //SqlCommand cmd = new SqlCommand("sp_Qustions_Select1", con);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Qustions", con);
            //cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                con.Open();
                // SqlDataReader rd = cmd.ExecuteReader(); 
                DataTable dt = new DataTable();
                //dt.Load(rd);
                sda.Fill(dt);
                dataGridView1.Rows.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = false;
                    dataGridView1.Rows[n].Cells[1].Value = item[Attributes.Variables.Qustions_text.ToString()].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item[Attributes.Variables.Type_Of_Qustion.ToString()].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item[Attributes.Variables.Qustion_order.ToString()].ToString();

                }
                // View the data in datagridview 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
        public abstract void Delete(int id, int idFroType); 
    }
}
