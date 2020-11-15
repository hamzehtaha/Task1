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
                MessageBox.Show(Constant.MessageError);
                Constant.Erros.Log(ex.Message);
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
                MessageBox.Show(Constant.MessageError);
                Constant.Erros.Log(ex.Message);
            }
        }
        public Stars()
        {

        }
        public static void ShowQuestion()
        {
            //  This Function for get Question From data base and add it in my ListOfAllQuestion
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand(Constant.ProcdureQuestionSelectAll, Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlCommand Command1 = new SqlCommand(Constant.ProcdureStarsSelectAll, Connection);
            Command1.CommandType = CommandType.StoredProcedure;
            Stars stars;
            try
            {
                Connection.Open();
                Command.Parameters.AddWithValue(Constant.Type_Of_QustionString, Constant.StarsString);
                SqlDataReader Reader = Command.ExecuteReader();

                stars = new Stars();
                List<Stars> ListOfStars = new List<Stars>();
                while (Reader.Read())
                {
                    stars.Id = Convert.ToInt32(Reader[Constant.IDString]);
                    stars.NewText = Reader[Constant.Qustions_textString].ToString();
                    stars.Order = Convert.ToInt32(Reader[Constant.Qustion_orderString]);
                    stars.TypeOfQuestion = Constant.StarsString;
                    ListOfStars.Add(stars);
                    stars = new Stars();
                }
                Reader.Close();

                for (int i = 0; i < ListOfStars.Count; ++i)
                {

                    Command1.Parameters.AddWithValue(Constant.AtsMark+ Constant.Qus_IDString, ListOfStars.ElementAt(i).Id);
                    SqlDataReader Reader1 = Command1.ExecuteReader();
                    while (Reader1.Read())
                    {
                        ListOfStars.ElementAt(i).NumberOfStars = Convert.ToInt32(Reader1[Constant.Number_Of_StarsString]);
                        ListOfStars.ElementAt(i).IdForType = Convert.ToInt32(Reader1[Constant.IDString]);
                        Constant.ListOfAllQuestion.Add(ListOfStars.ElementAt(i));
                    }
                    Command1.Parameters.Clear();
                    Reader1.Close();
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
            //  This Function override from Question and immplment it For Add Stras Question 
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand(Constant.ProcdureQuestionInsert, Connection);
            SqlCommand Command1 = new SqlCommand(Constant.ProcdureQuestionSelectForMax, Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue(Constant.AtsMark+ Constant.Qustions_textString, att[0]);
            Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustion_orderString, Convert.ToInt32(att[1]));
            Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString, Constant.StarsString);
            int id = -1;
            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
                SqlDataReader Reader = Command1.ExecuteReader();
                while (Reader.Read())
                    id = Convert.ToInt32(Reader[Constant.IDString]);
                Reader.Close();

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
                    SqlCommand cmd3 = new SqlCommand(Constant.ProcdureStarsInsert, Connection);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue(Constant.AtsMark+ Constant.Number_Of_StarsString, Convert.ToInt32(att[2]));
                    cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qus_IDString, id);
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
            //  This Function override from Question and immplment it For Edit Stras Question 

            SqlConnection Connection = DataBaseConnections.GetConnection();
            try
            {
                Connection.Open();
                SqlCommand Command3 = new SqlCommand(Constant.ProcdureStarsUpdate, Connection);
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qus_IDString, Convert.ToInt32(Att[3]));
                Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Number_Of_StarsString,Convert.ToInt32(Att[2]));
                Command3.ExecuteNonQuery();
                Command3.Parameters.Clear();
                Command3.CommandText = Constant.ProcdureQuestionUpdate;
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Convert.ToInt32(Att[4]));
                Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustions_textString, Att[0]);
                Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustion_orderString, Convert.ToInt32(Att[1]));
                Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustion_orderString, Constant.StarsString);
                Command3.ExecuteNonQuery();
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

        public override void Delete(int id, int idFroType)
        {
            //  This Function override from Question and immplment it For Delete Stras Question 

            SqlConnection Connection = DataBaseConnections.GetConnection();
            try
            {
                Connection.Open();
                SqlCommand Command3 = new SqlCommand(Constant.ProcdureStarsDelete, Connection);
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, IdForType);
                Command3.ExecuteNonQuery();
                Command3.Parameters.Clear();
                Command3.CommandText = Constant.ProcdureQuestionDelete;
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Constant.AtsMark+ Constant.IDString, Id);
                Command3.ExecuteNonQuery();
                Command3.Parameters.Clear();
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
    }
}
