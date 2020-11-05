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

namespace Task1
{
    public partial class QuestionsInformation : Form
    {
        public int id = -1;
        public string type = "";
        public DataGridView dt;
        int EdOrAd = 0; 
        public QuestionsInformation(DataGridView dt,int id,string type, int EdOrAd)
        {
            InitializeComponent();
            HidePanel(); 
            this.dt = dt;
            this.id = id;
            this.type = type;
            this.EdOrAd = EdOrAd;
            if (EdOrAd == 2)
            {
                panel1.Visible = false;
                InitHide(); 
            }
            else
            {
                panel1.Visible = true; 
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

            if (type.Equals("Slider"))
            {
                panel2.Visible = true;
                panel3.Visible = false;
                panel4.Visible = false;
            }
            else if (type.Equals("Smily"))
            {
                panel2.Visible = false;
                panel3.Visible = true;
                panel4.Visible = false;
            }
            else if (type.Equals("Stars"))
            {
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = true;
            }
        }
        private void HidePanel()
        {
            // for hide the textbox 
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
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
                panel2.Visible = true;

            }
            else if (radioButton1.Checked == false)
            {
                panel2.Visible = false;
                panel3.Visible = true;
                panel4.Visible = true;

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // for radio Button cahnges 

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
            // for radio Button cahnges 
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
        private void Clear()
        {
            // to clear every thing and back the curser in the start point 
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox8.Text = null;
            if (EdOrAd == 1)
            {
                if (radioButton1.Checked)
                    radioButton1.Checked = false;
                else if (radioButton2.Checked)
                    radioButton2.Checked = false;
                else if (radioButton3.Checked)
                    radioButton3.Checked = false;
                HidePanel();
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
                else if (Convert.ToInt32(textBox4.Text) < 2 || Convert.ToInt32(textBox4.Text) > 5)
                {
                    MessageBox.Show("Your Number of smiles is more or less the range (2-5)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                return true;
            }
            else if (radioButton3.Checked)
            {
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
        private bool CheckValid()
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox8.Text == "" && textBox6.Text == "" && textBox7.Text == "")
            {
                return true;
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (EdOrAd == 1)
            {
                if (radioButton1.Checked)
                {
                    if (CheckValidate())
                    {
                        int StartValue = Convert.ToInt32(textBox2.Text);
                        int EndValue = Convert.ToInt32(textBox3.Text);
                        string Qus = textBox1.Text;
                        string StartValueCap = textBox5.Text;
                        string EndValueCap = textBox6.Text;
                        int QustionOrder = Convert.ToInt32(textBox8.Text);
                        SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                        try
                        {
                            int id = InsertQuestion();
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
                                Slider.ShowQuestion();
                                MessageBox.Show("Your Qustion is Added", "Added!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Clear();
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
                else if (radioButton2.Checked)
                {


                    if (CheckValidate())
                    {
                        int NumberOfSmiles = Convert.ToInt32(textBox4.Text);
                        string Qus = textBox1.Text;
                        int QustionOrder = Convert.ToInt32(textBox8.Text);
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
                                cmd3.Parameters.Clear();
                                Smiles.ShowQuestion();
                                MessageBox.Show("Your Qustion is Added", "Added!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Clear();
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
                                cmd3.Parameters.Clear();
                                Stars.ShowQuestion();
                                MessageBox.Show("Your Qustion is Added", "Added!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Clear();
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
                Qustions.GetData(dt);
            }else if (EdOrAd == 2)
            {
                string Result = "You Update is ", NewQustion = "";
                int Order = -1;
                Slider obj = null;
                Smiles obj1 = null;
                Stars obj2 = null;
                if (!CheckValid())
                {
                    for (int i = 0; i < Qustions.lissSlid.Count; ++i)
                    {

                        if (Qustions.lissSlid.ElementAt(i).Id == id)
                        {
                            if (Qustions.lissSlid.ElementAt(i) is Slider)
                            {
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

                                SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                                try
                                {
                                    con.Open();
                                    SqlCommand cmd3 = new SqlCommand("sp_Slider_Update10", con);
                                    cmd3.CommandType = CommandType.StoredProcedure;
                                    cmd3.Parameters.AddWithValue("@ID", obj.IdForType);
                                    cmd3.Parameters.AddWithValue("@Start_Value", obj.StartV);
                                    cmd3.Parameters.AddWithValue("@End_Value", obj.EndV);
                                    cmd3.Parameters.AddWithValue("@Start_Value_Cap", obj.StartC);
                                    cmd3.Parameters.AddWithValue("@End_Value_Cap", obj.EndC);
                                    cmd3.ExecuteNonQuery();
                                    cmd3.Parameters.Clear();
                                    cmd3.CommandText = "sp_Qustion_update7";
                                    cmd3.CommandType = CommandType.StoredProcedure;
                                    cmd3.Parameters.AddWithValue("@ID", obj.Id);
                                    cmd3.Parameters.AddWithValue("@Qustions_text", obj.Qustion);
                                    cmd3.Parameters.AddWithValue("@Qustion_order", obj.Order);
                                    cmd3.Parameters.AddWithValue("@Type_Of_Qustion", obj.TypeOfQuestion);
                                    cmd3.ExecuteNonQuery();
                                    Clear();
                                    MessageBox.Show(Result);
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
                            else if (Qustions.lissSlid.ElementAt(i) is Smiles)
                            {
                                int smiles = -1;
                                obj1 = (Smiles)Qustions.lissSlid.ElementAt(i);
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
                                if (textBox4.Text != "")
                                {
                                    if (IsNumber(textBox4.Text))
                                    {
                                        if (Convert.ToInt32(textBox4.Text) >= 2 && Convert.ToInt32(textBox4.Text) <= 5)
                                        {
                                            smiles = Convert.ToInt32(textBox4.Text);
                                            obj1.NumberOfSmiles = smiles;
                                            Result += " Smiles";
                                            ///haha
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
                                SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                                try
                                {
                                    con.Open();
                                    ///////////////////////////////////////////////////////////////////////
                                    SqlCommand cmd3 = new SqlCommand("sp_Smily_Update10", con);
                                    cmd3.CommandType = CommandType.StoredProcedure;
                                    cmd3.Parameters.AddWithValue("@ID", obj1.idForType);
                                    if (textBox6.Text != "")
                                    {
                                        cmd3.Parameters.AddWithValue("@Number_of_smily", smiles);
                                    }
                                    else
                                    {
                                        cmd3.Parameters.AddWithValue("@Number_of_smily", obj1.NumberOfSmiles);
                                    }
                                    cmd3.ExecuteNonQuery();
                                    cmd3.Parameters.Clear();
                                    cmd3.CommandText = "sp_Qustion_update7";
                                    cmd3.CommandType = CommandType.StoredProcedure;
                                    cmd3.Parameters.AddWithValue("@ID", obj1.Id);
                                    cmd3.Parameters.AddWithValue("@Qustions_text", obj1.Qustion);
                                    cmd3.Parameters.AddWithValue("@Qustion_order", obj1.Order);
                                    cmd3.Parameters.AddWithValue("@Type_Of_Qustion", obj1.TypeOfQuestion);

                                    cmd3.ExecuteNonQuery();
                                    Clear();
                                    MessageBox.Show(Result); ;
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
                            else if (Qustions.lissSlid.ElementAt(i) is Stars)
                            {
                                int stars = -1;

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
                                if (textBox7.Text != "")
                                {
                                    if (IsNumber(textBox7.Text))
                                    {
                                        if (Convert.ToInt32(textBox7.Text) <= 10)
                                        {

                                            stars = Convert.ToInt32(textBox7.Text);
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
                                SqlConnection con = new SqlConnection(@"data source=HAMZEH; database=Survey; integrated security=SSPI");
                                try
                                {
                                    con.Open();
                                    ///////////////////////////////////////////////////////////////////////
                                    SqlCommand cmd3 = new SqlCommand("sp_Stars_Update10", con);
                                    cmd3.CommandType = CommandType.StoredProcedure;
                                    cmd3.Parameters.AddWithValue("@ID", obj2.idForType);
                                    cmd3.Parameters.AddWithValue("@Number_Of_Stars", obj2.NumberOfStars);
                                    cmd3.ExecuteNonQuery();
                                    cmd3.Parameters.Clear();
                                    cmd3.CommandText = "sp_Qustion_update7";
                                    cmd3.CommandType = CommandType.StoredProcedure;
                                    cmd3.Parameters.AddWithValue("@ID", obj2.Id);
                                    cmd3.Parameters.AddWithValue("@Qustions_text", obj2.Qustion);
                                    cmd3.Parameters.AddWithValue("@Qustion_order", obj2.Order);
                                    cmd3.Parameters.AddWithValue("@Type_Of_Qustion", obj2.TypeOfQuestion);
                                    cmd3.ExecuteNonQuery();
                                    Clear();
                                    MessageBox.Show(Result); ;
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
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("There is no any Update ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                Qustions.GetData(dt);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
