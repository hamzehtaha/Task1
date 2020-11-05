using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Collections;
using System.Runtime.InteropServices;

namespace Task1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private int ID = -1;
        private string Type;
        
        public Form1()
        { 
            InitializeComponent();
            Slider.ShowQuestion();
            Smiles.ShowQuestion();
            Stars.ShowQuestion();
            Qustions.GetData(dataGridView1); 
        }

        private void getData() {
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

        private void fill(int number) {
            // for fill the array in true for stars 
            for (int i = 0; i <number; ++i) {
                Smiles.f[i] = true; 
            }
        }
        private bool CheckStar(int number)
        {
            // this function to check in stars 
            bool f = true;
            for (int i = 0; i < 10; ++i)
            {
                if (i != number && Smiles.f[i] == false)
                {
                    f = false;
                }
            }
            return f; 
        }
      
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // to go to the Add page 
            QuestionsInformation f = new QuestionsInformation(dataGridView1,-1,"",1); 
          //  FQuorm2 f = new Form2(dataGridView1);
            f.Show();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
       
        }
  
        private void metroButton1_Click(object sender, EventArgs e)
        {
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // this function for delete the answer 
            List<int> del = new List<int>();
            Slider obj1 = null;
            Stars obj3 = null;
            Smiles obj2 = null; 
            foreach (   DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)dataGridView1.Rows[row.Index].Cells[0].Value == true)
                {
                    string s = dataGridView1.Rows[row.Index].Cells[1].Value.ToString();
                    for (int i = 0; i < Qustions.lissSlid.Count; ++i)
                        if (s.Equals(Qustions.lissSlid.ElementAt(i).Qustion))
                            del.Add(Qustions.lissSlid.ElementAt(i).Id); 
                }
            }
            if (del.Count == 0)
            {
                MessageBox.Show("This is not select any Question !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else { 
            DialogResult dialogResult = MessageBox.Show("Are you sure to Delete this answer ?", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes && del.Count > 0)
                {
                    for (int j = 0; j < del.Count; ++j)
                    {
                        for (int i = 0; i < Qustions.lissSlid.Count; ++i)
                        {
                            if (Qustions.lissSlid.ElementAt(i).Id == del.ElementAt(j))
                            {
                                if (Qustions.lissSlid.ElementAt(i) is Slider)
                                {
                                    SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                                    try
                                    {
                                        con.Open();
                                        SqlCommand cmd3 = new SqlCommand("sp_Slider_Delete", con);
                                        cmd3.CommandType = CommandType.StoredProcedure;
                                        obj1 = (Slider)Qustions.lissSlid.ElementAt(i);
                                        cmd3.Parameters.AddWithValue("@ID", obj1.IdForType);
                                        cmd3.ExecuteNonQuery();
                                        cmd3.Parameters.Clear();
                                        cmd3.CommandText = "sp_Qustion_Delete";
                                        cmd3.CommandType = CommandType.StoredProcedure;
                                        cmd3.Parameters.AddWithValue("@ID", obj1.Id);
                                        cmd3.ExecuteNonQuery();
                                        cmd3.Parameters.Clear();
                                        Qustions.lissSlid.Remove(obj1);
                                        //MessageBox.Show("This Answer is Deleted");
                                        break;
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
                                else if (Qustions.lissSlid.ElementAt(i) is Smiles)
                                {
                                    SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                                    try
                                    {
                                        con.Open();
                                        SqlCommand cmd3 = new SqlCommand("sp_Smily_Delete", con);
                                        cmd3.CommandType = CommandType.StoredProcedure;
                                        obj2 = (Smiles)Qustions.lissSlid.ElementAt(i);
                                        cmd3.Parameters.AddWithValue("@ID", obj2.idForType);
                                        cmd3.ExecuteNonQuery();
                                        cmd3.Parameters.Clear();
                                        cmd3.CommandText = "sp_Qustion_Delete";
                                        cmd3.CommandType = CommandType.StoredProcedure;
                                        cmd3.Parameters.AddWithValue("@ID", obj2.Id);
                                        cmd3.ExecuteNonQuery();
                                        cmd3.Parameters.Clear();
                                        Qustions.lissSlid.Remove(obj2);
                                       
                                        break;
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
                                else if (Qustions.lissSlid.ElementAt(i) is Stars)
                                {
                                    SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                                    try
                                    {
                                        con.Open();
                                        SqlCommand cmd3 = new SqlCommand("sp_Stars_Delete", con);
                                        cmd3.CommandType = CommandType.StoredProcedure;
                                        obj3 = (Stars)Qustions.lissSlid.ElementAt(i);
                                        cmd3.Parameters.AddWithValue("@ID", obj3.idForType);
                                        cmd3.ExecuteNonQuery();
                                        cmd3.Parameters.Clear();
                                        cmd3.CommandText = "sp_Qustion_Delete";
                                        cmd3.CommandType = CommandType.StoredProcedure;
                                        cmd3.Parameters.AddWithValue("@ID", obj3.Id);
                                        cmd3.ExecuteNonQuery();
                                        cmd3.Parameters.Clear();
                                        Qustions.lissSlid.RemoveAt(i);
                                        break;
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

                    }
                    Qustions.GetData(dataGridView1); 
                    MessageBox.Show("Your Questions is deleted !");
                }
            }
            
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // this function for go to the edit page and give me the id 
            List<int> del = new List<int>();
            List<string> types = new List<string>(); 
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)dataGridView1.Rows[row.Index].Cells[0].Value == true)
                {
                    string s = dataGridView1.Rows[row.Index].Cells[1].Value.ToString();
                    for (int i = 0; i < Qustions.lissSlid.Count; ++i)
                        if (s.Equals(Qustions.lissSlid.ElementAt(i).Qustion))
                        {
                            del.Add(Qustions.lissSlid.ElementAt(i).Id);
                            types.Add(Qustions.lissSlid.ElementAt(i).TypeOfQuestion); 
                        }
                }
            }
            if (del.Count > 1)
            {
                MessageBox.Show("You Can not choose more than one Question for Edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (del.Count == 0)
            {
                MessageBox.Show("Please Choose one Question for Edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (del.Count == 1)
            {
                QuestionsInformation f = new QuestionsInformation(dataGridView1, del.ElementAt(0), types.ElementAt(0),2);
                f.Show();
            }
        }

    }
}
