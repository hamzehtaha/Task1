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

namespace Task1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private int ID;
        private string Type; 
        public Form1()
        { 
            InitializeComponent();
            Hidden();
            
            getData();
         }
        private void Hidden() {
            pictureBox16.Visible = false;
            
        }
        private void getData() {
            SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
            SqlCommand cmd1 = new SqlCommand("select * from Qustions", con);
             try
            {
                con.Open();
                string s = "",type;
                int or = 0;
                dataGridView1.ColumnCount = 3;
                dataGridView1.RowCount = 100; 
                SqlDataReader rd = cmd1.ExecuteReader();
                List<string> li = new List<string>();
                List<int> li1 = new List<int>();
                List<int> liOrder = new List<int>();
                List<string> liQus = new List<string>(); 
                int c = 0;
                int id;
                
                while (rd.Read()) {
                    s = rd["Qustions_text"].ToString(); 
                    or = Convert.ToInt32(rd["Qustion_order"]);
                    id = Convert.ToInt32(rd["ID"]);
                    type = rd["Type_Of_Qustion"].ToString();
                    dataGridView1.Rows[c].Cells[0].Value = s;
                    dataGridView1.Rows[c].Cells[2].Value = or;
                    dataGridView1.Rows[c].Cells[1].Value = type;
                    ++c;
                    li1.Add(id); 
                    li.Add(type);
                    liOrder.Add(or);
                    liQus.Add(s); 
                }
                
                c = 0;
                rd.Close();
                for (int i = 0; i <li1.Count; ++i)
                {
                    if (li.ElementAt(i).Equals("Slider"))
                    {
                        cmd1.CommandText = "select * from Slider where Qus_ID=" +li1.ElementAt(i);
                        SqlDataReader rd2 = cmd1.ExecuteReader();
                        while (rd2.Read())
                        {
                            Qustions obj = new Slider(li1.ElementAt(i), Convert.ToInt32(rd2["ID"]), liQus.ElementAt(i), li.ElementAt(i), liOrder.ElementAt(i), Convert.ToInt32(rd2["Start_Value"]), Convert.ToInt32(rd2["End_Value"]), rd2["Start_Value_Cap"].ToString(), rd2["End_Value_Cap"].ToString());
                            Slider obj1 =(Slider) obj;
                            obj1.IdForType = Convert.ToInt32(rd2["ID"]); 
                            Qustions.lissSlid.Add(obj);

                        }
                        rd2.Close();

                    }
                    else if (li.ElementAt(i).Equals("Smily"))
                    {
                        
                        cmd1.CommandText = "select * from Smily where Qus_ID=" + li1.ElementAt(i);
                        SqlDataReader rd2 = cmd1.ExecuteReader();
                        while (rd2.Read())
                        {
                            Qustions obj = new Smiles(li1.ElementAt(i),Convert.ToInt32(rd2["ID"]), liQus.ElementAt(i), li.ElementAt(i), liOrder.ElementAt(i), Convert.ToInt32(rd2["Number_of_smily"]));
                            Smiles obj1 = (Smiles)obj;
                            obj1.idForType = Convert.ToInt32(rd2["ID"]);
                            Qustions.lissSlid.Add(obj);
                        }
                        rd2.Close(); 
                    }
                    else if (li.ElementAt(i).Equals("Stars"))
                    {
                        cmd1.CommandText = "select * from stars where Qus_ID=" + li1.ElementAt(i);
                        
                        SqlDataReader rd2 = cmd1.ExecuteReader();
                        while (rd2.Read())
                        {
                             
                            Qustions obj = new Stars(li1.ElementAt(i), Convert.ToInt32(rd2["ID"]), liQus.ElementAt(i), li.ElementAt(i), liOrder.ElementAt(i), Convert.ToInt32(rd2["Number_Of_Stars"]));
                            Qustions.lissSlid.Add(obj);
                        }
                        rd2.Close(); 


                    }
                }

             }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                
                con.Close(); 
                
            }

        }
        private void fill(int number) {
            for (int i = 0; i <number; ++i) {
                Smiles.f[i] = true; 
            }
        }
        private bool CheckStar(int number)
        {
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
            Form2 f = new Form2();
            f.Show();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             
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
            getData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (ID == 0) {
                
                DataGridViewRow row = dataGridView1.Rows[0];
                ID = Qustions.lissSlid.ElementAt(0).Id;
                Type = Qustions.lissSlid.ElementAt(0).TypeOfQuestion; 

            }
            Form3 f = new Form3(ID,Type);
            f.Show();
        }

    }
}
