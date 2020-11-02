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
            Hidden();
            Slider.ShowQuestion();
            Smiles.ShowQuestion();
            Stars.ShowQuestion();
            getData(); 
        }
        private void Hidden() {
            pictureBox16.Visible = false;
        }
        private void getData() {
            // Get Data Function to Get data from database 
            SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
            SqlCommand cmd = new SqlCommand("sp_Qustions_Select1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader(); 
                DataTable dt = new DataTable();
                dt.Load(rd);
                dataGridView1.DataSource = dt;
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
            Form2 f = new Form2();
            f.Show();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             // this function for which cell is choose 
            int rowindex = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[rowindex];
            string s = row.Cells[0].Value.ToString();
            foreach (Qustions c in Qustions.lissSlid)
                if (c.Qustion.Equals(s))
                {
                    ID = c.Id;
                    Type = c.TypeOfQuestion; 
                }
                
 
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
                if (Smiles.f[0])
                {
                    pictureBox6.Image = Properties.Resources.StarYellow;
                    Smiles.f[0] = false;
                }
                else 
                {
                    pictureBox6.Image = Properties.Resources.star;
                    Smiles.f[0] = true;
                }
            

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

                if (Smiles.f[1])
                {
                    pictureBox6.Image = Properties.Resources.StarYellow;
                    pictureBox7.Image = Properties.Resources.StarYellow;
                    Smiles.f[1] = false;
                fill(1);
                }
                else if (CheckStar(1))
                {
                    pictureBox6.Image = Properties.Resources.star;
                    pictureBox7.Image = Properties.Resources.star;
                    Smiles.f[1] = true;
                }
           
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
                if (Smiles.f[2])
                {
                    pictureBox6.Image = Properties.Resources.StarYellow;
                    pictureBox7.Image = Properties.Resources.StarYellow;
                    pictureBox8.Image = Properties.Resources.StarYellow;
                fill(2);
                    Smiles.f[2] = false;
                }
                else if (CheckStar(2))
                {
                    pictureBox6.Image = Properties.Resources.star;
                    pictureBox7.Image = Properties.Resources.star;
                    pictureBox8.Image = Properties.Resources.star;
                    Smiles.f[2] = true;
                }
            
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            
            
                if (Smiles.f[3])
                {
                    pictureBox6.Image = Properties.Resources.StarYellow;
                    pictureBox7.Image = Properties.Resources.StarYellow;
                    pictureBox8.Image = Properties.Resources.StarYellow;
                    pictureBox9.Image = Properties.Resources.StarYellow;
                fill(3);
                    Smiles.f[3] = false;
                }
                else if (CheckStar(3))
                {
                    pictureBox6.Image = Properties.Resources.star;
                    pictureBox7.Image = Properties.Resources.star;
                    pictureBox8.Image = Properties.Resources.star;
                    pictureBox9.Image = Properties.Resources.star;
                    Smiles.f[3] = true;
                }
            
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
        
                if (Smiles.f[4])
                {
                    pictureBox6.Image = Properties.Resources.StarYellow;
                    pictureBox7.Image = Properties.Resources.StarYellow;
                    pictureBox8.Image = Properties.Resources.StarYellow;
                    pictureBox9.Image = Properties.Resources.StarYellow;
                    pictureBox10.Image = Properties.Resources.StarYellow;
                fill(4);
                Smiles.f[4] = false;
                }
                else if (CheckStar(4)){
                    pictureBox6.Image = Properties.Resources.star;
                    pictureBox7.Image = Properties.Resources.star;
                    pictureBox8.Image = Properties.Resources.star;
                    pictureBox9.Image = Properties.Resources.star;
                    pictureBox10.Image = Properties.Resources.star;
                    Smiles.f[4] = true;

                }
            
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            
            
                if (Smiles.f[5])
                {
                    pictureBox6.Image = Properties.Resources.StarYellow;
                    pictureBox7.Image = Properties.Resources.StarYellow;
                    pictureBox8.Image = Properties.Resources.StarYellow;
                    pictureBox9.Image = Properties.Resources.StarYellow;
                    pictureBox10.Image = Properties.Resources.StarYellow;
                    pictureBox11.Image = Properties.Resources.StarYellow;
                fill(5);
                Smiles.f[5] = false;
                }
                else if (CheckStar(5))
                {
                    pictureBox6.Image = Properties.Resources.star;
                    pictureBox7.Image = Properties.Resources.star;
                    pictureBox8.Image = Properties.Resources.star;
                    pictureBox9.Image = Properties.Resources.star;
                    pictureBox10.Image = Properties.Resources.star;
                    pictureBox11.Image = Properties.Resources.star;
                    Smiles.f[5] = true;
                }
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        { 
                if (Smiles.f[6])
                {
                    pictureBox6.Image = Properties.Resources.StarYellow;
                    pictureBox7.Image = Properties.Resources.StarYellow;
                    pictureBox8.Image = Properties.Resources.StarYellow;
                    pictureBox9.Image = Properties.Resources.StarYellow;
                    pictureBox10.Image = Properties.Resources.StarYellow;
                    pictureBox11.Image = Properties.Resources.StarYellow;
                    pictureBox12.Image = Properties.Resources.StarYellow;
                fill(6);
                Smiles.f[6] = false;
                }
                else if (CheckStar(6))
            {
                    pictureBox6.Image = Properties.Resources.star;
                    pictureBox7.Image = Properties.Resources.star;
                    pictureBox8.Image = Properties.Resources.star;
                    pictureBox9.Image = Properties.Resources.star;
                    pictureBox10.Image = Properties.Resources.star;
                    pictureBox11.Image = Properties.Resources.star;
                    pictureBox12.Image = Properties.Resources.star;
                    Smiles.f[6] = true;
                }
            
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            
            
                if (Smiles.f[7])
                {
                    pictureBox6.Image = Properties.Resources.StarYellow;
                    pictureBox7.Image = Properties.Resources.StarYellow;
                    pictureBox8.Image = Properties.Resources.StarYellow;
                    pictureBox9.Image = Properties.Resources.StarYellow;
                    pictureBox10.Image = Properties.Resources.StarYellow;
                    pictureBox11.Image = Properties.Resources.StarYellow;
                    pictureBox12.Image = Properties.Resources.StarYellow;
                    pictureBox13.Image = Properties.Resources.StarYellow;
                fill(7);
                Smiles.f[7] = false;
                }
                else if (CheckStar(7))
                {
                    pictureBox6.Image = Properties.Resources.star;
                    pictureBox7.Image = Properties.Resources.star;
                    pictureBox8.Image = Properties.Resources.star;
                    pictureBox9.Image = Properties.Resources.star;
                    pictureBox10.Image = Properties.Resources.star;
                    pictureBox11.Image = Properties.Resources.star;
                    pictureBox12.Image = Properties.Resources.star;
                    pictureBox13.Image = Properties.Resources.star;
                    Smiles.f[7] = true;
                }
            
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
                if (Smiles.f[8])
                {
                    pictureBox6.Image = Properties.Resources.StarYellow;
                    pictureBox7.Image = Properties.Resources.StarYellow;
                    pictureBox8.Image = Properties.Resources.StarYellow;
                    pictureBox9.Image = Properties.Resources.StarYellow;
                    pictureBox10.Image = Properties.Resources.StarYellow;
                    pictureBox11.Image = Properties.Resources.StarYellow;
                    pictureBox12.Image = Properties.Resources.StarYellow;
                    pictureBox13.Image = Properties.Resources.StarYellow;
                    pictureBox14.Image = Properties.Resources.StarYellow;
                fill(8);
                Smiles.f[8] = false;
                }
                else if (CheckStar(8))
                {
                    pictureBox6.Image = Properties.Resources.star;
                    pictureBox7.Image = Properties.Resources.star;
                    pictureBox8.Image = Properties.Resources.star;
                    pictureBox9.Image = Properties.Resources.star;
                    pictureBox10.Image = Properties.Resources.star;
                    pictureBox11.Image = Properties.Resources.star;
                    pictureBox12.Image = Properties.Resources.star;
                    pictureBox13.Image = Properties.Resources.star;
                    pictureBox14.Image = Properties.Resources.star;
                    Smiles.f[8] = true;
                }
            
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            
                if (Smiles.f[9])
                {
                    pictureBox6.Image = Properties.Resources.StarYellow;
                    pictureBox7.Image = Properties.Resources.StarYellow;
                    pictureBox8.Image = Properties.Resources.StarYellow;
                    pictureBox9.Image = Properties.Resources.StarYellow;
                    pictureBox10.Image = Properties.Resources.StarYellow;
                    pictureBox11.Image = Properties.Resources.StarYellow;
                    pictureBox12.Image = Properties.Resources.StarYellow;
                    pictureBox13.Image = Properties.Resources.StarYellow;
                    pictureBox14.Image = Properties.Resources.StarYellow;
                    pictureBox15.Image = Properties.Resources.StarYellow;
                fill(9);
                Smiles.f[9] = false;
                }
                else if (CheckStar(9))
                {
                    pictureBox6.Image = Properties.Resources.star;
                    pictureBox7.Image = Properties.Resources.star;
                    pictureBox8.Image = Properties.Resources.star;
                    pictureBox9.Image = Properties.Resources.star;
                    pictureBox10.Image = Properties.Resources.star;
                    pictureBox11.Image = Properties.Resources.star;
                    pictureBox12.Image = Properties.Resources.star;
                    pictureBox13.Image = Properties.Resources.star;
                    pictureBox14.Image = Properties.Resources.star;
                    pictureBox15.Image = Properties.Resources.star;
                    Smiles.f[9] = true;
                }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
             
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // to the refresh the data 
            Slider.ShowQuestion();
            Smiles.ShowQuestion();
            Stars.ShowQuestion();
            getData(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // this function for delete the answer 
            DialogResult dialogResult = MessageBox.Show("Are you sure to Delete this answer ?", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Slider obj1 = null;
                Smiles obj2 = null;
                Stars  obj3 = null; 
                if (ID == -1 && Qustions.lissSlid.Count>0)
                {
                    // if user not choose by defual choose the first question 
                    DataGridViewRow row = dataGridView1.Rows[0];
                    ID = Qustions.lissSlid.ElementAt(0).Id;
                    Type = Qustions.lissSlid.ElementAt(0).TypeOfQuestion;

                }
                // delete from data base 
                for (int i = 0; i<Qustions.lissSlid.Count; ++i)
                {
                    if (Qustions.lissSlid.ElementAt(i).Id == ID)
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
                                MessageBox.Show("This Answer is Deleted");
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
                        }else if (Qustions.lissSlid.ElementAt(i) is Smiles)
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
                                MessageBox.Show("This Answer is Deleted");
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
                                MessageBox.Show("This Answer is Deleted");
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
                getData(); 
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // for take the id for cell which choose 
            if (ID == -1) {
                
                DataGridViewRow row = dataGridView1.Rows[0];
                ID = Qustions.lissSlid.ElementAt(0).Id;
                Type = Qustions.lissSlid.ElementAt(0).TypeOfQuestion; 

            }
            // this function for go to the edit page and give me the id 
            Form3 f = new Form3(ID,Type);
            f.Show();
        }

    }
}
