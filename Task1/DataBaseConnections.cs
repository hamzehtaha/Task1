using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;
using System.Windows.Forms;
using Task1;
namespace Survey
{
    /** This Class For Data Base and get Connection with data base 
     * and actions with database using inly this class 
     * 
    **/
    public  class DataBaseConnections
    {
        /*
         * This Strings attrubites  for connection string 
         * */
        private static string ServerName = ConfigurationManager.AppSettings["Server"];
        private static string ProviderName = ConfigurationManager.AppSettings["ProviderName"];
        private static string Database = ConfigurationManager.AppSettings["Database"];
        private static string UserId = ConfigurationManager.AppSettings["UserId"];
        private static string Password = ConfigurationManager.AppSettings["Password"];
        private static string connectionString = "Data Source=" + ServerName + "; Initial Catalog =" + Database + "; User ID = " + UserId + "; Password=" + Password;
        /***
         * This strings for database for Some operations in database 
         * in SQL

         * */

        private const string JoinSmileAndQustion = "select Qustions.ID,Smily.ID ,Qustions.Qustions_text,Qustions.Qustion_order,Smily.Number_of_smily from Qustions INNER JOIN Smily ON Smily.Qus_ID = Qustions.ID";
        private const string JoinSliderAndQuestion = "select Qustions.ID, Slider.ID ,Qustions.Qustions_text,Qustions.Qustion_order,Slider.Start_Value,Slider.End_Value,Slider.Start_Value_Cap,Slider.End_Value_Cap from Qustions INNER JOIN Slider ON Slider.Qus_ID = Qustions.ID;";
        private const string JoinStarsAndQuestion = "select Qustions.ID,Stars.ID ,Qustions.Qustions_text,Qustions.Qustion_order,Stars.Number_Of_Stars from Qustions INNER JOIN Stars ON Stars.Qus_ID = Qustions.ID;";
        private const string ProcdureQuestionSelectForMax = "select max(ID) as ID from Qustions";
        private const string SelectStarFromQuestion = "SELECT * FROM Qustions";
        private const string DeleteStar = "DELETE FROM Stars Where ID = @ID;";
        private const string UpdateSlider = "UPDATE Slider SET Start_Value =@Start_value, End_Value = @End_Value,Start_Value_Cap =@Start_Value_Cap, End_Value_Cap = @End_Value_Cap where Qus_ID = @ID;";
        private const string UpdateSmile = "UPDATE Smily SET Number_of_smily = @Number_of_smily where Qus_ID = @ID;";
        private const string UpdateStar = "UPDATE Stars SET Number_Of_Stars = @Number_Of_Stars where Qus_ID = @ID;";
        private const string DeleteSlider = "DELETE FROM Slider Where ID = @ID;";
        private const string DeleteSmily = "DELETE FROM Smily Where ID = @ID;";
        private const string InsertInSlider = "INSERT INTO Slider(Start_Value,End_Value,Start_Value_Cap,End_Value_Cap,Qus_ID) VALUES(@Start_Value,@End_Value, @Start_Value_Cap,@End_Value_Cap,@Qus_ID);";
        private const string InsertInSmile = "INSERT INTO Smily(Number_of_smily,Qus_ID) VALUES(@Number_of_smily,@Qus_ID);";
        private const string InsertInStar = "INSERT INTO Stars(Number_Of_Stars,Qus_ID) VALUES(@Number_Of_Stars,@Qus_ID);";
        private const string DeleteQustionAttrubites = "DELETE FROM Qustions Where ID = @ID;";
        private const string UpdateQuestion = "update Qustions Set Qustions_text = @Qustions_text, Qustion_order=@Qustion_order where ID = @ID;";
        private const string InsertIntoQustion = "INSERT INTO Qustions(Qustions_text, Type_Of_Qustion,Qustion_order) VALUES(@NewQuestion,@TypeQustion,@Order);";
       /***
        * This function for add or edit and delete and select from database 
        * 
        * */
        private static int AddQustionInDataBase(Qustions Question)
        {
            SqlConnection Connection = DataBaseConnections.GetConnection();
           
            SqlCommand Command = new SqlCommand(InsertIntoQustion, Connection);
            SqlCommand Command1 = new SqlCommand(ProcdureQuestionSelectForMax, Connection);
            int Id = -1;
            try
            {
                Command.CommandText = InsertIntoQustion;
                Command.Parameters.AddWithValue("@NewQuestion", Question.NewText);
                Command.Parameters.AddWithValue("@TypeQustion", Question.TypeOfQuestion);
                Command.Parameters.AddWithValue("@Order", Question.Order);
                Connection.Open();
                Command.ExecuteNonQuery();
                SqlDataReader Reader = Command1.ExecuteReader();
                while (Reader.Read())
                    Id = Convert.ToInt32(Reader["ID"]);
                Reader.Close();
            }

            catch (Exception ex)
            {

                MessageBox.Show(Survey.Properties.Resource1.MessageError);
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                string MethodName = frame.GetMethod().Name;
                StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
                StaticObjects.DoneOrNot = Constant.Not;
                return Id;
            }
            finally
            {
                Connection.Close();
                Command.Parameters.Clear();
                Command1.Parameters.Clear();
            }
            return Id;
        }
        private static void EditAttrubitesForQuestion(Qustions Question)
        {
            SqlConnection Connection = DataBaseConnections.GetConnection();
            try
            {
                Connection.Open();
                
                SqlCommand cmd3 = new SqlCommand(UpdateQuestion, Connection);
                cmd3.Parameters.AddWithValue("@Qustions_text", Question.NewText);
                cmd3.Parameters.AddWithValue("@Qustion_order", Question.Order);
                cmd3.Parameters.AddWithValue("@ID", Question.Id);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Survey.Properties.Resource1.MessageError);
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int LineNumber = frame.GetFileLineNumber();
                string MethodName = frame.GetMethod().Name;
                StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
            }
            finally
            {
                Connection.Close();
            }
        }
        private static void DeleteQustion(int Id)
        {
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlCommand Command3 = null;
            try
            {
                Connection.Open();
                
                Command3 = new SqlCommand(DeleteQustionAttrubites, Connection);
                Command3.Parameters.AddWithValue("@ID", Id);
                Command3.ExecuteNonQuery();
                Command3.Parameters.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Survey.Properties.Resource1.MessageError);
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                string MethodName = frame.GetMethod().Name;
                StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
            }
            finally
            {
                Connection.Close();
            }

        }
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
        public static void GetQuestionFromDataBase()
        {
            List<Qustions> TempListOfQustion = new List<Qustions>();
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlDataReader DataReader = null;
            Smiles NewSmile = null;
            Slider NewSlider = null;
            Stars NewStars = null; 
            try
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand(JoinSmileAndQustion, Connection);
                DataReader = cmd.ExecuteReader();
                StaticObjects.ListOfAllQuestion.Clear(); 
                while (DataReader.Read())
                {
                    NewSmile = new Smiles();
                    NewSmile.Id = Convert.ToInt32(DataReader.GetValue(0));
                    NewSmile.IdForType = Convert.ToInt32(DataReader.GetValue(1));
                    NewSmile.NewText = DataReader.GetValue(2) + Constant.Empty;
                    NewSmile.Order = Convert.ToInt32(DataReader.GetValue(3));
                    NewSmile.NumberOfSmiles = Convert.ToInt32(DataReader.GetValue(4));
                    NewSmile.TypeOfQuestion = TypeOfQuestion.Smily.ToString();
                    StaticObjects.ListOfAllQuestion.Add(NewSmile);
                }
                DataReader.Close(); 
                cmd.CommandText = JoinSliderAndQuestion;
                DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    NewSlider = new Slider();
                    NewSlider.Id = Convert.ToInt32(DataReader.GetValue(0));
                    NewSlider.IdForType = Convert.ToInt32(DataReader.GetValue(1));
                    NewSlider.NewText = DataReader.GetValue(2) + Constant.Empty;
                    NewSlider.Order = Convert.ToInt32(DataReader.GetValue(3));
                    NewSlider.TypeOfQuestion = TypeOfQuestion.Slider.ToString();
                    NewSlider.StartValue = Convert.ToInt32(DataReader.GetValue(4));
                    NewSlider.EndValue = Convert.ToInt32(DataReader.GetValue(5));
                    NewSlider.StartCaption = DataReader.GetValue(6) + Constant.Empty;
                    NewSlider.EndCaption = DataReader.GetValue(7) + Constant.Empty;
                    StaticObjects.ListOfAllQuestion.Add(NewSlider);
                }
                DataReader.Close();
                cmd.CommandText = JoinStarsAndQuestion;
                DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    NewStars = new Stars();
                    NewStars.Id = Convert.ToInt32(DataReader.GetValue(0));
                    NewStars.IdForType = Convert.ToInt32(DataReader.GetValue(1));
                    NewStars.NewText = DataReader.GetValue(2) + Constant.Empty;
                    NewStars.Order = Convert.ToInt32(DataReader.GetValue(3));
                    NewStars.NumberOfStars = Convert.ToInt32(DataReader.GetValue(4));
                    NewStars.TypeOfQuestion = TypeOfQuestion.Stars.ToString(); 
                    StaticObjects.ListOfAllQuestion.Add(NewStars);
                }
                }
            catch (Exception ex)
            {
                MessageBox.Show(Survey.Properties.Resource1.MessageError);
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                string MethodName = frame.GetMethod().Name;
                StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
            }
            finally
            {
                DataReader.Close(); 
                Connection.Close();
            }

        }
        public static void AddQuestion (Qustions Question)
        {
            // For Add Question in my data base and will pass my Attrubites from HomePage.cs AND pass the type of Question and add it in database 
            SqlConnection Connection = DataBaseConnections.GetConnection();
            int Id = AddQustionInDataBase(Question);
            if (Id != -1)
            {
                if (TypeOfQuestion.Slider.ToString() == Question.TypeOfQuestion)
                {
                    Slider SliderQuestion = (Slider)Question;
                    try
                    {
                        Connection.Open();
                        SqlCommand Command = new SqlCommand(InsertInSlider, Connection);
                        Command.Parameters.AddWithValue("@Start_Value", SliderQuestion.StartValue);
                        Command.Parameters.AddWithValue("@End_Value", SliderQuestion.EndValue);
                        Command.Parameters.AddWithValue("@Start_Value_Cap", SliderQuestion.StartCaption);
                        Command.Parameters.AddWithValue("@End_Value_Cap", SliderQuestion.EndCaption);
                        Command.Parameters.AddWithValue("@Qus_ID", Id);
                        SliderQuestion.Id = Id;
                        Command.ExecuteNonQuery();
                        StaticObjects.ListOfAllQuestion.Add(SliderQuestion);
                        StaticObjects.DoneOrNot = Constant.Done;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.MessageError);
                        StackTrace st = new StackTrace(ex, true);
                        StackFrame frame = st.GetFrame(0);
                        int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                        string MethodName = frame.GetMethod().Name;
                        StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
                        StaticObjects.DoneOrNot = Constant.Not;

                    }
                    finally
                    {
                        Connection.Close();
                    }
                }
                else if (TypeOfQuestion.Smily.ToString() == Question.TypeOfQuestion)
                {
                    Smiles SmileQuestion = (Smiles)Question;
                    try
                    {
                        Connection.Open();
                        SqlCommand Command3 = new SqlCommand(InsertInSmile, Connection);
                        Command3.Parameters.AddWithValue("@Number_of_smily", SmileQuestion.NumberOfSmiles);
                        Command3.Parameters.AddWithValue("@Qus_ID", Id);
                        Command3.ExecuteNonQuery();
                        Command3.Parameters.Clear();
                        SmileQuestion.Id = Id;
                        StaticObjects.ListOfAllQuestion.Add(SmileQuestion);
                        StaticObjects.DoneOrNot = Constant.Done;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.MessageError);
                        StackTrace st = new StackTrace(ex, true);
                        StackFrame frame = st.GetFrame(0);
                        int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                        string MethodName = frame.GetMethod().Name;
                        StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
                        StaticObjects.DoneOrNot = Constant.Not;

                    }
                    finally
                    {
                        Connection.Close();
                    }
                } else if (TypeOfQuestion.Stars.ToString() == Question.TypeOfQuestion) {

                    Stars StarQuestion = (Stars)Question;
                    try
                    {
                        Connection.Open();
                        SqlCommand cmd3 = new SqlCommand(InsertInStar, Connection);
                        cmd3.Parameters.AddWithValue("@Number_Of_Stars", StarQuestion.NumberOfStars);
                        cmd3.Parameters.AddWithValue("@Qus_ID", Id);
                        cmd3.ExecuteNonQuery();
                        cmd3.Parameters.Clear();
                        StarQuestion.Id = Id;
                        StaticObjects.ListOfAllQuestion.Add(StarQuestion);
                        StaticObjects.DoneOrNot = Constant.Done;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Survey.Properties.Resource1.MessageError);
                        StackTrace st = new StackTrace(ex, true);
                        StackFrame frame = st.GetFrame(0);
                        int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                        string MethodName = frame.GetMethod().Name;
                        StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
                        StaticObjects.DoneOrNot = Constant.Not;
                    }
                    finally
                    {
                        Connection.Close();
                    }
                }                
                }
            }
        public static void EditQuestion (Qustions Question)
        {
            // For Edit Question and get the data from HomePage.cs Attrubite and show the user old data and will edit and edit it from database
            SqlConnection Connection = DataBaseConnections.GetConnection();
            try
            {
                if (Question is Slider)
                {
                    
                    Slider SliderForEdit = (Slider)Question; 
                     Connection.Open();
                    SqlCommand cmd3 = new SqlCommand(UpdateSlider, Connection);
                    cmd3.Parameters.AddWithValue("@Start_value", SliderForEdit.StartValue);
                    cmd3.Parameters.AddWithValue("@End_Value", SliderForEdit.EndValue);
                    cmd3.Parameters.AddWithValue("@Start_Value_Cap", SliderForEdit.StartCaption);
                    cmd3.Parameters.AddWithValue("@End_Value_Cap", SliderForEdit.EndCaption);
                    cmd3.Parameters.AddWithValue("@ID", SliderForEdit.Id); 
                    cmd3.ExecuteNonQuery();
                    cmd3.Parameters.Clear();
                    EditAttrubitesForQuestion(SliderForEdit); 
                }
                else if (Question is Smiles)
                {
                    Smiles SmileForEdit = (Smiles)Question;
                    
                    Connection.Open();
                    SqlCommand Command3 = new SqlCommand(UpdateSmile, Connection);
                    Command3.Parameters.AddWithValue("@Number_of_smily", SmileForEdit.NumberOfSmiles);
                    Command3.Parameters.AddWithValue("@ID", SmileForEdit.Id);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    EditAttrubitesForQuestion(SmileForEdit);
                }
                else if (Question is Stars)
                {
                    Stars StarForEdit = (Stars)Question;
                    
                    Connection.Open();
                    SqlCommand Command3 = new SqlCommand(UpdateStar, Connection);
                    Command3.Parameters.AddWithValue("@Number_Of_Stars", StarForEdit.NumberOfStars);
                    Command3.Parameters.AddWithValue("@ID", StarForEdit.Id);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    EditAttrubitesForQuestion(StarForEdit);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(Survey.Properties.Resource1.MessageError);
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                string MethodName = frame.GetMethod().Name;
                StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
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
                    
                    Command3 = new SqlCommand(DeleteSlider, Connection);
                    Command3.Parameters.AddWithValue("@ID", idFroType);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    DeleteQustion(Id);
                }
                else if (Question is Smiles)
                {
                    
                    Command3 = new SqlCommand(DeleteSmily, Connection);
                    Command3.Parameters.AddWithValue("@ID", idFroType);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    DeleteQustion(Id);
                }
                else
                {
                    
                    Command3 = new SqlCommand(DeleteStar, Connection);
                    Command3.Parameters.AddWithValue("@ID", idFroType);
                    Command3.ExecuteNonQuery();
                    Command3.Parameters.Clear();
                    DeleteQustion(Id);

                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(Survey.Properties.Resource1.MessageError);
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                string MethodName = frame.GetMethod().Name;
                StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
            }
            finally
            {
                Connection.Close(); 
            }
        }
        public static void GetData(DataGridView ListViewQuestion)
        {
            //This Function For get data and Show it in my datagridview 
            SqlConnection Connection = DataBaseConnections.GetConnection();
            SqlDataAdapter SqlAdapter = new SqlDataAdapter(SelectStarFromQuestion, Connection);

            try
            {
                Connection.Open();
                DataTable DataTableView = new DataTable();
                SqlAdapter.Fill(DataTableView);
                ListViewQuestion.Rows.Clear();
                foreach (DataRow item in DataTableView.Rows)
                {
                    int Index = ListViewQuestion.Rows.Add();
                    ListViewQuestion.Rows[Index].Cells[0].Value = item["Qustions_text"].ToString();
                    ListViewQuestion.Rows[Index].Cells[1].Value = item["Type_Of_Qustion"].ToString();
                    ListViewQuestion.Rows[Index].Cells[2].Value = item["Qustion_order"].ToString();
                }
                // View the data in datagridview 
            }
            catch (Exception ex)
            {

                MessageBox.Show(Survey.Properties.Resource1.MessageError);
                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(0);
                int LineNumber = Convert.ToInt32(ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(' ')));
                string MethodName = frame.GetMethod().Name;
                StaticObjects.Erros.Log(ex.Message, LineNumber, MethodName);
            }
            finally
            {
                Connection.Close();
            }

        }
    }
}
