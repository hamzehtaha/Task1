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
    public partial class Form3 : Form
    {
        private int id;
        private string Type;
        public Form3(int id,string Type)
        {
            this.id = id;
            this.Type = Type; 
            InitializeComponent();
            InitHide(); 

        }
        private void InitHide() {
            
            if (Type.Equals("Slider"))
            {
                panel2.Visible = true;
                panel3.Visible = false;
                panel4.Visible = false;
            }
            else if (Type.Equals("Smily"))
            {
                panel2.Visible = false;
                panel3.Visible = true;
                panel4.Visible = false;
            }
            else if (Type.Equals("Stars")) {
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
       
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private bool IsNumber(string number)
        {

            return int.TryParse(number, out int n);

        }
        private bool CheckValid()
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox8.Text == "")
            {
                return true; 
            }
            return false; 
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            string Result = "You Update is ", NewQustion="";
            int Order = -1;
            Slider obj = null;
            Smiles obj1 = null;
            Stars obj2 = null; 
            for (int i = 0; i < Qustions.lissSlid.Count; ++i)
            {
                
                if (Qustions.lissSlid.ElementAt(i).Id == id)
                {
                    if (Qustions.lissSlid.ElementAt(i) is Slider)
                    {

                        if (CheckValid())
                        {
                            MessageBox.Show("There is no any Update ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break; 
                        }
                        else
                        {
                            string StC = "", EndC = "";
                            int St = -1, End = -1 ;
                            obj = (Slider)Qustions.lissSlid.ElementAt(i);

                            if (textBox1.Text != "")
                            {
                                NewQustion = textBox1.Text;
                                Qustions.lissSlid.ElementAt(i).Qustion = NewQustion;
                                Result += "New Qustion ";
                            }
                            if (textBox8.Text != "")
                            {
                                if (IsNumber(textBox8.Text))
                                {
                                    Order = Convert.ToInt32(textBox8.Text);
                                    obj.Order = Order;
                                    Result += " ,Order";
                                }
                                else
                                {
                                    MessageBox.Show("Your order should be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            }
                            if (textBox2.Text != "")
                            {
                                if (IsNumber(textBox2.Text))
                                {
                                    if (Convert.ToInt32(textBox2.Text) > 0 && Convert.ToInt32(textBox2.Text) <= 100)
                                    {
                                        St = Convert.ToInt32(textBox2.Text);
                                        obj.StartV = St;
                                        Result += " ,Start Value";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Your Start Value is less than 0 or Greater than 100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Your Start Value is not a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            }
                            if (textBox3.Text != "")
                            {
                                if (IsNumber(textBox3.Text))
                                {
                                    if (Convert.ToInt32(textBox3.Text) > 0 && Convert.ToInt32(textBox3.Text) <= 100)
                                    {
                                        if (Convert.ToInt32(textBox3.Text) > obj.StartV)
                                        {
                                            End = Convert.ToInt32(textBox3.Text);
                                            obj.EndV = End;
                                            Result += " ,End Value";

                                        }
                                        else
                                        {
                                            MessageBox.Show("Your End Value Should be greater than Start Value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Your Start Value is less than 0 or Greater than 100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Your End Value is not a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            }
                            if (textBox4.Text != "")
                            {
                                if (!IsNumber(textBox4.Text))
                                {
                                    StC = textBox4.Text;
                                    obj.StartC = StC;
                                    Result += " ,Start Caption";
                                }
                                else
                                {
                                    MessageBox.Show("Your Start Caption Must not be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            }
                            if (textBox5.Text != "")
                            {
                                if (!IsNumber(textBox5.Text))
                                {
                                    EndC = textBox5.Text;
                                    obj.EndC = EndC;
                                    Result += " ,End Caption";
                                }
                                else
                                {
                                    MessageBox.Show("Your End Caption Must not be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            }
                          
                            SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                            try
                            {
                                con.Open();
                                SqlCommand cmd3 = new SqlCommand("sp_Slider_Update10", con);
                                cmd3.CommandType = CommandType.StoredProcedure;
                                cmd3.Parameters.AddWithValue("@ID", obj.IdForType);
                                if (textBox2.Text != "")
                                {
                                    cmd3.Parameters.AddWithValue("@Start_Value", St);
                                }
                                else
                                {
                                    cmd3.Parameters.AddWithValue("@Start_Value", obj.StartV);
                                }
                                if (textBox3.Text != "")
                                    cmd3.Parameters.AddWithValue("@End_Value", End);
                                else
                                    cmd3.Parameters.AddWithValue("@End_Value", obj.EndV);
                                if (textBox4.Text != "")
                                    cmd3.Parameters.AddWithValue("@Start_Value_Cap", StC);
                                else
                                    cmd3.Parameters.AddWithValue("@Start_Value_Cap", obj.StartC);
                                if (textBox5.Text != "")
                                    cmd3.Parameters.AddWithValue("@End_Value_Cap", EndC);
                                else
                                    cmd3.Parameters.AddWithValue("@End_Value_Cap", obj.EndC);
                                cmd3.ExecuteNonQuery();
                                cmd3.Parameters.Clear();
                                cmd3.CommandText = "sp_Qustion_update7";
                                cmd3.CommandType = CommandType.StoredProcedure;
                                cmd3.Parameters.AddWithValue("@ID", obj.Id);
                                if (textBox1.Text != "")
                                    cmd3.Parameters.AddWithValue("@Qustions_text", NewQustion);
                                else
                                    cmd3.Parameters.AddWithValue("@Qustions_text", obj.Qustion);
                                if (textBox8.Text != "")
                                    cmd3.Parameters.AddWithValue("@Qustion_order", Order);
                                else
                                    cmd3.Parameters.AddWithValue("@Qustion_order", obj.Order);
                                cmd3.Parameters.AddWithValue("@Type_Of_Qustion", obj.TypeOfQuestion);

                                cmd3.ExecuteNonQuery();
                                ClearTexts();
                                MessageBox.Show("The new update is saved.");
                                //MessageBox.Show(obj.StartV+" "+St);



                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);


                            }
                            finally
                            {
                                con.Close();
                            }

                        }
                    }
                    else if (Qustions.lissSlid.ElementAt(i) is Smiles)
                    {
                        int smiles = -1; 
                        if (CheckValid())
                        {
                            MessageBox.Show("There is no any Update ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break; 
                        }
                        obj1 = (Smiles)Qustions.lissSlid.ElementAt(i);
                        if (textBox1.Text != "")
                        {
                            NewQustion = textBox1.Text;
                            Qustions.lissSlid.ElementAt(i).Qustion = NewQustion;
                            Result += "New Qustion ";
                        }
                        if (textBox8.Text != "")
                        {
                            if (IsNumber(textBox8.Text))
                            {
                                Order = Convert.ToInt32(textBox8.Text);
                                obj1.Order = Order;
                                Result += " ,Order";
                            }
                            else
                            {
                                MessageBox.Show("Your order should be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        } if (textBox6.Text != "")
                        {
                            if (IsNumber(textBox6.Text))
                            {
                                if (Convert.ToInt32(textBox6.Text)>=2 && Convert.ToInt32(textBox6.Text) <= 5)
                                {
                                    smiles = Convert.ToInt32(textBox6.Text);
                                    obj1.NumberOfSmiles = smiles;
                                    Result += " Smiles"; 
                                }
                                else
                                {
                                    MessageBox.Show("Your Smiles Should be greater than or equal 2 and less than or equal 5", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }else
                            {
                                MessageBox.Show("Your Smiles Should be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                        try
                        {
                            con.Open();
                            ///////////////////////////////////////////////////////////////////////
                            SqlCommand cmd3 = new SqlCommand("sp_Slider_Update10", con);
                            cmd3.CommandType = CommandType.StoredProcedure;
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message); 
                        }
                        finally
                        {
                            con.Close(); 
                        }

                    }
                
                }
            }
        }

        private void ClearTexts()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox1.Focus(); 
        }
    }
}
