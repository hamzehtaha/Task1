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
    class Stars : Qustions
    {
        public int NumberOfStars { get; set; }
        public int IdForType { get; set;  }
        public Stars(int Id,int IdForType, string NewText, string TypeOfQuestion, int Order , int NumberOfStars) {
            this.NewText = NewText;
            this.NumberOfStars = NumberOfStars;
            this.Order = Order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.Id = Id;
            this.IdForType = IdForType; 
        }
        public Stars(int IdForType, string NewText, string TypeOfQuestion, int Order, int NumberOfStars)
        {
            this.NewText = NewText;
            this.NumberOfStars = NumberOfStars;
            this.Order = Order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.IdForType = IdForType; 

        }
        public Stars()
        {

        }
        public static void ShowQuestion()
        {

            SqlConnection con = Attributes.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_Qustions_SelectAll2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd1 = new SqlCommand("sp_Stars_SelectAll2", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            Stars stars;
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue(Attributes.Variables.Type_Of_Qustion.ToString(), Attributes.Variables.Stars.ToString());
                SqlDataReader rd = cmd.ExecuteReader();

                stars = new Stars();
                List<Stars> ListOfStars = new List<Stars>();
                while (rd.Read())
                {
                    stars.Id = Convert.ToInt32(rd[Attributes.Variables.ID.ToString()]);
                    stars.NewText = rd[Attributes.Variables.Qustions_text.ToString()].ToString();
                    stars.TypeOfQuestion = Attributes.Variables.Stars.ToString();
                    stars.Order = Convert.ToInt32(rd[Attributes.Variables.Qustion_order.ToString()]);
                    ListOfStars.Add(stars);
                    stars = new Stars();
                }
                rd.Close();

                for (int i = 0; i < ListOfStars.Count; ++i)
                {

                    cmd1.Parameters.AddWithValue("@"+ Attributes.Variables.Qus_ID, ListOfStars.ElementAt(i).Id);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                    while (rd1.Read())
                    {
                        ListOfStars.ElementAt(i).NumberOfStars = Convert.ToInt32(rd1[Attributes.Variables.Number_Of_Stars.ToString()]);
                        ListOfStars.ElementAt(i).IdForType = Convert.ToInt32(rd1[Attributes.Variables.ID.ToString()]);
                        Attributes.ListOfAllQuestion.Add(ListOfStars.ElementAt(i));
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
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@"+ Attributes.Variables.Qustions_text, att[0]);
            cmd.Parameters.AddWithValue("@"+ Attributes.Variables.Qustion_order, Convert.ToInt32(att[1]));
            cmd.Parameters.AddWithValue("@"+ Attributes.Variables.Type_Of_Qustion, Attributes.Variables.Stars.ToString());
            int id = -1;
            try
            {
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
                    SqlCommand cmd3 = new SqlCommand("sp_Stars_Insert6", con);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.Number_Of_Stars, Convert.ToInt32(att[2]));
                    cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.Qus_ID, id);
                    cmd3.ExecuteNonQuery();
                    cmd3.Parameters.Clear();
                    Stars.ShowQuestion();
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close(); 
                }
            }
        }

        public override void EditQuestion(string[] Att)
        {
            SqlConnection con = Attributes.GetConnection();
            try
            {
                con.Open();
                ///////////////////////////////////////////////////////////////////////
                SqlCommand cmd3 = new SqlCommand("sp_Stars_Update10", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.Qus_ID, Convert.ToInt32(Att[3]));
                cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.Number_Of_Stars,Convert.ToInt32(Att[2]));
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_update7";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.ID, Convert.ToInt32(Att[4]));
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.Qustions_text, Att[0]);
                cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.Qustion_order, Convert.ToInt32(Att[1]));
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.Qustion_order, Attributes.Variables.Stars.ToString());
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
                SqlCommand cmd3 = new SqlCommand("sp_Stars_Delete", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@" + Attributes.Variables.ID, IdForType);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_Delete";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.ID, Id);
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
