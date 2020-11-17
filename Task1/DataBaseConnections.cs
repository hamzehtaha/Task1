using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;
using System.Windows.Forms;
using Task1;
namespace Survey
{
    // This Class For Data Base and get Connection with data base  
    public static class DataBaseConnections
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings[Constant.ConnectionString].ConnectionString;
        public static SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(DataBaseConnections.connectionString);
            }
            catch (Exception ex)
            {
                return new SqlConnection(DataBaseConnections.connectionString);
            }
        }
        public static void GetQuestionFromDataBase(Qustions Question)
        {
            // to get data from data base and put it in my list
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand(Constant.ProcdureQuestionSelectAll, Connection);
            Command.CommandType = CommandType.StoredProcedure;
            SqlCommand Command1 = null; 
            if (Question is Slider)
            {
                Command1 = new SqlCommand(Constant.ProcdureSliderSelectAll, Connection);
                Command1.CommandType = CommandType.StoredProcedure;
                try
                {
                    Connection.Open();
                    Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString, Constant.SliderString);
                    SqlDataReader SqlRedaer = Command.ExecuteReader();
                    List<Slider> ListOfSlider = new List<Slider>();
                    while (SqlRedaer.Read())
                    {
                        Question.Id = Convert.ToInt32(SqlRedaer[Constant.IDString]);
                        Question.NewText = SqlRedaer[Constant.Qustions_textString].ToString();
                        Question.TypeOfQuestion = Constant.SliderString;
                        Question.Order = Convert.ToInt32(SqlRedaer[Constant.Qustion_orderString]);
                        ListOfSlider.Add((Slider)Question);
                        Question = new Slider();

                    }
                    SqlRedaer.Close();

                    for (int i = 0; i < ListOfSlider.Count; ++i)
                    {

                        Command1.Parameters.AddWithValue(Constant.AtsMark + Constant.Qus_IDString, ListOfSlider.ElementAt(i).Id);
                        SqlDataReader SqlReader1 = Command1.ExecuteReader();
                        while (SqlReader1.Read())
                        {
                            ListOfSlider.ElementAt(i).StartValue = Convert.ToInt32(SqlReader1[Constant.Start_ValueString]);
                            ListOfSlider.ElementAt(i).EndValue = Convert.ToInt32(SqlReader1[Constant.End_ValueString]);
                            ListOfSlider.ElementAt(i).StartCaption = SqlReader1[Constant.Start_Value_CapString].ToString();
                            ListOfSlider.ElementAt(i).EndCaption = SqlReader1[Constant.End_Value_CapString].ToString();
                            ListOfSlider.ElementAt(i).IdForType = Convert.ToInt32(SqlReader1[Constant.IDString]);
                            StaticObjects.ListOfAllQuestion.Add(ListOfSlider.ElementAt(i));
                        }
                        Command1.Parameters.Clear();
                        SqlReader1.Close();
                    }
                    Command.Parameters.Clear();


                }
                catch (Exception ex)
                {

                    MessageBox.Show(Constant.MessageError);
                    StaticObjects.Erros.Log(ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }else if (Question is Smiles)
            {
                Command1 = new SqlCommand(Constant.ProcdureSmilesSelectAll, Connection);
                Command1.CommandType = CommandType.StoredProcedure;
                try
                {
                    Connection.Open();
                    Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString, Constant.SmilyString); ;
                    SqlDataReader Reader = Command.ExecuteReader();
                    List<Smiles> ListSmiles = new List<Smiles>();
                    while (Reader.Read())
                    {
                        Question.Id = Convert.ToInt32(Reader[Constant.IDString]);
                        Question.NewText = Reader[Constant.Qustions_textString].ToString();
                        Question.TypeOfQuestion = Constant.SmilyString;
                        Question.Order = Convert.ToInt32(Reader[Constant.Qustion_orderString]);
                        ListSmiles.Add((Smiles)Question);
                        Question = new Smiles();
                    }
                    Reader.Close();

                    for (int i = 0; i < ListSmiles.Count; ++i)
                    {

                        Command1.Parameters.AddWithValue(Constant.AtsMark + Constant.Qus_IDString, ListSmiles.ElementAt(i).Id);
                        SqlDataReader Reader1 = Command1.ExecuteReader();
                        while (Reader1.Read())
                        {
                            ListSmiles.ElementAt(i).NumberOfSmiles = Convert.ToInt32(Reader1[Constant.Number_of_smilyString]);
                            ListSmiles.ElementAt(i).IdForType = Convert.ToInt32(Reader1[Constant.IDString]);
                            StaticObjects.ListOfAllQuestion.Add(ListSmiles.ElementAt(i));
                        }
                        Command1.Parameters.Clear();
                        Reader1.Close();
                    }
                    Command.Parameters.Clear();


                }
                catch (Exception ex)
                {

                    MessageBox.Show(Constant.ErrorString);
                    StaticObjects.Erros.Log(ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }else if (Question is Stars)
            {
                Command1 = new SqlCommand(Constant.ProcdureStarsSelectAll, Connection);
                Command1.CommandType = CommandType.StoredProcedure;
                try
                {
                    Connection.Open();
                    Command.Parameters.AddWithValue(Constant.Type_Of_QustionString, Constant.StarsString);
                    SqlDataReader Reader = Command.ExecuteReader();
                    List<Stars> ListOfStars = new List<Stars>();
                    while (Reader.Read())
                    {
                        Question.Id = Convert.ToInt32(Reader[Constant.IDString]);
                        Question.NewText = Reader[Constant.Qustions_textString].ToString();
                        Question.Order = Convert.ToInt32(Reader[Constant.Qustion_orderString]);
                        Question.TypeOfQuestion = Constant.StarsString;
                        ListOfStars.Add((Stars)Question);
                        Question = new Stars();
                    }
                    Reader.Close();

                    for (int i = 0; i < ListOfStars.Count; ++i)
                    {

                        Command1.Parameters.AddWithValue(Constant.AtsMark + Constant.Qus_IDString, ListOfStars.ElementAt(i).Id);
                        SqlDataReader Reader1 = Command1.ExecuteReader();
                        while (Reader1.Read())
                        {
                            ListOfStars.ElementAt(i).NumberOfStars = Convert.ToInt32(Reader1[Constant.Number_Of_StarsString]);
                            ListOfStars.ElementAt(i).IdForType = Convert.ToInt32(Reader1[Constant.IDString]);
                            StaticObjects.ListOfAllQuestion.Add(ListOfStars.ElementAt(i));
                        }
                        Command1.Parameters.Clear();
                        Reader1.Close();
                    }
                    Command.Parameters.Clear();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(Constant.MessageError);
                    StaticObjects.Erros.Log(ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }

        }
        public static void AddQuestion (Qustions Question, string [] Attrubite)
        {
            // For Add Question in my data base and will pass my Attrubites from HomePage.cs AND pass the type of Question and add it in database 
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command = new SqlCommand(Constant.ProcdureQuestionInsert, Connection);
            SqlCommand Command1 = new SqlCommand(Constant.ProcdureQuestionSelectForMax, Connection);
            int id = -1;
            try
            {
                Command.CommandType = CommandType.StoredProcedure;
                Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustions_textString, Attrubite[0]);
                Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustion_orderString, Convert.ToInt32(Attrubite[1]));
                Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString,Attrubite[2]);
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
                StaticObjects.Erros.Log(ex.Message);
                Constant.DoneOrNot = Constant.Not; 
            }
            finally
            {
                Connection.Close();
                Command.Parameters.Clear();
                Command1.Parameters.Clear();
            }
            if (id != -1)
            {
                if (Question is Slider)
                {
                    try
                    {
                        Connection.Open();
                        Command.CommandText = Constant.ProcdureSliderInsert;
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Start_ValueString, Convert.ToInt32(Attrubite[3]));
                        Command.Parameters.AddWithValue(Constant.AtsMark + Constant.End_ValueString, Convert.ToInt32(Attrubite[4]));
                        Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Start_Value_CapString, Attrubite[5]);
                        Command.Parameters.AddWithValue(Constant.AtsMark + Constant.End_Value_CapString, Attrubite[6]);
                        Command.Parameters.AddWithValue(Constant.AtsMark + Constant.Qus_IDString, id);
                        Command.ExecuteNonQuery();
                        DataBaseConnections.GetQuestionFromDataBase(new Slider());
                        Constant.DoneOrNot = Constant.Done; 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Constant.ErrorString);
                        StaticObjects.Erros.Log(ex.Message);
                        Constant.DoneOrNot = Constant.Not;

                    }
                    finally
                    {
                        Connection.Close();
                    }

                }
                else if (Question is Smiles)
                {
                    try
                    {
                        Connection.Open();
                        SqlCommand Command3 = new SqlCommand(Constant.ProcdureSmilesInsert, Connection);
                        Command3.CommandType = CommandType.StoredProcedure;
                        Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Number_of_smilyString, Convert.ToInt32(Attrubite[3]));
                        Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qus_IDString, id);
                        Command3.ExecuteNonQuery();
                        Command3.Parameters.Clear();
                        DataBaseConnections.GetQuestionFromDataBase(new Smiles());
                        Constant.DoneOrNot = Constant.Done;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(Constant.ErrorString);
                        StaticObjects.Erros.Log(ex.Message);
                        Constant.DoneOrNot = Constant.Not;
                    }
                    finally
                    {
                        Connection.Close();
                    }
                }
                else if (Question is Stars)
                {
                    try
                    {
                        Connection.Open();
                        SqlCommand cmd3 = new SqlCommand(Constant.ProcdureStarsInsert, Connection);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Number_Of_StarsString, Convert.ToInt32(Attrubite[3]));
                        cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qus_IDString, id);
                        cmd3.ExecuteNonQuery();
                        cmd3.Parameters.Clear();
                        DataBaseConnections.GetQuestionFromDataBase(new Stars());
                        Constant.DoneOrNot = Constant.Done;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Constant.ErrorString);
                        StaticObjects.Erros.Log(ex.Message);
                        Constant.DoneOrNot = Constant.Not;
                    }
                    finally
                    {
                        Connection.Close();
                    }
                }
            }
        }
        public static void EditQuestion (Qustions Question, string[] Attrubite)
        {
            // For Edit Question and get the data from HomePage.cs Attrubite and show the user old data and will edit and edit it from database
            SqlConnection Connection = DataBaseConnections.GetConnection();
            try
            {
                if (Question is Slider)
                {
                    Connection.Open();
                    SqlCommand cmd3 = new SqlCommand(Constant.ProcdureSliderUpdate, Connection);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Convert.ToInt32(Attrubite[6]));
                    if (Attrubite[2] != Constant.Empty)
                        cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Start_ValueString, Convert.ToInt32(Attrubite[2]));
                    if (Attrubite[3] != Constant.Empty)
                        cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.End_ValueString, Convert.ToInt32(Attrubite[3]));
                    if (Attrubite[4] != Constant.Empty)
                        cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Start_Value_CapString, Attrubite[4]);
                    if (Attrubite[5] != Constant.Empty)
                        cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.End_Value_CapString, Attrubite[5]);
                    cmd3.ExecuteNonQuery();
                    cmd3.Parameters.Clear();
                    cmd3.CommandText = Constant.ProcdureQuestionUpdate;
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Convert.ToInt32(Attrubite[7]));
                    if (Attrubite[0] != Constant.Empty)
                        cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustions_textString, Attrubite[0]);
                    if (Attrubite[1] != Constant.Empty)
                        cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustion_orderString, Attrubite[1]);
                    cmd3.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString, Constant.SliderString.ToString());
                    cmd3.ExecuteNonQuery();

                }else if (Question is Smiles)
                {
                    Connection.Open();
                    ///////////////////////////////////////////////////////////////////////
                    SqlCommand Command3 = new SqlCommand(Constant.ProcdureSmilesUpdate, Connection);
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Convert.ToInt32(Attrubite[3]));
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Number_of_smilyString, Convert.ToInt32(Attrubite[2]));
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    Command3.CommandText = Constant.ProcdureQuestionUpdate;
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Convert.ToInt32(Attrubite[4]));
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustions_textString, Attrubite[0]);
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustion_orderString, Convert.ToInt32(Attrubite[1]));
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString, Constant.SmilyString.ToString());
                    Command3.ExecuteNonQuery();
                }else if (Question is Stars)
                {
                    Connection.Open();
                    SqlCommand Command3 = new SqlCommand(Constant.ProcdureStarsUpdate, Connection);
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Convert.ToInt32(Attrubite[3]));
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Number_Of_StarsString, Convert.ToInt32(Attrubite[2]));
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    Command3.CommandText = Constant.ProcdureQuestionUpdate;
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Convert.ToInt32(Attrubite[4]));
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustions_textString, Attrubite[0]);
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Qustion_orderString, Convert.ToInt32(Attrubite[1]));
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.Type_Of_QustionString, Constant.StarsString);
                    Command3.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close();
            }

        }
        public static void DeleteQuestion (Qustions Question, int Id, int idFroType)
        {
            // For delete the Question and the value passing for me from Homepage.cs and Delete the Question by id and delete it from database
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command3 = null; 
            try
            {
                Connection.Open();
                if (Question is Slider)
                {
                    Command3 = new SqlCommand(Constant.ProcdureSliderDelete, Connection);
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, idFroType);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    Command3.CommandText = Constant.ProcdureQuestionDelete;
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Id);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                }
                else if (Question is Smiles)
                {
                    Command3 = new SqlCommand(Constant.ProcdureSmilesDelete, Connection);
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, idFroType);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    Command3.CommandText = Constant.ProcdureQuestionDelete;
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Id);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                }
                else
                {
                    Command3 = new SqlCommand(Constant.ProcdureStarsDelete, Connection);
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, idFroType);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    Command3.CommandText = Constant.ProcdureQuestionDelete;
                    Command3.CommandType = CommandType.StoredProcedure;
                    Command3.Parameters.AddWithValue(Constant.AtsMark + Constant.IDString, Id);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(Constant.MessageError);
                StaticObjects.Erros.Log(ex.Message);
            }
            finally
            {
                Connection.Close(); 
            }
        }
    }
}
