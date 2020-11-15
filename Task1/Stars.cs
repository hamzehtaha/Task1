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
    class Stars : Qustions
    {
        public int NumberOfStars { get; set; }
        public int IdForType { get; set;  }
        public Stars(int Id,int IdForType, string NewText, string TypeOfQuestion, int Order , int NumberOfStars) {
            try
            {
                this.NewText = NewText;
                this.NumberOfStars = NumberOfStars;
                this.Order = Order;
                this.TypeOfQuestion = TypeOfQuestion;
                this.Id = Id;
                this.IdForType = IdForType;
            }catch(Exception ex)
            {
                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);
            }
        }
        public Stars(int IdForType, string NewText, string TypeOfQuestion, int Order, int NumberOfStars)
        {
            try
            {
                this.NewText = NewText;
                this.NumberOfStars = NumberOfStars;
                this.Order = Order;
                this.TypeOfQuestion = TypeOfQuestion;
                this.IdForType = IdForType;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);
            }
        }
        public Stars()
        {

        }
        public static void ShowQuestion()
        {

            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand("sp_Qustions_SelectAll2", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlCommand Command1 = new SqlCommand("sp_Stars_SelectAll2", Connection);
            Command1.CommandType = CommandType.StoredProcedure;
            Stars stars;
            try
            {
                Connection.Open();
                Command.Parameters.AddWithValue(Attributes.Type_Of_QustionString, Attributes.StarsString);
                SqlDataReader Reader = Command.ExecuteReader();

                stars = new Stars();
                List<Stars> ListOfStars = new List<Stars>();
                while (Reader.Read())
                {
                    stars.Id = Convert.ToInt32(Reader[Attributes.IDString]);
                    stars.NewText = Reader[Attributes.Qustions_textString].ToString();
                    stars.Order = Convert.ToInt32(Reader[Attributes.Qustion_orderString]);
                    stars.TypeOfQuestion = Attributes.StarsString;
                    ListOfStars.Add(stars);
                    stars = new Stars();
                }
                Reader.Close();

                for (int i = 0; i < ListOfStars.Count; ++i)
                {

                    Command1.Parameters.AddWithValue(Attributes.AtsMark+ Attributes.Qus_IDString, ListOfStars.ElementAt(i).Id);
                    SqlDataReader Reader1 = Command1.ExecuteReader();
                    while (Reader1.Read())
                    {
                        ListOfStars.ElementAt(i).NumberOfStars = Convert.ToInt32(Reader1[Attributes.Number_Of_StarsString]);
                        ListOfStars.ElementAt(i).IdForType = Convert.ToInt32(Reader1[Attributes.IDString]);
                        Attributes.ListOfAllQuestion.Add(ListOfStars.ElementAt(i));
                    }
                    Command1.Parameters.Clear();
                    Reader1.Close();
                }
                Command.Parameters.Clear();


            }
            catch (Exception ex)
            {
                MessageBox.Show(Attributes.MessageError);
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
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue(Attributes.AtsMark+ Attributes.Qustions_textString, att[0]);
            Command.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qustion_orderString, Convert.ToInt32(att[1]));
            Command.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Type_Of_QustionString, Attributes.StarsString);
            int id = -1;
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                SqlDataReader Reader = Command1.ExecuteReader();
                while (Reader.Read())
                    id = Convert.ToInt32(Reader[Attributes.IDString]);
                Reader.Close();

            }

            catch (Exception ex)
            {

                MessageBox.Show(Attributes.MessageError);
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
                    SqlCommand cmd3 = new SqlCommand("sp_Stars_Insert6", Connection);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue(Attributes.AtsMark+Attributes.Number_Of_StarsString, Convert.ToInt32(att[2]));
                    cmd3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qus_IDString, id);
                    cmd3.ExecuteNonQuery();
                    cmd3.Parameters.Clear();
                    Stars.ShowQuestion();
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Connection.Close(); 
                }
            }
        }

        public override void EditQuestion(string[] Att)
        {
            SqlConnection Connection = DataBaseConnections.GetConnection();
            try
            {
                Connection.Open();
                ///////////////////////////////////////////////////////////////////////
                SqlCommand Command3 = new SqlCommand("sp_Stars_Update10", Connection);
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qus_IDString, Convert.ToInt32(Att[3]));
                Command3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Number_Of_StarsString,Convert.ToInt32(Att[2]));
                Command3.ExecuteNonQuery();
                Command3.Parameters.Clear();
                Command3.CommandText = "sp_Qustion_update7";
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.IDString, Convert.ToInt32(Att[4]));
                Command3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qustions_textString, Att[0]);
                Command3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qustion_orderString, Convert.ToInt32(Att[1]));
                Command3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qustion_orderString, Attributes.StarsString);
                Command3.ExecuteNonQuery();
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

        public override void Delete(int id, int idFroType)
        {
            SqlConnection Connection = DataBaseConnections.GetConnection();
            try
            {
                Connection.Open();
                SqlCommand Command3 = new SqlCommand("sp_Stars_Delete", Connection);
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.IDString, IdForType);
                Command3.ExecuteNonQuery();
                Command3.Parameters.Clear();
                Command3.CommandText = "sp_Qustion_Delete";
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Attributes.AtsMark+ Attributes.IDString, Id);
                Command3.ExecuteNonQuery();
                Command3.Parameters.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

        }
    }
}
