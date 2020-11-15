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
    class Smiles :Qustions 
    {
        public Smiles(int Id,int IdForType, string NewText, string TypeOfQuestion, int Order,int NumberOfSmiles) {
            try
            {
                this.NewText = NewText;
                this.NumberOfSmiles = NumberOfSmiles;
                this.Order = Order;
                this.TypeOfQuestion = TypeOfQuestion;
                this.Id = Id;
                this.IdForType = IdForType;
            }catch (Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                Constant.Erros.Log(ex.Message);
            }
        }
        public Smiles(int idForType, string NewText, string TypeOfQuestion, int Order, int NumberOfSmiles)
        {
            try
            {
                this.NewText = NewText;
                this.NumberOfSmiles = NumberOfSmiles;
                this.Order = Order;
                this.TypeOfQuestion = TypeOfQuestion;
                this.IdForType = idForType;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                Constant.Erros.Log(ex.Message);
            }

        }
        public Smiles() { }
        public int NumberOfSmiles { get; set; }
        public int IdForType { get; set; }
        public static void ShowQuestion()
        {
            //  This Function for get Question From data base and add it in my ListOfAllQuestion
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand(Constant.ProcdureQuestionSelectAll, Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlCommand Command1 = new SqlCommand(Constant.ProcdureSmilesSelectAll, Connection);
            Command1.CommandType = CommandType.StoredProcedure;
            Smiles smile;
            try
            {
                Connection.Open();
                Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString, Constant.SmilyString); ;
                SqlDataReader Reader = Command.ExecuteReader();

                smile = new Smiles();
                List<Smiles> ListSmiles = new List<Smiles>();
                while (Reader.Read())
                {
                    smile.Id = Convert.ToInt32(Reader[Constant.IDString]);
                    smile.NewText = Reader[Constant.Qustions_textString].ToString();
                    smile.TypeOfQuestion = Constant.SmilyString;
                    smile.Order = Convert.ToInt32(Reader[Constant.Qustion_orderString]);
                    ListSmiles.Add(smile);
                    smile = new Smiles();
                }
                Reader.Close();

                for (int i = 0; i < ListSmiles.Count; ++i)
                {

                    Command1.Parameters.AddWithValue(Constant.AtsMark+ Constant.Qus_IDString, ListSmiles.ElementAt(i).Id);
                    SqlDataReader Reader1 = Command1.ExecuteReader();
                    while (Reader1.Read())
                    {
                        ListSmiles.ElementAt(i).NumberOfSmiles = Convert.ToInt32(Reader1[Constant.Number_of_smilyString]);
                        ListSmiles.ElementAt(i).IdForType = Convert.ToInt32(Reader1[Constant.IDString]);
                        Constant.ListOfAllQuestion.Add(ListSmiles.ElementAt(i));
                    }
                    Command1.Parameters.Clear();
                    Reader1.Close();
                }
                Command.Parameters.Clear();


            }
            catch (Exception ex)
            {

                MessageBox.Show(Constant.ErrorString);
                Constant.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public override void AddQuestion(string[] Att)
        {
            //  This Function override from Question and immplment it For Add Smily Question 
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand(Constant.ProcdureQuestionInsert, Connection);
            SqlCommand Command1 = new SqlCommand(Constant.ProcdureQuestionSelectForMax, Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue(Constant.AtsMark+ Constant.Qustions_textString, Att[0]);
            Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustion_orderString, Convert.ToInt32(Att[1]));
            Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString, Constant.SmilyString);
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
                Console.WriteLine(ex.Message);
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
                    SqlCommand Command3 = new SqlCommand(Constant.ProcdureSmilesInsert, Connection);
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Constant.AtsMark+ Constant.Number_of_smilyString,Convert.ToInt32(Att[2]));
                    Command3.Parameters.AddWithValue(Constant.AtsMark+ Constant.Qus_IDString, id);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    Smiles.ShowQuestion();
                }
                catch(Exception ex)
                {

                    MessageBox.Show(Constant.ErrorString);
                    Constant.Erros.Log(ex.Message);
                }
                finally
                {
                    Connection.Close(); 
                }
            }
        }

        public override void EditQuestion(string[] Att)
        {
            //  This Function override from Question and immplment it For Edit Smily Question 
            SqlConnection Connection = DataBaseConnections.GetConnection();
            try
            {
                Connection.Open();
                ///////////////////////////////////////////////////////////////////////
                SqlCommand Command3 = new SqlCommand(Constant.ProcdureSmilesUpdate, Connection);
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Constant.AtsMark+ Constant.IDString, Convert.ToInt32(Att[3]));
                if (Att[2] != "")
                    Command3.Parameters.AddWithValue(Constant.AtsMark+ Constant.Number_of_smilyString,Att[2]);
                Command3.ExecuteNonQuery();
                Command3.Parameters.Clear();
                Command3.CommandText = Constant.ProcdureQuestionUpdate;
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Convert.ToInt32(Att[4]));
                Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustions_textString,Att[0]);
                Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustion_orderString, Convert.ToInt32(Att[1]));
                Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString, Constant.SmilyString.ToString());
                Command3.ExecuteNonQuery();

            }
            catch(Exception ex)
            {

                MessageBox.Show(Constant.ErrorString);
                Constant.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close(); 
            }
        }

        public override void Delete(int Id, int IdFroType)
        {
            //  This Function override from Question and immplment it For Delete Smily Question 
            SqlConnection Connection = DataBaseConnections.GetConnection();
            try
            {
                Connection.Open();
                SqlCommand Command3 = new SqlCommand(Constant.ProcdureSmilesDelete, Connection);
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Constant.AtsMark+ Constant.IDString,IdForType);
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

                MessageBox.Show(Constant.ErrorString);
                Constant.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
