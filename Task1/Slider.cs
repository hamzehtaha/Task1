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
    class Slider : Qustions
    {
        public int IdForType{get; set;}
        public int StartV { get; set; }
        public int EndV { get; set; }
        public string StartC { get; set; }
        public string EndC { get; set; }

        public Slider(int Id, int idForType, string Qustion ,string TypeOfQuestion, int order, int  StartV,int EndV , string StartC,string EndC) {
            this.Qustion = Qustion;
            this.Order = order; 
            this.StartV = StartV;
            this.EndV = EndV;
            this.StartC = StartC;
            this.EndC = EndC;
            this.Id = Id; 
            this.TypeOfQuestion = TypeOfQuestion;
            this.IdForType = idForType; 
        }
        public Slider(string Qustion, string TypeOfQuestion, int idForType,int order, int StartV, int EndV, string StartC, string EndC)
        {
            this.Qustion = Qustion;
            this.Order = order;
            this.StartV = StartV;
            this.EndV = EndV;
            this.StartC = StartC;
            this.EndC = EndC;
            this.TypeOfQuestion = TypeOfQuestion;
            this.IdForType = idForType;
        }
        public Slider()
        {

        }
        public static  void ShowQuestion()
        {
            
            SqlConnection con = new SqlConnection(Qustions.connectionString);
            SqlCommand cmd = new SqlCommand("sp_Qustions_SelectAll2", con);
            cmd.CommandType = CommandType.StoredProcedure; 

            SqlCommand cmd1 = new SqlCommand("sp_Slider_SelectAll2", con);
            cmd1.CommandType = CommandType.StoredProcedure; 
            Slider obj;
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Type_Of_Qustion", "Slider");
                SqlDataReader rd = cmd.ExecuteReader();
                 
                obj  = new Slider();
                List<Slider> li1 = new List<Slider>(); 
                while (rd.Read())
                {
                    obj.Id = Convert.ToInt32(rd["ID"]);
                    obj.Qustion = rd["Qustions_text"].ToString(); 
                    obj.TypeOfQuestion = "Slider";
                    obj.Order = Convert.ToInt32(rd["Qustion_order"]);
                    li1.Add(obj);
                    obj = new Slider(); 

                }
                rd.Close();
               
                for (int i = 0; i<li1.Count; ++i)
                {

                    cmd1.Parameters.AddWithValue("@Qus_ID", li1.ElementAt(i).Id);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                    while (rd1.Read())
                    {
                        li1.ElementAt(i).StartV= Convert.ToInt32(rd1["Start_Value"]);
                        li1.ElementAt(i).EndV = Convert.ToInt32(rd1["End_Value"]);
                        li1.ElementAt(i).StartC = rd1["Start_Value_Cap"].ToString();
                        li1.ElementAt(i).EndC = rd1["End_Value_Cap"].ToString();
                        li1.ElementAt(i).IdForType = Convert.ToInt32(rd1["ID"]);
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
        public override void AddQuestion(string [] att)
        {
            Console.WriteLine("Done0");
            
            SqlConnection con = new SqlConnection(Qustions.connectionString);
            SqlCommand cmd = new SqlCommand("sp_Qustion_Insert3", con);
            SqlCommand cmd1 = new SqlCommand("select max(ID) as ID from Qustions", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Qustions_text", att[0]);
            cmd.Parameters.AddWithValue("@Qustion_order", Convert.ToInt32(att[1]));
            cmd.Parameters.AddWithValue("@Type_Of_Qustion", "Slider");
            
            int id = -1;
            try{
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
            Console.WriteLine("Done1");
            if (id != -1)
            {
                try
                {
                    con.Open();
                    cmd.CommandText = "sp_Slider_Insert1";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Start_Value", Convert.ToInt32(att[2]));
                    cmd.Parameters.AddWithValue("@End_Value", Convert.ToInt32(att[3]));
                    cmd.Parameters.AddWithValue("@Start_Value_Cap", att[4]);
                    cmd.Parameters.AddWithValue("@End_Value_Cap", att[5]);
                    cmd.Parameters.AddWithValue("@Qus_ID", id);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Done1");
                    Slider.ShowQuestion();
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

        public override void EditQuestion(string[] att)
        {
            SqlConnection con = new SqlConnection(Qustions.connectionString);
            try
            {
                con.Open();
                SqlCommand cmd3 = new SqlCommand("sp_Slider_Update10", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID",Convert.ToInt32(att[6]));
                if (att[2]!="")
                cmd3.Parameters.AddWithValue("@Start_Value",Convert.ToInt32(att[2]));
                if (att[3]!="")
                cmd3.Parameters.AddWithValue("@End_Value", Convert.ToInt32(att[3]));
                if (att[4]!="")
                cmd3.Parameters.AddWithValue("@Start_Value_Cap", att[4]);
                if (att[5]!="" )
                cmd3.Parameters.AddWithValue("@End_Value_Cap", att[5]);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_update7";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID",Convert.ToInt32(att[7]));
                if (att[0] !="")
                cmd3.Parameters.AddWithValue("@Qustions_text",att[0]);
                if (att[1] !="")
                cmd3.Parameters.AddWithValue("@Qustion_order",att[1] );
                cmd3.Parameters.AddWithValue("@Type_Of_Qustion","Slider");
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
                SqlCommand cmd3 = new SqlCommand("sp_Slider_Delete", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID", idFroType);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                cmd3.CommandText = "sp_Qustion_Delete";
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@ID",Id);
                cmd3.ExecuteNonQuery();
                cmd3.Parameters.Clear();
                
                //MessageBox.Show("This Answer is Deleted");
                
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
