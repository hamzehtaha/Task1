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
                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);
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
                MessageBox.Show(Attributes.MessageError);
                Attributes.Erros.Log(ex.Message);
            }

        }
        public Smiles() { }
        public int NumberOfSmiles { get; set; }
        public int IdForType { get; set; }
        public static void ShowQuestion()
        {
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand("sp_Qustions_SelectAll2", Connection);
            Command.CommandType = CommandType.StoredProcedure;

            SqlCommand Command1 = new SqlCommand("sp_Smily_SelectAll2", Connection);
            Command1.CommandType = CommandType.StoredProcedure;
            Smiles smile;
            try
            {
                Connection.Open();
                Command.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Type_Of_QustionString, Attributes.SmilyString); ;
                SqlDataReader Reader = Command.ExecuteReader();

                smile = new Smiles();
                List<Smiles> ListSmiles = new List<Smiles>();
                while (Reader.Read())
                {
                    smile.Id = Convert.ToInt32(Reader[Attributes.IDString]);
                    smile.NewText = Reader[Attributes.Qustions_textString].ToString();
                    smile.TypeOfQuestion = Attributes.SmilyString;
                    smile.Order = Convert.ToInt32(Reader[Attributes.Qustion_orderString]);
                    ListSmiles.Add(smile);
                    smile = new Smiles();
                }
                Reader.Close();

                for (int i = 0; i < ListSmiles.Count; ++i)
                {

                    Command1.Parameters.AddWithValue(Attributes.AtsMark+ Attributes.Qus_IDString, ListSmiles.ElementAt(i).Id);
                    SqlDataReader Reader1 = Command1.ExecuteReader();
                    while (Reader1.Read())
                    {
                        ListSmiles.ElementAt(i).NumberOfSmiles = Convert.ToInt32(Reader1[Attributes.Number_of_smilyString]);
                        ListSmiles.ElementAt(i).IdForType = Convert.ToInt32(Reader1[Attributes.IDString]);
                        Attributes.ListOfAllQuestion.Add(ListSmiles.ElementAt(i));
                    }
                    Command1.Parameters.Clear();
                    Reader1.Close();
                }
                Command.Parameters.Clear();


            }
            catch (Exception ex)
            {

                MessageBox.Show(Attributes.ErrorString);
                Attributes.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public override void AddQuestion(string[] Att)
        {
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand("sp_Qustion_Insert3", Connection);
            SqlCommand Command1 = new SqlCommand("select max(ID) as ID from Qustions", Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue(Attributes.AtsMark+ Attributes.Qustions_textString, Att[0]);
            Command.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qustion_orderString, Convert.ToInt32(Att[1]));
            Command.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Type_Of_QustionString, Attributes.SmilyString);
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
                    SqlCommand Command3 = new SqlCommand("sp_Smiles_Insert5", Connection);
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Attributes.AtsMark+ Attributes.Number_of_smilyString,Convert.ToInt32(Att[2]));
                    Command3.Parameters.AddWithValue(Attributes.AtsMark+ Attributes.Qus_IDString, id);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    Smiles.ShowQuestion();
                }
                catch(Exception ex)
                {

                    MessageBox.Show(Attributes.ErrorString);
                    Attributes.Erros.Log(ex.Message);
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
                SqlCommand Command3 = new SqlCommand("sp_Smily_Update10", Connection);
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Attributes.AtsMark+ Attributes.IDString, Convert.ToInt32(Att[3]));
                if (Att[2] != "")
                    Command3.Parameters.AddWithValue("@"+ Attributes.Number_of_smilyString,Att[2]);
                Command3.ExecuteNonQuery();
                Command3.Parameters.Clear();
                Command3.CommandText = "sp_Qustion_update7";
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.IDString, Convert.ToInt32(Att[4]));
                Command3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qustions_textString,Att[0]);
                Command3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Qustion_orderString, Convert.ToInt32(Att[1]));
                Command3.Parameters.AddWithValue(Attributes.AtsMark + Attributes.Type_Of_QustionString, Attributes.SmilyString.ToString());
                Command3.ExecuteNonQuery();

            }
            catch(Exception ex)
            {

                MessageBox.Show(Attributes.ErrorString);
                Attributes.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close(); 
            }
        }

        public override void Delete(int Id, int IdFroType)
        {
            SqlConnection Connection = DataBaseConnections.GetConnection();
            try
            {
                Connection.Open();
                SqlCommand Command3 = new SqlCommand("sp_Smily_Delete", Connection);
                Command3.CommandType = CommandType.StoredProcedure;
                Command3.Parameters.AddWithValue(Attributes.AtsMark+ Attributes.IDString,IdForType);
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

                MessageBox.Show(Attributes.ErrorString);
                Attributes.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
