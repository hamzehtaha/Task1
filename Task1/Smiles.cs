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
    class Smiles :Qustions 
    {
        public Smiles(int Id,int IdForType, string NewText, string TypeOfQuestion, int Order,int NumberOfSmiles) {
            this.NewText = NewText;
            this.NumberOfSmiles = NumberOfSmiles;
            this.Order = Order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.Id = Id;
            this.IdForType = IdForType; 
        }
        public Smiles(int idForType, string NewText, string TypeOfQuestion, int Order, int NumberOfSmiles)
        {
            this.NewText = NewText;
            this.NumberOfSmiles = NumberOfSmiles;
            this.Order = Order;
            this.TypeOfQuestion = TypeOfQuestion;
            this.IdForType = idForType; 


        }
        public Smiles() { }
        public int NumberOfSmiles { get; set; }
        public int IdForType { get; set; }
        public static void ShowQuestion()
        {
            SqlConnection con = Attributes.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_Qustions_SelectAll2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd1 = new SqlCommand("sp_Smily_SelectAll2", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            Smiles smile;
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@" + Attributes.Variables.Type_Of_Qustion, Attributes.Variables.Smily.ToString()); ;
                SqlDataReader rd = cmd.ExecuteReader();

                smile = new Smiles();
                List<Smiles> ListSmiles = new List<Smiles>();
                while (rd.Read())
                {
                    smile.Id = Convert.ToInt32(rd[Attributes.Variables.ID.ToString()]);
                    smile.NewText = rd[Attributes.Variables.Qustions_text.ToString()].ToString();
                    smile.TypeOfQuestion = Attributes.Variables.Smily.ToString();
                    smile.Order = Convert.ToInt32(rd[Attributes.Variables.Qustion_order.ToString()]);
                    ListSmiles.Add(smile);
                    smile = new Smiles();
                }
                rd.Close();

                for (int i = 0; i < ListSmiles.Count; ++i)
                {

                    cmd1.Parameters.AddWithValue("@"+ Attributes.Variables.Qus_ID, ListSmiles.ElementAt(i).Id);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                    while (rd1.Read())
                    {
                        ListSmiles.ElementAt(i).NumberOfSmiles = Convert.ToInt32(rd1[Attributes.Variables.Number_of_smily.ToString()]);
                        ListSmiles.ElementAt(i).IdForType = Convert.ToInt32(rd1[Attributes.Variables.ID.ToString()]);
                        Attributes.ListOfAllQuestion.Add(ListSmiles.ElementAt(i));
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

        public override void AddQuestion(string[] Att)
        {
            SqlConnection con = Attributes.GetConnection();
            SqlCommand cmd = new SqlCommand("sp_Qustion_Insert3", con);
            SqlCommand cmd1 = new SqlCommand("select max(ID) as ID from Qustions", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@"+ Attributes.Variables.Qustions_text, Att[0]);
            cmd.Parameters.AddWithValue("@"+ Attributes.Variables.Qustion_order, Convert.ToInt32(Att[1]));
            cmd.Parameters.AddWithValue("@"+ Attributes.Variables.Type_Of_Qustion, Attributes.Variables.Smily.ToString());
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
                    SqlCommand cmd3 = new SqlCommand("sp_Smiles_Insert5", con);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.Number_of_smily,Convert.ToInt32(Att[2]));
                    cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.Qus_ID, id);
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
            SqlConnection con = Attributes.GetConnection();
            try
            {
                con.Open();
                ///////////////////////////////////////////////////////////////////////
                SqlCommand cmd3 = new SqlCommand("sp_Smily_Update10", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.ID, Convert.ToInt32(Att[3]));
                if (Att[2] != "")
                    cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.Number_of_smily,Att[2]);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_update7";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.ID, Convert.ToInt32(Att[4]));
                cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.Qustions_text,Att[0]);
                cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.Qustion_order, Convert.ToInt32(Att[1]));
                cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.Type_Of_Qustion, Attributes.Variables.Smily.ToString());
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

        public override void Delete(int Id, int IdFroType)
        {
            SqlConnection con = Attributes.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand("sp_Smily_Delete", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@"+ Attributes.Variables.ID,IdForType);
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
