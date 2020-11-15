using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Survey;
using System.Windows.Forms;

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
            try
            {
                this.NewText = NewText;
                this.Order = Order;
                this.StartValue = StartValue;
                this.EndValue = EndValue;
                this.StartCaption = StartCaption;
                this.EndCaption = EndCaption;
                this.Id = Id;
                this.TypeOfQuestion = TypeOfQuestion;
                this.IdForType = IdForType;
            }catch (Exception ex)
            {
                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);
            }
        }
        public Slider(string NewText, string TypeOfQuestion, int IdForType,int Order, int StartValue, int EndValue, string StartCaption, string EndCaption)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);
            }
        }
        public Slider()
        {

        }
        public static  void ShowQuestion()
        {
            
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand("sp_Qustions_SelectAll2", Connection);
            Command.CommandType = CommandType.StoredProcedure; 

            SqlCommand Command1 = new SqlCommand("sp_Slider_SelectAll2", Connection);
            Command1.CommandType = CommandType.StoredProcedure; 
            Slider slider;
            try
            {
                Connection.Open();
                Command.Parameters.AddWithValue("@"+ Attributes.Type_Of_QustionString, Attributes.SliderString);
                SqlDataReader SqlRedaer = Command.ExecuteReader();

                slider = new Slider();
                List<Slider> ListOfSlider = new List<Slider>(); 
                while (SqlRedaer.Read())
                {
                    slider.Id = Convert.ToInt32(SqlRedaer[Attributes.IDString]);
                    slider.NewText = SqlRedaer[Attributes.Qustions_textString].ToString();
                    slider.TypeOfQuestion = Attributes.SliderString;
                    slider.Order = Convert.ToInt32(SqlRedaer[Attributes.Qustion_orderString]);
                    ListOfSlider.Add(slider);
                    slider = new Slider(); 

                }
                SqlRedaer.Close();
               
                for (int i = 0; i< ListOfSlider.Count; ++i)
                {

                    Command1.Parameters.AddWithValue("@"+ Attributes.Qus_IDString, ListOfSlider.ElementAt(i).Id);
                    SqlDataReader SqlReader1 = Command1.ExecuteReader();
                    while (SqlReader1.Read())
                    {
                        ListOfSlider.ElementAt(i).StartValue = Convert.ToInt32(SqlReader1[Attributes.Start_ValueString]);
                        ListOfSlider.ElementAt(i).EndValue = Convert.ToInt32(SqlReader1[Attributes.End_ValueString]);
                        ListOfSlider.ElementAt(i).StartCaption = SqlReader1[Attributes.Start_Value_CapString].ToString();
                        ListOfSlider.ElementAt(i).EndCaption = SqlReader1[Attributes.End_Value_CapString].ToString();
                        ListOfSlider.ElementAt(i).IdForType = Convert.ToInt32(SqlReader1[Attributes.IDString]);
                        Attributes.ListOfAllQuestion.Add(ListOfSlider.ElementAt(i));
                    }
                    Command1.Parameters.Clear();
                    SqlReader1.Close(); 
                }
                Command.Parameters.Clear();
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Smomething went wrong!");
                Attributes.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public override void AddQuestion(string [] att)
        {
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand("sp_Qustion_Insert3", Connection);
            SqlCommand Command1 = new SqlCommand("select max(ID) as ID from Qustions", Connection);
            int id = -1;
            try{
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@" + Attributes.Qustions_textString, att[0]);
                Command.Parameters.AddWithValue("@" + Attributes.Qustion_orderString, Convert.ToInt32(att[1]));
                Command.Parameters.AddWithValue("@" + Attributes.Type_Of_QustionString, Attributes.SliderString);
                Connection.Open();
                Command.ExecuteNonQuery();
                SqlDataReader rd = Command1.ExecuteReader();
                while (rd.Read())
                    id = Convert.ToInt32(rd[Attributes.IDString]);
                rd.Close();

            }
 
            catch (Exception ex)
            {

                MessageBox.Show("Smomething went wrong!");
                Attributes.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close();
                Command.Parameters.Clear();
                Command1.Parameters.Clear();
            }
            if (id != -1)
            {
                try
                {
                    Connection.Open();
                    Command.CommandText = "sp_Slider_Insert1";
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue(Attributes.AtsMark+ Attributes.Start_ValueString, Convert.ToInt32(att[2]));
                    Command.Parameters.AddWithValue(Attributes.AtsMark + Attributes.End_ValueString, Convert.ToInt32(att[3]));
                    Command.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Start_Value_CapString, att[4]);
                    Command.Parameters.AddWithValue(Attributes.AtsMark + Attributes.End_Value_CapString, att[5]);
                    Command.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qus_IDString, id);
                    Command.ExecuteNonQuery();
                    Slider.ShowQuestion();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                }
                finally
                {
                    Connection.Close(); 
                }
            }
        }

        public override void EditQuestion(string[] att)
        {
            SqlConnection con = DataBaseConnections.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand("sp_Slider_Update10", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.IDString, Convert.ToInt32(att[6]));
                if (att[2]!="")
                cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Start_ValueString, Convert.ToInt32(att[2]));
                if (att[3]!="")
                cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.End_ValueString, Convert.ToInt32(att[3]));
                if (att[4]!="")
                cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Start_Value_CapString, att[4]);
                if (att[5]!="" )
                cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.End_Value_CapString, att[5]);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_update7";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.IDString, Convert.ToInt32(att[7]));
                if (att[0] !="")
                cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qustions_textString, att[0]);
                if (att[1] !="")
                cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qustion_orderString, att[1] );
                cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Type_Of_QustionString, Attributes.SliderString.ToString());
                cmd3.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);

            }
            finally
            {
                con.Close();
            }

        }

        public override void Delete(int id, int idFroType)
        {
            SqlConnection con = DataBaseConnections.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand("sp_Slider_Delete", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.IDString, idFroType);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_Delete";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.IDString, Id);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
