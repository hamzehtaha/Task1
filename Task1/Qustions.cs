using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
   abstract class Qustions
    {
        public static List<Qustions> lissSlid = new List<Qustions>(); 
        public string Qustion { get; set; }
        public int Order { get; set; }
        public int Id { get; set; }
        public string TypeOfQuestion { get; set;  }
        public void ShowQuestion() { }
        public static void GetData(DataGridView dataGridView1)
        {
            // Get Data Function to Get data from database 
            SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
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
                    dataGridView1.Rows[n].Cells[1].Value = item["Qustions_text"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["Type_Of_Qustion"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item["Qustion_order"].ToString();

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
    }
}
