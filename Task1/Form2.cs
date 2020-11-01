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

namespace Task1
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        public Form2()
        {
            InitializeComponent();
            HidePanel(); 
        }
        private void HidePanel() {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            


        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private int InsertQuestion() {
            int QustionOrder = Convert.ToInt32(textBox8.Text);
            string Qus = textBox1.Text;
            SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
            SqlCommand cmd = new SqlCommand("sp_Qustion_Insert3", con);
            SqlCommand cmd1 = new SqlCommand("select max(ID) as ID from Qustions", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qustions_text", Qus);
            cmd.Parameters.AddWithValue("@Qustion_order", QustionOrder);
            if (radioButton1.Checked)
                cmd.Parameters.AddWithValue("@Type_Of_Qustion", "Slider");
            else if (radioButton2.Checked)
                cmd.Parameters.AddWithValue("@Type_Of_Qustion", "Smily");
            else if (radioButton3.Checked)
                cmd.Parameters.AddWithValue("@Type_Of_Qustion", "Stars");
            int id = -1; 
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader rd = cmd1.ExecuteReader();
                while (rd.Read())
                    id = Convert.ToInt32(rd["ID"]);
                rd.Close();

                return id; 
            }
            catch (Exception ex)
            {
                return id; 
            }
            finally {
                con.Close(); 
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) {
                panel2.Visible = true; 
            
            }else if (radioButton1.Checked == false)
            {
                panel2.Visible = false;
                panel3.Visible = true;
                panel4.Visible = true; 

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private bool IsNumber(string number) {
            
            return int.TryParse(number, out int n);
        
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            if (radioButton2.Checked == true)
            {
                panel3.Visible = true;
                panel2.Visible = false;
                panel4.Visible = false;

            }
            else if (radioButton2.Checked == false)
            {
                panel3.Visible = false;

            }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                panel3.Visible = false;
                panel2.Visible = false;
                panel4.Visible = true;

            }
            else if (radioButton3.Checked == false)
            {
                panel4.Visible = false;

            }
        }
        private void Clear() {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox8.Text = null;
            if (radioButton1.Checked)
                radioButton1.Checked = false;
            else if (radioButton2.Checked)
                radioButton2.Checked = false;
            else if (radioButton3.Checked)
                radioButton3.Checked = false;
            HidePanel();
            textBox1.Focus();
        }
        private bool CheckValidate() {
            if (textBox1.Text == "")
            {
                MessageBox.Show("You Must Fill Your Qustion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (IsNumber(textBox1.Text))
            {
                MessageBox.Show("Your Question is Wrong maybe Contain a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (textBox8.Text == "")
            {
                MessageBox.Show("You Must Fill Question Oreder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!IsNumber(textBox8.Text))
            {
                MessageBox.Show("The Oreder of Question Should be Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; 
            }
            if (radioButton1.Checked)
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("You Must Fill Start Value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (!IsNumber(textBox2.Text))
                {
                    MessageBox.Show("Your start Value is not a Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (Convert.ToInt32(textBox2.Text) <= 0)
                {
                    MessageBox.Show("Your start Value less than or equal 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (textBox3.Text == "")
                {
                    MessageBox.Show("You Must Fill End Value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (!IsNumber(textBox3.Text))
                {
                    MessageBox.Show("Your end Value is not a Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (Convert.ToInt32(textBox3.Text) <= 0)
                {
                    MessageBox.Show("Your end Value less than or equal 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (Convert.ToInt32(textBox3.Text) > 100)
                {
                    MessageBox.Show("Your end Value is greater than 100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (Convert.ToInt32(textBox2.Text) >= Convert.ToInt32(textBox3.Text))
                {
                    MessageBox.Show("Your Start Value greater than or Equal End Value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (textBox5.Text == "")
                {
                    MessageBox.Show("You Must Fill Start Value Caption", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (IsNumber(textBox5.Text))
                {
                    MessageBox.Show("Your Caption of start value caption is invlaid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (textBox6.Text == "")
                {
                    MessageBox.Show("You Must Fill End Value Caption", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (IsNumber(textBox6.Text))
                {
                    MessageBox.Show("Your Caption of end value caption  is invlaid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            else if (radioButton2.Checked)
            {
                if (textBox4.Text == "")
                {
                    MessageBox.Show("You Must Fill Number of Smile Faces", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                else if (!IsNumber(textBox4.Text))
                {
                    MessageBox.Show("Your Number of smiles is not a Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (Convert.ToInt32(textBox4.Text) <= 0)
                {
                    MessageBox.Show("Your Number of smiles is less than or Equal zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                else if (Convert.ToInt32(textBox4.Text) <2 || Convert.ToInt32(textBox4.Text)>5)
                {
                    MessageBox.Show("Your Number of smiles is more or less the range (2-5)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                return true;
            }
            else if (radioButton3.Checked) {
                if (textBox7.Text == "")
                {
                    MessageBox.Show("You Must Fill Number of the number of stars", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                else if (!IsNumber(textBox7.Text))
                {
                    MessageBox.Show("Your Number of Stars is not a Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (Convert.ToInt32(textBox7.Text) <= 0)
                {
                    MessageBox.Show("Your Number of Stars is less than or Equal zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                else if (Convert.ToInt32(textBox7.Text) > 10)
                {
                    MessageBox.Show("Your Number of Stars is more than 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                return true;
            }
            MessageBox.Show("You Should Choose the Type of your Question", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false; 
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
             if (radioButton1.Checked)
            {
                if (CheckValidate()) {
                    int StartValue = Convert.ToInt32(textBox2.Text);
                    int EndValue = Convert.ToInt32(textBox3.Text);
                    string Qus = textBox1.Text;
                    string StartValueCap = textBox5.Text;
                    string EndValueCap = textBox6.Text;
                    int QustionOrder = Convert.ToInt32(textBox8.Text);
                   // Qustions obj = new Slider(Qus, "Slider", QustionOrder,StartValue, EndValue, StartValueCap, EndValueCap);
                    SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                    try
                    {
                        int id = InsertQuestion();
                        //obj.Id = id; 
                        if (id != -1)
                        {
                            con.Open();
                            SqlCommand cmd3 = new SqlCommand("sp_Slider_Insert1", con);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.AddWithValue("@Start_Value", StartValue);
                            cmd3.Parameters.AddWithValue("@End_Value", EndValue);
                            cmd3.Parameters.AddWithValue("@Start_Value_Cap", StartValueCap);
                            cmd3.Parameters.AddWithValue("@End_Value_Cap", EndValueCap);
                            cmd3.Parameters.AddWithValue("@Qus_ID", id);
                            cmd3.ExecuteNonQuery();
                            MessageBox.Show("Your Qustion is Added", "Added!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Clear();
                           // Qustions.lissSlid.Add(obj);
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
            }
            else if (radioButton2.Checked) {


                if (CheckValidate())
                {
                    int NumberOfSmiles = Convert.ToInt32(textBox4.Text);
                    string Qus = textBox1.Text;
                    int QustionOrder = Convert.ToInt32(textBox8.Text);
                    //Qustions obj = new Smiles(Qus,"Smiles",QustionOrder, NumberOfSmiles);
                    SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                    try
                    {
                        int id = InsertQuestion();
                        if (id != -1)
                        {
                            con.Open();
                            SqlCommand cmd3 = new SqlCommand("sp_Smiles_Insert5", con);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.AddWithValue("@Number_of_smily", NumberOfSmiles);
                            cmd3.Parameters.AddWithValue("@Qus_ID", id);
                            cmd3.ExecuteNonQuery();
                            MessageBox.Show("Your Qustion is Added", "Added!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Clear();
                           // Qustions.lissSlid.Add(obj);
                        }
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
            else if (radioButton3.Checked)
            {
                if (CheckValidate())
                {
                    int NumberOfStars = Convert.ToInt32(textBox7.Text);
                    string Qus = textBox1.Text;
                    int QustionOrder = Convert.ToInt32(textBox8.Text);
                //    Qustions obj = new Stars(Qus,"Stars", QustionOrder, NumberOfStars);
                    SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                    try
                    {
                        int id = InsertQuestion();
                        if (id != -1)
                        {
                            con.Open();
                            SqlCommand cmd3 = new SqlCommand("sp_Stars_Insert6", con);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.AddWithValue("@Number_Of_Stars", NumberOfStars);
                            cmd3.Parameters.AddWithValue("@Qus_ID", id);
                            cmd3.ExecuteNonQuery();
                            MessageBox.Show("Your Qustion is Added", "Added!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Clear();
                           // Qustions.lissSlid.Add(obj);
                        }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            this.Close(); 
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
