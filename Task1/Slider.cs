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
                MessageBox.Show(Constant.MessageError);
                Constant.Erros.Log(ex.Message);
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
                MessageBox.Show(Constant.MessageError);
                Constant.Erros.Log(ex.Message);
            }
        }
        public Slider()
        {

        }
        public static void ShowQuestion()
        {
            //This function to get a Question from data base and add it in my ListOfAllQuestion 
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand(Constant.ProcdureQuestionSelectAll, Connection);
            Command.CommandType = CommandType.StoredProcedure; 

            SqlCommand Command1 = new SqlCommand(Constant.ProcdureSliderSelectAll, Connection);
            Command1.CommandType = CommandType.StoredProcedure; 
            Slider slider;
            try
            {
                Connection.Open();
                Command.Parameters.AddWithValue(Constant.AtsMark+ Constant.Type_Of_QustionString, Constant.SliderString);
                SqlDataReader SqlRedaer = Command.ExecuteReader();

                slider = new Slider();
                List<Slider> ListOfSlider = new List<Slider>(); 
                while (SqlRedaer.Read())
                {
                    slider.Id = Convert.ToInt32(SqlRedaer[Constant.IDString]);
                    slider.NewText = SqlRedaer[Constant.Qustions_textString].ToString();
                    slider.TypeOfQuestion = Constant.SliderString;
                    slider.Order = Convert.ToInt32(SqlRedaer[Constant.Qustion_orderString]);
                    ListOfSlider.Add(slider);
                    slider = new Slider(); 

                }
                SqlRedaer.Close();
               
                for (int i = 0; i< ListOfSlider.Count; ++i)
                {

                    Command1.Parameters.AddWithValue(Constant.AtsMark+ Constant.Qus_IDString, ListOfSlider.ElementAt(i).Id);
                    SqlDataReader SqlReader1 = Command1.ExecuteReader();
                    while (SqlReader1.Read())
                    {
                        ListOfSlider.ElementAt(i).StartValue = Convert.ToInt32(SqlReader1[Constant.Start_ValueString]);
                        ListOfSlider.ElementAt(i).EndValue = Convert.ToInt32(SqlReader1[Constant.End_ValueString]);
                        ListOfSlider.ElementAt(i).StartCaption = SqlReader1[Constant.Start_Value_CapString].ToString();
                        ListOfSlider.ElementAt(i).EndCaption = SqlReader1[Constant.End_Value_CapString].ToString();
                        ListOfSlider.ElementAt(i).IdForType = Convert.ToInt32(SqlReader1[Constant.IDString]);
                        Constant.ListOfAllQuestion.Add(ListOfSlider.ElementAt(i));
                    }
                    Command1.Parameters.Clear();
                    SqlReader1.Close(); 
                }
                Command.Parameters.Clear();
                

            }
            catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                Constant.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
        public override void AddQuestion(string [] att)
        {
            //  This Function override from Question and immplment it For Add Slider Question 
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand(Constant.ProcdureQuestionInsert, Connection);
            SqlCommand Command1 = new SqlCommand(Constant.ProcdureQuestionSelectForMax, Connection);
            int id = -1;
            try{
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustions_textString, att[0]);
                Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustion_orderString, Convert.ToInt32(att[1]));
                Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString, Constant.SliderString);
                Connection.Open();
                Command.ExecuteNonQuery();
                SqlDataReader rd = Command1.ExecuteReader();
                while (rd.Read())
                    id = Convert.ToInt32(rd[Constant.IDString]);
                rd.Close();

            }
 
            catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                Constant.Erros.Log(ex.Message);
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
                    Command.CommandText = Constant.ProcdureSliderInsert;
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue(Constant.AtsMark+ Constant.Start_ValueString, Convert.ToInt32(att[2]));
                    Command.Parameters.AddWithValue(Constant.AtsMark + Constant.End_ValueString, Convert.ToInt32(att[3]));
                    Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Start_Value_CapString, att[4]);
                    Command.Parameters.AddWithValue(Constant.AtsMark + Constant.End_Value_CapString, att[5]);
                    Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Qus_IDString, id);
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
            //  This Function override from Question and immplment it For Edit Slider Question 
            SqlConnection con = DataBaseConnections.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand(Constant.ProcdureSliderUpdate, con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Convert.ToInt32(att[6]));
                if (att[2]!="")
                cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Start_ValueString, Convert.ToInt32(att[2]));
                if (att[3]!="")
                cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.End_ValueString, Convert.ToInt32(att[3]));
                if (att[4]!="")
                cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Start_Value_CapString, att[4]);
                if (att[5]!="" )
                cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.End_Value_CapString, att[5]);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = Constant.ProcdureQuestionUpdate;
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Convert.ToInt32(att[7]));
                if (att[0] !="")
                cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustions_textString, att[0]);
                if (att[1] !="")
                cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustion_orderString, att[1] );
                cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString, Constant.SliderString.ToString());
                cmd3.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                Constant.Erros.Log(ex.Message);

            }
            finally
            {
                con.Close();
            }

        }

        public override void Delete(int id, int idFroType)
        {
            //  This Function override from Question and immplment it For Delete Question 
            SqlConnection con = DataBaseConnections.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand(Constant.ProcdureSliderDelete, con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, idFroType);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = Constant.ProcdureQuestionDelete;
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Id);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(Constant.MessageError);
                Constant.Erros.Log(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
