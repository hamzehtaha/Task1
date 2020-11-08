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
using System.Configuration; 
namespace Task1
{
    public partial class QuestionsInformation : Form
    {
        public int id = -1;
        public string type = "";
        public DataGridView dt;
        int EdOrAd = 0;
        public QuestionsInformation(DataGridView dt, int id, string type, int EdOrAd)
        {
            InitializeComponent();
            this.dt = dt;
            this.id = id;
            this.type = type;
            this.EdOrAd = EdOrAd;
            if (EdOrAd == 2)
            {
                panel1.Visible = false;

            }
            else
            {
                panel1.Visible = true;
                panel2.Visible = false;
            }
            if (type.Equals("Slider"))
            {
                Show();
            } else if (type.Equals("Smily"))
            {
                InitHide();
                label3.Text = "Number Of Smiles";
            }else if (type.Equals("Stars"))
            {
                InitHide();
                label3.Text = "Number Of Stars";
            }
        }
        private int InsertQuestion()
        {
            // this function for insert the question in databse 
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
            finally
            {
                con.Close();
            }
        }
        private void InitHide()
        {
            panel2.Visible = true;
            textBox3.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;

        }
        private void Show()
        {
            panel2.Visible = true;
            textBox3.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
        }
        private bool IsNumber(string number)
        {
            // to check the string is number or not ? 
            return int.TryParse(number, out int n);

        }
        private void QuestionsInformation_Load(object sender, EventArgs e)
        {

        }

        private void questionDetalis1_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // for radio Button cahnges 
            if (radioButton1.Checked == true)
            {
                Show(); 
                label3.Text = "Start Value";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // for radio Button cahnges 

            if (radioButton2.Checked == true)
            {
                InitHide(); 

                label3.Text = "Number Of Smiles"; 
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            // for radio Button cahnges 
            if (radioButton3.Checked == true)
            {
                InitHide(); 
                label3.Text = "Number Of Stars";

            }

        }
        private void Clear()
        {
            // to clear every thing and back the curser in the start point 
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox8.Text = null;
            if (EdOrAd == 1)
            {
                if (radioButton1.Checked)
                    radioButton1.Checked = false;
                else if (radioButton2.Checked)
                    radioButton2.Checked = false;
                else if (radioButton3.Checked)
                    radioButton3.Checked = false;
                panel2.Visible = false; 
            }
            textBox1.Focus();
        }
        private bool CheckValidate()
        {
            // this function for check text box is valid or not in some rules 
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
                MessageBox.Show("You Must Fill Question Order", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!IsNumber(textBox8.Text))
            {
                MessageBox.Show("The Oreder of Question Should be Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (Convert.ToInt32(textBox8.Text) <= 0)
            {
                MessageBox.Show("The Oreder of Question Should be greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (textBox2.Text == "")
                {
                    MessageBox.Show("You Must Fill Number of Smile Faces", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                else if (!IsNumber(textBox2.Text))
                {
                    MessageBox.Show("Your Number of smiles is not a Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (Convert.ToInt32(textBox2.Text) <= 0)
                {
                    MessageBox.Show("Your Number of smiles is less than or Equal zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                else if (Convert.ToInt32(textBox2.Text) < 2 || Convert.ToInt32(textBox2.Text) > 5)
                {
                    MessageBox.Show("Your Number of smiles is more or less the range (2-5)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                return true;
            }
            else if (radioButton3.Checked)
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("You Must Fill Number of the number of stars", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                else if (!IsNumber(textBox2.Text))
                {
                    MessageBox.Show("Your Number of Stars is not a Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (Convert.ToInt32(textBox2.Text) <= 0)
                {
                    MessageBox.Show("Your Number of Stars is less than or Equal zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                else if (Convert.ToInt32(textBox2.Text) > 10)
                {
                    MessageBox.Show("Your Number of Stars is more than 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                return true;
            }
            MessageBox.Show("You Should Choose the Type of your Question", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        private bool CheckValid()
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox2.Text == "" && textBox5.Text == "" && textBox8.Text == "" && textBox6.Text == "" && textBox2.Text == "")
            {
                return true;
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (EdOrAd == 1)
            {
                Qustions Bigobj = null;
                string[] att = null;
                bool f = false; 
                if (radioButton1.Checked)
                {
                    f = CheckValidate(); 
                    if (f)
                    {
                        att = new string[6];
                        att[0] = textBox1.Text;
                        att[1] = textBox8.Text;
                        att[2] = textBox2.Text;
                        att[3] = textBox3.Text;
                        att[4] = textBox5.Text;
                        att[5] = textBox6.Text;
                        Console.WriteLine("Done1");
                        Bigobj = Program.QusFac.GetQustion("Slider");
                    }
                }
                else if (radioButton2.Checked)
                {
                    f = CheckValidate(); 
                    if (f)
                    {
                        att = new string[3];
                        att[0] = textBox1.Text;
                        att[1] = textBox8.Text;
                        att[2] = textBox2.Text;
                        Bigobj = Program.QusFac.GetQustion("Smily");

                    }
                }
                else if (radioButton3.Checked)
                {
                    f =  CheckValidate();
                    if (f)
                    {
                        att = new string[3];
                        att[0] = textBox1.Text;
                        att[1] = textBox8.Text;
                        att[2] = textBox2.Text;
                        Bigobj = Program.QusFac.GetQustion("Stars");
                    }
                }
                if (f)
                {
                    Bigobj.AddQuestion(att);
                    MessageBox.Show("Yout Question is Add");
                    Qustions.GetData(dt);
                    this.Close(); 
                }
            }
            else if (EdOrAd == 2)
            {
                string Result = "You Update is ", NewQustion = "";
                int Order = -1;
                Qustions BigObj = null; 
                Slider obj = null;
                Smiles obj1 = null;
                Stars obj2 = null;
                string[]att = null; 
                if (!CheckValid())
                {
                    for (int i = 0; i < Qustions.lissSlid.Count; ++i)
                    {

                        if (Qustions.lissSlid.ElementAt(i).Id == id)
                        {
                            if (Qustions.lissSlid.ElementAt(i) is Slider)
                            {
                                att = new string[8]; 
                                string StC = "", EndC = "";
                                int St = -1, End = -1;
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
                                        if (Order > 0)
                                        {
                                            obj.Order = Order;
                                            Result += " ,Order";
                                        }
                                        else
                                        {
                                            MessageBox.Show("Your order should be greater than 0 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            break;
                                        }
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
                                            MessageBox.Show("Your End Value is less than 0 or Greater than 100", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Your End Value is not a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                }
                                if (textBox5.Text != "")
                                {
                                    if (!IsNumber(textBox5.Text))
                                    {
                                        
                                        StC = textBox5.Text;
                                        obj.StartC = StC;
                                        Result += " ,Start Caption";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Your Start Caption Must not be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                }
                                if (textBox6.Text != "")
                                {
                                    if (!IsNumber(textBox6.Text))
                                    {
                                        EndC = textBox6.Text;
                                        obj.EndC = EndC;
                                        Result += " ,End Caption";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Your End Caption Must not be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                }
                                att[0] = obj.Qustion; 
                                att[1] = obj.Order + ""; 
                                att[2] = obj.StartV + "";
                                att[3] = obj.EndV + ""; 
                                att[4] = obj.StartC; 
                                att[5] = obj.EndC; 
                                att[6] = obj.IdForType + "";
                                att[7] = obj.Id + "";
                                BigObj = Program.QusFac.GetQustion("Slider");
                                Clear(); 
                                MessageBox.Show(Result); 
                            }
                            else if (Qustions.lissSlid.ElementAt(i) is Smiles)
                            {
                                int smiles = -1;
                                obj1 = (Smiles)Qustions.lissSlid.ElementAt(i);
                                att = new string[5]; 
                                if (textBox1.Text != "")
                                {
                                    NewQustion = textBox1.Text;
                                    obj1.Qustion = NewQustion;
                                    Result += "New Qustion ";
                                }
                                if (textBox8.Text != "")
                                {
                                    if (IsNumber(textBox8.Text))
                                    {
                                        Order = Convert.ToInt32(textBox8.Text);
                                        if (Order > 0)
                                        {
                                            obj1.Order = Order;
                                            Result += " ,Order";
                                        }
                                        else
                                        {
                                            MessageBox.Show("Your order should be greater than 0 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            break;
                                        }
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
                                        if (Convert.ToInt32(textBox2.Text) >= 2 && Convert.ToInt32(textBox2.Text) <= 5)
                                        {
                                            smiles = Convert.ToInt32(textBox2.Text);
                                            obj1.NumberOfSmiles = smiles;
                                            Result += " Smiles";
                                        }
                                        else
                                        {
                                            MessageBox.Show("Your Smiles Should be greater than or equal 2 and less than or equal 5", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Your Smiles Should be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                }
                                att[3] = obj1.idForType + "";
                                att[4] = obj1.Id + "";
                                att[2] = obj1.NumberOfSmiles + "";
                                att[1] = obj1.Order + "";
                                att[0] = obj1.Qustion;
                                BigObj = Program.QusFac.GetQustion("Smily"); 
                                Clear();
                                MessageBox.Show(Result); ;
                            }
                            else if (Qustions.lissSlid.ElementAt(i) is Stars)
                            {
                                int stars = -1;
                                att = new string[5]; 
                                obj2 = (Stars)Qustions.lissSlid.ElementAt(i);
                                if (textBox1.Text != "")
                                {
                                    NewQustion = textBox1.Text;
                                    obj2.Qustion = NewQustion;
                                    Result += "New Qustion ";
                                }
                                if (textBox8.Text != "")
                                {
                                    if (IsNumber(textBox8.Text))
                                    {
                                        Order = Convert.ToInt32(textBox8.Text);
                                        if (Order > 0)
                                        {
                                            obj2.Order = Order;
                                            Result += " ,Order";
                                        }
                                        else
                                        {
                                            MessageBox.Show("Your order should be greater than 0 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            break;
                                        }
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
                                        if (Convert.ToInt32(textBox2.Text) <= 10)
                                        {

                                            stars = Convert.ToInt32(textBox2.Text);
                                            if (stars > 0)
                                            {
                                                obj2.NumberOfStars = stars;
                                                Result += " Stars";
                                            }
                                            else
                                            {
                                                MessageBox.Show("Your Stars Should be greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            MessageBox.Show("Your Stars Should be less than 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Your Stars Should be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                    }
                                }
                                att[3] = obj2.idForType + "";
                                att[4] = obj2.Id + "";
                                att[2] = obj2.NumberOfStars + "";
                                att[1] = obj2.Order + "";
                                att[0] = obj2.Qustion;
                                BigObj = Program.QusFac.GetQustion("Stars");
                                Clear();
                                MessageBox.Show(Result); ;                                                                                                     
                            }
                            BigObj.EditQuestion(att);
                            this.Close();
                            break;
                        }
                    }
                }
                else                          
                    MessageBox.Show("There is no any Update ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                Qustions.GetData(dt);

            }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch))
            {
                e.Handled = true;
                MessageBox.Show("only numbers"); 
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch))
            {
                e.Handled = true;
                MessageBox.Show("only numbers");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char ch = e.KeyChar;
            if (!Char.IsDigit(ch))
            {
                e.Handled = true;
                MessageBox.Show("only numbers");
            }
        }
    }
}
