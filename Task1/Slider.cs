using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Survey;

namespace Task1
{
    class Slider : Qustions
    {
        public int IdForType{get; set;}
        public int StartValue { get; set; }
        public int EndValue { get; set; }
        public string StartCaption { get; set; }
        public string EndCaption { get; set; }
        public Slider(int Id, int IdForType, string NewText, string TypeOfQuestion, int Order, int StartValue, int EndValue, string StartCaption, string EndCaption) {
            this.NewText = NewText;
            this.Order = Order; 
            this.StartValue = StartValue;
            this.EndValue = EndValue;
            this.StartCaption = StartCaption;
            this.EndCaption = EndCaption;
            this.Id = Id; 
            this.TypeOfQuestion = TypeOfQuestion;
            this.IdForType = IdForType; 
        }
        public Slider(string NewText, string TypeOfQuestion, int IdForType,int Order, int StartValue, int EndValue, string StartCaption, string EndCaption)
        {
            this.NewText = NewText;
            this.Order = Order;
            this.StartValue = StartValue;
            this.EndValue = EndValue;
            this.StartCaption = StartCaption;
            this.EndCaption = EndCaption;
            this.TypeOfQuestion = TypeOfQuestion;
            this.IdForType = IdForType;
        }
        public Slider()
        {

        }
        public static  void ShowQuestion()
        {
            
            SqlConnection con = Attributes.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_Qustions_SelectAll2", con);
            cmd.CommandType = CommandType.StoredProcedure; 

            SqlCommand cmd1 = new SqlCommand("sp_Slider_SelectAll2", con);
            cmd1.CommandType = CommandType.StoredProcedure; 
            Slider slider;
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@"+ Attributes.Variables.Type_Of_Qustion, Attributes.Variables.Slider.ToString());
                SqlDataReader rd = cmd.ExecuteReader();

                slider = new Slider();
                List<Slider> ListOfSlider = new List<Slider>(); 
                while (rd.Read())
                {
                    slider.Id = Convert.ToInt32(rd["ID"]);
                    slider.NewText = rd[Attributes.Variables.Qustions_text.ToString()].ToString();
                    slider.TypeOfQuestion = Attributes.Variables.Slider.ToString();
                    slider.Order = Convert.ToInt32(rd[Attributes.Variables.Qustion_order.ToString()]);
                    ListOfSlider.Add(slider);
                    slider = new Slider(); 

                }
                rd.Close();
               
                for (int i = 0; i< ListOfSlider.Count; ++i)
                {

                    cmd1.Parameters.AddWithValue("@"+ Attributes.Variables.Qus_ID, ListOfSlider.ElementAt(i).Id);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                    while (rd1.Read())
                    {
                        ListOfSlider.ElementAt(i).StartValue = Convert.ToInt32(rd1[Attributes.Variables.Start_Value.ToString()]);
                        ListOfSlider.ElementAt(i).EndValue = Convert.ToInt32(rd1[Attributes.Variables.End_Value.ToString()]);
                        ListOfSlider.ElementAt(i).StartCaption = rd1[Attributes.Variables.Start_Value_Cap.ToString()].ToString();
                        ListOfSlider.ElementAt(i).EndCaption = rd1[Attributes.Variables.End_Value_Cap.ToString()].ToString();
                        ListOfSlider.ElementAt(i).IdForType = Convert.ToInt32(rd1[Attributes.Variables.ID.ToString()]);
                        Attributes.ListOfAllQuestion.Add(ListOfSlider.ElementAt(i));
                    }
                    cmd1.Parameters.Clear(); 
                    rd1.Close(); 
                }
                cmd.Parameters.Clear();
                

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
        public override void AddQuestion(string [] att)
        {
            SqlConnection con = Attributes.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_Qustion_Insert3", con);
            SqlCommand cmd1 = new SqlCommand("select max(ID) as ID from Qustions", con);
            int id = -1;
            try{
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@" + Attributes.Variables.Qustions_text, att[0]);
                cmd.Parameters.AddWithValue("@" + Attributes.Variables.Qustion_order, Convert.ToInt32(att[1]));
                cmd.Parameters.AddWithValue("@" + Attributes.Variables.Type_Of_Qustion, Attributes.Variables.Slider.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader rd = cmd1.ExecuteReader();
                while (rd.Read())
                    id = Convert.ToInt32(rd[Attributes.Variables.ID.ToString()]);
                rd.Close();

            }
 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                con.Close();
                cmd.Parameters.Clear();
                cmd1.Parameters.Clear();
            }
            if (id != -1)
            {
                try
                {
                    con.Open();
                    cmd.CommandText = "sp_Slider_Insert1";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@"+ Attributes.Variables.Start_Value, Convert.ToInt32(att[2]));
                    cmd.Parameters.AddWithValue("@" + Attributes.Variables.End_Value, Convert.ToInt32(att[3]));
                    cmd.Parameters.AddWithValue("@" + Attributes.Variables.Start_Value_Cap, att[4]);
                    cmd.Parameters.AddWithValue("@" + Attributes.Variables.End_Value_Cap, att[5]);
                    cmd.Parameters.AddWithValue("@" + Attributes.Variables.Qus_ID, id);
                    cmd.ExecuteNonQuery();
                    Slider.ShowQuestion();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                }
                finally
                {
                    con.Close(); 
                }
            }
        }

        public override void EditQuestion(string[] att)
        {
            SqlConnection con = Attributes.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand("sp_Slider_Update10", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.ID, Convert.ToInt32(att[6]));
                if (att[2]!="")
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.Start_Value, Convert.ToInt32(att[2]));
                if (att[3]!="")
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.End_Value, Convert.ToInt32(att[3]));
                if (att[4]!="")
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.Start_Value_Cap, att[4]);
                if (att[5]!="" )
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.End_Value_Cap, att[5]);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_update7";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.ID, Convert.ToInt32(att[7]));
                if (att[0] !="")
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.Qustions_text, att[0]);
                if (att[1] !="")
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.Qustion_order, att[1] );
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.Type_Of_Qustion, Attributes.Variables.Slider.ToString());
                cmd3.ExecuteNonQuery();
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

        public override void Delete(int id, int idFroType)
        {
            SqlConnection con = Attributes.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand("sp_Slider_Delete", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.ID, idFroType);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_Delete";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.ID, Id);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                
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
