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
    class Smiles :Qustions 
    {
        public static Boolean[] f = new Boolean[10];
        public Smiles(int Id,int idForType, string Qustion,string TypeOfQuestion, int order,int NumberOfSmiles) {
            this.Qustion = Qustion;
            this.NumberOfSmiles = NumberOfSmiles;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.Id = Id;
            this.idForType = idForType; 
        }
        public Smiles(int idForType, string Qustion, string TypeOfQuestion, int order, int NumberOfSmiles)
        {
            this.Qustion = Qustion;
            this.NumberOfSmiles = NumberOfSmiles;
            this.Order = order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.idForType = idForType; 


        }
        public Smiles() { }
        public int NumberOfSmiles { get; set; }
        public int idForType { get; set; }
        public static void ShowQuestion()
        {
            SqlConnection con = new SqlConnection(Qustions.connectionString);
            SqlCommand cmd = new SqlCommand("sp_Qustions_SelectAll2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd1 = new SqlCommand("sp_Smily_SelectAll2", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            Smiles obj;
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Type_Of_Qustion", "Smily");
                SqlDataReader rd = cmd.ExecuteReader();

                obj = new Smiles();
                List<Smiles> li1 = new List<Smiles>();
                while (rd.Read())
                {
                    obj.Id = Convert.ToInt32(rd["ID"]);
                    obj.Qustion = rd["Qustions_text"].ToString();
                    obj.TypeOfQuestion = "Smily";
                    obj.Order = Convert.ToInt32(rd["Qustion_order"]);
                    li1.Add(obj);
                    obj = new Smiles();
                }
                rd.Close();

                for (int i = 0; i < li1.Count; ++i)
                {

                    cmd1.Parameters.AddWithValue("@Qus_ID", li1.ElementAt(i).Id);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                    while (rd1.Read())
                    {
                        li1.ElementAt(i).NumberOfSmiles = Convert.ToInt32(rd1["Number_of_smily"]);
                        li1.ElementAt(i).idForType = Convert.ToInt32(rd1["ID"]);
                        Qustions.lissSlid.Add(li1.ElementAt(i));
                        //Console.WriteLine(li1.ElementAt(i).Qustion);
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

        public override void AddQuestion(string[] att)
        {
            Console.WriteLine("Done6");
            SqlConnection con = new SqlConnection(Qustions.connectionString);
            SqlCommand cmd = new SqlCommand("sp_Qustion_Insert3", con);
            SqlCommand cmd1 = new SqlCommand("select max(ID) as ID from Qustions", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qustions_text", att[0]);
            cmd.Parameters.AddWithValue("@Qustion_order", Convert.ToInt32(att[1]));
            cmd.Parameters.AddWithValue("@Type_Of_Qustion", "Smily");
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
                    SqlCommand cmd3 = new SqlCommand("sp_Smiles_Insert5", con);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue("@Number_of_smily",Convert.ToInt32(att[2]));
                    cmd3.Parameters.AddWithValue("@Qus_ID", id);
                    cmd3.ExecuteNonQuery();
                    cmd3.Parameters.Clear();
                    Smiles.ShowQuestion();
                }
                catch(Exception ex)
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
                SqlCommand cmd3 = new SqlCommand("sp_Smily_Update10", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID", Convert.ToInt32(Att[3]));
                if (Att[2] != "")
                    cmd3.Parameters.AddWithValue("@Number_of_smily",Att[2]);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_update7";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID", Convert.ToInt32(Att[4]));
                cmd3.Parameters.AddWithValue("@Qustions_text",Att[0]);
                cmd3.Parameters.AddWithValue("@Qustion_order", Convert.ToInt32(Att[1]));
                cmd3.Parameters.AddWithValue("@Type_Of_Qustion","Smily");
                cmd3.ExecuteNonQuery();

            }
            catch(Exception ex)
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
                SqlCommand cmd3 = new SqlCommand("sp_Smily_Delete", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID",idForType);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_Delete";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID", id);
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
