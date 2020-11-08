using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Task1
{
    class Stars : Qustions
    {
        public int NumberOfStars { get; set; }
        public int idForType { get; set;  }
        public Stars(int Id,int idForType, string Qustion,string TypeOfQuestion, int order , int NumberOfStars) {
            this.Qustion = Qustion;
            this.NumberOfStars = NumberOfStars;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.Id = Id;
            this.idForType = idForType; 
        }
        public Stars(int idForType, string Qustion, string TypeOfQuestion, int order, int NumberOfStars)
        {
            this.Qustion = Qustion;
            this.NumberOfStars = NumberOfStars;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.idForType = idForType; 

        }
        public Stars()
        {

        }
        public static void ShowQuestion()
        {

            SqlConnection con = new SqlConnection(Qustions.connectionString);
            SqlCommand cmd = new SqlCommand("sp_Qustions_SelectAll2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd1 = new SqlCommand("sp_Stars_SelectAll2", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            Stars obj;
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Type_Of_Qustion", "Stars");
                SqlDataReader rd = cmd.ExecuteReader();

                obj = new Stars();
                List<Stars> li1 = new List<Stars>();
                while (rd.Read())
                {
                    obj.Id = Convert.ToInt32(rd["ID"]);
                    obj.Qustion = rd["Qustions_text"].ToString();
                    obj.TypeOfQuestion = "Stars";
                    obj.Order = Convert.ToInt32(rd["Qustion_order"]);
                    li1.Add(obj);
                    obj = new Stars();
                }
                rd.Close();

                for (int i = 0; i < li1.Count; ++i)
                {

                    cmd1.Parameters.AddWithValue("@Qus_ID", li1.ElementAt(i).Id);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                    while (rd1.Read())
                    {
                        li1.ElementAt(i).NumberOfStars = Convert.ToInt32(rd1["Number_Of_Stars"]);
                        li1.ElementAt(i).idForType = Convert.ToInt32(rd1["ID"]);
                        Qustions.lissSlid.Add(li1.ElementAt(i));
                       // Console.WriteLine(li1.ElementAt(i).Qustion);
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
            Console.WriteLine("Done3");
            SqlConnection con = new SqlConnection(Qustions.connectionString);
            SqlCommand cmd = new SqlCommand("sp_Qustion_Insert3", con);
            SqlCommand cmd1 = new SqlCommand("select max(ID) as ID from Qustions", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qustions_text", att[0]);
            cmd.Parameters.AddWithValue("@Qustion_order", Convert.ToInt32(att[1]));
            cmd.Parameters.AddWithValue("@Type_Of_Qustion", "Stars");
            int id = -1;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader rd = cmd1.ExecuteReader();
                while (rd.Read())
                    id = Convert.ToInt32(rd["ID"]);
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
                    cmd3.Parameters.AddWithValue("@Number_Of_Stars", Convert.ToInt32(att[2]));
                    cmd3.Parameters.AddWithValue("@Qus_ID", id);
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
            SqlConnection con = new SqlConnection(Qustions.connectionString);
            try
            {
                con.Open();
                ///////////////////////////////////////////////////////////////////////
                SqlCommand cmd3 = new SqlCommand("sp_Stars_Update10", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID",Convert.ToInt32(Att[3]));
                cmd3.Parameters.AddWithValue("@Number_Of_Stars", Convert.ToInt32(Att[2]));
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_update7";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID",Convert.ToInt32(Att[4]));
                cmd3.Parameters.AddWithValue("@Qustions_text", Att[0]);
                cmd3.Parameters.AddWithValue("@Qustion_order", Convert.ToInt32(Att[1]));
                cmd3.Parameters.AddWithValue("@Type_Of_Qustion","Stars");
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
            SqlConnection con = new SqlConnection(Qustions.connectionString);
            try
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand("sp_Stars_Delete", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID",idForType);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_Delete";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID",Id);
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
